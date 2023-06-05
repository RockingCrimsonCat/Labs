using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Bot.Models
{
    public class MovieReviews
    {

        public string Copyright { get; set; }
        public int Num_results { get; set; }
        public List<Movies> Results { get; set; }

    }


    public class Movies
    {
        public string Display_title { get; set; }
        public int Critics_pick { get; set; }
        public string Byline { get; set; }
        public string Headline { get; set; }
        public string Summary_short { get; set; }
        public string Publication_date { get; set; }

        public string Date_updated { get; set; }
        public Link Link { get; set; }
    }
    public class Link
    {
        public string Type { get; set; }
        public string Url { get; set; }
        public string Suggested_link_text { get; set; }
    }
}
