using GroovyApi.Models;
using GroovyApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GroovyApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseService _databaseService;
        public UserController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            List<User> users = _databaseService.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            User user = _databaseService.GetUserById(id);
            return Ok(user);
        }

        [HttpGet("username/{username}")]
        public ActionResult<User> GetUserByName(string username)
        {
            User user = _databaseService.GetUserByUserName(username);
            return Ok(user);
        }

        [HttpGet]
        [Route("{id}/favourite/songs")]
        public ActionResult<List<Song>> GetFavouriteSongs(int id)
        {
            List<Song> songs = _databaseService.GetUserFavouriteSongs(id);
            return songs;
        }

        [HttpGet]
        [Route("{id}/favourite/artists")]
        public ActionResult<List<Artist>> GetFavouriteArtists(int id)
        {
            List<Artist> artists = _databaseService.GetUserFavouriteArtists(id);
            return artists;
        }

        [HttpGet]
        [Route("{id}/favourite/genres")]
        public ActionResult<List<Genre>> GetFavouriteGenres(int id)
        {
            List<Genre> genres = _databaseService.GetUserFavouriteGenres(id);
            return genres;
        }

        [HttpPost]
        public ActionResult CreateUser([FromBody] User user)
        {
            int id = _databaseService.AddUser(user);
            if (id <= 0)
            {
                return BadRequest("User creation failed, check if email/username is already in use or if there are problems with DB");
            }

            user.Id = id;
            return CreatedAtAction(nameof(GetUserById), new { id = id }, user);
        }

        [HttpPost]
        [Route("{id}/favourite/songs/{songId}")]
        public ActionResult AddSongToFavourite(int id, int songId)
        {
            int createdRowId = _databaseService.AddSongToUserFavourite(songId, id);
            if (createdRowId <= 0)
            {
                return BadRequest($"Song {songId} or user {id} does not exist");
            }

            return Ok(createdRowId);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteUser(int id)
        {
            bool success = _databaseService.DeleteUser(id);
            if (!success)
            {
                return BadRequest("Invalid user id " + id);
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}/favourite/songs/{songId}")]
        public ActionResult DeleteSongFromFavourite(int id, int songId)
        {
            int affectedRows = _databaseService.DeleteSongFromUserFavourite(songId, id);
            if (affectedRows <= 0)
            {
                return BadRequest($"Could not delete relation, check if song {songId} or user {id} exist");
            }

            return Ok(affectedRows);
        }
    }
}
