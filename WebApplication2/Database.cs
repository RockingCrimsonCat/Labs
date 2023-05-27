using Microsoft.AspNetCore.Mvc;
using Npgsql;
using NY_API.Model;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Resources;
using System.Xml.Linq;
using Test.Model;
using WebApplication2;


namespace NY_API
{
    public class DatabaseMostPopular
    {
        NpgsqlConnection connection = new NpgsqlConnection(Constants.Connect);
   
        public async Task InsertMostPopularAsync (MostPopular mostPopular, string period)
        {
            
            var sql = "insert into public.\"MostPopular\"(\"Url\", \"Source\", \"Published_date\", \"ByLine\", \"Title\", \"Abstract\", \"Time\")"
                + $"values (@Url,@Source,@Published_date,@ByLine,@Title,@Abstract,@Time)";
            
            NpgsqlCommand command = new NpgsqlCommand (sql, connection);
           
            foreach (var result in mostPopular.Results)
            {
                command.Parameters.AddWithValue("Url", result.Url);
                command.Parameters.AddWithValue("Source", result.Source);
                command.Parameters.AddWithValue("Published_date", result.Published_date);
                command.Parameters.AddWithValue("ByLine", result.ByLine);
                command.Parameters.AddWithValue("Title", result.Title);
                command.Parameters.AddWithValue("Abstract", result.Abstract);
   
            }

            command.Parameters.AddWithValue("Time", DateTime.Now);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync ();
            await connection.CloseAsync ();
     

        }
        public async Task<List<Articles>> SelectStatist()
        {
            List<Articles> articles = new List<Articles>();
            await connection.OpenAsync();
            var sql = "select \"Url\", \"Source\", \"Published_date\", \"ByLine\", \"Title\", \"Abstract\", \"Time\" from public.\"MostPopular\"";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            NpgsqlDataReader reader = command.ExecuteReader ();
            while (await reader.ReadAsync())
            {
                articles.Add(new Articles { 
                    Url = reader.GetString(0), 
                    Source = reader.GetString(1), 
                    Published_date = reader.GetString(2), 
                    ByLine = reader.GetString(3), 
                    Title = reader.GetString(4), 
                    Abstract = reader.GetString(5), 
                    Time = reader.GetString(6) });
            }
            await connection.CloseAsync();
            return articles;
        }
    }
   public class DatabaseMovieReviews
    {
        NpgsqlConnection connection = new NpgsqlConnection(Constants.Connect);
        public async Task InsertMovieReviewsAsync(MovieReviews movieReviews, string period)
        {

            var sql = "insert into public.\"MovieReviews\"(\"Display_title\", \"Critics_pick\", \"Byline\", \"Headline\", \"Summary_short\", \"Publication_date\")"
                + $"values (@Display_title,@Critics_pick,@Byline,@Headline,@Summary_short,@Publication_date)";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);

            foreach (var result in movieReviews.Results)
            {
                command.Parameters.AddWithValue("Display_title", result.Display_title);
                command.Parameters.AddWithValue("Critics_pick", result.Critics_pick);
                command.Parameters.AddWithValue("Byline", result.Byline);
                command.Parameters.AddWithValue("Headline", result.Headline);
                command.Parameters.AddWithValue("Summary_short", result.Summary_short);
                command.Parameters.AddWithValue("Publication_date", result.Publication_date);
            }

           
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();

        }

    }
    
    public class DatabaseUsersReview
    {
        NpgsqlConnection connection = new NpgsqlConnection(Constants.Connect);
        public async Task InsertUsersReviewAsync(UsersReview usersReview, string name, string type, string url, string summaryShort, int numResult)
        {

            var sql = "insert into public.\"UsersReview\"(\"Name\", \"Type\", \"Url\", \"SummaryShort\", \"NumResult\", \"Time\")"
                + $"values (@Name,@Type,@Url,@SummaryShort,@NumResult,@Time)";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);


            command.Parameters.AddWithValue("Name", name);
            command.Parameters.AddWithValue("Type", type);
            command.Parameters.AddWithValue("Url", url);
            command.Parameters.AddWithValue("SummaryShort", summaryShort);
            command.Parameters.AddWithValue("NumResult", numResult);

            command.Parameters.AddWithValue("Time", DateTime.Now);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();

        }


        public async Task<List<StatistUsersReview>> SelectStatistUsersReview()
        {
            List<StatistUsersReview> review = new List<StatistUsersReview>();
            await connection.OpenAsync();
            var sql = "select \"Name\", \"Type\", \"Url\", \"SummaryShort\", \"NumResult\" from public.\"UsersReview\"";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (await reader.ReadAsync())
            {
                review.Add(new StatistUsersReview
                {
                    Name = reader.GetString(0),
                    Type = reader.GetString(1),
                    Url = reader.GetString(2),
                    SummaryShort = reader.GetString(3),
                    NumResult = reader.GetInt32(4)
                });
            }
            await connection.CloseAsync();
            return review;
        }
        public async Task UpdateLastStatistUsersReview(StatistUsersReview updatedReview)
        {
  
            await connection.OpenAsync();
           
            var sql = "update public.\"UsersReview\" set \"Type\" = @Type, \"Url\" = @Url, \"SummaryShort\" = @SummaryShort, \"NumResult\" = @NumResult where \"Name\" = @Name";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("Name", updatedReview.Name);
            command.Parameters.AddWithValue("Type", updatedReview.Type);
            command.Parameters.AddWithValue("Url", updatedReview.Url);
            command.Parameters.AddWithValue("SummaryShort", updatedReview.SummaryShort);
            command.Parameters.AddWithValue("NumResult", updatedReview.NumResult);
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }
        public async Task DeleteUsersReview(string name)
        {
            await connection.OpenAsync();
            
            var sql = "delete from public.\"UsersReview\" where \"Name\" = @Name";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("name", name);
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }

    }
   
}
