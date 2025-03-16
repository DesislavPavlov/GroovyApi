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

        [HttpPost]
        public ActionResult CreateUser([FromBody] User user)
        {
            int id = _databaseService.AddUser(user);
            if (id <= 0 || id == null)
            {
                return BadRequest("User creation failed, check if email/username is already in use or if there are problems with DB");
            }

            user.Id = id;

            return CreatedAtAction(nameof(GetUserById), new { id = id }, user);
        }
    }
}
