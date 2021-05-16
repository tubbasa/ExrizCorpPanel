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

namespace ExrizCorpPanel.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class LanguageManagamentController : Controller
    {
        ILanguageRepository _lang;
        IHostingEnvironment host;
        IImageRepository _image;
        IStringResourceRepository _resource;
        IResourceVariableRepository _resourceVar;
        public LanguageManagamentController(ILanguageRepository lang, IHostingEnvironment _host, IImageRepository image, IStringResourceRepository resource, IResourceVariableRepository resourceVar)
        {
            _resource = resource;
            _resourceVar = resourceVar;
            _image = image;
            host = _host;
            _lang = lang;
        }
        #region InsertLanguage
        public IActionResult AddLanguage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddLanguage(LanguageRequest lang)
        {
            Language language = new Language();
            language.LangName = lang.LangName;
            language.LangSymbol = lang.LangSymbol;
            var id= _lang.InsertAndGetId(language);
            string newFileName = "";
            if (lang.icon.Count != 0)
            {

                FileUpload file = new FileUpload(host);
                foreach (IFormFile source in lang.icon)
                {
                    string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');
                    var wanted = "languageIcon_" + id;
                    filename = file.EnsureCorrectFilename(filename, wanted);
                    newFileName = "\\uploads\\languages\\" + filename;
                    using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "\\uploads\\languages\\")))
                        source.CopyToAsync(output);

                }
                var ImageId = _image.InsertAndgetId(new Data.Models.DB.Image { ImageAltTag = language.LangName, ImageTag = "", ImageTitle = language.LangName, ImageUrl = newFileName, IsBelongGallery = false });
                var selected = _lang.GetById(id);
                selected.LangFlagIcon = ImageId;
                _lang.Update(selected);
                _lang.Save();


            }
            TempData["result"] = 1;
            return RedirectToAction("AddLanguage", "LanguageManagament");
        }




        #endregion

        #region UpdateLanguage
        public IActionResult UpdateLanguage(int id)
        {
            var selected = _lang.GetById(id);
            LanguageRequest re = new LanguageRequest();
            re.LangFlagIcon = _image.GetById(Convert.ToInt32(selected.LangFlagIcon)).ImageUrl;
            re.LangName = selected.LangName;
            re.LangSymbol = selected.LangSymbol;
            re.Id = selected.Id;

            return View(re);
        }

        [HttpPost]
        public IActionResult UpdateLanguage([FromForm]LanguageRequest lang)
        {
            Language selected = _lang.GetById(lang.Id);
            selected.LangName = lang.LangName;
            selected.LangSymbol = lang.LangSymbol;
            var id = selected.Id;
            string newFileName = "";
            if (lang.icon.Count != 0)
            {

                FileUpload file = new FileUpload(host);
                foreach (IFormFile source in lang.icon)
                {
                    string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');
                    var wanted = "languageIcon_" + id;
                    filename = file.EnsureCorrectFilename(filename, wanted);
                    newFileName = "\\uploads\\languages\\" + filename;
                    using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "\\uploads\\languages\\")))
                    {
                        source.CopyTo(output);
                        output.Flush();
                        output.Close();
                    }
                  


                }
                var selectedImage = _image.GetById(Convert.ToInt32(selected.LangFlagIcon));
                selectedImage.ImageUrl = newFileName;
                selectedImage.ImageAltTag = lang.LangName;
                selectedImage.ImageTitle = lang.LangName;
                _image.Update(selectedImage);
                _image.Save();
                _lang.Update(selected);
                _lang.Save();


            }
            TempData["result"] = 1;
            return RedirectToAction("UpdateLanguage", "LanguageManagament",new { id=selected.Id});
        }
        #endregion

        #region GetLanguage
        public IActionResult GetLanguageById(int id)
        {
            var selected = _lang.GetById(id);
            return View(selected);
        }


        public IActionResult GetLanguage()
        {
            var languageList = _lang.GetAll().Select(x=>new LanguageRequest {LangName=x.LangName,LangSymbol=x.LangSymbol,Id=x.Id,LangFlagIcon=_image.GetById(Convert.ToInt32(x.LangFlagIcon)).ImageUrl });
            return View(languageList);
        }

        #endregion

        #region DeleteLanguage

        public ActionResult deleteLanguage(int id)
        {
            try
            {
                _lang.Delete(id);
                _lang.Save();
                TempData["statu"] = 1;
                return RedirectToAction("GetLanguage");
                
            }
            catch (Exception ex)
            {
                TempData["statu"] = 0;
                return RedirectToAction("GetLanguage");
            }
        }


        #endregion

        #region InsertDataVariable
        public IActionResult InsertDataVariable()
        {
            var languages = _lang.GetAll().ToList();
            TempData["lang"] = languages;
            return View();
        }

        [HttpPost]
        public int postAddDataVariable([FromBody]StringResource resource)
        {
            var id = _resource.InsertAndGetId(resource);
            return id;
        }

        #endregion

        #region UpdateDataVariable

        public IActionResult UpdateDataVariable(int id)
        {
            var languages = _lang.GetAll().ToList();
            TempData["lang"] = languages;
            var selected = _resource.GetById(id);
            StringResourceRequest model = new StringResourceRequest();
            model.languages = _lang.GetAll().ToList();
            model.Id = selected.Id;
            model.ResourceName = selected.ResourceName;
            model.resources = _resourceVar.GetMany(x=> x.ResourceId == id).ToList();
            return View(model);
        }

        [HttpPost]
        public int postUpdateDataVariable([FromBody]StringResource resource)
        {
            _resource.Update(resource);
            _resource.Save();
            return resource.Id;
        }

        #endregion

        #region GetDataVariable
        public IActionResult GetDataVariable()
        {
            GetStringResources model = new GetStringResources();
            var resourceList = _resource.GetAll().ToList();
            return View(resourceList);
        }
        #endregion

        #region DeleteDataVariable
        public IActionResult DeleteDataVariable(int id)
        {
            var resourceDetails = _resourceVar.GetMany(x=>x.ResourceId == id);
            foreach (var item in resourceDetails)
            {
                _resourceVar.Delete(item.Id);
            }
            _resourceVar.Save();
            _resource.Delete(id);
            _resource.Save();
            return RedirectToAction("GetDataVariable");
        }
        #endregion

        #region InsertDataVariableDetail

        [HttpPost]
        public int postAddDataVariableDetail([FromBody]ResourceVariable resource)
        {
            var id = _resourceVar.InsertAndGetId(resource);
            return id;
        }

        [HttpPost]
        public int postUpdateDataVariableDetail([FromBody]ResourceVariable resource)
        {
            try
            {
                _resourceVar.Update(resource);
                _resourceVar.Save();
                return resource.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #endregion

        #region OtherActions

        public IActionResult countLanguage()
        {
            //dil idsi query string olarak gönderilecek ve kontorl edilecek
            return View();
        }
        #endregion 
    }
}