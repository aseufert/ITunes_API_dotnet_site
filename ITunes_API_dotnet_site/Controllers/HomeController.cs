using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;
using ITunes_API_dotnet_site.DataAccess;
using ITunes_API_dotnet_site.Models;

namespace ITunes_API_dotnet_site.Controllers
{
    public class HomeController : Controller
    {
        string BASE_URL = "https://rss.itunes.apple.com/api/v1/us/ios-apps/{0}/all/100/explicit.json";
        string itunesData = "";

        public ApplicationDbContext dbContext;

        // this is a constructor. has same name as class
        // sets connection of entity framework to dbcontext
        public HomeController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public ActionResult Index(string option)
        {
            // use query string to determine top free or top paid
            string[] options = { "top-paid", "top-free" };
            string param = Array.IndexOf(options, option) != -1 ? option : "top-free";
            // set paid boolean
            bool paid = param == "top-paid";
            // format url
            string url = string.Format(BASE_URL, param);
            // set page title
            ViewData["Title"] = param == "top-free" ? "Top Free" : "Top Paid";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );

            Models.OuterJSON apiResults = null;

            client.BaseAddress = new Uri(url);


            TopApp[] data = dbContext.TopApps
                                .Include(c => c.appGenres)
                                .ThenInclude(ag => ag.Genre)
                                .Where(c => c.paid == paid)
                                .Take(100)
                                .ToArray();

            // if there's no data in db, call api
            if (data.Length == 0)
            {
                // prep objects for db initialization
                List<Models.Genre> dbGenres = dbContext.AppGenre.ToList();
                List<Models.Genre> updateGenres = new List<Models.Genre>();
                var dbApps = new List<Models.TopApp>();
                var dbAppGenres = new List<Models.TopAppGenres>();
                // make async call 
                HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    itunesData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }
                if (!itunesData.Equals(""))
                {
                    apiResults = JsonConvert.DeserializeObject<OuterJSON>(itunesData);
                    data = apiResults.feed.results;

                    foreach (Models.TopApp app in apiResults.feed.results)
                    {
                        foreach (Models.Genre genre in app.genres)
                        {
                            Models.TopAppGenres newGenre = new Models.TopAppGenres();
                            newGenre.TopApp = app;
                            var genreIdx = dbGenres.FindIndex(item => item.genreId == genre.genreId);
                            if (genreIdx == -1)
                            {
                                var updateIdx = updateGenres.FindIndex(item => item.genreId == genre.genreId);
                                if (updateIdx == -1)
                                {
                                    updateGenres.Add(genre);
                                    newGenre.Genre = genre;
                                } else
                                {
                                    newGenre.Genre = updateGenres[updateIdx];
                                }
                            } else
                            {
                                newGenre.Genre = dbGenres[genreIdx];
                            }
                            dbAppGenres.Add(newGenre);
                        }
                        app.paid = paid;
                        dbApps.Add(app);
                    }
                    foreach (Models.Genre item in dbGenres)
                    {
                        Console.WriteLine(item.genreId);
                    }
                    dbContext.TopApps.AddRange(dbApps);
                    dbContext.AppGenre.AddRange(updateGenres);
                    dbContext.TopAppGenres.AddRange(dbAppGenres);

                    dbContext.SaveChanges();
                }

            }
            return View(data);
        }
    }
}
