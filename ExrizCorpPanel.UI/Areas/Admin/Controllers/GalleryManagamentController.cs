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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExrizCorpPanel.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class GalleryManagamentController : Controller
    {
        ILanguageRepository _lang;
        IGaleryRepository _gallery;
        IGalleryMappingRepository _mapping;
        IImageRepository _image;
        IHostingEnvironment host;
        public GalleryManagamentController(ILanguageRepository lang, IGaleryRepository gallery, IGalleryMappingRepository mapping, IHostingEnvironment _host, IImageRepository image)
        {
            _lang = lang;
            _gallery = gallery;
            _mapping = mapping;
            host = _host;
            _image = image;
        }
        #region InsertGallery
        public IActionResult AddGallery()


        {
            var language = _lang.GetAll();
            var liste = new List<SelectListItem>();
            liste.Add(new SelectListItem { Text = "Lütfen dil seçin", Value = "-1", Selected = true });
            foreach (var item in language)
            {
                liste.Add(new SelectListItem { Text = item.LangName, Value = item.Id.ToString(), Selected = false });
            }
            //liste.Items.a
            TempData["lang"] = liste;
            return View();
        }
        

        [HttpPost]
        public int postGallery([FromBody]GalleryRequest galery)
        {
            var gallery = new Galery();
            gallery.GaleryLink = galery.GaleryLink;
            gallery.GaleryName = galery.GaleryName;
           var id= _gallery.InsertAndgetId(gallery);
           
            TempData["result"] = 1;
            return id;
        }

        [HttpPost]
        public IActionResult UploadFilesAjax(int id)
        {
            var gelenId = Request.Query.FirstOrDefault(x=>x.Key=="id");
            long size = 0;
            var files = Request.Form.Files;
            string newFileName = "";
            if (files.Count != 0)
            {
                FileUpload file = new FileUpload(host);
                var sayac = 0;
                foreach (IFormFile source in files)
                {
                    string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');
                    var wanted = "gallery_" + id + "-" + sayac;
                    filename = file.EnsureCorrectFilename(filename, wanted);
                    newFileName = "/uploads/galleries/" + filename;
                    using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "/uploads/galleries/")))
                        source.CopyTo(output);
                    var imageId = _image.InsertAndgetId(new Data.Models.DB.Image { ImageAltTag = "", ImageTag = "", ImageTitle = "", ImageUrl = newFileName, IsBelongGallery = true, GalleryId = id });
                    sayac++;
                }

            }
            TempData["result"] = 1;
            string message = $"{files.Count} file(s)/{ size} bytes uploaded successfully!";
            return Json(message);
        }

        [HttpPost]
        public int postGalleryMapping([FromBody]List<GalleryLanguageMappingRequest> galery)
        {
            foreach (var item in galery)
            {
                var gallery = new GalleryLanguageMapping();
                gallery.GalleryContent = item.GalleryContent;
                gallery.GalleryId = item.GalleryId;
                gallery.GalleryTitle = item.GalleryTitle;
                gallery.Langıd = item.Langıd;
                _mapping.Insert(gallery);
            }

            _mapping.Save();
            TempData["result"] = 1;
            return 1;
        }

        public IActionResult uploadGalleryImages(GalleryRequest galery)
        {
            string newFileName = "";
            if (galery.images.Count != 0)
            {
                FileUpload file = new FileUpload(host);
                var sayac = 0;
                foreach (IFormFile source in galery.images)
                {
                    string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');
                    var wanted = "gallery_" + galery.Id + "-" + sayac;
                    filename = file.EnsureCorrectFilename(filename, wanted);
                    newFileName = "\\uploads\\galleries\\" + filename;
                    using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "\\uploads\\galleries\\")))
                        source.CopyTo(output);
                    var imageId = _image.InsertAndgetId(new Data.Models.DB.Image { ImageAltTag = "", ImageTag = "", ImageTitle = "", ImageUrl = newFileName, IsBelongGallery = true, GalleryId = galery.Id });
                    sayac++;
                }

            }
            TempData["result"] = 1;
            return RedirectToAction("AddGallery","GalleryManagament");
        }

        #endregion

        #region UpdateGallery
        public IActionResult UpdateGallery(int id)
        {
            var selected = _gallery.GetById(id);
            var language = _lang.GetAll();
            var liste = new List<SelectListItem>();
            liste.Add(new SelectListItem { Text = "Lütfen dil seçin", Value = "-1", Selected = true });
            foreach (var item in language)
            {
                liste.Add(new SelectListItem { Text = item.LangName, Value = item.Id.ToString(), Selected = false });
            }
            //liste.Items.a
            TempData["lang"] = liste;
            return View(selected);
        }
        

        [HttpPost]
        public IActionResult PostUpdateGallery([FromBody] GalleryRequest galery)
        {
            var gallery = _gallery.GetById(galery.Id);
            gallery.GaleryLink = galery.GaleryLink;
            gallery.GaleryName = galery.GaleryName;

            _gallery.Update(gallery);
            _gallery.Save();

            TempData["result"] = 1;
            return RedirectToAction("UpdateGallery", "GalleryManagament");      
        }

        [HttpPost]
        public int postupdateGalleryMapping([FromBody]List<GalleryLanguageMappingRequest> galery)
        {
            foreach (var item in galery)
            {
                var gallery = _mapping.GetById(item.Id);
                gallery.GalleryContent = item.GalleryContent;
                gallery.GalleryId = item.GalleryId;
                gallery.GalleryTitle = item.GalleryTitle;
                gallery.Langıd = item.Langıd;
                _mapping.Update(gallery);
                _mapping.Save();
            }

            TempData["result"] = 1;
            return 1;
        }

        [HttpPost]
        public Image GetImageDetail(int id)
        {
           var selected= _image.GetById(id);
            return selected;
        }


        [HttpPost]
        public int updateImageDetail([FromBody]Image image)
        {
            var selected = _image.GetById(image.Id);
            selected.ImageAltTag = image.ImageAltTag;
            selected.ImageTag = image.ImageTag;
            selected.ImageTitle = image.ImageTitle;
            _image.Update(selected);
            _image.Save();
            return 1;
        }
        [HttpPost]
        public IActionResult PostUploadFilesAjax(int id,string galeryName)
        {
            var gelenId = Request.Query.FirstOrDefault(x => x.Key == "id");
            long size = 0;
            var files = Request.Form.Files;
            string newFileName = "";
            if (files.Count != 0)
            {
                int sayac = 0;
                FileUpload file = new FileUpload(host);
                var lastId = _image.GetMany(x=>x.GalleryId==id).LastOrDefault();
                if (lastId!=null)
                {
                   sayac = Convert.ToInt32(lastId.ImageUrl.Split('-')[1].Split('.')[0]) + 1;
                }
             
                foreach (IFormFile source in files)
                {
                    var tags = galeryName.Replace(' ',',');
                    string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');
                    var wanted = "gallery_" + id + "-" + sayac;
                    filename = file.EnsureCorrectFilename(filename, wanted);
                    newFileName = "/uploads/galleries/" + filename;
                    using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "/uploads/galleries/")))
                        source.CopyTo(output);
                  
                    var imageId = _image.InsertAndgetId(new Data.Models.DB.Image { ImageAltTag = galeryName+""+sayac, ImageTag = tags, ImageTitle = galeryName + "" + sayac, ImageUrl = newFileName, IsBelongGallery = true, GalleryId = id });
                    sayac++;
                }

            }
            var list = _image.GetMany(x=>x.GalleryId==id);
            TempData["result"] = 1;
      
            return Json(list);
        }
        #endregion

        #region GetGallery
        public IActionResult GetGalleryById(int id)
        {
            return View();
        }


        public IActionResult GetGallery()
        {
            var galleryList = _gallery.GetAll();
            ViewData["list"] = galleryList.ToList();
            //dil idsi query string olarak gönderilecek ve kontorl edilecek
            return View(galleryList.ToList());
        }

        #endregion

        #region DeleteGallery

        public int deleteGallery(int id)
        {
            _gallery.Delete(id);
            _gallery.Save();
            return 1;
        }

        public int deleteImageFromGallery(int id)
        {
            _image.Delete(id);
            _image.Save();
            return 1;
        }


        #endregion


        #region OtherActions

        public IActionResult countGallery()
        {
            //dil idsi query string olarak gönderilecek ve kontorl edilecek
            return View();
        }
        #endregion 

    }
}