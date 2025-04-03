using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using GroovyApi.Models;


namespace GroovyApi.Services
{
    public class YouTubeTrendingService
    {
        private readonly YouTubeService _youTubeService;

        // apikey is dependency(heroin) injected from program.cs
        public YouTubeTrendingService(string apiKey)
        {
            _youTubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = "YouTubeTrendingSongsAPI"
            });
        }

        /// <summary>
        /// Returns a list with trending songs.
        /// </summary>
        /// <returns></returns>
        public async Task<List<TrendingSong>> GetTrendingSongsAsync()
        {
            var videosRequest = _youTubeService.Videos.List("snippet");
            videosRequest.Chart = VideosResource.ListRequest.ChartEnum.MostPopular;
            videosRequest.RegionCode = "BG"; // this is the region
            videosRequest.MaxResults = 5;
            videosRequest.VideoCategoryId = "10"; // the 10 represents the music category in trending

            var videosResponse = await videosRequest.ExecuteAsync();

            var trendingSongs = videosResponse.Items.Select(item => new TrendingSong
            {
                Title = item.Snippet.Title, // video title
                Artist = item.Snippet.ChannelTitle, // the CHANNEL title. may not always be the artist name but it usually is
                ThumbnailUrl = item.Snippet.Thumbnails?.Medium.Url, // returns a medium sized thumbnail. NOT SQUARE !!!! dont know how to get it cropped :/
                MusicLink = $"https://www.youtube.com/watch?v={item.Id}", // returns the link of the video
                VideoId = item.Id
            }).ToList();

            return trendingSongs;
        }
    }
}
