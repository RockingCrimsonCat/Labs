using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Polling;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;
using Kursova.Bot.Models;
using Npgsql;
using static Kursova.Bot.User;
using static Kursova.Bot.User.DatabaseUsersReview;
using static Kursova.Bot.Models.StatistUsersReview;
using System.Xml.Linq;


namespace Kursova.Bot
{
    public class TheNYTimesBot
    {
        static TelegramBotClient botClient = new TelegramBotClient("6289218308:AAEePctPfaOQwrI35g4SF-9f0lvjQQZCtcA");
        

        CancellationToken cancellationToken = new CancellationToken();
        ReceiverOptions receiverOptions = new ReceiverOptions { AllowedUpdates = { } };
       
        public async Task Start()
        {
            botClient.StartReceiving(HandlerUpdateAsync, HandlerError, receiverOptions, cancellationToken);
            var botMe = await botClient.GetMeAsync();
            Console.WriteLine($"Бот {botMe.Username} почав працювати");

        }

        public Task HandlerError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"An error occured in the telegram bot's API:\n {apiRequestException.ErrorCode}" +
                $"\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            return Task.CompletedTask;
        }

        public async Task HandlerUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandlerMessageAsync(botClient, update);
            }

        }


        private Dictionary<long, string> currentStage = new Dictionary<long, string>();

        public async Task HandlerMessageAsync(ITelegramBotClient botClient, Update update)
        {

            var message = update.Message;
            if (!currentStage.ContainsKey(message.Chat.Id))
            {
                currentStage.Add(message.Chat.Id, "home");
            }


            switch (currentStage[message.Chat.Id]) //чекає відповідь юзера
            {

                case "/query":
                        await GetMovieReviewsByQuery(message.Text);
                    break;
                case "/DoMyOwnReview":
                    await GetUsersReview(message.Text);
                    break;
                case "✍️ Update my review":
                    await GetUpdateUsersReview(message.Text);
                    break;
                case "❌ Delete my review":
                    await DeleteReview(message.Text);
                    break;
                case "/delete":
                    await DeleteReview(message.Text);
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Your review has been successfully deleted");
                    break;
                default:
                    
                    break;
            }
            switch (message.Text) //змінює вміст змінної (для всіх)
            {
                case "/query":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Enter only one word from the title of the movie or the word that is contained in the title.\n " +
                        "Do not enter more than one word to avoid mistakes.");
                    currentStage[message.Chat.Id] = "/query";
                    break;

                case "📰 Popular articles on NYTimes.com.":
                    currentStage[message.Chat.Id] = "nytimes";
                    break;

                case "/1":
                    currentStage[message.Chat.Id] = "/1";
                    break;
                case "/7":
                    currentStage[message.Chat.Id] = "/7";
                    break;
                case "/30":
                    currentStage[message.Chat.Id] = "/30";
                    break;
                case "/start":
                    currentStage[message.Chat.Id] = "/start";
                    break;
                case "/keyboard":
                    currentStage[message.Chat.Id] = "/keyboard";
                    break;
                case "🎥 Search for movie reviews":
                    currentStage[message.Chat.Id] = "🎥 Search for movie reviews";
                    break;
                case "📝 Create my own review":
                    currentStage[message.Chat.Id] = "📝 Create my own review";
                    break;
                case "/DoMyOwnReview":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Start typing. To avoid mistakes, follow the example and write each paragraph on a new line");
                    currentStage[message.Chat.Id] = "/DoMyOwnReview";
                    break;
                case "✍️ Update my review":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "You have the opportunity to change your previously made review. To do this, initially indicate the name of what you were reviewing. " +
                        "Then follow the example and commands from \"Create my own review\". You can change one paragraph or all paragraphs. But you cannot change the name. " +
                        "For example, if you want to change the rating of your review, leave all other paragraphs unchanged." +
                        "\nExample:" +
                        "\nSchindler's List " +
                        "\nFilm" +
                        "\nhttps://www.nytimes.com/1993/12/15/movies/review-film-schindler-s-list-imagining-the-holocaust-to-remember-it" +
                        "\n1993 American epic historical drama directed by Steven Spielberg and based on " +
                        "the novel Schindler's Ark by Thomas Keneally. Tells about the German businessman and NSDAP member " +
                        "Oskar Schindler, who saved more than a thousand Polish Jews from death during the Holocaust. " +
                        "\n9 (⬅️ changed )");
                    currentStage[message.Chat.Id] = "✍️ Update my review";
                    break;
                case "📓 Look all my reviews":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Here your all reviews");
                    currentStage[message.Chat.Id] = "📓 Look all my reviews";
                    break;
                case "❌ Delete my review":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Select this command to delete /delete \n" +
                        "Then enter the name of what you reviewed");
                    currentStage[message.Chat.Id] = "❌ Delete my review";
                    break;
                case "/delete":
                   
                    currentStage[message.Chat.Id] = "/delete";
                    break;
            }
            switch (currentStage[message.Chat.Id]) //не чекає відповіді
            {
                case "nytimes":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Select the command that means the period (in days) for which you want to get " +
                        "a list of the most visited articles. " +
                        "\n/1\n/7\n/30");
                    break;
                case "/1":
                    await GetArticlesByPeriod("1");
                    break;

                case "/7":
                    await GetArticlesByPeriod("7");
                    break;

                case "/30":
                    await GetArticlesByPeriod("30");
                    break;
                case "/start":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Welcome to the New York Times bot. Select " +
                        "command to continue /keyboard");
                    break;
              

                case "/keyboard":
                    ReplyKeyboardMarkup replyKeyboardMarkup = new ReplyKeyboardMarkup(
                        new[]
                        {
                new KeyboardButton[] { "📰 Popular articles on NYTimes.com." },
                new KeyboardButton[] { "🎥 Search for movie reviews", "📝 Create my own review" },
                new KeyboardButton[] { "✍️ Update my review", "❌ Delete my review" },
                new KeyboardButton[] { "📓 Look all my reviews" }

                        }
                    )
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Select menu item:", replyMarkup: replyKeyboardMarkup);
                    break;
                case "🎥 Search for movie reviews":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Select this command /query");
                    break;

                case "📝 Create my own review":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "You have the opportunity to create your own review. " +
                        "\nFirst, get name to what you are reviewing.  " +
                        "\nNext, specify the type; it can be a movie, a book, an article, whatever you wish. " +
                        "\nAfter you can add a link and a short description." +
                        "\nAt the end, enter your own estimate in integer format.\nSelect this command to get started /DoMyOwnReview " +
                        "\nExample:" +
                        "\nSchindler's List" +
                        "\nFilm " +
                        "\nhttps://www.nytimes.com/1993/12/15/movies/review-film-schindler-s-list-imagining-the-holocaust-to-remember-it." +
                        "\n1993 American epic historical drama directed by Steven Spielberg and based on " +
                        "the novel Schindler's Ark by Thomas Keneally. " +
                        "Tells about the German businessman and NSDAP member Oskar Schindler, " +
                        "who saved more than a thousand Polish Jews from death during the Holocaust." +
                        "\n10");
                    break;
                case "📓 Look all my reviews":
                    StatistUsersReview usersReview = new StatistUsersReview();
                    await GetAllUsersReview(message.Chat.Id);
                    break;
                case "❌ Delete my review":
                    await DeleteReview(message.Text);
                    break;
            }
            async Task GetArticlesByPeriod(string period)
            {
                User userArticles = new User();
                var resultArticles = userArticles.GetMostPopularAsync(period);
                foreach (var item in resultArticles.Result.Results)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"URL: {item.Url}\n" +
                    $"Source: {item.Source}\n" +
                    $"Published date: {item.Published_date}\n" +
                    $"ByLine: {item.ByLine}\n" +
                    $"Title: {item.Title}\n" +
                    $"Abstract: {item.Abstract}");

                }
                currentStage[message.Chat.Id] = "";
            }
            async Task GetMovieReviewsByQuery(string query)
            {
                User userMovieReviews = new User();
                var resultMovieReviews = userMovieReviews.GetMovieReviewsAsync(query);

                await botClient.SendTextMessageAsync(message.Chat.Id, $"{resultMovieReviews.Result.Copyright}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Number of Results: {resultMovieReviews.Result.Num_results}");

                foreach (var item in resultMovieReviews.Result.Results)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Display Title: {item.Display_title}\n" +
                     $"Critics Pick: {item.Critics_pick}\n" +
                     $"Byline: {item.Byline}\n" +
                     $"Headline: {item.Headline}\n" +
                     $"Summary Short: {item.Summary_short}\n" +
                     $"Publication Date: {item.Publication_date}\n" +
                     $"Date Updated: {item.Date_updated}\n" +
                     $"Link Type: {item.Link.Type}\n" +
                     $"Link URL: {item.Link.Url}\n" +
                     $"Link Text: {item.Link.Suggested_link_text}\n");

                }
                currentStage[message.Chat.Id] = "";
            }

            async Task GetUsersReview(string input)
            {
                

                string[] lines = input.Split('\n');
                if (lines.Length >= 5)
                {

                    string name = lines[0];
                    string type = lines[1];
                    string url = lines[2];
                    string summaryShort = lines[3];

                    int numResult = int.Parse(lines[4]);

                    var id = message.Chat.Id;
                    await DoUsersReview(name, type, url, summaryShort, numResult, id);
                    
                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Error in the entered data type");
                }
                currentStage[message.Chat.Id] = "";
            }

            async Task GetUpdateUsersReview(string input)
            {
                User usersReviews = new User();

                string[] lines = input.Split('\n');
                if (lines.Length >= 5)
                {

                    string name = lines[0];
                    string type = lines[1];
                    string url = lines[2];
                    string summaryShort = lines[3];
                    int numResult = int.Parse(lines[4]);
                    var id = message.Chat.Id;
                    User usersReviewsUpdate = new User();
                    StatistUsersReview usersReview = new StatistUsersReview();
                    var updatedReviews = usersReview.PutUpdatesReview(name, type, url, summaryShort, numResult, id);
                    DatabaseUsersReview databaseUsersReview = new DatabaseUsersReview();
                   
                     await databaseUsersReview.UpdateUsersReview(updatedReviews);
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Your review has been successfully modified\nName: {updatedReviews.Name}\n" +
           $"Type: {updatedReviews.Type}\n" +
           $"URL: {updatedReviews.Url}\n" +
           $"Summary Short: {updatedReviews.SummaryShort} \n" +
           $"Number of Results: {updatedReviews.NumResult}");
                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Error in the entered data type");
                }
                currentStage[message.Chat.Id] = "";
            }
            async Task DoUsersReview(string name, string type, string url, string summaryShort, int numResult, long id)
            {
                User usersReviews = new User();
                UsersReview usersReview = new UsersReview();
                var resultReviews = usersReviews.PutReview(name, type, url, summaryShort, numResult, id);

                await botClient.SendTextMessageAsync(message.Chat.Id, $"Name: {resultReviews.Name}\n" +
            $"Type: {resultReviews.Type}\n" +
            $"URL: {resultReviews.Url}\n" +
            $"Summary Short: {resultReviews.SummaryShort} \n" +
            $"Number of Results: {resultReviews.NumResult}");
                DatabaseUsersReview databaseUsersReview = new DatabaseUsersReview();
                await databaseUsersReview.InsertUsersReviewAsync(usersReview, name, type, url, summaryShort, numResult, id);
                currentStage[message.Chat.Id] = "";
            }
             async Task<List<StatistUsersReview>> SelectStatistUsersReview(long id)
            {
                NpgsqlConnection connection = new NpgsqlConnection(Constants.Connect);
                List<StatistUsersReview> review = new List<StatistUsersReview>();
                await connection.OpenAsync();
                var sql = $"select \"Name\", \"Type\", \"Url\", \"SummaryShort\", \"NumResult\" from public.\"UsersReview\" where \"Id\" = {message.Chat.Id}";
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
                currentStage[message.Chat.Id] = "";
                return review;
            }
            async Task GetAllUsersReview(long id)
            {
                StatistUsersReview usersReview = new StatistUsersReview();

               var result = SelectStatistUsersReview(id).Result;

                foreach (var item in result)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Name: {item.Name}\n" +
                $"Type: {item.Type}\n" +
                $"URL: {item.Url}\n" +
                $"Summary Short: {item.SummaryShort} \n" +
                $"Number of Results: {item.NumResult}");
                    currentStage[message.Chat.Id] = "";
                }
            }
            async Task DeleteReview(string name)
            {
                DatabaseUsersReview database = new DatabaseUsersReview();
                await database.DeleteUsersReview(name);
                currentStage[message.Chat.Id] = "";
            }
            return;  
            }
            
        }
    }
 

