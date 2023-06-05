using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Bot.Models
{
    public class MostPopular
    {
        public string Copyright { get; set; }
        public List<Article> Results { get; set; }


    }
    public class Article
    {
        public string Url { get; set; }
        public string Source { get; set; }
        public string Published_date { get; set; }
        public string ByLine { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
    }
}
