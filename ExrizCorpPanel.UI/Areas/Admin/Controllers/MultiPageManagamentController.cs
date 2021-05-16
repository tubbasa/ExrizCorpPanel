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
using Syncfusion.EJ2.DocumentEditor;

namespace ExrizCorpPanel.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class MultiPageManagamentController : Controller
    {

        IPageRepository _page;
        IPageDetailRepository _pageDetail;
        IPageTypeRepository _pageType;
        ILanguageRepository _lang;
        IImageRepository _image;
        ISlugRepository _slug;
        IHostingEnvironment host;
        public MultiPageManagamentController(IPageRepository page, IImageRepository image, IPageDetailRepository pageDetail, ILanguageRepository language, IHostingEnvironment _host, IPageTypeRepository pageType, ISlugRepository slug)
        {
            _page = page;
            _pageDetail = pageDetail;
            _lang = language;
            _image = image;
            host = _host;
            _pageType = pageType;
            _slug = slug;

        }
        #region InsertPage
        public IActionResult AddPage()
        {
            var languages = _lang.GetAll();
            var languagSelectList = new List<SelectListItem>();
            foreach (var item in languages)
            {
                languagSelectList.Add(new SelectListItem { Text = item.LangName, Value = item.Id.ToString(), Selected = false });
            }
            
            TempData["lang"] = languagSelectList;
            var pageType = _pageType.GetAll().Select(x => new SelectListItem { Text = x.PageName, Value = x.Id.ToString(), Selected = false }).ToList();
            pageType.Add(new SelectListItem { Text="Lütfen bir tip seçin.",Value="0",Selected=true});
            
            TempData["types"] = pageType;
            return View();
        }

        [HttpPost]
        public IActionResult postaddPage([FromBody] PageRequest page)
        {
            var newpage = new Page();
            newpage.PageName = page.PageName;
            newpage.TypeId = page.SelectedType;
            newpage.IsIndexable = Convert.ToBoolean(page.IsIndexableStr);
            var id = _page.InsertAndGetId(newpage);
            _slug.Insert(new GlobalUrls { ControllerName = "MultiPage", EntityId = id, EntityName = "Pages", EntityTitle = page.PageName, SlugName = Slugify.GenerateSlug(page.PageName), IsExternalLink = false });
       
            var mainType = _slug.Get(x => x.ControllerName == "MultiPage" && x.EntityName == "Pages");
            if (mainType == null)
            {
                _slug.Insert(new GlobalUrls { ControllerName = "MultiPage", EntityId = 0, EntityName = "Pages", EntityTitle = "Pages", SlugName = null, IsExternalLink = false });
            
            }
            _slug.Save();
            return Json(id);
        }


        #endregion

        #region UpdatePage
        public IActionResult UpdatePage(int id)
        {
            var selected = _page.GetById(id);
            PageRequest pageReq = new PageRequest();
            pageReq.Id = selected.Id;
            pageReq.IsIndexable = selected.IsIndexable.Value;
            pageReq.PageName = selected.PageName;
            var detailList = _pageDetail.GetMany(x=>x.PageId==id).ToList();
            
            var languages = _lang.GetAll();
            var LanguageList = new List<SelectListItem>();
            foreach (var item in languages)
            {
                LanguageList.Add(new SelectListItem { Text=item.LangName,Value=item.Id.ToString(),Selected=false});
            }
            TempData["lang"] = LanguageList;
            var pageType = _pageType.GetAll().Select(x => new SelectListItem { Text = x.PageName, Value = x.Id.ToString(), Selected = false }).ToList();
            foreach (var x in pageType)
            {
                if (x.Value == selected.TypeId.ToString())
                {
                    x.Selected = true;
                }
            }
            if (pageType.FirstOrDefault(x=>x.Selected==true)==null)
            {
                pageType.Add(new SelectListItem { Text = "Lütfen bir tip seçin.", Value = "0", Selected = true });
            }
            List<PageDetailRequest> listOfDetail = new List<PageDetailRequest>();
            foreach (var item in detailList)
            {
                PageDetailRequest req = new PageDetailRequest();
                req.Content = item.Content;
                req.Id = item.Id;
                req.ImageId = item.ImageId;
                if (req.ImageId  == null)
                {
                    req.ImageUrl = "no-image.png";
                }
                else
                {
                    req.ImageUrl = _image.GetById(item.ImageId.Value).ImageUrl;
                }
                req.LangId = item.LangId;
                req.PageId = item.PageId;
                req.SeoKeywords = item.SeoKeywords;
                req.SubTitle = item.SubTitle;
                req.Title = item.Title;
                listOfDetail.Add(req);
            }

            TempData["DetailPages"] = listOfDetail;
            //pageType.ForEach(x =>
            //{
            //    if (x.Value == selected.Id.ToString())
            //    {
            //        x.Selected = true;
            //    }
            //});

            TempData["types"] = pageType;

            return View(pageReq);
        }

        [HttpPost]
        public int postupdatePage([FromBody] Page page)
        {
            Page newPage = new Page();
            newPage.IsIndexable = page.IsIndexable;
            newPage.PageName = page.PageName;
            newPage.Id = page.Id;
            newPage.TypeId = page.TypeId;
            _page.Update(newPage);
            _page.Save();

            return newPage.Id;
        }
        #endregion

        #region GetPage
        public IActionResult GetPageById(int id)
        {
            var selected = _page.GetById(id);
            return View(selected);
        }


        public IActionResult GetPage()
        {
            var list = _page.GetAll();
            return View(list);
        }

        #endregion

        #region DeletePage

        public IActionResult deletePage(int id)
        {
            var details = _pageDetail.GetMany(x=>x.PageId == id);
            foreach (var item in details)
            {
                _pageDetail.Delete(item.Id);
            }
            _pageDetail.Save();
            _page.Delete(id);
            _page.Save();
            return RedirectToAction("GetPage");
        }


        #endregion

        #region InsertPageType
        public IActionResult AddPageType()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPageType([FromForm] PageTypes page)
        {
            page.PageTypeSystemName = Slugify.GenerateSlug(page.PageName);
            var id = _pageType.InsertAndGetId(page);
            _slug.Insert(new GlobalUrls { ControllerName="MultiPage",EntityId= page.Id,EntityName= "Categories", EntityTitle = page.PageName,SlugName= Slugify.GenerateSlug(page.PageName),IsExternalLink=false});
            _slug.Save();
            var mainType = _slug.Get(x=>x.ControllerName=="MultiPage"&& x.EntityName =="Categories");
            if (mainType==null)
            {
                _slug.Insert(new GlobalUrls { ControllerName="MultiPage",EntityId =0,EntityName="Categories",EntityTitle="Categories",SlugName=null,IsExternalLink=false});
                _slug.Save();
            }
            return RedirectToAction("GetPageType");
        }


        #endregion

        #region UpdatePageType
        public IActionResult UpdatePageType(int id)
        {
            var pageType = _pageType.GetById(id);
            return View(pageType);
        }

        [HttpPost]
        public IActionResult UpdatePageType([FromForm] PageTypes page)
        {
            PageTypes newPage = new PageTypes();
            newPage.PageName = page.PageName;
            newPage.PageTypeSystemName = Slugify.GenerateSlug(page.PageName);
            newPage.Id = page.Id;
            _pageType.Update(newPage);
            _pageType.Save();
            var selectedSlug = _slug.Get(x=>x.EntityId== newPage.Id && x.ControllerName == "MultiPage"&& x.EntityName == "Categories");
            if (selectedSlug!=null)
            {
                selectedSlug.EntityTitle = page.PageName;
                selectedSlug.SlugName = Slugify.GenerateSlug(page.PageName);
                _slug.Update(selectedSlug);
            }
            else
            {
                _slug.Insert(new GlobalUrls { ControllerName = "MultiPage", EntityId = page.Id, EntityName = "Categories", EntityTitle = page.PageName, SlugName = Slugify.GenerateSlug(page.PageName), IsExternalLink = false });
            }
            
            _slug.Save();

            return RedirectToAction("GetPageType");
        }
        #endregion

        #region GetPageType
        public IActionResult GetPageTypeById(int id)
        {
            var selected = _pageType.GetById(id);
            return View(selected);
        }


        public IActionResult GetPageType()
        {
            var list = _pageType.GetAll();
            return View(list);
        }

        #endregion

        #region DeletePageType

        public IActionResult deletePageType(int id)
        {
            var pages = _page.GetMany(x => x.TypeId == id);
            foreach (var item in pages)
            {
                var details = _pageDetail.GetMany(x=>x.PageId == item.Id);
                foreach (var detail in details)
                {
                    _pageDetail.Delete(item.Id);
                    
                }
                _pageDetail.Save();
                _page.Delete(item.Id);
            }
            _page.Save();
            _pageType.Delete(id);
            _pageType.Save();
            return RedirectToAction("GetPageType");
        }


        #endregion

        #region InsertPageDetail
        public IActionResult AddPageDetail()
        {
            return View();
        }

        [HttpPost]
        public int postaddPageDetail([FromBody] PageDetail pageDetail)
        {
            try
            {

                var id = _pageDetail.InsertAndGetId(pageDetail);
                return id;
            }
            catch (Exception ex)
            {
                return 0;
                throw;
            }
        }


        #endregion

        #region UpdatePageDetail
        public IActionResult UpdatePageDetail(int id)
        {
            return View();
        }

        [HttpPost]
        public int postupdatePageDetail([FromBody] PageDetail details)
        {
            try
            {
               
                    var selected = _pageDetail.GetById(details.Id);
                    selected.Content = details.Content;
                    selected.SeoKeywords = details.SeoKeywords;
                    selected.SubTitle = details.SubTitle;
                    selected.Title = details.Title;
                    _pageDetail.Update(selected);
              
                _pageDetail.Save();
                return selected.Id;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }
        #endregion

        #region GetPageDetail
        public IActionResult GetPageDetail(int id)
        {
            var selected = _pageDetail.GetById(id);
            return View(selected);
        }


        public IActionResult GetPageDetail()
        {
            var list = _pageDetail.GetAll();
            return View(list);
        }

        #endregion

        #region DeletePageDetail

        public IActionResult deletePageDetail(int id)
        {
            _pageDetail.Delete(id);
            _pageDetail.Save();
            //var selected = _pageDetail.GetById(id);
            //selected.Content = null;
            //selected.SeoKeywords = null;
            //selected.SlugName = null;
            //selected.SubTitle = null;
            //selected.Title = null;
            //_pageDetail.Update(selected);

            return View();
        }


        #endregion

        #region OtherActions

        public int countPage()
        {
            var count = _page.count();
            //dil idsi query string olarak gönderilecek ve kontorl edilecek
            return count;
        }

        public int countDetails()
        {
            var count = _pageDetail.count();
            return count;
        }

        public IList<PageDetailRequest> GetCurrentDetailPages(int id)
        {
            var pages = _pageDetail.GetMany(x => x.PageId == id).Select(x=>new PageDetailRequest { Content=x.Content,Id=x.Id,ImageUrl=x.Image.ImageUrl,ImageId=x.ImageId,LangId=x.LangId,PageId=x.PageId,SeoKeywords=x.SeoKeywords,SubTitle=x.SubTitle,Title=x.Title}).ToList();
            return pages;
        }

        [HttpPost]
        public int PostUploadFilesAjax( int detailId)
        {
            //var gelenId = Request.Query.FirstOrDefault(x => x.Key == "id");
            long size = 0;
            var detail = _pageDetail.GetById(detailId);
            var files = Request.Form.Files;
            string newFileName = "";
            int newImageId = 0;
            if (detail.ImageId==null)
            {
                if (files.Count != 0)
                {
                    int sayac = 0;
                    FileUpload file = new FileUpload(host);
                    foreach (IFormFile source in files)
                    {
                        var tags = _pageDetail.GetById(detailId).Title.Replace(' ', ',');
                        string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');
                        var wanted = "pageDetail_" + detailId;
                        filename = file.EnsureCorrectFilename(filename, wanted);
                        newFileName = "/uploads/pageDetail/" + filename;
                        using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "/uploads/pageDetail/")))
                            source.CopyTo(output);

                        newImageId = _image.InsertAndgetId(new Data.Models.DB.Image { ImageAltTag = detail.Title, ImageTag = tags, ImageTitle = detail.Title, ImageUrl = newFileName, IsBelongGallery = false });
                    }

                }
                detail.ImageId = newImageId;
                _pageDetail.Update(detail);
                _pageDetail.Save();
                TempData["result"] = 1;
            }
         

            return 1;
        }

     
        public int deleteImageFromDetail(int id)
        {
      
            var detail=_pageDetail.Get(x=>x.ImageId==id);
            detail.ImageId = null;
            _pageDetail.Update(detail);
            _pageDetail.Save();
            _image.Delete(id);
            _image.Save();
            return 1;
        }
        #endregion 
    }
}