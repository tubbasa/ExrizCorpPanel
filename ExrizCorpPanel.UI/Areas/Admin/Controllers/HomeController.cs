using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ExrizCorpPanel.UI.Areas.Admin.CustomFilter;
using Microsoft.AspNetCore.Mvc;
using ExrizCorpPanel.UI.Areas.Admin.Models;
using ExrizCorpPanel.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExrizCorpPanel.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    [LoginFilter]
    public class HomeController : Controller
    {
        ISiteDetailRepository _repo;



        public HomeController(ISiteDetailRepository repo)
        {
            _repo = repo;
        }

        
        public IActionResult Index()
        {
            var site = _repo.GetAll();
            var siteName = site.FirstOrDefault();
            if (siteName != null)
            {
                TempData["siteName"] = siteName.SiteName;
                TempData["apiDetail"] = siteName.AnalyticsApikey;
            }
            else
            {
                TempData["siteName"] = "Has not got any site detail";
                TempData["apiDetail"] = "";
            }
            return View();
        }

      
    }
}
