using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Text.Json;

namespace HttpClientDemo
{

    class Game
    {
        public int id { get; set; }
        public string title { get; set; }
        public int year { get; set; }
        public decimal price { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost");
                //HTTP GET
                var responseTask = client.GetAsync("games");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                   
                    var ret = readTask.Result;


                    var games = JsonSerializer.Deserialize<List<Game>>(ret);


                    foreach (var game in games)
                    {
                        Console.WriteLine(game.title);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}