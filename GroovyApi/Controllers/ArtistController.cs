using GroovyApi.Models;
using GroovyApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GroovyApi.Controllers
{
    [Route("api/artists")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly DatabaseService _databaseService;
        private readonly FileService _fileService;
        public ArtistController(DatabaseService databaseService, FileService fileService)
        {
            _databaseService = databaseService;
            _fileService = fileService;
        }

        [HttpGet]
        public ActionResult<List<Artist>> GetArtists()
        {
            List<Artist> artists = _databaseService.GetArtists();
            return Ok(artists);
        }

        [HttpPost]
        public async Task<ActionResult> AddArtist([FromForm] AddArtistModel addArtistModel)
        {
            // Extract artist
            Artist artist = new Artist()
            {
                Name = addArtistModel.Name,
                Color = addArtistModel.Color,
                ImageUrl = "https://localhost:7021/uploads/" + addArtistModel.Image.FileName
            };

            // Add to artist table
            int artistId = _databaseService.AddArtist(artist);
            if (artistId <= 0)
            {
                return BadRequest(new { error = "Error adding artist to artist table" });
            }
            addArtistModel.Id = artistId;

            // Add artist to genre relations
            List<int> addedGenreIds = _databaseService.AddArtistGenres(artistId, addArtistModel.GenreIds);
            if (addedGenreIds.Count <= 0 || addedGenreIds == null)
            {
                return BadRequest(new { error = "Error with adding artist to genre relations." });
            }

            // Add photo to database
            if (await _fileService.SaveFileAsync(addArtistModel.Image) == null)
            {
                return BadRequest(new { error = "Image uploading error." });
            }

            return CreatedAtAction(nameof(GetArtists), new { id = artistId }, addArtistModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteArtist(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { error = "Invalid artist ID" });
            }

            // Get required information
            string imageUrl = _databaseService.GetArtistById(id).ImageUrl;

            // Delete from artist table
            if (!_databaseService.DeleteArtist(id))
            {
                return BadRequest(new { error = "Could not delete artist." });
            }

            // Delete relations to genre
            List<string> urisToDelete = _databaseService.DeleteOrphanedArtistsAndSongsAndReturnFileUrls();
            urisToDelete.Add(imageUrl);

            // Delete media
            _fileService.DeleteFiles(urisToDelete);

            return NoContent();
        }
    }
}
