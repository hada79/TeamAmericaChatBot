namespace Program {
    using Data.DAL;
    using Data.Models;
    using Data.Repositories;
    using HtmlAgilityPack;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;


    class GameScraper {
        
        private static Uri _uriSchedule = new Uri("https://apps.dashplatform.com");
        private static string _path = "/dash/index.php";
        private static string _queryParams = "?action=schedule_location?mysam_company=sportslink";
        private static Uri _botUri = new Uri("http://localhost:3979/api/messages");
        private static GameRepository gameRepo;

        static void Main(string[] args) {

            gameRepo = new GameRepository();
            List<Game> games = getGamesFromSportsConnectionWebsite();
            foreach (Game g in games) {
                bool saved = gameRepo.Create(g);
            }            
            

            //triggerNotifyUsersViaAPI();
        }

        private static void triggerNotifyUsersViaAPI() {

            
           //call the API to notify players on Tuesday

        }

        private static DateTime getNextMondaysDate() {
            DateTime today = DateTime.Today;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysUntilMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
            DateTime nextMonday = today.AddDays(daysUntilMonday);
            return nextMonday;
        }

        private static List<Game> getGamesFromSportsConnectionWebsite() {
            string date = "11/21/2016";
            DateTime d = DateTime.Parse(date);
            string htmlString = getHTMLofSchedule(d);//getNextMondaysDate());
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlString);
            
            HtmlNodeCollection col = doc.DocumentNode.SelectNodes("//div[contains(@class,'event-tile')]");

            List<Game> games = new List<Game>();
            foreach (HtmlNode div in col) {
                if(div.InnerText.Contains("Team America")) {
                    Game game = new Game();
                    game.GameId = Guid.NewGuid();
                    var title = div.Descendants()
                        .Where(n => n.Name.Equals("strong"))
                        .Single();
                    game.Title = title.InnerText;
                    var type = div.Descendants()
                        .Where(n => n.Name.Equals("em"))
                        .Single();
                    game.Type = type.InnerText;
                    var time = div.Descendants()
                        .Where(n => n.GetAttributeValue("class", "").Equals("event-time"))
                        .Single();
                    var timeText = time.InnerText;
                    timeText = timeText.Replace(" at ", " ");
                    
                    game.DateTime = DateTime.Parse(timeText);

                    var field = div.Descendants()
                        .Where(n => n.GetAttributeValue("class", "").Equals("event-location"))
                        .Single();
                    game.Field = field.InnerText.Trim();                    

                    games.Add(game);
                }
            }
            return games;
        }

        private static string getHTMLofSchedule(DateTime gameDate) {
            string stringContent = null;
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler)) {
                client.BaseAddress = _uriSchedule;

                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("facilityID", "1"));
                postData.Add(new KeyValuePair<string, string>("eventType", "g"));
                postData.Add(new KeyValuePair<string, string>("startDate", gameDate.Date.ToString()));
                postData.Add(new KeyValuePair<string, string>("endDate", gameDate.Date.ToString()));
                postData.Add(new KeyValuePair<string, string>("run", "true"));
                postData.Add(new KeyValuePair<string, string>("mysam_company", "sportslink"));

                HttpContent content = new FormUrlEncodedContent(postData);

                cookieContainer.Add(_uriSchedule, new Cookie("mysam_company", "sportslink"));

                var response = client.PostAsync(_path + _queryParams, content).Result;
                return stringContent = response.Content.ReadAsStringAsync().Result;                
            }
        }
    }
}
