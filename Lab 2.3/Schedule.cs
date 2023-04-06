using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab_2._3
{
    class Schedule
    {
        protected string startTime { get; set; }
        protected string endTime { get; set; }
        protected string fullTime { get; set; }
       
        public string FullTime(string startTime, string endTime)
        {
            fullTime = $"Leson starts at {startTime} and lasts until {endTime}";
            return fullTime;
        }

        protected string date { get; set; }
        protected string place { get; set; }
        protected string commentInfo { get; set; }
        
        public string CommentInfo(string info, string placeAndTime)
        {
            string commentInfo = placeAndTime + info;
            return commentInfo;
        }
        protected List<string> fullInfo { get; set; }


        public List <string> FullInfo (string commentInfo)
        {
            List <string> fullInfo = new List <string> ();
                fullInfo.Add(commentInfo);
                Console.WriteLine(commentInfo);
            
            return fullInfo;
        }
        public List<string> AddDesObject(List<string> commentInfo)
        {
            List<string> fullInfo = new List<string>();
            fullInfo.AddRange (commentInfo);
            foreach (string comment in fullInfo)
            {
                Console.WriteLine(comment);
            }
            

            return fullInfo;
        }

    }

    //  Методи: за датою - записи, очистити, додати, замінити.
    class Date : Schedule
        {

            public string WriteDate(string newDate)
            {
                string date = $"Leson date: {newDate}";
                Console.WriteLine(date);
                return date;
            }
            public void ClearDate(string date)
            {
                date.Remove(1);
            
            Console.WriteLine("Date was cleared");
             
            }
            public string AddDate(string newDate)
            {
                string date = $"Leson date: {newDate}";
                Console.WriteLine($"New date was added. {date}");
                return date;
            }
            public string RewriteDate(string message, string newDate)
            {
                if (message == "yes")
                {
                 date = $"Leson date: {newDate}";
                Console.WriteLine($"Date was changed. {date}");
                    return date;
                }
                else
                {
                    return message = $"Date was not changed. Current date {date}";
                }

            }

        }
        // за місцем - час початку і тривалість, чи можна вставити новий пункт.
          class Place : Schedule
        {
            public string TimeStartAndDuration(string place, string fullTime)
            {
              string placeAndTime = place + ". " + fullTime;
              return placeAndTime;
            }
            public string TryInsertItem(string newCommentInfo)
            {
              if (newCommentInfo == commentInfo)
            {
                string message = "This item has been already added";
                Console.WriteLine(message);
                return message;
            }
              else
            {
                commentInfo = newCommentInfo;
                return commentInfo;
            }
            }
        }
    }

