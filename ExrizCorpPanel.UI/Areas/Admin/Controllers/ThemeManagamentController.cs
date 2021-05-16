using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExrizCorpPanel.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class ThemeManagamentController : Controller
    {
        #region InsertTheme
        public IActionResult AddTheme()
        {
            return View();
        }

        [HttpPost]
        public IActionResult addTheme()
        {
            return View();
        }


        #endregion

        #region UpdateTheme
        public IActionResult UpdateTheme()
        {
            return View();
        }

        [HttpPost]
        public IActionResult updateTheme()
        {
            return View();
        }
        #endregion

        #region GetTheme
        public IActionResult GetTheme(int id)
        {
            return View();
        }


        public IActionResult GetTheme()
        {
            //dil idsi query string olarak gönderilecek ve kontorl edilecek
            return View();
        }

        #endregion

        #region DeleteTheme

        public IActionResult deleteTheme(int id)
        {
            return View();
        }


        #endregion

        #region OtherActions

        public IActionResult countTheme()
        {
            //dil idsi query string olarak gönderilecek ve kontorl edilecek
            return View();
        }
        #endregion 
    }
}