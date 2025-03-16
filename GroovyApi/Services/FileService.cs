using System.Security.Cryptography;

namespace GroovyApi.Services
{
    public class FileService
    {
        private string UploadsFolder => Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        public async Task<string?> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            if (!Directory.Exists(this.UploadsFolder))
            {
                Directory.CreateDirectory(this.UploadsFolder);
            }

            //var fileHash = await HashFile(file);
            var existingFile = Directory.GetFiles(UploadsFolder).FirstOrDefault(f => Path.GetFileName(f) == file.FileName);
            if (existingFile != null)
            {
                return $"/Uploads/{Path.GetFileName(existingFile)}";
            }

            //var fileName = $"{fileHash}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(this.UploadsFolder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/Uploads/{file.FileName}";
        }

        public FileStream? GetFile(string fileName)
        {
            var filePath = Path.Combine(this.UploadsFolder, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return null;
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return fileStream;
        }

        public bool DeleteFile(string fileName)
        {
            var filePath = Path.Combine(this.UploadsFolder, fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return false;
            }

            System.IO.File.Delete(filePath);
            return true;
        }

        public bool DeleteFiles(List<string> fileNames)
        {
            foreach (string fileName in fileNames)
            {
                var filePath = Path.Combine(this.UploadsFolder, fileName);
                if (!System.IO.File.Exists(filePath))
                {
                    return false;
                }

                System.IO.File.Delete(filePath);
            }

            return true;
        }

        private async Task<string> HashFile(IFormFile file)
        {
            using var sha256 = SHA256.Create();
            using var stream = file.OpenReadStream();
            var hashBytes = await sha256.ComputeHashAsync(stream);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
