using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ExrizCorpPanel.UI.Areas.Admin.Helpers;
using ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExrizCorpPanel.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class SocialMediaManagamentController : Controller
    {
        ILanguageRepository _lang;
        ISocialMediaTypesRepository _types;
        ISocialMediaObjectRepository _object;
        IImageRepository _image;
        IHostingEnvironment host;

        public SocialMediaManagamentController(ILanguageRepository lang, ISocialMediaTypesRepository smtype, ISocialMediaObjectRepository smobject, IImageRepository image, IHostingEnvironment _host)
        {
            _lang = lang;
            _types = smtype;
            _object = smobject;
            _image = image;
            host = _host;
        }

        #region InsertSocialType
        public IActionResult AddSocialType()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSocialType([FromForm] SocialTypeRequest type)
        {

            SocialMediaTypes newtype = new SocialMediaTypes();
            newtype.SocialMediaName = type.SocialMediaName;
            var id = _types.InsertAndGetId(newtype);

            string newFileName = "";
            if (type.MediaIcon != null)
            {
                FileUpload file = new FileUpload(host);
                //foreach (IFormFile source in post.images)
                //{
                string filename = ContentDispositionHeaderValue.Parse(type.MediaIcon.ContentDisposition).FileName.ToString().Trim('"');
                var wanted = "socialMediaTypes_" + id;
                filename = file.EnsureCorrectFilename(filename, wanted);
                newFileName = "\\uploads\\socialMediaTypes\\" + filename;
                using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "\\uploads\\socialMediaTypes\\")))
                    type.MediaIcon.CopyTo(output);

                //}
                var ImageId = _image.InsertAndgetId(new Data.Models.DB.Image { ImageAltTag = type.SocialMediaName, ImageTag = "", ImageTitle = type.SocialMediaName, ImageUrl = newFileName, IsBelongGallery = false });
                var selected = _types.GetById(id);
                selected.SocialMediaImageId = ImageId;
                _types.Update(selected);
                _types.Save();


            }

            TempData["result"] = 1;
            return RedirectToAction("AddSocialType");
        }


        #endregion

        #region UpdateSocialType
        public IActionResult UpdateSocialType(int id)
        {
            var selected = _types.GetById(id);
            var newType = new SocialTypeRequest();
            newType.Id = selected.Id;
            newType.SocialMediaName = selected.SocialMediaName;
            newType.MediaIconImage = _image.GetById(selected.SocialMediaImageId);
            return View(newType);
        }

        [HttpPost]
        public IActionResult UpdateSocialType([FromForm] SocialTypeRequest type)
        {

            var selected = _types.GetById(type.Id);
            selected.SocialMediaName = type.SocialMediaName;
            _types.Update(selected);
            _types.Save();

            string newFileName = "";
            if (type.MediaIcon != null)
            {
                FileUpload file = new FileUpload(host);
                //foreach (IFormFile source in post.images)
                //{
                string filename = ContentDispositionHeaderValue.Parse(type.MediaIcon.ContentDisposition).FileName.ToString().Trim('"');
                var wanted = "socialMediaTypes_" + type.Id;
                filename = file.EnsureCorrectFilename(filename, wanted);
                newFileName = "\\uploads\\socialMediaTypes\\" + filename;
                using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "\\uploads\\socialMediaTypes\\")))
                    type.MediaIcon.CopyTo(output);

                //}
                var ImageId = _image.InsertAndgetId(new Data.Models.DB.Image { ImageAltTag = type.SocialMediaName, ImageTag = "", ImageTitle = type.SocialMediaName, ImageUrl = newFileName, IsBelongGallery = false });
                var select = _types.GetById(type.Id);
                selected.SocialMediaImageId = ImageId;
                _types.Update(selected);
                _types.Save();


            }
      

            return RedirectToAction("GetSocialType");
        }
        #endregion

        #region GetSocialType
        public IActionResult GetSocialTypeById(int id)
        {
            var selected = _types.GetById(id);
            return View(selected);
        }


        public IActionResult GetSocialType()
        {
            var list = _types.GetAll().Select(x => new SocialTypeRequest { Id = x.Id, SocialMediaName = x.SocialMediaName }).ToList();
            return View(list);
        }

        #endregion

        #region DeleteSocialType

        public int deleteSocialType(int id)
        {
            var smobject = _object.GetMany(x => x.TypeId == id);
            foreach (var item in smobject)
            {
                _object.Delete(item.Id);
            }
            _object.Save();
            _types.Delete(id);
            _types.Save();
            return 1;
        }


        #endregion

        #region InsertSocialObject
        public IActionResult AddSocialObject()
        {
            var list = _types.GetAll();
            var typeList = new List<SelectListItem>();
            foreach (var item in list)
            {
                typeList.Add(new SelectListItem { Text = item.SocialMediaName, Selected = false, Value = item.Id.ToString() });
            }
            ViewBag.TypeList = typeList;
            return View();
        }

        [HttpPost]
        public IActionResult AddSocialObject([FromForm]SocialMediaObject obj)
        {
            _object.Insert(obj);
            _object.Save();
            TempData["result"] = 1;
            return RedirectToAction("AddSocialObject");
        }


        #endregion

        #region UpdateSocialObject
        public IActionResult UpdateSocialObject(int id)
        {
            var list = _types.GetAll();
            var typeList = new List<SelectListItem>();
            foreach (var item in list)
            {
                typeList.Add(new SelectListItem { Text = item.SocialMediaName, Selected = false, Value = item.Id.ToString() });
            }
            ViewBag.TypeList = typeList;
            var selected = _object.GetById(id);
            return View(selected);
        }

        [HttpPost]
        public IActionResult UpdateSocialObject([FromForm] SocialMediaObject obj)
        {
            var selected = _object.GetById(obj.Id);
            selected.ProfileName = obj.ProfileName;
            selected.Smprofile = obj.Smprofile;
            selected.TypeId = obj.TypeId;
            _object.Update(selected);
            _object.Save();

            return RedirectToAction("GetSocialObject");
        }
        #endregion

        #region GetSocialObject
        public IActionResult GetSocialObjectById(int id)
        {
            var selected = _object.GetById(id);
            return View(selected);
        }


        public IActionResult GetSocialObject()
        {
            var list = _object.GetAll();
            return View(list);
        }

        #endregion

        #region DeleteSocialObject

        public int deleteSocialObject(int id)
        {
            _object.Delete(id);
            _object.Save();
            return 1;
        }

        public int deleteImageFromSmType(int id)
        {
            try
            {
                var selected = _types.GetById(id);
                _image.Delete(id);
                _image.Save();
                selected.SocialMediaImageId = 0;
                _types.Update(selected);
                _types.Save();
                return 200;
            }
            catch (Exception)
            {
                return 400;
            }
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