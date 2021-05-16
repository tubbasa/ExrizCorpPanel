using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
using ExrizCorpPanel.UI.Areas.Admin.Helpers;
using ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExrizCorpPanel.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class SiteSettingManagamentController : Controller
    {
        ISiteDetailRepository _site;
        IImageRepository _image;
        private IHostingEnvironment host;
        public SiteSettingManagamentController(ISiteDetailRepository site, IImageRepository image, IHostingEnvironment _host)
        {
            _site = site;
            _image = image;
            host = _host;
        }
        #region InsertSettings
        public IActionResult AddSettings()
        {
            return View();
        }

        [HttpPost]
        public IActionResult postAddSettings([FromBody] SiteDetail site)
        {
            var siteDetail = _site.GetAll().FirstOrDefault();
            siteDetail.GsmNo = site.GsmNo;
            siteDetail.SiteAddress = site.SiteAddress;
            siteDetail.SiteDesc = site.SiteDesc;
            siteDetail.SiteName = site.SiteName;
            siteDetail.SiteTagLine = site.SiteTagLine;
            siteDetail.SiteTags = site.SiteTags;
            siteDetail.SiteTitle = site.SiteTitle;
            siteDetail.TelNo = site.TelNo;
            _site.Insert(site);
            _site.Save();
            return RedirectToAction("GetSettings");
        }


        #endregion

        #region UpdateSettings
        public IActionResult UpdateSettings()
        {
            var selected = _site.GetAll().FirstOrDefault();
            SiteDetailRequest model = new SiteDetailRequest();
            model.AnalyticsApikey = selected.AnalyticsApikey;
            model.FaviconImageId = selected.FaviconImageId;
            model.GsmNo = selected.GsmNo;
            model.Id = selected.Id;
            model.LogoImageId = selected.LogoImageId;
            model.SiteSmallLogoId = selected.SmallLogoImageId;
            model.MenuBranches = selected.MenuBranches;
            model.SiteAddress = selected.SiteAddress;
            if (selected.LogoImageId !=null)
            {
                model.SiteBigLogoStr = _image.GetById(selected.LogoImageId.Value).ImageUrl;
            }

            if (selected.SmallLogoImageId !=null)
            {
                model.SiteSmallLogoStr = _image.GetById(selected.SmallLogoImageId.Value).ImageUrl;
            }
      
            
            model.SiteTagLine = selected.SiteTagLine;
            model.SiteTags = selected.SiteTags;
            model.SiteTitle = selected.SiteTitle;
            model.SiteName = selected.SiteName;
            model.SiteDesc = selected.SiteDesc;
            model.TelNo = selected.TelNo;
            model.ThemeId = selected.ThemeId;
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateSettings([FromForm] SiteDetailRequest site)
        {
            var siteDetail = _site.GetAll().FirstOrDefault();
            if (siteDetail!=null)
            {
                siteDetail.GsmNo = site.GsmNo;
                siteDetail.TelNo = site.TelNo;
                siteDetail.SiteAddress = site.SiteAddress;
                siteDetail.SiteAddress = site.SiteAddress;
                siteDetail.SiteDesc = site.SiteDesc;
                siteDetail.SiteName = site.SiteName;
                siteDetail.SiteTagLine = site.SiteTagLine;
                siteDetail.SiteTags = site.SiteTags;
                siteDetail.SiteTitle = site.SiteTitle;
                siteDetail.SiteTagLine = site.SiteTagLine;
                siteDetail.AnalyticsApikey = site.AnalyticsApikey;
                siteDetail.TelNo = site.TelNo;

                if (siteDetail.LogoImageId != null && site.SiteBigLogo !=null)
                {
                    _image.Delete(siteDetail.LogoImageId.Value);
                    _image.Delete(siteDetail.SmallLogoImageId.Value);
                    siteDetail.SmallLogoImageId = null;
                    siteDetail.LogoImageId = null;
                    _site.Update(siteDetail);
                    _site.Save();
                    _image.Save();
                }

                FileUpload file = new FileUpload(host);

                string bigLogofilename = ContentDispositionHeaderValue.Parse(site.SiteBigLogo.ContentDisposition).FileName.ToString().Trim('"');
                var wanted = "siteBigLogo" + siteDetail.Id;
                bigLogofilename = file.EnsureCorrectFilename(bigLogofilename, wanted);
                var newFileName = "\\uploads\\siteDetails\\" + bigLogofilename;
                using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(bigLogofilename, "\\uploads\\siteDetails\\")))
                    site.SiteBigLogo.CopyToAsync(output);
                
                //small logo

                string smallLogofilename = ContentDispositionHeaderValue.Parse(site.SiteSmallLogo.ContentDisposition).FileName.ToString().Trim('"');
                var wantedStr = "siteSmallLogo" + siteDetail.Id;
                smallLogofilename = file.EnsureCorrectFilename(smallLogofilename, wantedStr);
                var newSmallFileName = "\\uploads\\siteDetails\\" + smallLogofilename;
                using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(smallLogofilename, "\\uploads\\siteDetails\\")))
                    site.SiteSmallLogo.CopyToAsync(output);



                int imageId = siteDetail.LogoImageId ?? 0;
                var image = _image.GetById(imageId);
                if (image==null)
                {
                    image = new Image();
                }
                image.ImageAltTag = siteDetail.SiteName;
                image.ImageTitle = siteDetail.SiteName;
                image.ImageUrl = newFileName;
                image.IsBelongGallery = false;
                _image.Update(image);
                _image.Save();
                siteDetail.LogoImageId = image.Id;

                //small logo 

                int smallImageId = siteDetail.SmallLogoImageId ?? 0;
                var smallimage = _image.GetById(smallImageId);
                if (smallimage == null)
                {
                    smallimage = new Image();
                }
                smallimage.ImageAltTag = siteDetail.SiteName;
                smallimage.ImageTitle = siteDetail.SiteName;
                smallimage.ImageUrl = newSmallFileName;
                smallimage.IsBelongGallery = false;
                _image.Update(smallimage);
                _image.Save();
                siteDetail.SmallLogoImageId = smallimage.Id;


                _site.Update(siteDetail);
                _site.Save();

            }
            else
            {
                var detail = new SiteDetail();
                detail.GsmNo = site.GsmNo;
                detail.SiteAddress = site.SiteAddress;
                detail.SiteDesc = site.SiteDesc;
                detail.SiteName = site.SiteName;
                detail.SiteTagLine = site.SiteTagLine;
                detail.SiteTags = site.SiteTags;
                detail.SiteTitle = site.SiteTitle;
                detail.TelNo = site.TelNo;
                detail.AnalyticsApikey = site.AnalyticsApikey;

                _site.Insert(detail);
                _site.Save();
            }

            return RedirectToAction("UpdateSettings");
        }
        #endregion

        #region GetSettings
        public IActionResult GetSettingsById(int id)
        {
            var selected = _site.GetById(id);
                
            return View(selected);
        }


        public IActionResult GetSettings()
        {
            var list = _site.GetAll();
            return View(list);
        }

        #endregion

        #region DeleteSettings

        public int deleteSettings(int id)
        {
            _site.Delete(id);
            _site.Save();
            return 1;
        }


        #endregion

        #region OtherActions

        public IActionResult countSettings()
        {
            //dil idsi query string olarak gönderilecek ve kontorl edilecek
            return View();
        }
        #endregion 
    }
}