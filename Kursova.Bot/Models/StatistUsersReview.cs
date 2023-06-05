using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using static Kursova.Bot.User;

namespace Kursova.Bot.Models
{
    public class StatistUsersReview
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string? Url { get; set; }
        public string? SummaryShort { get; set; }
        public int NumResult { get; set; }
        public long Id { get; set; }
        public StatistUsersReview PutUpdatesReview(string name, string type, string url, string summaryShort, int numResult, long id)
        {
            StatistUsersReview usersReview = new StatistUsersReview();
            usersReview.Name = name;
            usersReview.Type = type;
            usersReview.Url = url;
            usersReview.SummaryShort = summaryShort;
            usersReview.NumResult = numResult;
            usersReview.Id = id;
            return usersReview;
        }
      
    }
}
