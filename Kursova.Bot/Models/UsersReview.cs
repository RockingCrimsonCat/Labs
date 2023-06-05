using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Bot.Models
{
    public class UsersReview
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string SummaryShort { get; set; }
        public int NumResult { get; set; }
        public long Id { get; set; }
    }
}
