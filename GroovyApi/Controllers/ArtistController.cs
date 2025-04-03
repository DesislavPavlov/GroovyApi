using GroovyApi.Models;
using GroovyApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet]
        [Route("genres")]
        public ActionResult<Dictionary<int, List<int>>> GetArtistGenreRelations()
        {
            List<int> artistIds = _databaseService.GetArtists().Select(a => a.Id).ToList();
            if (artistIds == null || artistIds.Count == 0)
            {
                return NotFound(new { error = "No artists in database" });
            }

            Dictionary<int, List<int>> dict = _databaseService.GetGenreIdsOfArtists(artistIds);
            if (dict == null)
            {
                return Ok(new Dictionary<int, List<int>>());
            }

            return Ok(dict);
        }

        [HttpPost]
        public async Task<ActionResult> AddArtist([FromForm] string name, [FromForm] string color, [FromForm] string genreIds, IFormFile image)
        {
            // Add image to database
            string imageFileUri = await _fileService.SaveFileAsync(image);
            if (imageFileUri == null)
            {
                return BadRequest(new { error = "Image uploading error." });
            }

            // Extract artist
            Artist artist = new Artist()
            {
                Name = name,
                Color = color,
                ImageUrl = "https://localhost:7021/uploads/" + imageFileUri
            };

            // Add to artist table
            int artistId = _databaseService.AddArtist(artist);
            if (artistId <= 0)
            {
                return BadRequest("Error adding artist to artist table");
            }
            artist.Id = artistId;

            // Add artist to genre relations
            List<int> addedGenreIds = _databaseService.AddArtistGenres(artistId, JsonConvert.DeserializeObject<List<int>>(genreIds));
            if (addedGenreIds.Count <= 0 || addedGenreIds == null)
            {
                return BadRequest("Error with adding artist to genre relations.");
            }

            return CreatedAtAction(nameof(GetArtists), new { id = artistId }, artist);
        }

        [HttpPost]
        [Route("click")]
        public ActionResult TrackArtistAndGenresClick([FromBody] ArtistActivityModel artistActivity)
        {
            if (artistActivity == null || artistActivity.ArtistId <= 0 || artistActivity.UserId <= 0)
            {
                return BadRequest("Invalid artist-activity data");
            }

            // Track user-artist click
            int idOrAffectedRows = _databaseService.AddUserArtistClick(artistActivity.UserId, artistActivity.ArtistId);
            if (idOrAffectedRows == null || idOrAffectedRows <= 0)
            {
                return BadRequest($"Artist {artistActivity.ArtistId} or user does not exist.");
            }

            // Track user-genre clicks for all genres of artist
            List<int> genreIds = _databaseService.GetGenresOfArtist(artistActivity.ArtistId).Select(g => g.Id).ToList();
            int affectedRowsGenres = _databaseService.AddBatchUserGenreClick(artistActivity.UserId, genreIds);
            if (affectedRowsGenres == null || affectedRowsGenres <= 0)
            {
                return BadRequest($"Error adding genre relations to artist {artistActivity.ArtistId}.");
            }

            return Ok(idOrAffectedRows);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateArtist(int id, [FromForm] string name, [FromForm] string imageUrl, [FromForm] string color, [FromForm] string genreIds, IFormFile? image)
        {
            // Update image in database
            string imageFileUri = "";
            if (image != null)
            {
                _fileService.DeleteFile(imageUrl);

                imageFileUri = await _fileService.SaveFileAsync(image);
                if (imageFileUri == null)
                {
                    return BadRequest(new { error = "Image uploading error." });
                }
            }

            // Extract artist
            Artist artist = new Artist()
            {
                Name = name,
                Color = color,
                ImageUrl = string.IsNullOrEmpty(imageFileUri) ? imageUrl : "https://localhost:7021/uploads/" + imageFileUri
            };

            // Update artist in table
            bool success = _databaseService.UpdateArtist(id, artist, JsonConvert.DeserializeObject<List<int>>(genreIds));
            if (!success)
            {
                return BadRequest("Error updating artist in artist table");
            }

            return NoContent();
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
