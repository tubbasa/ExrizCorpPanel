using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExrizCorpPanel.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class ContactFormManagamentController : Controller
    {
        #region InsertContactForm
        public IActionResult AddContactForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult addContactForm()
        {
            return View();
        }


        #endregion

        #region UpdateContactForm
        public IActionResult UpdateContactForm(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult updateContactForm()
        {
            return View();
        }
        #endregion

        #region GetContactForm
        public IActionResult GetContactForm(int id)
        {
            return View();
        }


        public IActionResult GetContactForm()
        {
            //dil idsi query string olarak gönderilecek ve kontorl edilecek
            return View();
        }

        #endregion

        #region DeleteContactForm

        public IActionResult deleteContactForm(int id)
        {
            return View();
        }


        #endregion
        
        #region OtherActions

        public IActionResult countContactForm()
        {
            //dil idsi verilecek
            return View();
        }
        #endregion 
    }
}