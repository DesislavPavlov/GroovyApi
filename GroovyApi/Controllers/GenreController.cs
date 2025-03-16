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
