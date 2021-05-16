using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ExrizCorpPanel.UI.Areas.Admin.Helpers;
using ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.DisplayTypes;
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
    public class BlogManagamentController : Controller
    {
        private ICategoryRepository _category;
        private IBlogRepository _blog;
        private IBlogPostRepository _blogPost;
        private ILanguageRepository _language;
        private ICategoryLanguageMappingRepository _categoryLang;
        private IImageRepository _image;
        private IHostingEnvironment host;
        private ISlugRepository _slug;

        public BlogManagamentController(ICategoryRepository category, IBlogRepository blog, IBlogPostRepository blogPost, ILanguageRepository language, ICategoryLanguageMappingRepository categoryLang, IImageRepository image, IHostingEnvironment _host, ISlugRepository slug)
        {
            _category = category;
            _blog = blog;
            _blogPost = blogPost;
            _language = language;
            _categoryLang = categoryLang;
            _image = image;
            host = _host;
            _slug = slug;
        }

        [HttpGet]
        #region InsertBlogCategory
        public IActionResult AddBlogCategory()
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
            var categories = _category.GetAll();
            var categoryList = new List<SelectListItem>();
            categoryList.Add(new SelectListItem { Text = "Eklediğiniz kategori bir alt kategori ise bir  kategori seçin", Value = "0", Selected = true });
            foreach (var category in categories)
            {
                categoryList.Add(new SelectListItem { Text = category.FriendlyName, Value = category.Id.ToString(), Selected = false });
            }
            ViewBag.Categories= categoryList;
            return View();
        }

        [HttpPost]
        public int addBlogCategory([FromBody]CategoryRequest category)
        {
            int id = 0;
            if (category.RelatedCategoryId=="0")
            {
                CategoryRequest req = new CategoryRequest();
                req.FriendlyName = category.FriendlyName;
                id = _category.InsertAndgetId(new Data.Models.DB.Category { FriendlyName = category.FriendlyName });
            }
            else
            {
                id = _category.InsertAndgetId(new Data.Models.DB.Category { FriendlyName = category.FriendlyName,RelatedCategoryId=Convert.ToInt32(category.RelatedCategoryId) });
            }
            var mainSlug = _slug.Get(x => x.ControllerName == "Blog" && x.EntityName == "Categories" && x.EntityTitle == "Categories");
            if (mainSlug == null)
            {
                _slug.Insert(new GlobalUrls { ControllerName = "Blog", EntityName = "Categories", EntityTitle = "Categories", IsExternalLink = false });
            }
            _slug.Insert(new GlobalUrls { EntityId = id,EntityName = "Categories", ControllerName="Blog",EntityTitle= category.FriendlyName,SlugName = Slugify.GenerateSlug(category.FriendlyName),IsExternalLink=false});
            _slug.Save();
            return id;
        }
        [HttpPost]
        public int addBlogCategoryLanguageMapping([FromBody]List<Models.CustomModels.RequestTypes.CategoryLanguageMapping> data)
        {
            foreach (var item in data)
            {
                _categoryLang.Insert(new Data.Models.DB.CategoryLanguageMapping { CategoryDesc = item.CategoryDesc ?? "", CategoryName = item.CategoryName ?? "", CategoryId = item.CategoryId, LangId = item.LangId, Tags = item.Tags ?? "" });

            }
            _categoryLang.Save();

            return 0;
        }


        #endregion

        #region UpdateBlogCategory
        public IActionResult UpdateBlogCategory(int id)
        {
            var language = _language.GetAll();
            var liste = new List<SelectListItem>();
            liste.Add(new SelectListItem { Text = "Lütfen dil seçin", Value = "-1", Selected = true });
            foreach (var item in language)
            {
                liste.Add(new SelectListItem { Text = item.LangName, Value = item.Id.ToString(), Selected = false });
            }
            var selected = _category.GetById(id);
            //var model = new DisplayCategory {Id=selected.Id,FriendlyName=selected.FriendlyName,LanguageVariables=new  }
            //liste.Items.a
            TempData["lang"] = liste;
          

            return View(selected);
        }

        [HttpPost]
        public int updateBlogCategory([FromBody]CategoryRequest category)
        {
            var selected = _category.GetById(category.Id);
            selected.FriendlyName = category.FriendlyName;
            _category.Update(selected);
            _category.Save();
            return category.Id;
        }
        [HttpPost]
        public int updateBlogCategoryLanguageMapping([FromBody]List<Models.CustomModels.RequestTypes.CategoryLanguageMapping> data)
        {
            foreach (var item in data)
            {
                var selected = _categoryLang.GetById(item.Id);
                selected.CategoryDesc = item.CategoryDesc ?? "";
                selected.CategoryName = item.CategoryName ?? "";
                selected.Tags = item.Tags ?? "";
                _categoryLang.Update(selected);

            }
            _categoryLang.Save();

            return 0;
        }

        #endregion

        #region GetBlogCategory
        public IActionResult GetBlogCategory()
        {
            var list = _category.GetAll();
            //dil idsi query string olarak gönderilecek ve kontorl edilecek
            return View(list);
        }

        #endregion

        #region DeleteBlogCategory

        public IActionResult deleteBlogCategory(int id)
        {
            var blogs = _blog.GetMany(x => x.CategoryId == id);
            if (blogs != null)
            {
                foreach (var item in blogs)
                {
                    item.CategoryId = null;
                }
                _blog.Save();
            }
            _category.Delete(id);
            _category.Save();

            return RedirectToAction("GetBlogCategory", "BlogManagament");
        }


        #endregion

        #region InsertBlog
        public IActionResult AddBlog()
        {
            BlogRequest model = new BlogRequest();
            var list = _category.GetAll();
            model.CategoryList = new List<SelectListItem>();
            model.CategoryList.Add(new SelectListItem { Text = "Lütfen Kategori seçin", Value = "-1", Selected = true });
            foreach (var item in list)
            {
                model.CategoryList.Add(new SelectListItem { Text = item.FriendlyName, Selected = false, Value = item.Id.ToString() });
            }

            var categories = _category.GetAll();
            var categoryList = new List<SelectListItem>();
            categoryList.Add(new SelectListItem { Text = "Eklediğiniz kategori bir alt kategori ise bir  kategori seçin", Value = "0", Selected = true });
            foreach (var category in categories)
            {
                categoryList.Add(new SelectListItem { Text = category.FriendlyName, Value = category.Id.ToString(), Selected = false });
            }
            
            ViewBag.Categories = categoryList;
            return View(model);
        }

        [HttpPost]
        public IActionResult postBlog(BlogRequest blog)
        {
            try
            {
                Blog newBlog = new Blog();
                newBlog.CategoryId = blog.CategoryId;
                newBlog.Name = blog.Name;
                _blog.Insert(newBlog);
                _blog.Save();
                TempData["result"] = 1;
                var mainSlug = _slug.Get(x =>  x.ControllerName == "Blog" && x.EntityName =="Posts"&&x.EntityTitle == "Posts");
                if (mainSlug ==null)
                {
                    _slug.Insert(new GlobalUrls {ControllerName = "Blog",EntityName = "Posts", EntityTitle= "Posts" ,IsExternalLink = false });
                }
                _slug.Insert(new GlobalUrls { ControllerName ="Blog", EntityName = "Posts", EntityId = newBlog.Id,EntityTitle =newBlog.Name,IsExternalLink= false,SlugName = Slugify.GenerateSlug(newBlog.Name)});
                _slug.Save();
                return RedirectToAction("AddBlog", "BlogManagament");
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        #endregion

        #region UpdateBlog
        public IActionResult UpdateBlog(int id)
        {
            var selected = _blog.GetById(id);
            BlogRequest model = new BlogRequest();
            var list = _category.GetAll();
            model.CategoryList = new List<SelectListItem>();
            model.CategoryList.Add(new SelectListItem { Text = "Lütfen Kategori seçin", Value = "-1", Selected = true });
            foreach (var item in list)
            {
                if (item.Id == selected.CategoryId)
                {
                    model.CategoryList.Add(new SelectListItem { Text = item.FriendlyName, Selected = true, Value = item.Id.ToString() });
                }
                else
                {
                    model.CategoryList.Add(new SelectListItem { Text = item.FriendlyName, Selected = false, Value = item.Id.ToString() });
                }

            }
            model.Id = selected.Id;
            model.Name = selected.Name;
            model.CategoryId = selected.CategoryId;
            var categories = _category.GetAll();
            var categoryList = new List<SelectListItem>();
            categoryList.Add(new SelectListItem { Text = "Blog için bir kategori seçiniz.", Value = "0", Selected = true });
            foreach (var category in categories)
            {
                categoryList.Add(new SelectListItem { Text = category.FriendlyName, Value = category.Id.ToString(), Selected = false });
            }
            ViewBag.Categories = categoryList;

            return View(model);
        }

        [HttpPost]
        public IActionResult PostUpdateBlog(BlogRequest blog)
        {
            var edited = _blog.GetById(blog.Id);
            edited.Name = blog.Name;
            edited.CategoryId = blog.CategoryId;
            _blog.Update(edited);
            _blog.Save();
            return RedirectToAction("GetBlog", "BlogManagament");
        }
        #endregion

        #region GetBlog

        public IActionResult GetBlog()
        {
            var model = _blog.GetAll();
            return View(model);
        }

        #endregion

        #region DeleteBlog

        public IActionResult deleteBlog(int id)
        {
            _blog.Delete(id);
            _blog.Save();
            return RedirectToAction("GetBlog", "BlogManagament");
        }


        #endregion

        #region InsertBlogPost

        [HttpGet]
        public IActionResult AddBlogPost()
        {
            var liste = _blog.GetAll();
            var model = new BlogPostRequest();
            model.blogs = new List<SelectListItem>();
            model.blogs.Add(new SelectListItem { Text = "Lütfen Üst Blog seçin", Value = "-1", Selected = true });
            foreach (var item in liste)
            {
                model.blogs.Add(new SelectListItem { Text = item.Name, Selected = false, Value = item.Id.ToString() });
            }

            var language = _language.GetAll();
            var langList = new List<SelectListItem>();
            langList.Add(new SelectListItem { Text = "Lütfen dil seçin", Value = "-1", Selected = true });
            foreach (var item in language)
            {
                langList.Add(new SelectListItem { Text = item.LangName, Value = item.Id.ToString(), Selected = false });
            }
            //liste.Items.a
            TempData["lang"] = langList;
            return View(model);
        }

        [HttpPost]
        public IActionResult postBlogPost(BlogPostRequest post)
        {
            try
            {
                var newPost = new BlogPost();
                newPost.LangId = post.LangId;
                newPost.BlogId = post.BlogId;
                newPost.Content = post.Content;
                newPost.SubTitle = post.SubTitle;
                newPost.Title = post.Title;
                newPost.Date = DateTime.Now.Date;
                var id = _blogPost.InsertAndgetId(newPost);
                var slug = Slugify.GenerateSlug(newPost.Title);
                _slug.Insert(new GlobalUrls { EntityId=id,EntityName= "Posts", EntityTitle=newPost.Title,ControllerName="Blog",SlugName=slug});
                _slug.Save();
                string newFileName = "";
                if (post.images.Count != 0)
                {
                    FileUpload file = new FileUpload(host);
                    foreach (IFormFile source in post.images)
                    {
                        string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');
                        var wanted = "blogPost_" + id;
                        filename = file.EnsureCorrectFilename(filename, wanted);
                        newFileName = "/uploads/blogPost/" + filename;
                        using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "/uploads/blogPost/")))
                            source.CopyToAsync(output);

                    }
                    var ImageId = _image.InsertAndgetId(new Data.Models.DB.Image { ImageAltTag = post.Title, ImageTag = "", ImageTitle = post.Title, ImageUrl = newFileName, IsBelongGallery = false });
                    var selected = _blogPost.GetById(id);
                    selected.ImageId = ImageId;
                    _blogPost.Update(selected);
                    _blogPost.Save();


                }
                TempData["result"] = 1;
                return RedirectToAction("AddBlogPost", "BlogManagament");
            }
            catch (Exception)
            {
                TempData["result"] = -1;
                throw;
            }

        }


        #endregion

        #region UpdateBlogPost
        public IActionResult UpdateBlogPost(int id)
        {
            var list = _blog.GetAll();
            var selected = _blogPost.GetById(id);
            var model = new BlogPostRequest();
            model.blogs = new List<SelectListItem>();
        
            model.blogs.Add(new SelectListItem { Text = "Lütfen Üst Blog seçin", Value = "-1", Selected = true });
            foreach (var item in list)
            {
                if (item.Id == selected.LangId)
                {
                    model.blogs.Add(new SelectListItem { Text = item.Name, Selected = true, Value = item.Id.ToString() });
                }
                else
                {
                    model.blogs.Add(new SelectListItem { Text = item.Name, Selected = false, Value = item.Id.ToString() });
                }
                
            }
            var language = _language.GetAll();
            var liste = new List<SelectListItem>();
            liste.Add(new SelectListItem { Text = "Lütfen dil seçin", Value = "-1", Selected = true });
            foreach (var item in language)
            {
                if (item.Id==selected.LangId)
                {
                    liste.FirstOrDefault().Selected = false;
                    liste.Add(new SelectListItem { Text = item.LangName, Value = item.Id.ToString(), Selected = true });
                }
                else
                {
                  
                    liste.Add(new SelectListItem { Text = item.LangName, Value = item.Id.ToString(), Selected = false });
                }
              
            }
            
            model.SubTitle = selected.SubTitle;
            model.Title = selected.Title;
            model.Tags = selected.Tags;
            model.Id = selected.Id;
            model.Content = selected.Content;
            var image = _image.Get(x => x.Id == selected.ImageId);
            model.ImageId = selected.ImageId;
            string imageUrl = "";
            if (image != null)

            {
                model.ImageUrl = image.ImageUrl;
            }
            else
            {
                imageUrl="";
            }


            //liste.Items.a
            TempData["lang"] = liste;
            return View(model);
        }

        [HttpPost]
        public IActionResult PostUpdateBlogPost(BlogPostRequest post)
        {
            try
            {
                var newPost = _blogPost.GetById(post.Id);
                newPost.LangId = post.LangId;
                if (post.BlogId==-1)
                {
                    newPost.BlogId = null;
                }
                else
                {
                    newPost.BlogId = post.BlogId;
                }
                newPost.Content = post.Content;
                newPost.SubTitle = post.SubTitle;
                newPost.Title = post.Title;
                newPost.Tags = post.Tags;
                newPost.LangId = post.LangId;
                //_blogPost.Update(newPost);
                //_blogPost.Save();
                string newFileName = "";
                if (post.images != null)
                {
                    FileUpload file = new FileUpload(host);
                    foreach (IFormFile source in post.images)
                    {
                        string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');
                        var wanted = "blogPost_" + post.Id;
                        filename = file.EnsureCorrectFilename(filename, wanted);
                        newFileName = "\\uploads\\banners\\" + filename;
                        using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "\\uploads\\banners\\")))
                            source.CopyToAsync(output);

                    }
                    int imageId = post.ImageId ?? 0;
                    var image = _image.GetById(imageId);
                    image.ImageAltTag = post.Title;
                    image.ImageTitle = post.Title;
                    image.ImageUrl = newFileName;
                    image.IsBelongGallery = false;
                    _image.Update(image);
                    _image.Save();
                    newPost.ImageId = image.Id;
                    _blogPost.Update(newPost);
                    _blogPost.Save();



                }
                else
                {
                    _blogPost.Update(newPost);
                    _blogPost.Save();
                }
                TempData["result"] = 1;
                return RedirectToAction("AddBlogPost", "BlogManagament");
            }
            catch (Exception)
            {
                TempData["result"] = -1;
                throw;
            }
        }
        #endregion

        #region GetBlogPost
        public IActionResult GetBlogPost()
        {
            var list = _blogPost.GetAll().ToList();
            return View(list);
        }

        #endregion

        #region DeleteBlogPost

        public IActionResult deleteBlogPost(int id)
        {
            _blogPost.Delete(id);
            _blogPost.Save();
            return RedirectToAction("GetBlogPost","BlogManagament");
        }


        #endregion


        #region OtherActions

        public IActionResult countBlog()
        {
            //dil idsi query string olarak gönderilecek ve kontorl edilecek
            return View();
        }
        public IActionResult countBlogPost()
        {
            //dil idsi query string olarak gönderilecek ve kontorl edilecek
            return View();
        }

        [HttpPost]
        public int PostUploadFilesAjax(int detailId)
        {
            //var gelenId = Request.Query.FirstOrDefault(x => x.Key == "id");
            long size = 0;
            var detail = _blogPost.GetById(detailId);
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
                        var tags = _blogPost.GetById(detailId).Title.Replace(' ', ',');
                        string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');
                        var wanted = "blogPost" + detailId;
                        filename = file.EnsureCorrectFilename(filename, wanted);
                        newFileName = "\\uploads\\blogPost\\" + filename;
                        using (FileStream output = System.IO.File.Create(file.GetPathAndFilename(filename, "\\uploads\\blogPost\\")))
                            source.CopyTo(output);

                        newImageId = _image.InsertAndgetId(new Data.Models.DB.Image { ImageAltTag = detail.Title, ImageTag = tags, ImageTitle = detail.Title, ImageUrl = newFileName, IsBelongGallery = false });
                    }

                }
                detail.ImageId = newImageId;
                _blogPost.Update(detail);
                _blogPost.Save();
                TempData["result"] = 1;
            }


            return 1;
        }
        #endregion 

    }
}