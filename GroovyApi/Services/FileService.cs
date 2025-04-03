using System.Security.Cryptography;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace GroovyApi.Services
{
    public class FileService
    {
        private string uploadsFolder => Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        private readonly YoutubeClient _youtubeClient;

        public FileService()
        {
            _youtubeClient = new YoutubeClient();
        }

        public async Task<string?> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            if (!Directory.Exists(this.uploadsFolder))
            {
                Directory.CreateDirectory(this.uploadsFolder);
            }

            var fileHash = await HashFile(file);
            var existingFile = Directory.GetFiles(uploadsFolder).FirstOrDefault(f => Path.GetFileName(f) == fileHash);
            if (existingFile != null)
            {
                return Path.GetFileName(existingFile);
            }

            var fileName = $"{fileHash}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(this.uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public FileStream? GetFile(string fileName)
        {
            var filePath = Path.Combine(this.uploadsFolder, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return null;
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return fileStream;
        }

        public bool DeleteFile(string fileUri)
        {
            // Take the hash from the URI
            Uri uri = new Uri(fileUri);
            string fileName = Path.GetFileName(uri.AbsolutePath);

            // Delete
            var filePath = Path.Combine(this.uploadsFolder, fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return false;
            }

            System.IO.File.Delete(filePath);
            return true;
        }

        public bool DeleteFiles(List<string> fileUris)
        {
            foreach (string fileUri in fileUris)
            {
                // Take the hash from the URI
                Uri uri = new Uri(fileUri);
                string fileName = Path.GetFileName(uri.AbsolutePath);

                // Delete
                var filePath = Path.Combine(this.uploadsFolder, fileName);
                if (!System.IO.File.Exists(filePath))
                {
                    return false;
                }

                System.IO.File.Delete(filePath);
            }

            return true;
        }

        public async Task<List<string>> SaveYoutubeSongFiles(List<string> videoIds)
        {
            var savedFilePaths = new List<string>();

            foreach (string videoId in videoIds)
            {
                string videoUrl = $"https://www.youtube.com/watch?v={videoId}";
                IFormFile formFile = await ConvertYoutubeUrlToIFormFile(videoUrl);

                savedFilePaths.Add(await SaveFileAsync(formFile));
            }

            return savedFilePaths;
        }

        public async Task<IFormFile> ConvertYoutubeUrlToIFormFile(string videoUrl)
        {
            var video = await _youtubeClient.Videos.GetAsync(videoUrl);

            var streamManifest = await _youtubeClient.Videos.Streams.GetManifestAsync(video.Id);
            var audioStreamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

            var tempFilePath = Path.GetTempFileName();
            await _youtubeClient.Videos.Streams.DownloadAsync(audioStreamInfo, tempFilePath);

            var fileBytes = await System.IO.File.ReadAllBytesAsync(tempFilePath);

            // Cleans the file name from dirty little symbols
            string sanitizedFileName = SanitizeFileName(video.Title) + ".mp3";

            // Creates IFormFile
            IFormFile formFile = new FormFile(
                new MemoryStream(fileBytes),
                0,
                fileBytes.Length,
                "audio",
                sanitizedFileName
            );

            // Delete temp
            System.IO.File.Delete(tempFilePath);

            return formFile;


        }

        //public async Task<string> GetExampleUrl(string title)
        //{
        //    string sanitizedFileName = SanitizeFileName(title) + ".mp3";
        //    var fileHash = await HashFile(file);
        //    var fileName = $"{fileHash}{Path.GetExtension(file.FileName)}";
        //    var filePath = Path.Combine(this.uploadsFolder, fileName);

        //}

        private async Task<string> HashFile(IFormFile file)
        {
            using var sha256 = SHA256.Create();
            using var stream = file.OpenReadStream();
            var hashBytes = await sha256.ComputeHashAsync(stream);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
        private string SanitizeFileName(string fileName)
        {
            // Define invalid characters
            var invalidChars = Path.GetInvalidFileNameChars();

            // Replace invalid characters with an underscore or remove them
            var sanitizedFileName = string.Join("_", fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));

            return sanitizedFileName;
        }

    }
}
