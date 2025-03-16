namespace GroovyApi.Models
{
    public class AddSongModel
    {
        public string Title { get; set; }
        public IFormFile Song { get; set; }
        public IFormFile Cover { get; set; }
        public string Color { get; set; }
        public List<int> ArtistIds { get; set; }
        public List<int> GenreIds { get; set; }
    }
}
