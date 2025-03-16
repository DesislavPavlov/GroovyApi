using GroovyApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GroovyApi.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FileController : Controller
    {
        private readonly FileService _fileService;

        public FileController(FileService fileService)
        {
            _fileService = fileService;
        }


        [HttpGet("avatars")]
        public ActionResult GetAvatars()
        {
            //List<FileStream> files = new List<FileStream>();
            ////files.Add(_fileService.GetFile("avatar_admin.webp"));
            ////files.Add(_fileService.GetFile("avatar_guest.webp"));
            //files.Add(_fileService.GetFile("avatar_guitar.webp"));
            //files.Add(_fileService.GetFile("avatar_normal.webp"));
            //files.Add(_fileService.GetFile("avatar_rock.webp"));
            //files.Add(_fileService.GetFile("avatar_singer.webp"));

            string[] urls = new string[]
            {
                "https://localhost:7021/uploads/avatar_guitar.webp",
                "https://localhost:7021/uploads/avatar_normal.webp",
                "https://localhost:7021/uploads/avatar_rock.webp",
                "https://localhost:7021/uploads/avatar_singer.webp",
            };
            return Ok(urls);
        }
    }
}
