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
    public class AppsController : Controller
    {
        string BASE_URL = "http://itunes.apple.com/lookup?entity=software&id={0}";
        string appData = "";

        public ApplicationDbContext dbContext;

        // this is a constructor. has same name as class
        // sets connection of entity framework to dbcontext
        public AppsController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        [Route("Apps/Detail/{id:int:min(1)}")]
        public IActionResult Detail(int id)
        {
            string url = string.Format(BASE_URL, id);
            // initialize web request
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );

            Models.DetailJSON resp = null;

            client.BaseAddress = new Uri(url);
            // run point query
            Application data = dbContext.MobileApp
                .Where(p => p.trackId == id)
                .SingleOrDefault();
            // if there's no data in db, call api
            if (data == null)
            {
                HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    appData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!appData.Equals(""))
                {
                    // deserialize object, grab results from api, and save
                    resp = JsonConvert.DeserializeObject<DetailJSON>(appData);
                    data = resp.results[0];
                    dbContext.MobileApp.Add(data);

                    dbContext.SaveChanges();
                }
            }
            ViewData["title"] = "App Detail Page";

            return View(data);
        }
    }
}