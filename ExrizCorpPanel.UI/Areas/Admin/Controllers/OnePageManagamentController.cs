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
    public class OnePageManagamentController : Controller
    {
        private readonly IUserRepository _user;
        private IOnePageAreaRepository _onePageArea;
        private IAreaDetailRepository _areaDetail;
        private IAreaTypeRepository _areaType;
        private ILanguageRepository _lang;
        private IImageRepository _image;
        private IHostingEnvironment host;

        public OnePageManagamentController(IUserRepository user, IOnePageAreaRepository onePageArea,  IAreaDetailRepository areaDetail, ILanguageRepository lang, IImageRepository image, IHostingEnvironment _host, IAreaTypeRepository areaType)
        {
            this._user = user;
            this._onePageArea = onePageArea;
            this._areaDetail = areaDetail;
            this._lang = lang;
            this.host = _host;
            this._image = image;
            this._areaType = areaType;
        }


        #region InsertArea
        public IActionResult AddAreaType()
        {
            //var languages = _lang.GetAll().ToList();
            //TempData["lang"] = languages;
            return View();
        }

        //areaId
        [HttpPost]
        public IActionResult AddAreaType([FromForm] AreaType area)
        {
            var id = _areaType.InsertAndGetId(area);
            return RedirectToAction("GetAreaType");
        }


        #endregion

        #region UpdateArea
        public IActionResult UpdateAreaType(int id)
        {
            //var languages = _lang.GetAll().ToList();
            //TempData["lang"] = languages;
            //var details = GetCurrentAreaDetails(id);
            //TempData["details"] = details;
            var selected = _areaType.GetById(id);
            return View(selected);
        }

        [HttpPost]
        public IActionResult UpdateAreaType([FromForm] AreaType area)
        {
            _areaType.Update(area);
            _areaType.Save();
            return RedirectToAction("GetAreaType");
        }
        #endregion

        #region GetArea
        public IActionResult GetAreaTypeById(int id)
        {
            var selected = _areaType.GetById(id);
            return View(selected);
        }


        public IActionResult GetAreaType()
        {
            var list = _areaType.GetAll();
            return View(list);
        }

        #endregion

        #region DeleteAreaType

        public IActionResult deleteAreaType(int id)
        {
            var areas = _onePageArea.GetMany(x=>x.TypeId == id);
            foreach (var item in areas)
            {
                _onePageArea.Delete(item.Id);

                var details = _areaDetail.GetMany(x => x.AreaId == item.Id);
                foreach (var detail in details)
                {
                    _areaDetail.Delete(detail.Id);
                }
            }

            _areaType.Delete(id);
            _areaType.Save();          
            _areaDetail.Save();
            _onePageArea.Save();
            return RedirectToAction("GetAreaType");
        }


        #endregion


        #region InsertArea
        public IActionResult AddArea()
        {
            var languages = _lang.GetAll().ToList();
            TempData["lang"] = languages;
            var typeList = _areaType.GetAll();
            List<SelectListItem> selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text="Lütfen bir tip seçiniz.",Value ="0", Selected=true});
            foreach (var item in typeList)
            {
                selectItems.Add(new SelectListItem { Text = item.AreaSystemTypeName, Value= item.Id.ToString(),Selected=false});
            }
            TempData["types"] = selectItems;
            return View();
        }

        //areaId
        [HttpPost]
        public int postaddArea([FromBody] PageArea area)
        {
         var id= _onePageArea.InsertAndGetId(area);
            return id;
        }


        #endregion

        #region UpdateArea
        public IActionResult UpdateArea(int id)
        {
            var languages = _lang.GetAll().ToList();
            TempData["lang"] = languages;
            var details = GetCurrentAreaDetails(id);
            TempData["details"] = details;
            var selected = _onePageArea.GetById(id);
            var typeList = _areaType.GetAll();
            List<SelectListItem> selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = "Lütfen bir tip seçiniz.", Value = "0", Selected = true });
            foreach (var item in typeList)
            {
                if (selected.TypeId == item.Id)
                {
                    selectItems.Add(new SelectListItem { Text = item.AreaSystemTypeName, Value = item.Id.ToString(), Selected = true });
                }
                else
                {
                    selectItems.Add(new SelectListItem { Text = item.AreaSystemTypeName, Value = item.Id.ToString(), Selected = false });
                }
               
            }
            TempData["types"] = selectItems;
            return View(selected);
        }

        [HttpPost]
        public int postupdateArea([FromBody] PageArea area)
        {
            _onePageArea.Update(area);
            _onePageArea.Save();
            return area.Id;
        }
        #endregion

        #region GetArea
        public IActionResult GetAreaById(int id)
        {
            var selected = _onePageArea.GetById(id);
            return View(selected);
        }


        public IActionResult GetArea()
        {
            var list = _onePageArea.GetAll();
            return View(list);
        }

        #endregion

        #region DeleteArea

        public IActionResult deleteArea(int id)
        {
            var details = _areaDetail.GetMany(x=>x.AreaId==id);
            foreach (var item in details)
            {
                _areaDetail.Delete(item.Id);
            }
            _areaDetail.Save();
            _onePageArea.Delete(id);
            _onePageArea.Save();
            return RedirectToAction("GetArea");
        }


        #endregion

        #region InsertAreaDetail
        //public IActionResult AddAreaDetail()
        //{
        //    return View();
        //}

        [HttpPost]
        public int postaddAreaDetail([FromBody] AreaDetail detail)
        {
            
               var id= _areaDetail.InsertAndGetId(detail);
            return id;
        }


        #endregion

        #region UpdateAreaDetail
        public IActionResult UpdateAreaDetail()
        {
            return View();
        }

        [HttpPost]
        public int postupdateAreaDetail([FromBody] AreaDetail detail)
        {
            var selected = _areaDetail.GetById(detail.Id);
            selected.Title = detail.Title;
            selected.Content = detail.Content;
            _areaDetail.Update(selected);
            
            _areaDetail.Save();
            return detail.Id;
        }
        #endregion

        #region GetAreaDetail
        public IActionResult GetAreaDetailById(int id)
        {
            var selected = _areaDetail.GetById(id);
            return View(selected);
        }


        public IActionResult GetAreaDetail()
        {
            var list = _areaDetail.GetAll();
            return View(list);
        }

        #endregion

        #region DeleteAreaDetail

        public int deleteAreaDetail(int id)
        {
            _areaDetail.Delete(id);
            _areaDetail.Save();
            return 1;
        }


        #endregion

        #region OtherActions

        public int countArea()
        {
            return _onePageArea.count();
        }
        public IList<AreaDetailRequest> GetCurrentAreaDetails(int id)
        {
            var details = _areaDetail.GetMany(x => x.AreaId == id).Select(x => new AreaDetailRequest {Content=x.Content,Id=x.Id,ImageUrl=_image.GetById(Convert.ToInt32(x.ImageId))!=null? _image.GetById(Convert.ToInt32(x.ImageId)).ImageUrl:"",ImageId=x.ImageId,LangId=x.LangId,Title=x.Title}).ToList();
            return details;
        }


        public int countAreaDetail()
        {
            //dil idsi query string olarak gönderilecek ve kontorl edilecek
            return   _areaDetail.count();
        }

        [HttpPost]
        public int PostUploadFilesAjax(int areaId)
        {
            //var gelenId = Request.Query.FirstOrDefault(x => x.Key == "id");
            long size = 0;
            var detail = _areaDetail.GetById(areaId);
            var files = Request.Form.Files;
            string newFileName = "";
            int newImageId = 0;
            if (detail.ImageId == null)
            {
                if (files.Count != 0)
                {
                    int sayac = 0;
                    FileUpload file = new FileUpload(host);
                    foreach (IFormFile source in files)
                    {
                        var tags = _areaDetail.GetById(areaId).Title.Replace(' ', ',');
                        string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');
                        var wanted = "area_" + areaId;
                        filename = file.EnsureCorrectFilename(filename, wanted);
                        newFileName = "/uploads/onePageArea/" + filename;
                        using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "/uploads/onePageArea/")))
                            source.CopyTo(output);

                        newImageId = _image.InsertAndgetId(new Data.Models.DB.Image { ImageAltTag = detail.Title, ImageTag = tags, ImageTitle = detail.Title, ImageUrl = newFileName, IsBelongGallery = false });
                    }

                }
                detail.ImageId = newImageId;
                _areaDetail.Update(detail);
                _areaDetail.Save();
                TempData["result"] = 1;
            }


            return 1;
        }
        public int deleteImageFromDetail(int id)
        {

            var detail = _areaDetail.Get(x => x.ImageId == id);
            detail.ImageId = null;
            _areaDetail.Update(detail);
            _areaDetail.Save();
            _image.Delete(id);
            _image.Save();
            return 1;
        }


        #endregion
    }
}