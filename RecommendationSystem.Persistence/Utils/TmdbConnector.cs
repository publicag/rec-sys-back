using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using RecommendationSystem.Application.Interfaces;

namespace RecommendationSystem.Persistence.Utils
{
    public class TmdbConnector : ITmdbMovieConnector
    {
        private readonly string _tmdbImageResource = "https://image.tmdb.org/t/p/original";
        private readonly string _imagesApi = "https://api.themoviedb.org/3/movie/";
        private readonly string _apiKey = "**";
        private readonly string _language = "en";
        private readonly HttpClient _httpClient;

        public TmdbConnector()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GenerateImagePathAsync(int id)
        {
            var uriBuilder = new UriBuilder(_imagesApi + id + "/images");

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["api_key"] = _apiKey;
            query["include_image_language"] = _language;
            uriBuilder.Query = query.ToString();

            var imageResponse = await _httpClient.GetAsync(uriBuilder.Uri, CancellationToken.None);

            var response = await imageResponse.Content.ReadAsStringAsync(CancellationToken.None);

            dynamic images = JsonDocument.Parse(response).RootElement.GetProperty("backdrops");
            if (images.GetArrayLength() != 0)
                return _tmdbImageResource + images[0].GetProperty("file_path").ToString();
            else
                return "";
        }
    }
}
