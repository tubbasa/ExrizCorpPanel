using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ExrizCorpPanel.UI.Areas.Admin.CustomFilter;
using ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.DisplayTypes;
using ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes;
using ExrizCorpPanel.Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExrizCorpPanel.UI.Areas.Admin.Helpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using ExrizCorpPanel.Data.Models.DB;

namespace ExrizCorpPanel.UI.Areas.Admin.Controllers
{
    [LoginFilter]
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class BannerManagamentController : Controller
    {
    
        private IBannerRepository _banner;
      
        private ILanguageRepository _language;

        private IImageRepository _image;

        private IHostingEnvironment host;
            
        public BannerManagamentController(ILanguageRepository language, IBannerRepository banner, IHostingEnvironment _host, IImageRepository image) 
        {
            this._banner = banner;
            this._language = language;
            this._image = image;
            host = _host;

        }

        #region InsertBanner
        public IActionResult AddBanner()
        {
            var language = _language.GetAll();
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
        public IActionResult PostBanner([FromForm]BannerRequest banner)
        {

            try
            {
               var id= _banner.InsertAndgetId(new Data.Models.DB.Banner { BannerContent = banner.BannerContent, BannerLink = banner.BannerLink, BannerName = banner.BannerName, LangId = banner.LangId });
                //var id = _banner.GetLast();

                string newFileName = "";
                if (banner.banner.Count!=0)
                {
                    FileUpload file = new FileUpload(host);
                foreach (IFormFile source in banner.banner)
                {
                    string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');
                        var wanted = "banner_" + id;
                    filename = file.EnsureCorrectFilename(filename, wanted);
                        newFileName = "\\uploads\\banners\\" + filename;
                    using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "\\uploads\\banners\\")))
                        source.CopyToAsync(output);
                    
                }
                   var ImageId= _image.InsertAndgetId(new Data.Models.DB.Image { ImageAltTag = banner.BannerName, ImageTag = "", ImageTitle = banner.BannerName, ImageUrl = newFileName, IsBelongGallery = false });
                    var selected = _banner.GetById(id);
                    selected.Image = ImageId;
                    _banner.Update(selected);
                    _banner.Save();


                }
                TempData["result"] = 1;
                return RedirectToAction("AddBanner", "BannerManagament");
            }
            catch (Exception ex)
            {
                TempData["result"] = -1;
                throw;
            }
        }


        #endregion

        #region UpdateBanner
        public IActionResult UpdateBanner(int id)
        {
            var language = _language.GetAll();
            var selected = _banner.GetById(id);
            var liste = new List<SelectListItem>();
            liste.Add(new SelectListItem { Text = "Lütfen dil seçin", Value = "-1", Selected = true });
            foreach (var item in language)
            {
                if (item.Id== selected.LangId)
                {
                    liste.Add(new SelectListItem { Text = item.LangName, Value = item.Id.ToString(), Selected = true });

                }
                else
                {
                    liste.Add(new SelectListItem { Text = item.LangName, Value = item.Id.ToString(), Selected = false });

                }
            }
            var selectedImage = _image.Get(x=>x.Id==selected.Image);
            string imageUrl = "";
            if (selectedImage!=null)
            {
                imageUrl = selectedImage.ImageUrl;
            }
            else
            {
                imageUrl = null;
            }
            var model = new BannerRequest { Id = selected.Id, BannerContent = selected.BannerContent, BannerLink = selected.BannerLink, BannerName = selected.BannerName, LangId = selected.LangId, Image = selected.Image,imageUrl=imageUrl };
         
            //liste.Items.a
            TempData["lang"] = liste;

            return View(model);
        }

        [HttpPost]
        public IActionResult PostUpdateBanner(BannerRequest banner)
        {
            try
            {
                var editedBanner = _banner.GetById(banner.Id);
                editedBanner.BannerContent = banner.BannerContent;
                editedBanner.BannerLink = banner.BannerLink;
                editedBanner.BannerName = banner.BannerName;
                editedBanner.LangId = banner.LangId;
                _banner.Update(editedBanner);
                _banner.Save();
                //var id = _banner.GetLast();

                string newFileName = "";
                int imageId = 0;
                if (banner.banner!=null)
                {
                    FileUpload file = new FileUpload(host);
                    foreach (IFormFile source in banner.banner)
                    {
                        string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');
                        var wanted = "banner_" + banner.Id;
                        filename = file.EnsureCorrectFilename(filename, wanted);
                        newFileName = "\\uploads\\banners\\" + filename;
                        using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "\\uploads\\banners\\")))
                            source.CopyToAsync(output);

                    }
                     
                    Image img = new Image();
                    img.Id = imageId;
                    img.ImageAltTag = banner.BannerName;
                    img.ImageTag = "";
                    img.ImageTitle = banner.BannerName;
                    img.ImageUrl = newFileName;
                    img.IsBelongGallery = false;
                     _image.Update(img);
                  
                    _image.Save();
                    imageId = img.Id;



                }

                editedBanner.Image = imageId;
                _banner.Update(editedBanner);
                _banner.Save();
                TempData["result"] = 1;
                return RedirectToAction("AddBanner", "BannerManagament");
            }
            catch (Exception ex)
            {
                TempData["result"] = -1;
                throw;
            }
        }
        #endregion
        
        public IActionResult GetBanner()
        {

            var banners = _banner.GetAll().Select(x=>new DisplayBanner { Id = x.Id, BannerContent = x.BannerContent, BannerLink = x.BannerLink, BannerName = x.BannerName, LangName = x.Lang.LangName }).OrderBy(x => x.LangId);
            return View(banners);
        }

   

        #region DeleteBanner

        public IActionResult deleteBanner(int id)
        {
            _banner.Delete(id);
            _banner.Save();
            return RedirectToAction("GetBanner","BannerManagament");
        }


        #endregion


        #region OtherActions

        public IActionResult countBanner()
        {
            //dil idsi query string olarak gönderilecek ve kontorl edilecek
            return View();
        }
        #endregion

        //[HttpPost]
        //public int PostUploadFilesAjax(int areaId)
        //{
        //    //var gelenId = Request.Query.FirstOrDefault(x => x.Key == "id");
        //    long size = 0;
        //    var detail = _areaDetail.GetById(areaId);
        //    var files = Request.Form.Files;
        //    string newFileName = "";
        //    int newImageId = 0;
        //    if (detail.ImageId == null)
        //    {
        //        if (files.Count != 0)
        //        {
        //            int sayac = 0;
        //            FileUpload file = new FileUpload(host);
        //            foreach (IFormFile source in files)
        //            {
        //                var tags = _areaDetail.GetById(areaId).Title.Replace(' ', ',');
        //                string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');
        //                var wanted = "area_" + areaId;
        //                filename = file.EnsureCorrectFilename(filename, wanted);
        //                newFileName = "\\uploads\\onePageArea\\" + filename;
        //                using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "\\uploads\\onePageArea\\")))
        //                    source.CopyTo(output);

        //                newImageId = _image.InsertAndgetId(new Data.Models.DB.Image { ImageAltTag = detail.Title, ImageTag = tags, ImageTitle = detail.Title, ImageUrl = newFileName, IsBelongGallery = false });
        //            }

        //        }
        //        detail.ImageId = newImageId;
        //        _areaDetail.Update(detail);
        //        _areaDetail.Save();
        //        TempData["result"] = 1;
        //    }


        //    return 1;
        //}
        public int deleteImageFromDetail(int id)
        {

            var detail = _banner.Get(x => x.Image == id);
            detail.Image = null;
            _banner.Update(detail);
            _banner.Save();
            _image.Delete(id);
            _image.Save();
            return 1;
        }


    }
}