using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.DisplayTypes;
using ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExrizCorpPanel.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class FooterManagamentController : Controller
    {
        IFooterTypeRepository _type;
        IFooterObjectsRepository _object;

        public FooterManagamentController(IFooterTypeRepository type, IFooterObjectsRepository footerObject)
        {
            _type = type;
            _object = footerObject;
        }

        #region InsertFooterType
        public IActionResult AddFooterType()
        {
            return View();
        }

        [HttpPost]
        public IActionResult postFooterType([FromBody] FooterType type)
        {
            try
            {
                _type.Insert(type);
                _type.Save();
                TempData["result"] = 1;
                return RedirectToAction("AddFooterType", "FooterManagament");
            }
            catch (Exception)
            {
                TempData["result"] = -1;
                return RedirectToAction("AddFooterType", "FooterManagament");
            }
        }


        #endregion

        #region UpdateFooterType
        public IActionResult UpdateFooterType(int id)
        {
            var selected = _type.GetById(id);
            return View(selected);
        }
        
        [HttpPost]
        public IActionResult UpdateFooterType( FooterType type)
        {
            try
            {
                var selected = _type.GetById(type.Id);
                selected.TypeName = type.TypeName;
                _type.Update(selected);
                _type.Save();
                return RedirectToAction("GetFooterType");
            }
            catch (Exception)
            {
                return View(type.Id);
            }
        }
        #endregion

        #region GetFooterType

        public IActionResult GetFooterType()
        {
            var liste = _type.GetAll();
            return View(liste);
        }

        #endregion

        #region DeleteFooterType

        public IActionResult deleteFooterType(int id)
        {
            _type.Delete(id);
            _type.Save();
            return RedirectToAction("GetFooterType", "FooterManagament");
        }


        #endregion

        #region InsertFooterObject
        public IActionResult AddFooterObject()
        {
            var model = new FooterObjectRequest();
            model.footerTypes = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            var liste = _type.GetAll();
            model.footerTypes.Add(new SelectListItem { Text = "Lütfen tip seçin", Value = "-1", Selected = true });
            foreach (var item in liste)
            {
                model.footerTypes.Add(new SelectListItem { Text = item.TypeName, Value = item.Id.ToString(), Selected = false });
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AddFooterObject(FooterObjectRequest footer)
        {
            var newObject = new FooterObjects();
            newObject.Content = footer.Content;
            newObject.ObjectName = footer.ObjectName;
            newObject.TypeId = footer.TypeId;
            _object.Insert(newObject);
            _object.Save();
            return RedirectToAction("AddFooterObject", "FooterManagament");
        }


        #endregion

        #region UpdateFooterObject
        public IActionResult UpdateFooterObject(int id)
        {
            var selected = _object.GetById(id);
            var model = new FooterObjectRequest();
            model.Content = selected.Content;
            model.ObjectName = selected.ObjectName;
            model.Id = selected.Id;
            model.footerTypes = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            var liste = _type.GetAll();
            model.footerTypes.Add(new SelectListItem { Text = "Lütfen tip seçin", Value = "-1", Selected = true });
            foreach (var item in liste)
            {
                if (item.Id==selected.TypeId)
                {
                    model.footerTypes.Add(new SelectListItem { Text = item.TypeName, Value = item.Id.ToString(), Selected = true });
                    model.footerTypes.FirstOrDefault().Selected = false;
                }
                else
                {
                    model.footerTypes.Add(new SelectListItem { Text = item.TypeName, Value = item.Id.ToString(), Selected = false });

                }
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateFooterObject(FooterObjectRequest footer)
        {
            var selected = _object.GetById(footer.Id);
            selected.Content = footer.Content;
            selected.ObjectName = footer.ObjectName;
            selected.TypeId = footer.TypeId;
            _object.Update(selected);
            _object.Save();
            return RedirectToAction("GetFooterObject", "FooterManagament");
        }
        #endregion

        #region GetFooterObject
        public IActionResult GetFooterObject()
        {
            var list = _object.GetAll().Select(x=>new DisplayFooterObject{ Id=x.Id,Content=x.Content,ObjectName=x.ObjectName,TypeId=x.TypeId,TypeName=x.Type.TypeName});
            return View(list);
        }

        #endregion

        #region DeleteFooterObject

        public IActionResult deleteFooterObject(int id)
        {
            _object.Delete(id);
            _object.Save();
            return View();
        }


        #endregion

        #region OtherActions

        public IActionResult countFooterType()
        {
            //dil idsi verilecek
            return View();
        }

        public IActionResult countFooterObject()
        {
            //dil idsi verilecek
            return View();
        }
        #endregion 
    }
}