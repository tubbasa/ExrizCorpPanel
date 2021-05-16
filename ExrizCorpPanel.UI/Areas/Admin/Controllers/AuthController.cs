using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using ExrizCorpPanel.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using  ExrizCorpPanel.Data.Models.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ExrizCorpPanel.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class AuthController : Controller
    {
        #region sessionKeys
        #endregion


        #region User
        private readonly IUserRepository _authrepo;
        public AuthController(IUserRepository authrepo)
        {
            _authrepo = authrepo;
        }
        #endregion
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User kullanici)
        {
            var KullaniciVarMi = _authrepo.GetMany(x => x.Email == kullanici.Email && x.Password == kullanici.Password).SingleOrDefault();
            if (KullaniciVarMi != null)
            {
                if (KullaniciVarMi.IsAdmin== true)
                {
                    HttpContext.Session.SetString("email", KullaniciVarMi.Email);
                    HttpContext.Session.SetString("password", KullaniciVarMi.Password);
                    TempData["userInfo"] = KullaniciVarMi.Name;
                    return   new RedirectToRouteResult(new RouteValueDictionary { { "area", "admin" }, { "controller", "SliderManagament" }, { "action", "AddSliderGallery" } });

                }
                else
                {
                    ViewBag.Mesaj = "Yetkisiz Kullanıcı!";
                    return View();
                }
            }
            ViewBag.Mesaj = "Kullanıcı Bulunamadı";
            return View();
        }
    }
}