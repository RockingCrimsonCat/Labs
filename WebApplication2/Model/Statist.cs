using Microsoft.AspNetCore.Mvc;
using Test.Model;
using WebApplication2.Clients;
using WebApplication2.Controllers;

namespace NY_API
{
    public class Statist
    {
        public List<Articles> MostPopularArticlesResults { get; set; }
        public Statist ()
        {
            MostPopularArticlesResults = new List<Articles> ();
        }
    }
    public class Articles
    {
        public string Url { get; set; }
        public string Source { get; set; }
        public string Published_date { get; set; }
        public string ByLine { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Time { get; set; }
    }
}
