using System.Net.Http;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Test.Model;
using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;
using NY_API.Model;

namespace WebApplication2.Clients
    
{
    public class User
    {
        private HttpClient _httpClient;
        private static string _adress;
        private static string _apikey;

        public User()
        {
            _adress = Constants.adress;
            _apikey = Constants.apiKey;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_adress);
        }
        public async Task<MostPopular> GetMostPopularAsync (string period)
        {
            
            var responce = await _httpClient.GetAsync($"/svc/mostpopular/v2/viewed/{period}.json?api-key={_apikey}");
            responce.EnsureSuccessStatusCode();
            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<MostPopular>(content); 
            return result;
        }

        public async Task<MovieReviews> GetMovieReviewsAsync (string query)
        {
            var responce = await _httpClient.GetAsync($"/svc/movies/v2/reviews/search.json?query={query}&api-key={_apikey}");
            responce.EnsureSuccessStatusCode();
            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<MovieReviews>(content);
            return result;
        }

      

    }
}
