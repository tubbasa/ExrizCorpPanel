using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
using ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes;
using Microsoft.AspNetCore.Mvc;

namespace ExrizCorpPanel.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class UserManagamentController : Controller
    {
        IUserRepository _user;
        public UserManagamentController(IUserRepository user)
        {
            _user = user;
        }
        #region InsertUser
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser([FromForm] User user)
        {
            _user.Insert(user);
            _user.Save();
            return View();
        }


        #endregion

        #region UpdateUser
        public IActionResult UpdateUser(int id)
        {
            var selected = _user.GetById(id);
            return View(selected);
        }

        [HttpPost]
        public IActionResult UpdateUser([FromForm ]User user)
        {
            try
            {
                var selected = _user.GetById(user.Id);
                selected.Email = user.Email;
                selected.IsAdmin = user.IsAdmin;
                selected.IsEditor = user.IsEditor;
                selected.IsWebUser = user.IsWebUser;
                selected.Name = user.Name;
                selected.UserName = user.UserName;
                selected.Password = user.Password;
                selected.SurName = user.SurName;
                _user.Update(selected);
                _user.Save();
                return RedirectToAction("GetUser");
            }
            catch (Exception ex)
            {
                return RedirectToAction("GetUser");
            }
        }
        #endregion

        #region GetUser
        public IActionResult GetUserById(int id)
        {
            var selected = _user.GetById(id);
            return View(selected);
        }


        public IActionResult GetUser()
        {
            var list = _user.GetMany(x=>x.IsAdmin==true||x.IsEditor==true);
            return View(list);
        }
        public IActionResult GetWebUsers()
        {
            var list = _user.GetMany(x=>x.IsWebUser==true);
            return View(list);
        }

        #endregion

        #region DeleteUser

        public IActionResult deleteUser(int id)
        {
            _user.Delete(id);
            _user.Save();
            return RedirectToAction("GetUser");
        }


        #endregion

        #region OtherActions

        public int countUser()
        {
            return _user.count();
        }
        #endregion 
    }

    
}