using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursova.Bot.Models;
using Npgsql;

namespace Kursova.Bot
{
    public class Constants
    {
        public static string adress = "https://localhost:7173";
        public static string Connect = "Host=localhost;Username=postgres;Password=strongpass;Database=postgres";
    }
    public class User
    {
        private HttpClient _httpClient;
        private static string _adress;


        public User()
        {
            _adress = Constants.adress;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_adress);
           
        }
        public async Task<MostPopular> GetMostPopularAsync(string period)
        {

            var responce = await _httpClient.GetAsync($"/NY/{period}/mostpopular");
            responce.EnsureSuccessStatusCode();
            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<MostPopular>(content);
            return result;
        }

        public async Task<MovieReviews> GetMovieReviewsAsync(string query)
        {

            var responce = await _httpClient.GetAsync($"/NY/{query}/moviereviews");
            responce.EnsureSuccessStatusCode();
            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<MovieReviews>(content);
            return result;
        }
        public UsersReview PutReview(string name, string type, string url, string summaryShort, int numResult, long id)
        {
            UsersReview usersReview = new UsersReview();
            usersReview.Name = name;
            usersReview.Type = type;
            usersReview.Url = url;
            usersReview.SummaryShort = summaryShort;
            usersReview.NumResult = numResult;
            usersReview.Id = id;
            return usersReview;
        }
        public class DatabaseUsersReview
        {
            public async Task InsertUsersReviewAsync(UsersReview usersReview, string name, string type, string url, string summaryShort, int numResult, long id)
            {
                NpgsqlConnection connection = new NpgsqlConnection(Constants.Connect);
                var sql = "insert into public.\"UsersReview\"(\"Name\", \"Type\", \"Url\", \"SummaryShort\", \"NumResult\", \"Time\", \"Id\")"
                    + $"values (@Name,@Type,@Url,@SummaryShort,@NumResult,@Time, @Id)";

                NpgsqlCommand command = new NpgsqlCommand(sql, connection);


                command.Parameters.AddWithValue("Name", name);
                command.Parameters.AddWithValue("Type", type);
                command.Parameters.AddWithValue("Url", url);
                command.Parameters.AddWithValue("SummaryShort", summaryShort);
                command.Parameters.AddWithValue("NumResult", numResult);
                command.Parameters.AddWithValue("Id", id);
                command.Parameters.AddWithValue("Time", DateTime.Now);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();

            }
            
            public async Task UpdateLastStatistUsersReview(StatistUsersReview updatedReview)
            {
                NpgsqlConnection connection = new NpgsqlConnection(Constants.Connect);
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
            public async Task UpdateUsersReview(StatistUsersReview updatedReview)
            {
                DatabaseUsersReview database = new DatabaseUsersReview();
                await database.UpdateLastStatistUsersReview(updatedReview);
               
            }
            public async Task DeleteUsersReview(string name)
            {
                NpgsqlConnection connection = new NpgsqlConnection(Constants.Connect);
                await connection.OpenAsync();

                var sql = "delete from public.\"UsersReview\" where \"Name\" = @Name";
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("name", name);
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
        }   
       
    }
}
