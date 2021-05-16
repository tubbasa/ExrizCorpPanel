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
    public class SliderManagamentController : Controller
    {
        ILanguageRepository _lang;
        ISliderRepository _slider;
        ISliderGalleryRepository _sliderGallery;
        IImageRepository _image;
        IHostingEnvironment host;
        ISlugRepository _slug;
        public SliderManagamentController(ILanguageRepository lang, ISliderRepository slider, ISliderGalleryRepository sliderGallery, IImageRepository image, IHostingEnvironment _host, ISlugRepository slug)
        {
            _lang = lang;
            _slider = slider;
            _sliderGallery = sliderGallery;
            _image = image;
            host = _host;
            _slug = slug;
        }
        #region InsertSliderGallery
        public IActionResult AddSliderGallery()
        {
            var language = _lang.GetAll();
            var langListItem = new List<SelectListItem>();
            foreach (var item in language)
            {
                langListItem.Add(new SelectListItem { Text = item.LangName, Selected = false, Value = item.Id.ToString() });
            }
            ViewBag.Languages = langListItem;
            var url = _slug.GetAll();
            var urlListItem = new List<SelectListItem>();
            foreach (var item in url)
            {
                urlListItem.Add(new SelectListItem { Text = item.EntityTitle, Selected = false, Value = item.Id.ToString() });
            }

            ViewBag.Urls = urlListItem;
            return View();
        }

        [HttpPost]
        public int postaddSliderGallery([FromBody]SliderGallery gallery)
        {
            var id = _sliderGallery.InsertAndGetId(gallery);
            return id;
        }

        [HttpPost]
        public IActionResult PostUploadFilesAjax(int id)
        {

            long size = 0;

            var files = Request.Form.Files;

            if (files.Count != 0)
            {

                FileUpload file = new FileUpload(host);
                foreach (IFormFile source in files)
                {
                    var newFileName = "";
                    var temperatureSlider = _slider.InsertAndGetId(new Slider { });
                    var gelenId = Request.Query.FirstOrDefault(x => x.Key == "id");
                    var tags = "";
                    string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');
                    var wanted = "slider_" + temperatureSlider;
                    filename = file.EnsureCorrectFilename(filename, wanted);
                    newFileName = "/uploads/sliders/" + filename;
                    using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "/uploads/sliders/")))
                        source.CopyTo(output);

                    var ImageId = _image.InsertAndgetId(new Data.Models.DB.Image { ImageAltTag = "", ImageTag = "", ImageTitle = "", ImageUrl = newFileName });
                    var selected = _slider.GetById(temperatureSlider);
                    selected.GalleryId = id;
                    selected.ImageId = ImageId;
                    _slider.Update(selected);
                    _slider.Save();
                }

            }


            var images = _slider.GetMany(x => x.GalleryId == id);
            var list = new List<Image>();
            foreach (var item in images)
            {
                var image = _image.GetById(Convert.ToInt32(item.ImageId));
                list.Add(image);
            }

            TempData["result"] = 1;

            return Json(list);
        }

        #endregion

        #region UpdateSliderGallery
        public IActionResult UpdateSliderGallery(int id)
        {
            var language = _lang.GetAll();
            var langListItem = new List<SelectListItem>();
            foreach (var item in language)
            {
                langListItem.Add(new SelectListItem { Text = item.LangName, Selected = false, Value = item.Id.ToString() });
            }
            ViewBag.Languages = langListItem;
            var url = _slug.GetAll();
            var urlListItem = new List<SelectListItem>();
            foreach (var item in url)
            {
                urlListItem.Add(new SelectListItem { Text = item.EntityTitle, Selected = false, Value = item.Id.ToString() });
            }

            ViewBag.Urls = urlListItem;

            var sliders = _slider.GetMany(x=>x.GalleryId==id);
            var imageList = new List<Image>();
            foreach (var item in sliders)
            {
                var image = _image.GetById(Convert.ToInt32(item.ImageId));
                imageList.Add(image);
            }

            ViewBag.ImageList = imageList;

            var selected = _sliderGallery.GetById(id);
            return View(selected);
        }

        [HttpPost]
        public int postupdateSliderGallery([FromBody]SliderGallery gallery)
        {
            var selected = _sliderGallery.GetById(gallery.Id);
            selected.IsActive = gallery.IsActive;
            selected.SliderGalleryName = gallery.SliderGalleryName;
            _sliderGallery.Update(selected);
            _sliderGallery.Save();
            return 1;
        }
        #endregion

        #region GetSliderGallery
        public IActionResult GetSliderGalleryById(int id)
        {
            var selected = _sliderGallery.GetById(id);
            return View(selected);
        }


        public IActionResult GetSliderGallery()
        {
            var list = _sliderGallery.GetAll();
            return View(list);
        }

        #endregion

        #region DeleteSliderGallery

        public IActionResult deleteSliderGallery(int id)
        {


            try
            {
                var list = _slider.GetMany(x => x.GalleryId == id);
                var imagesList = new List<int>();
                foreach (var item in list)
                {
                    imagesList.Add(Convert.ToInt32(item.ImageId));
                    item.ImageId = 0;
                    _slider.Delete(item.Id);
                }

                foreach (var item in imagesList)
                {
                    _image.Delete(item);
                }
                _slider.Save();
                _image.Save();
                _sliderGallery.Delete(id);
                _sliderGallery.Save();
                TempData["result"] = 1;
                return RedirectToAction("GetSliderGallery");
            }
            catch (Exception ex)
            {

                throw;
            }
     
        }


        #endregion

        #region InsertSlider
        [HttpGet]
        public IActionResult AddSlider()
        {
            var language = _lang.GetAll();
            TempData["lang"] = language;
            return View();
        }

        [HttpPost]
        public int postaddSlider([FromBody] Slider slider)
        {
            var selected = _slider.Get(x => x.ImageId == slider.ImageId);
            selected.LangId = slider.LangId;
            selected.RawNumber = slider.RawNumber;
            selected.SliderName = slider.SliderName;
            selected.Url = slider.Url;
            _slider.Update(selected);
            _slider.Save();
            //foreach (var item in slider)
            //{
            //    Slider newslider = new Slider();
            //    newslider.LangId = item.LangId;
            //    newslider.RawNumber = item.RawNumber;
            //    newslider.SliderName = item.SliderName;
            //    newslider.Url = item.Url;
            //    newslider.GalleryId = item.GalleryId;
            //    var id=_slider.InsertAndGetId(newslider);
            //    FileUpload file = new FileUpload(host);
            //    string filename = ContentDispositionHeaderValue.Parse(item.image.ContentDisposition).FileName.ToString().Trim('"');
            //    var wanted = "slider" + id;
            //    filename = file.EnsureCorrectFilename(filename, wanted);
            //    var newFileName = "\\uploads\\sliderImages\\" + filename;
            //    using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "\\uploads\\sliderImages\\")))
            //        item.image.CopyTo(output);
            //    var imageId = _image.InsertAndgetId(new Data.Models.DB.Image { ImageAltTag = "", ImageTag = "", ImageTitle = "", ImageUrl = newFileName, IsBelongGallery = false, IsJob = false});
            //    var selected = _slider.GetById(id);
            //    selected.ImageId = imageId;
            //    _slider.Update(selected);
            //    _slider.Save();


            //}
            return 1;
        }


        #endregion

        #region UpdateSlider
        public IActionResult UpdateSlider(int id)
        {
            var selected = _slider.GetById(id);
            return View(selected);
        }

        [HttpPost]
        public int postupdateSlider([FromBody] SliderRequest slider)
        {
            var selected = _slider.Get(x=>x.ImageId==slider.ImageId);
            selected.GalleryId = slider.GalleryId;
            selected.LangId = slider.LangId;
            selected.RawNumber = slider.RawNumber;
            selected.SliderName = slider.SliderName;
            selected.Url = slider.Url;
            _slider.Update(selected);
            _slider.Save();

            return 1;
        }
        #endregion

        #region GetSlider
        public IActionResult GetSliderById(int id)
        {
            var selected = _slider.GetById(id);
            return View(selected);
        }


        public IActionResult GetSlider()
        {
            var list = _slider.GetAll();
            return View(list);
        }

        #endregion

        #region DeleteSlider

        public int deleteSlider(int id)
        {
            _slider.Delete(id);
            _slider.Save();
            return 1;
        }


        #endregion

        #region OtherActions

        public IActionResult countSlider()
        {
            //dil idsi query string olarak gönderilecek ve kontorl edilecek
            return View();
        }

        public int deleteImageFromSlider(int id)
        {
            var sliderId = _slider.Get(x => x.ImageId == id).Id;
            _slider.Delete(sliderId);
            _slider.Save();
            _image.Delete(id);
            _image.Save();
            return 1;
        }
        #endregion 
    }
}