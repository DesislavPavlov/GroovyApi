using GroovyApi.Models;
using GroovyApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GroovyApi.Controllers
{
    [Route("api/songs")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly DatabaseService _databaseService;
        private readonly FileService _fileService;
        public SongController(DatabaseService databaseService, FileService fileService)
        {
            _databaseService = databaseService;
            _fileService = fileService;
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


        [HttpPost]
        public async Task<ActionResult> AddSong([FromForm] AddSongModel addSongModel)
        {
            // Extract song
            Song song = new Song()
            {
                Title = addSongModel.Title,
                Color = addSongModel.Color,
                CoverUrl = addSongModel.Cover.FileName,
                SongUrl = addSongModel.Song.FileName
            };

            // Add to song table
            int songId = _databaseService.AddSong(song);
            if (songId <= 0)
            {
                return BadRequest(new { error = "Error adding song to song table" });
            }
            addSongModel.Id = songId;

            // Add song to artist relations
            List<int> addedArtistIds = _databaseService.AddSongArtists(songId, addSongModel.ArtistIds);
            if (addedArtistIds.Count <= 0 || addedArtistIds == null)
            {
                return BadRequest(new { error = "Error with adding song to artist relations." });
            }

            // Add song to genre relations
            List<int> addedGenreIds = _databaseService.AddSongGenres(songId, addSongModel.GenreIds);
            if (addedGenreIds.Count <= 0 || addedGenreIds == null)
            {
                return BadRequest(new { error = "Error with adding song to genre relations." });
            }

            // Add media to database
            if (await _fileService.SaveFileAsync(addSongModel.Cover) == null)
            {
                return BadRequest(new { error = "Error uploading song cover." });
            }

            if (await _fileService.SaveFileAsync(addSongModel.Song) == null)
            {
                return BadRequest(new { error = "Error uploading song audio." });
            }

            return CreatedAtAction(nameof(GetSongs), new { id = songId }, addSongModel);
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
