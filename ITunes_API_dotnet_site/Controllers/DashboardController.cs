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

    public class DashboardController : Controller
    {
        public ApplicationDbContext dbContext;

        // this is a constructor. has same name as class
        // sets connection of entity framework to dbcontext
        public DashboardController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            ViewData["Title"] = "Dashboard";
            // Queries - Aggregations
            // count
            int appCount = dbContext.MobileApp.Count();
            // avg rating
            double? avgUserRating = dbContext.MobileApp.Average(p => p.averageUserRating);
            // avg paid
            double? avgUserRatingPaid = dbContext.MobileApp
                .Where(i => i.formattedPrice != "Free")
                .Average(p => p.averageUserRating);
            // avg free
            double? avgUserRatingFree = dbContext.MobileApp
                .Where(i => i.formattedPrice == "Free")
                .Average(p => p.averageUserRating);

            ViewData["app_count"] = appCount;
            ViewData["avg_rating"] = avgUserRating;
            ViewData["avg_rating_paid"] = avgUserRatingPaid;
            ViewData["avg_rating_free"] = avgUserRatingFree;

            //initialize dashboard query object
            DashboardViewModel results = new DashboardViewModel();
            // Ratings
            Application[] ratings = dbContext.MobileApp
                .OrderByDescending(p => p.UserRatingCount)
                .Take(10)
                .ToArray();
            // Current Version Ratings
            Application[] averageUserRatings = dbContext.MobileApp
                .OrderByDescending(p => p.averageUserRating)
                .Take(10)
                .ToArray();

            results.ratings = ratings;
            results.averageRatings = averageUserRatings;

            return View(results);
        }
    }
}