using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
using ExrizCorpPanel.UI.Areas.Admin.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace ExrizCorpPanel.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class ReferenceManagamentController : Controller
    {
        ILanguageRepository _lang;
        IReferencesRepository _ref;
        IReferenceJobRepository _refjob;
        IImageRepository _image;
        IHostingEnvironment host;
        IJobLanguageRepository _joblang;

      
        public ReferenceManagamentController(ILanguageRepository lang, IReferencesRepository reference, IReferenceJobRepository job, IImageRepository image, IHostingEnvironment _host, IJobLanguageRepository joblang )
        {
            _lang = lang;
            _ref = reference;
            _refjob = job;
            _image = image;
            host = _host;
            _joblang = joblang;
        }

        #region InsertReference
        public IActionResult AddReference()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostAddReference([FromForm]ReferenceRequest reference)
        {
            //ReferenceRequest
            var newRef = new References();
            newRef.ReferenceName = reference.ReferenceName;
            newRef.Url = reference.Url;
            var id = _ref.InsertAndGetId(newRef);
            string newFileName = "";
            if (reference.ReferenceImage != null)
            {
                FileUpload file = new FileUpload(host);
                //foreach (IFormFile source in post.images)
                //{
                string filename = ContentDispositionHeaderValue.Parse(reference.ReferenceImage.ContentDisposition).FileName.ToString().Trim('"');
                var wanted = "reference_" + id;
                filename = file.EnsureCorrectFilename(filename, wanted);
                newFileName = "\\uploads\\reference\\" + filename;
                using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "\\uploads\\reference\\")))
                    reference.ReferenceImage.CopyTo(output);

                //}
                var ImageId = _image.InsertAndgetId(new Data.Models.DB.Image { ImageAltTag = reference.ReferenceName, ImageTag = "", ImageTitle = reference.ReferenceName, ImageUrl = newFileName, IsBelongGallery = false });
                var selected = _ref.GetById(id);
                selected.ReferenceImage = ImageId;
                _ref.Update(selected);
                _ref.Save();


            }
            TempData["result"] = 1;
            return RedirectToAction("GetReference", "ReferenceManagament");
          
        }


        #endregion

        #region UpdateReference
        public IActionResult UpdateReference(int id)
        {
            var selected = _ref.GetById(id);
            return View(selected);
        }

        [HttpPost]
        public ActionResult postupdateReference(ReferenceRequest reference)
        {
            try
            {
                var selectedRef = _ref.GetById(reference.Id);
                selectedRef.ReferenceName = reference.ReferenceName;
                selectedRef.Url = reference.Url;
               
                string newFileName = "";
                int ImageId = 0;
                if (reference.ReferenceImage != null)
                {
                    FileUpload file = new FileUpload(host);
                    //foreach (IFormFile source in post.images)
                    //{
                    string filename = ContentDispositionHeaderValue.Parse(reference.ReferenceImage.ContentDisposition).FileName.ToString().Trim('"');
                    var wanted = "reference_" + reference.Id;
                    filename = file.EnsureCorrectFilename(filename, wanted);
                    newFileName = "\\uploads\\reference\\" + filename;
                    using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "\\uploads\\reference\\")))
                        reference.ReferenceImage.CopyTo(output);

                    //}
                     ImageId = _image.InsertAndgetId(new Data.Models.DB.Image { ImageAltTag = reference.ReferenceName, ImageTag = "", ImageTitle = reference.ReferenceName, ImageUrl = newFileName, IsBelongGallery = false });
                    
                  


                }
                selectedRef.ReferenceImage = ImageId;
                _ref.Update(selectedRef);
                _ref.Save();
                TempData["result"] = 1;
                return RedirectToAction("GetReference", "ReferenceManagament");
            }
            catch (Exception)
            {
                TempData["result"] = 0;
                return RedirectToAction("GetReference", "ReferenceManagament");
            }
        }
        #endregion

        #region GetReference
        public IActionResult GetReferenceById(int id)
        {
            var selected = _ref.GetById(id);
            return View(selected);
        }


        public IActionResult GetReference()
        {
            var list = _ref.GetAll();
            return View(list);
        }

        #endregion

        #region DeleteReference

        public IActionResult deleteReference(int id)
        {
            try
            {
                var selectedImage = _ref.GetById(id).ReferenceImage;
                _ref.Delete(id);
                _ref.Save();
                if (selectedImage!=null)
                {
                    _image.Delete(Convert.ToInt32(selectedImage));
                    _image.Save();
                }
                TempData["result"] = 1;
                return RedirectToAction("GetReference");
            }
            catch (Exception)
            {
                TempData["result"] = 0;
                return RedirectToAction("GetReference");
                throw;
            }
        }


        #endregion

        #region InsertJob

        public int postAddJob([FromBody]ReferenceJobMapping job)
        {
           var id= _refjob.InsertAndGetId(job);
            return id;
        }
        #endregion

        #region InsertJobImage
        public IActionResult AddJobImage()
        {
            var references = _ref.GetAll();
            var referenceList  = new List<SelectListItem>();
            foreach (var item in references)
            {
                referenceList.Add(new SelectListItem {Text=item.ReferenceName,Value=item.Id.ToString(),Selected=false });
            }
            TempData["ref"] = referenceList;
            var language = _lang.GetAll().ToList();
            TempData["lang"] = language;
            return View();
        }

        [HttpPost]
        public int postupdateJob([FromBody] ReferenceJobMapping job)
        {
            var selected = _refjob.GetById(job.Id);
            selected.ReferenceId = job.ReferenceId;
            selected.JobSystemName = job.JobSystemName;
            _refjob.Update(selected);
            _refjob.Save();
            return job.Id;

        }
        [HttpPost]
        public int postaddJobDetail([FromBody]JobLanguageMapping job)
        {
            var id = _joblang.InsertAndGetId(job);
            return id;
        }


        #endregion

        #region UpdateJobImage
        public IActionResult UpdateJobImage(int id)
        {
            var selected = _refjob.GetById(id);
            var references = _ref.GetAll();
            var referenceList = new List<SelectListItem>();
            foreach (var item in references)
            {
                referenceList.Add(new SelectListItem { Text = item.ReferenceName, Value = item.Id.ToString(), Selected = false });
            }
            var jobs = _joblang.GetMany(x=>x.JobId==id).Select(x=> new JobLanguageRequest { Content=x.Content,Id=x.Id,JobId=x.JobId,JobName=x.JobName,images=_image.GetMany(y=>y.JobId==x.Id).ToList(),LangId=x.LangId}).ToList();
            TempData["details"] = jobs;

            TempData["ref"] = referenceList;
            var language = _lang.GetAll().ToList();
            TempData["lang"] = language;
            return View(selected);
        }

        [HttpPost]
        public int postupdateJobDetail([FromBody]JobLanguageMapping job)
        {
            try
            {
                var refJob = _joblang.GetById(job.Id);
                refJob.Content = job.Content;
                refJob.JobName = job.JobName;
                _joblang.Update(refJob);
                _joblang.Save();
                return job.Id;

            }
            catch (Exception)
            {
                return 2;
                throw;
            }
        }
        #endregion

        #region GetJobImage
        public IActionResult GetJobImageById(int id)
        {
            var selected = _refjob.GetById(id);
            return View(selected);
        }
        
        public IActionResult GetJobsByReference(int id)
        {
            var list = _refjob.GetMany(x=>x.ReferenceId== id);
            return View(list);
        }

        public IActionResult GetJobImage()
        {
            var referenceList = _ref.GetAll();
            var list = new List<JobByReferenceRequest>();
            foreach (var item in referenceList)
            {
                var count = _refjob.GetMany(x=>x.ReferenceId==item.Id).ToList().Count;
                list.Add(new JobByReferenceRequest { Count= count,ReferenceId=item.Id,ReferenceName=item.ReferenceName});
            }
            return View(list);
        }

        #endregion

        #region DeleteJobImage

        public IActionResult deleteJobDetail(int id)
        {
            _joblang.Delete(id);
            _joblang.Save();
            return View();
        }


        #endregion

        #region OtherActions

        public int countReference()
        {

            return _ref.count();
        }

        public int countJobImage()
        {

            return _refjob.count();
        }


        public int deleteImageFromReference(int id)
        {
            var selected = _ref.Get(x=>x.ReferenceImage==id);
            selected.ReferenceImage = null;
            _ref.Update(selected);
            _ref.Save();
            _image.Delete(id);
            _image.Save();
            return 1;
        }

        [HttpPost]
        public IActionResult PostUploadFilesAjax(int referenceId)
        {
            //var gelenId = Request.Query.FirstOrDefault(x => x.Key == "id");
            long size = 0;
            var detail = _joblang.GetById(referenceId);

            var files = Request.Form.Files;
            string newFileName = "";
            int newImageId = 0;

            if (files.Count != 0)
            {
                int sayac = 0;
                FileUpload file = new FileUpload(host);
                var lastId = _image.GetMany(x => x.JobId == referenceId).LastOrDefault();
                if (lastId != null)
                {
                    sayac = Convert.ToInt32(lastId.ImageUrl.Split('-')[1].Split('.')[0]) + 1;
                }

                foreach (IFormFile source in files)
                {
                    var tags = _joblang.GetById(referenceId).JobName.Replace(' ', ',');
                    string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');
                    var wanted = "job_" + referenceId + "-" + sayac; 
                    filename = file.EnsureCorrectFilename(filename, wanted);
                    newFileName = "\\uploads\\job\\" + filename;
                    using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "\\uploads\\job\\")))
                        source.CopyTo(output);
                    newImageId = _image.InsertAndgetId(new Data.Models.DB.Image { ImageAltTag = detail.JobName, ImageTag = tags, ImageTitle = detail.JobName, ImageUrl = newFileName, IsJob = true,JobId=referenceId });
                    sayac++;
                }

            }
            TempData["result"] = 1;


            var list = _image.GetMany(x => x.JobId == referenceId);
            return Json(list);
        }

        public int deleteImageFromJob(int id)
        {
            _image.Delete(id);
            _image.Save();
            return 1;
        }

        public IList<JobLanguageRequest> GetCurrentJobDetails(int id)
        {
            var details = _joblang.GetMany(x => x.Job.ReferenceId == id).Select(x => new JobLanguageRequest { Content = x.Content, Id = x.Id, JobName=x.JobName,images=_image.GetMany(y=>y.JobId==x.Id).ToList() }).ToList();
            return details;
        }

        #endregion 
    }
}