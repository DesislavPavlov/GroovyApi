namespace GroovyApi.Models
{
    public class AddArtistModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public IFormFile Image { get; set; }
        public List<int> GenreIds { get; set; }
    }
}
