using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITunes_API_dotnet_site.Controllers
{
    public class AboutController : Controller
    {
        [Route("About")]
        public IActionResult About(int id)
        {
            ViewData["title"] = "About Us";

            return View();
        }
    }
}