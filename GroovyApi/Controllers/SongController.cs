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
        [Route("{id}/related")]
        public ActionResult<List<Song>> GetRelatedSongs(int id)
        {
            List<Artist> artists = _databaseService.GetArtistsOfSong(id);
            List<int> artistIds = artists.Select(a => a.Id).ToList();

            List<Song> songs = _databaseService.GetSongsOfArtists(artistIds);

            Random rng = new Random();
            while (songs.Count > 5)
            {
                int deleteIndex = rng.Next(songs.Count);
                songs.RemoveAt(deleteIndex);
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
