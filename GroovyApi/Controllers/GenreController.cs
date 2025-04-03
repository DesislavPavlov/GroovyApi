using GroovyApi.Models;
using GroovyApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GroovyApi.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly DatabaseService _databaseService;
        private readonly FileService _fileService;
        public GenreController(DatabaseService databaseService, FileService fileService)
        {
            _databaseService = databaseService;
            _fileService = fileService;
        }

        [HttpGet]
        public ActionResult<List<Genre>> GetGenres()
        {
            List<Genre> genres = _databaseService.GetGenres();
            return Ok(genres);
        }

        [HttpPost]
        public ActionResult AddGenre([FromBody] Genre genre)
        {
            int genreId = _databaseService.AddGenre(genre);
            if (genreId <= 0)
            {
                return BadRequest(new { error = "Error adding genre to genre table" });
            }
            genre.Id = genreId;

            return CreatedAtAction(nameof(GetGenres), new { id = genreId }, genre);
        }

        [HttpPost]
        [Route("click")]
        public ActionResult<GenreActivityModel> TrackGenreClick([FromBody] GenreActivityModel genreActivity)
        {
            if (genreActivity == null || genreActivity.GenreId <= 0 || genreActivity.UserId <= 0)
            {
                return BadRequest("Invalid artist-activity data");
            }

            int id = _databaseService.AddUserGenreClick(genreActivity.UserId, genreActivity.GenreId);
            return Ok(id);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateGenre(int id, [FromBody] Genre genre)
        {
            bool success = _databaseService.UpdateGenre(id, genre);
            if (!success)
            {
                return BadRequest(new { error = "Invalid genre id or new genre model." });
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteGenre(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { error = "Invalid genre ID" });
            }

            // Delete from genre table
            if (!_databaseService.DeleteGenre(id))
            {
                return BadRequest(new { error = "Could not delete genre." });
            }

            // Delete relations to songs and artists
            List<string> urisToDelete = _databaseService.DeleteOrphanedArtistsAndSongsAndReturnFileUrls();

            if (urisToDelete != null && urisToDelete.Count != 0)
            {
                // Delete media
                _fileService.DeleteFiles(urisToDelete);
            }

            return NoContent();
        }
    }
}
