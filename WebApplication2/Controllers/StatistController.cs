using Microsoft.AspNetCore.Mvc;
using NY_API.Model;
using Test.Model;
using WebApplication2.Clients;

namespace NY_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatistController : ControllerBase
    {
        private readonly ILogger<StatistController> _logger;

        public StatistController(ILogger<StatistController> logger)
        {
            _logger = logger;
        }
        [HttpGet ("/mostpopular")]

       public List<Articles> MostPopularArticles()
        {
            DatabaseMostPopular database = new DatabaseMostPopular();
          
            return database.SelectStatist().Result;

        }
        [HttpGet("/usersreview")]

        public List<StatistUsersReview> UsersReview()
        {
            DatabaseUsersReview database = new DatabaseUsersReview();

            return database.SelectStatistUsersReview().Result;

        }
    }

}
