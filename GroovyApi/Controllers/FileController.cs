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


        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var filePath = await _fileService.SaveFileAsync(file);
            return filePath != null ? Ok(new { filePath }) : BadRequest("Invalid file upload!");
        }


        [HttpGet("{fileName}")]
        public IActionResult GetFile(string fileName)
        {
            var fileStream = _fileService.GetFile(fileName);
            return fileStream != null ? File(fileStream, "application/octet-stream", fileName) : NotFound("File not found!");
        }

        [HttpDelete("{fileName}")]
        public IActionResult DeleteFile(string fileName)
        {
            return _fileService.DeleteFile(fileName) ? Ok("File deleted successfully!") : NotFound("File not found!");
        }
    }
}
