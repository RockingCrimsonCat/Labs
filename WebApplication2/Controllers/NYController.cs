using Microsoft.AspNetCore.Mvc;
using Test.Model;
using NY_API.Model;
using WebApplication2.Clients;
using NY_API;
using Npgsql;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NYController : ControllerBase
    {

        private readonly ILogger<NYController> _logger;
      

        public NYController(ILogger<NYController> logger)
        {
            _logger = logger;   
        }

        [HttpPut("{name}/{type}/{url}/{summaryShort}/{numResult}/putreview")]
        public async Task<UsersReview> PutReviewAsync(string name, string type, string url, string summaryShort, int numResult)
        {
            DatabaseUsersReview databaseUsersReview = new DatabaseUsersReview();
            UsersReview usersReview = new UsersReview();
            usersReview.Name = name;
            usersReview.Type = type;
            usersReview.Url = url;
            usersReview.SummaryShort = summaryShort;
            usersReview.NumResult = numResult;
            await databaseUsersReview.InsertUsersReviewAsync(usersReview, name, type, url, summaryShort, numResult);
            return usersReview;
        }
        [HttpPost("/usersreview")]
        public async Task<IActionResult> UpdateUsersReview([FromBody] StatistUsersReview updatedReview)
        {
            DatabaseUsersReview database = new DatabaseUsersReview();
            await database.UpdateLastStatistUsersReview(updatedReview);
            return Ok();
        }


        [HttpDelete("/usersreview/{name}")]
        public async Task<IActionResult> DeleteUsersReview(string name)
        {
            DatabaseUsersReview database = new DatabaseUsersReview();
            await database.DeleteUsersReview(name);
            return Ok();
        }

        [HttpGet("{period}/mostpopular")]
        
        public MostPopular GetMostPopular(string period)
        {
            DatabaseMostPopular database = new DatabaseMostPopular();
            User user = new User ();
            var popularArticles = user.GetMostPopularAsync(period).Result;
             database.InsertMostPopularAsync (user.GetMostPopularAsync(period).Result, period);
            return popularArticles;
          
        }

        [HttpGet("{query}/moviereviews")]

        public MovieReviews GetMovieReviews(string query)
        {
            DatabaseMovieReviews databaseMovieReviews = new DatabaseMovieReviews();
            User user = new User();
            
            var movieReviews = user.GetMovieReviewsAsync(query).Result;
            databaseMovieReviews.InsertMovieReviewsAsync(user.GetMovieReviewsAsync(query).Result, query);
            return movieReviews;

        }



    }

}