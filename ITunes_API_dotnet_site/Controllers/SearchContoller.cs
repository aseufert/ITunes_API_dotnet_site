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
    public class SearchController : Controller
    {
        string BASE_URL = "http://itunes.apple.com/search?entity=software&limit=20&term={0}";
        string appData = "";

        [Route("Search")]
        public IActionResult Search(string term)
        {
            ViewData["Title"] = "Search";
            string url = string.Format(BASE_URL, term);
            // initialize web request
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );

            Models.SearchRoot resp = new Models.SearchRoot();
            if (!string.IsNullOrEmpty(term))
            {
                client.BaseAddress = new Uri(url);
                HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    appData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }
                if (!appData.Equals(""))
                {
                    // deserialize object, grab results from api, and save
                    resp = JsonConvert.DeserializeObject<SearchRoot>(appData);
                }
            }

            return View(resp);
        }
    }
}