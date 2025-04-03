using GroovyApi.Models;
using GroovyApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GroovyApi.Controllers
{
    [Route("api/songs")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly DatabaseService _databaseService;
        private readonly FileService _fileService;
        private readonly YouTubeTrendingService _youTubeTrendingService;

        public SongController(DatabaseService databaseService, FileService fileService, YouTubeTrendingService youTubeTrendingService)
        {
            _databaseService = databaseService;
            _fileService = fileService;
            _youTubeTrendingService = youTubeTrendingService;
        }

        [HttpGet]
        public ActionResult<List<Song>> GetSongs()
        {
            List<Song> songs = _databaseService.GetAllSongs();
            return Ok(songs);
        }

        [HttpGet("{id}")]
        public ActionResult<Song> GetSongById(int id)
        {
            Song song = _databaseService.GetSongById(id);
            return Ok(song);
        }

        [HttpGet]
        [Route("recommended")]
        public ActionResult<List<Song>> GetRecommendedSongs([FromQuery] int userId, [FromQuery] int currentSongId = -1)
        {
            List<Song> songs = _databaseService.GetRecommendedSongs(userId);

            if (songs == null || songs.Count <= 0)
            {
                return BadRequest($"User {userId} does not exist or there is a problem with DB");
            }

            if (currentSongId != -1)
            {
                songs.RemoveAll(s => s.Id == currentSongId);
            }

            return songs;
        }

        [HttpGet]
        [Route("{id}/related")]
        public ActionResult<List<Song>> GetRelatedSongs(int id)
        {
            List<Artist> artists = _databaseService.GetArtistsOfSong(id);
            List<int> artistIds = artists.Select(a => a.Id).ToList();

            List<Song> songs = _databaseService.GetSongsOfArtists(artistIds);

            Console.WriteLine(string.Join(", ", artistIds));
            Console.WriteLine(string.Join(", ", songs.Select(s => s.Id)));

            songs.RemoveAll(s => s.Id == id);

            return songs;
        }

        [HttpGet]
        [Route("search")]
        public ActionResult<List<Song>> GetSearchedSongs([FromQuery] string searchTerm)
        {
            List<Song> songs = _databaseService.GetSearchedSongs(searchTerm);
            if (songs == null || songs.Count == 0)
            {
                return new List<Song>();
            }

            return songs;
        }

        [HttpGet]
        [Route("artists")]
        public ActionResult<Dictionary<int, List<int>>> GetSongArtistRelations()
        {
            List<int> songIds = _databaseService.GetAllSongs().Select(s => s.Id).ToList();
            if (songIds == null || songIds.Count == 0)
            {
                return NotFound(new { error = "No songs in database" });
            }

            Dictionary<int, List<int>> dict = _databaseService.GetArtistIdsOfSongs(songIds);
            if (dict == null)
            {
                return Ok(new Dictionary<int, List<int>>());
            }

            return Ok(dict);
        }

        [HttpGet]
        [Route("genres")]
        public ActionResult<Dictionary<int, List<int>>> GetSongGenreRelations()
        {
            List<int> songIds = _databaseService.GetAllSongs().Select(s => s.Id).ToList();
            if (songIds == null || songIds.Count == 0)
            {
                return NotFound(new { error = "No songs in database" });
            }

            Dictionary<int, List<int>> dict = _databaseService.GetGenreIdsOfSongs(songIds);
            if (dict == null)
            {
                return Ok(new Dictionary<int, List<int>>());
            }

            return Ok(dict);
        }

        [HttpGet]
        [Route("trending")]
        public async Task<ActionResult<List<TrendingSong>>> GetTrendingSongs()
        {
            List<TrendingSong> songs = await _youTubeTrendingService.GetTrendingSongsAsync();
            return songs;
        }

        [HttpPost]
        [Route("trending")]
        public async Task<ActionResult<List<TrendingSong>>> PostTrendingSongs()
        {
            List<TrendingSong> songs = await _youTubeTrendingService.GetTrendingSongsAsync();
            if (songs == null || songs.Count == 0)
            {
                return NotFound(new { error = "Could not get trending songs." });
            }

            List<string> ids = songs.Select(s => s.VideoId).ToList();
            List<string> savedPaths = await _fileService.SaveYoutubeSongFiles(ids);
            if (savedPaths == null || savedPaths.Count == 0)
            {
                return BadRequest(new { error = "Could not save songs, check for problems with GET." });
            }

            return songs;
        }


        [HttpPost]
        public async Task<ActionResult> AddSong([FromForm] string title, [FromForm] string color, [FromForm] string artistIds, [FromForm] string genreIds, IFormFile cover, IFormFile audio)
        {
            // Add media to database
            string coverFileUrl = await _fileService.SaveFileAsync(cover);
            if (coverFileUrl == null)
            {
                return BadRequest(new { error = "Error uploading song cover." });
            }

            string audioFileUrl = await _fileService.SaveFileAsync(audio);
            if (audioFileUrl == null)
            {
                return BadRequest(new { error = "Error uploading song audio." });
            }


            // Extract song
            Song song = new Song()
            {
                Title = title,
                Color = color,
                CoverUrl = "https://localhost:7021/uploads/" + coverFileUrl,
                SongUrl = "https://localhost:7021/uploads/" + audioFileUrl
            };

            // Add to song table
            int songId = _databaseService.AddSong(song);
            if (songId <= 0)
            {
                return BadRequest(new { error = "Error adding song to song table" });
            }
            song.Id = songId;

            // Add song to artist relations
            List<int> addedArtistIds = _databaseService.AddSongArtists(songId, JsonConvert.DeserializeObject<List<int>>(artistIds));
            if (addedArtistIds.Count <= 0 || addedArtistIds == null)
            {
                return BadRequest(new { error = "Error with adding song to artist relations." });
            }

            // Add song to genre relations
            List<int> addedGenreIds = _databaseService.AddSongGenres(songId, JsonConvert.DeserializeObject<List<int>>(genreIds));
            if (addedGenreIds.Count <= 0 || addedGenreIds == null)
            {
                return BadRequest(new { error = "Error with adding song to genre relations." });
            }



            return CreatedAtAction(nameof(GetSongs), new { id = songId }, song);
        }

        [HttpPost]
        [Route("click")]
        public ActionResult TrackSongAndArtistsAndGenresClick([FromBody] SongActivityModel songActivity)
        {
            if (songActivity.SongId <= 0)
            {
                return BadRequest($"Invalid song id {songActivity.SongId}.");
            }

            // Track song click
            int affectedRows = _databaseService.AddSongClick(songActivity.SongId);
            if (affectedRows == null || affectedRows <= 0)
            {
                return BadRequest($"Song {songActivity.SongId} does not exist.");
            }

            // Track user-artist clicks for all artists of song
            List<int> artistIds = _databaseService.GetArtistsOfSong(songActivity.SongId).Select(a => a.Id).ToList();
            int affectedRowsArtists = _databaseService.AddBatchUserArtistClick(songActivity.UserId, artistIds);
            if (affectedRowsArtists == null || affectedRowsArtists <= 0)
            {
                return BadRequest($"Error adding artist relations to song {songActivity.SongId}.");
            }

            // Track user-genre clicks for all genres of song
            List<int> genreIds = _databaseService.GetGenresOfArtists(artistIds).Select(g => g.Id).ToList();
            int affectedRowsGenres = _databaseService.AddBatchUserGenreClick(songActivity.UserId, genreIds);
            if (affectedRowsGenres == null || affectedRowsGenres <= 0)
            {
                return BadRequest($"Error adding genre relations to song {songActivity.SongId}.");
            }

            return Ok(affectedRows);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateSong(int id, [FromForm] string title, [FromForm] string coverUrl, [FromForm] string songUrl, [FromForm] string color, [FromForm] string artistIds, [FromForm] string genreIds, IFormFile? image, IFormFile? audio)
        {
            // Update image in database
            string coverFileUri = "";
            string audioFileUri = "";
            if (image != null)
            {
                _fileService.DeleteFile(coverUrl);

                coverFileUri = await _fileService.SaveFileAsync(image);
                if (coverFileUri == null)
                {
                    return BadRequest(new { error = "Cover uploading error." });
                }
            }

            if (audio != null)
            {
                _fileService.DeleteFile(songUrl);

                audioFileUri = await _fileService.SaveFileAsync(audio);
                if (audioFileUri == null)
                {
                    return BadRequest(new { error = "Audio uploading error." });
                }
            }

            // Extract song
            Song song = new Song()
            {
                Title = title,
                Color = color,
                CoverUrl = string.IsNullOrEmpty(coverFileUri) ? coverUrl : "https://localhost:7021/uploads/" + coverFileUri,
                SongUrl = string.IsNullOrEmpty(audioFileUri) ? songUrl : "https://localhost:7021/uploads/" + audioFileUri
            };

            // Update song in table
            bool success = _databaseService.UpdateSong(id, song, JsonConvert.DeserializeObject<List<int>>(artistIds), JsonConvert.DeserializeObject<List<int>>(genreIds));
            if (!success)
            {
                return BadRequest("Error updating song in song table");
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteSong(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { error = "Invalid song ID" });
            }

            Song song = _databaseService.GetSongById(id);

            // Delete from song table
            if (!_databaseService.DeleteSong(id))
            {
                return BadRequest(new { error = "Could not delete song." });
            }

            // Delete media
            _fileService.DeleteFile(song.CoverUrl);
            _fileService.DeleteFile(song.SongUrl);

            return NoContent();
        }
    }
}
