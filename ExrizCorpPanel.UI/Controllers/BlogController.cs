using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
using ExrizCorpPanel.UI.Models.Components;
using ExrizCorpPanel.UI.Models.CustomModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExrizCorpPanel.UI.Controllers
{
    public class BlogController : Controller
    {
        private static IMenuRepository menu;
        private static IMenuItemsRepository menuItems;
        private static ISliderGalleryRepository gallery;
        private static ISliderRepository slider;
        private static ISlugRepository slugRepo;
        private static IBannerRepository banner;
        private static IReferenceJobRepository job;
        private static IJobLanguageRepository jobresource;
        private static IReferencesRepository reference;
        private static ISiteDetailRepository site;
        private static ISlugRepository slug;
        private static IThemesRepository theme;
        private static IImageRepository image;
        private static IBlogRepository blog;
        private static IBlogPostRepository blogPost;
        private static ISocialMediaObjectRepository socialObject;
        private static ISocialMediaTypesRepository socialType;
        private static IStringResourceRepository stringresource;
        private static IResourceVariableRepository resourcevariables;
        private static IAreaDetailRepository areaDetail;
        private static IOnePageAreaRepository area;
        private static IAreaTypeRepository areaType;
        private static IPageRepository page;
        private static IPageDetailRepository pageDetail;
        private static ICategoryRepository category;
        private readonly IHttpContextAccessor requestContext;

        private static LayoutModel layoutModel;

        public BlogController(IMenuRepository _menu, ISliderGalleryRepository _gallery, ISliderRepository _slider, IBannerRepository _banner, IReferenceJobRepository _job, IJobLanguageRepository _jobresource, IReferencesRepository _reference, ISiteDetailRepository _site, IMenuItemsRepository _menuItems, ISlugRepository _slug, IThemesRepository _theme, IImageRepository _image, ISocialMediaObjectRepository _socialObject, ISocialMediaTypesRepository _socialType, IStringResourceRepository _stringresource, IResourceVariableRepository _resourcevariables, IAreaDetailRepository _areaDetail, IOnePageAreaRepository _area, IAreaTypeRepository _areaType, ISlugRepository _slugRepo, IBlogRepository _blog, IBlogPostRepository _blogPost, IPageRepository _page, IPageDetailRepository _pageDetail, ICategoryRepository _category, IHttpContextAccessor _requestContext)
        {
            menu = _menu;
            menuItems = _menuItems;
            gallery = _gallery;
            slider = _slider;
            banner = _banner;
            job = _job;
            jobresource = _jobresource;
            reference = _reference;
            site = _site;
            slug = _slug;
            theme = _theme;
            image = _image;
            socialObject = _socialObject;
            socialType = _socialType;
            stringresource = _stringresource;
            resourcevariables = _resourcevariables;
            area = _area;
            areaDetail = _areaDetail;
            areaType = _areaType;
            slugRepo = _slugRepo;
            layoutModel = generateLayoutModel();
            blog = _blog;
            blogPost = _blogPost;
            page = _page;
            pageDetail = _pageDetail;
            page = _page;
            category = _category;
            requestContext = _requestContext;
            ViewBag.LayoutModel = layoutModel;
        }


     

    public string BaseUrl()
        {
            var request = requestContext;
            var dd = HttpContext.Request;
            // Now that you have the request you can select what you need from it.
            return string.Empty;
        }

        public LayoutModel generateLayoutModel()
        {
            var dd = Request;
            var menuId = menu.Get(x => x.IsForWeb == true && x.IsActive == true).Id;
            List<ExrizCorpPanel.UI.Models.Components.Menu> menus = new List<Models.Components.Menu>();
            var items = menuItems.GetMany(x => x.MenuId == menuId && x.RelatedMenuItemId == null);
            foreach (var item in items)
            {
                
                ExrizCorpPanel.UI.Models.Components.Menu mn = new Models.Components.Menu();
                mn.Id = item.Id;
                mn.IsIndexable = item.IsIndexable;
                mn.Keys = item.Keys;
                mn.LangId = item.LangId;
                mn.MenuId = item.MenuId;
                mn.RawNumber = item.RawNumber;
                mn.Title = item.Title;
                var selectedSlug = slug.GetById(item.UrlId);
                if (selectedSlug == null)
                {
                    mn.Url = "/";
                }
                else
                {
                    mn.Url = selectedSlug.ControllerName + "/" + selectedSlug.EntityName + "/" + selectedSlug.SlugName;
                }
                menus.Add(mn);
            }
            var test = getRelations(menus, Convert.ToInt32(site.GetAll().FirstOrDefault().MenuBranches));
            ViewBag.Menus = test;
            ViewBag.themeName = "Decor";
            ViewBag.layout = "_LayoutDecor";

            LayoutModel model = new LayoutModel();
            model.menu = test;
            var sitedetail = site.GetAll().FirstOrDefault();
            ExrizCorpPanel.UI.Models.Components.SiteDetail detail = new Models.Components.SiteDetail();
            detail.AnalyticsApikey = sitedetail.AnalyticsApikey;
            detail.GsmNo = sitedetail.GsmNo;
            detail.Id = sitedetail.Id;
            detail.LayoutName = theme.GetById(Convert.ToInt32(sitedetail.ThemeId)).LayoutName;
            detail.ThemeName = theme.GetById(Convert.ToInt32(sitedetail.ThemeId)).ThemeName;
            detail.LogoUrl = image.GetById(Convert.ToInt32(sitedetail.LogoImageId)).ImageUrl;
            detail.MenuBranches = sitedetail.MenuBranches;
            detail.SiteAddress = sitedetail.SiteAddress;
            detail.SiteDesc = sitedetail.SiteDesc;
            detail.SiteName = sitedetail.SiteName;
            detail.SiteTagLine = sitedetail.SiteTagLine;
            detail.SiteTags = sitedetail.SiteTags;
            detail.SiteTitle = sitedetail.SiteTitle;
            detail.TelNo = sitedetail.TelNo;
            detail.SiteEmail = sitedetail.SiteEmail;
            model.siteDetail = detail;
            List<ExrizCorpPanel.UI.Models.Components.SocialMedia> sm = new List<ExrizCorpPanel.UI.Models.Components.SocialMedia>();
            var allsobjects = socialObject.GetAll();
            foreach (var item in allsobjects)
            {
                sm.Add(new Models.Components.SocialMedia { Id = item.Id, ProfileName = item.ProfileName, Smprofile = item.Smprofile, TypeName = socialType.GetById(Convert.ToInt32(item.TypeId)).SocialMediaName });
            }
            model.socialMedia = sm;

            return model;

        }

        public List<ExrizCorpPanel.UI.Models.Components.Menu> getRelations(List<ExrizCorpPanel.UI.Models.Components.Menu> mainMenus, int count)
        {
            var menuListesi = new List<ExrizCorpPanel.UI.Models.Components.Menu>();
            var sayac = count - 1;

            var firstBatch = mainMenus;
            List<int> firstBatchIds = new List<int>();
            foreach (var item in firstBatch)
            {
                firstBatchIds.Add(item.Id);
            }

            List<ExrizCorpPanel.UI.Models.Components.Menu> secondBatch = new List<ExrizCorpPanel.UI.Models.Components.Menu>();
            foreach (var item in firstBatchIds)
            {
                var submenus = menuItems.GetMany(x => x.RelatedMenuItemId == item).Select(x => new ExrizCorpPanel.UI.Models.Components.Menu { Id = x.Id, IsIndexable = x.IsIndexable, Keys = x.Keys, LangId = x.LangId, MenuId = x.MenuId, RawNumber = x.RawNumber, Title = x.Title, Url = slug.GetById(x.UrlId).ControllerName + "/" + slug.GetById(x.UrlId).SlugName }); ;
                secondBatch = submenus.ToList();
                if (sayac != 0)
                {
                    mainMenus.FirstOrDefault(x => x.Id == item).RelatedMenu = submenus.ToList();
                }


            }

            List<ExrizCorpPanel.UI.Models.Components.Menu> thirdBatch = new List<ExrizCorpPanel.UI.Models.Components.Menu>();
            foreach (var item in secondBatch)
            {
                var submenus = menuItems.GetMany(x => x.RelatedMenuItemId == item.Id).Select(x => new ExrizCorpPanel.UI.Models.Components.Menu { Id = x.Id, IsIndexable = x.IsIndexable, Keys = x.Keys, LangId = x.LangId, MenuId = x.MenuId, RawNumber = x.RawNumber, Title = x.Title, Url = slug.GetById(x.UrlId).ControllerName + "/" + slug.GetById(x.UrlId).SlugName });
                thirdBatch = submenus.ToList();
                if (sayac != 0)
                {
                    if (submenus.Count() > 0)
                    {
                        mainMenus.FirstOrDefault(x => x.Id == item.Id).RelatedMenu = submenus.ToList();
                    }

                }
            }

            List<ExrizCorpPanel.UI.Models.Components.Menu> fourthBatch = new List<ExrizCorpPanel.UI.Models.Components.Menu>();
            foreach (var item in thirdBatch)
            {
                var submenus = menuItems.GetMany(x => x.RelatedMenuItemId == item.Id).Select(x => new ExrizCorpPanel.UI.Models.Components.Menu { Id = x.Id, IsIndexable = x.IsIndexable, Keys = x.Keys, LangId = x.LangId, MenuId = x.MenuId, RawNumber = x.RawNumber, Title = x.Title, Url = slug.GetById(x.UrlId).ControllerName + "/" + slug.GetById(x.UrlId).SlugName });
                fourthBatch = submenus.ToList();
                if (sayac != 0)
                {
                    if (submenus.Count() > 0)
                    {
                        mainMenus.FirstOrDefault(x => x.Id == item.Id).RelatedMenu = submenus.ToList();
                    }

                }
            }


            return mainMenus;
        }

        //[Route("Blog/Post")]
        //public IActionResult Index()
        //{
        //    var dd = Request.Path;
        //    BlogModel blogModel = generateBlogModel();
        //    ViewBag.LayoutModel = layoutModel;
        //    if (layoutModel.siteDetail.ThemeName != null)
        //    {
        //        var partialName = "~/Views/" + layoutModel.siteDetail.ThemeName + "/Blog/_Index.cshtml";
        //        ViewBag.LayoutModel = layoutModel;
        //        return View(partialName, blogModel);
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        [Route("Blog/Posts/{slugName}")]
        public IActionResult BlogPost(string slugName)
        {
            var dd = Request.Path;
            GlobalUrls selectedSlug = slug.Get(x => x.SlugName == slugName);
            BlogPostModel blogModel = generateBlogPostModel(Convert.ToInt32(selectedSlug.EntityId));
            ViewBag.LayoutModel = layoutModel;
            if (layoutModel.siteDetail.ThemeName != null)
            {
                var partialName = "~/Views/" + layoutModel.siteDetail.ThemeName + "/Blog/_BlogPost.cshtml";
                ViewBag.LayoutModel = layoutModel;
                return View(partialName, blogModel);
            }
            else
            {
                return View();
            }
        }

        [Route("Blog/Posts")]
        public IActionResult Posts()
        {
            var dd = Request.Path;
            BlogModel blogModel = generateBlogModel();
            ViewBag.LayoutModel = layoutModel;
            if (layoutModel.siteDetail.ThemeName != null)
            {
                var partialName = "~/Views/" + layoutModel.siteDetail.ThemeName + "/Blog/_Posts.cshtml";
                ViewBag.LayoutModel = layoutModel;
                return View(partialName, blogModel);
            }
            else
            {
                return View();
            }
        }

        [Route("Blog/Categories")]
        public IActionResult Categories()
        {
            BlogCategoriesModel model = generateBlogCategoriesModel();
            ViewBag.LayoutModel = layoutModel;
            if (layoutModel.siteDetail.ThemeName != null)
            {
                var partialName = "~/Views/" + layoutModel.siteDetail.ThemeName + "/Blog/_Categories.cshtml";
                ViewBag.LayoutModel = layoutModel;
                return View(partialName, model);
            }
            else
            {
                return View();
            }
        }

        [Route("Blog/Categories/{slugName}")]
        public IActionResult Category(string slugName)
        {
            var selectedCategory = slugRepo.Get(x=>x.SlugName == slugName && x.EntityName =="Categories" && x.ControllerName == "Blog");
            BlogCategoryModel model = generateBlogCategoryModel(selectedCategory.EntityId.Value);
            ViewBag.LayoutModel = layoutModel;
            if (layoutModel.siteDetail.ThemeName != null)
            {
                var partialName = "~/Views/" + layoutModel.siteDetail.ThemeName + "/Blog/_Category.cshtml";
                ViewBag.LayoutModel = layoutModel;
                return View(partialName, model);
            }
            else
            {
                return View();
            }
        }

        public BlogPostModel generateBlogPostModel(int PostId)
        {
            var dd = Request;
            var blogPostModel = blogPost.GetById(PostId);
            var relatedBlogModel = blog.GetById(Convert.ToInt32(blogPostModel.BlogId));
            BlogPostModel model = new BlogPostModel();
            model.BlogId = relatedBlogModel.Id;
            model.BlogName = relatedBlogModel.Name;
            model.CategoryId = Convert.ToInt32(relatedBlogModel.CategoryId);
            model.CategoryName = model.CategoryId == 0 ? "Kategorisiz" : category.GetById(Convert.ToInt32(model.CategoryId)).FriendlyName;
            model.Content = blogPostModel.Content;
            model.ImageId = blogPostModel.ImageId;
            model.ImageUrl = image.GetById(Convert.ToInt32(blogPostModel.ImageId)).ImageUrl;
            model.IsIndexable = blogPostModel.IsIndexable;
            model.LangId = blogPostModel.LangId;
            model.PostId = blogPostModel.Id;
            model.SubTitle = blogPostModel.SubTitle;
            model.Tags = blogPostModel.Tags;
            model.Title = blogPostModel.Title;
            //sessiondan sonra burayı session langıd sine göre çekilecek
            model.resources = stringresource.GetAll().Select(x=> new ExrizCorpPanel.UI.Models.Components.StringResource { Id=x.Id,ResourceName=x.ResourceName,variables=resourcevariables.GetMany(v=>v.ResourceId==x.Id&&v.LangId==1).Select(rs=>new ResourceVariables { Id=rs.Id,LangId=rs.LangId,ResourceContent=rs.ResourceContent,ResourceId=rs.ResourceId}).FirstOrDefault()}).ToList();
            model.recentPosts = blogPost.GetAll().TakeLast(5).Select(x=>new RecentPost { Slug=slug.Get(s=>s.EntityId==x.Id).SlugName,Title=x.Title,Date = x.Date != null? x.Date.Value.Date.ToShortDateString():"tarih girilmemiş.",Image = x.ImageId != null ? image.GetById(x.ImageId.Value).ImageUrl: ""}).ToList();
            model.blogCategories = category.GetAll().Select(x=> new BlogCategories {FriendlyName = x.FriendlyName,Id= x.Id,RelatedCategoryId= x.RelatedCategoryId }).ToList();
            //model.categ

            return model;


        }

        public BlogModel generateBlogModel()
        {
            var dd = Request;
            var blogPostList = blogPost.GetAll().Take(10);
           
            BlogModel model = new BlogModel();
            model.blogPost = new List<BlogPostModel>();
            foreach (var item in blogPostList)
            {
                var relatedBlogModel = blog.GetById(Convert.ToInt32(item.BlogId));
           
                BlogPostModel post = new BlogPostModel();
                post.BlogId = relatedBlogModel.Id;
                post.BlogName = relatedBlogModel.Name;
                post.CategoryId = Convert.ToInt32(relatedBlogModel.CategoryId);
                var catId = relatedBlogModel.CategoryId;
                post.CategoryName = catId == null ? "Kategorisiz" : category.GetById(Convert.ToInt32(relatedBlogModel.CategoryId)).FriendlyName;
                post.Content = item.Content;
                post.ImageId = item.ImageId;
                post.ImageUrl = image.GetById(Convert.ToInt32(item.ImageId)).ImageUrl;
                post.IsIndexable = item.IsIndexable;
                post.LangId = item.LangId;
                post.PostId = item.Id;
                post.SubTitle = item.SubTitle;
                post.Tags = item.Tags;
                post.Title = item.Title;
                post.Date = item.Date;
               
                var selectedSlug = slug.Get(x => x.EntityId == item.Id && x.EntityName == "Posts");
                post.Url = "/" + selectedSlug.ControllerName + "/" + selectedSlug.EntityName + "/" + selectedSlug.SlugName;
                //sessiondan sonra burayı session langıd sine göre çekilecek
                post.resources = stringresource.GetAll().Select(x => new ExrizCorpPanel.UI.Models.Components.StringResource { Id = x.Id, ResourceName = x.ResourceName, variables = resourcevariables.GetMany(v => v.ResourceId == x.Id && v.LangId == 1).Select(rs => new ResourceVariables { Id = rs.Id, LangId = rs.LangId, ResourceContent = rs.ResourceContent, ResourceId = rs.ResourceId }).FirstOrDefault() }).ToList();
                post.recentPosts = blogPost.GetAll().TakeLast(5).Select(x => new RecentPost { Slug = slug.Get(s => s.EntityId == x.Id).SlugName, Title = x.Title }).ToList();
                model.blogCategories = category.GetAll().Select(x => new BlogCategories { FriendlyName = x.FriendlyName, Id = x.Id, RelatedCategoryId = x.RelatedCategoryId }).ToList();
                model.blogPost.Add(post);
            }
         var recentPost = blogPostList.OrderByDescending(x=>x.Date).Take(4);
            model.recentPosts = new List<BlogPostModel>();
            foreach (var item in recentPost)
            {
                var relatedBlogModel = blog.GetById(Convert.ToInt32(item.BlogId));

                BlogPostModel post = new BlogPostModel();
                post.BlogId = relatedBlogModel.Id;
                post.BlogName = relatedBlogModel.Name;
                post.CategoryId = Convert.ToInt32(relatedBlogModel.CategoryId);
                var catId = relatedBlogModel.CategoryId;
                post.CategoryName = catId == null ? "Kategorisiz" : category.GetById(Convert.ToInt32(relatedBlogModel.CategoryId)).FriendlyName;
                post.Content = item.Content;
                post.ImageId = item.ImageId;
                post.ImageUrl = image.GetById(Convert.ToInt32(item.ImageId)).ImageUrl;
                post.IsIndexable = item.IsIndexable;
                post.LangId = item.LangId;
                post.PostId = item.Id;
                post.SubTitle = item.SubTitle;
                post.Tags = item.Tags;
                post.Title = item.Title;
            
                var selectedSlug = slug.Get(x => x.EntityId == item.Id && x.EntityName == "Posts");
                post.Url = "/" + selectedSlug.ControllerName + "/" + selectedSlug.EntityName + "/" + selectedSlug.SlugName;
                //sessiondan sonra burayı session langıd sine göre çekilecek
                post.resources = stringresource.GetAll().Select(x => new ExrizCorpPanel.UI.Models.Components.StringResource { Id = x.Id, ResourceName = x.ResourceName, variables = resourcevariables.GetMany(v => v.ResourceId == x.Id && v.LangId == 1).Select(rs => new ResourceVariables { Id = rs.Id, LangId = rs.LangId, ResourceContent = rs.ResourceContent, ResourceId = rs.ResourceId }).FirstOrDefault() }).ToList();
                post.recentPosts = blogPost.GetAll().TakeLast(5).Select(x => new RecentPost { Slug = slug.Get(s => s.EntityId == x.Id).SlugName, Title = x.Title }).ToList();
                model.blogCategories = category.GetAll().Select(x => new BlogCategories { FriendlyName = x.FriendlyName, Id = x.Id, RelatedCategoryId = x.RelatedCategoryId }).ToList();
                model.recentPosts.Add(post);
            }
           
            //model.categ

            return model;


        }

        public BlogCategoryModel generateBlogCategoryModel(int entityId)
        {
            var selectedCategory = category.Get(x=>x.Id == entityId);
            var selectedBlogs = blog.GetMany(x=>x.CategoryId == selectedCategory.Id);
            var postLists = new List<BlogPost>();
            foreach (var item in selectedBlogs)
            {
                var posts = blogPost.GetMany(x=>x.BlogId == item.Id);
                postLists.AddRange(posts);
            }


            BlogCategoryModel model = new BlogCategoryModel();
            model.blogPost = new List<BlogPostModel>();
            foreach (var item in postLists)
            {
                var relatedBlogModel = blog.GetById(Convert.ToInt32(item.BlogId));

                BlogPostModel post = new BlogPostModel();
                post.BlogId = relatedBlogModel.Id;
                post.BlogName = relatedBlogModel.Name;
                post.CategoryId = Convert.ToInt32(relatedBlogModel.CategoryId);
                var catId = relatedBlogModel.CategoryId;
                post.CategoryName = catId == null ? "Kategorisiz" : category.GetById(Convert.ToInt32(relatedBlogModel.CategoryId)).FriendlyName;
                post.Content = item.Content;
                post.ImageId = item.ImageId;
                post.ImageUrl = image.GetById(Convert.ToInt32(item.ImageId)).ImageUrl;
                post.IsIndexable = item.IsIndexable;
                post.LangId = item.LangId;
                post.PostId = item.Id;
                post.SubTitle = item.SubTitle;
                post.Tags = item.Tags;
                post.Title = item.Title;

                var selectedSlug = slugRepo.Get(x => x.EntityId == item.Id && x.EntityName == "Posts");
                post.Url = "/" + selectedSlug.ControllerName + "/" + selectedSlug.EntityName + "/" + selectedSlug.SlugName;
                //sessiondan sonra burayı session langıd sine göre çekilecek
                post.resources = stringresource.GetAll().Select(x => new ExrizCorpPanel.UI.Models.Components.StringResource { Id = x.Id, ResourceName = x.ResourceName, variables = resourcevariables.GetMany(v => v.ResourceId == x.Id && v.LangId == 1).Select(rs => new ResourceVariables { Id = rs.Id, LangId = rs.LangId, ResourceContent = rs.ResourceContent, ResourceId = rs.ResourceId }).FirstOrDefault() }).ToList();
                post.recentPosts = blogPost.GetAll().TakeLast(5).Select(x => new RecentPost { Slug = slugRepo.Get(s => s.EntityId == x.Id).SlugName, Title = x.Title }).ToList();
                model.blogPost.Add(post);
            }

            model.blogCategories = category.GetAll().Select(x => new BlogCategories { FriendlyName = x.FriendlyName, Id = x.Id, RelatedCategoryId = x.RelatedCategoryId }).ToList();
            //model.categ

            return model;


        }

        public BlogCategoriesModel generateBlogCategoriesModel()
        {
            BlogCategoriesModel model = new BlogCategoriesModel();
            model.categories = category.GetAll().Select(x => new BlogCategories { FriendlyName = x.FriendlyName, Id = x.Id, RelatedCategoryId = x.RelatedCategoryId,Slug = slug.Get(s=>s.EntityId==x.Id && s.EntityName == "Categories" && s.ControllerName =="Blog").SlugName , CountOfPost = blogPost.GetMany(bp=>bp.BlogId == blog.Get(b => b.CategoryId == x.Id).Id).Count() }).ToList();
            var recentPost = blogPost.GetAll().Take(10).OrderByDescending(x => x.Date).Take(4);
            model.recentPosts = new List<BlogPostModel>();
            foreach (var item in recentPost)
            {
                var relatedBlogModel = blog.GetById(Convert.ToInt32(item.BlogId));

                BlogPostModel post = new BlogPostModel();
                post.BlogId = relatedBlogModel.Id;
                post.BlogName = relatedBlogModel.Name;
                post.CategoryId = Convert.ToInt32(relatedBlogModel.CategoryId);
                var catId = relatedBlogModel.CategoryId;
                post.CategoryName = catId == null ? "Kategorisiz" : category.GetById(Convert.ToInt32(relatedBlogModel.CategoryId)).FriendlyName;
                post.Content = item.Content;
                post.ImageId = item.ImageId;
                post.ImageUrl = image.GetById(Convert.ToInt32(item.ImageId)).ImageUrl;
                post.IsIndexable = item.IsIndexable;
                post.LangId = item.LangId;
                post.PostId = item.Id;
                post.SubTitle = item.SubTitle;
                post.Tags = item.Tags;
                post.Title = item.Title;

                var selectedSlug = slug.Get(x => x.EntityId == item.Id && x.EntityName == "Posts");
                post.Url = "/" + selectedSlug.ControllerName + "/" + selectedSlug.EntityName + "/" + selectedSlug.SlugName;
                //sessiondan sonra burayı session langıd sine göre çekilecek
                post.resources = stringresource.GetAll().Select(x => new ExrizCorpPanel.UI.Models.Components.StringResource { Id = x.Id, ResourceName = x.ResourceName, variables = resourcevariables.GetMany(v => v.ResourceId == x.Id && v.LangId == 1).Select(rs => new ResourceVariables { Id = rs.Id, LangId = rs.LangId, ResourceContent = rs.ResourceContent, ResourceId = rs.ResourceId }).FirstOrDefault() }).ToList();
                post.recentPosts = blogPost.GetAll().TakeLast(5).Select(x => new RecentPost { Slug = slug.Get(s => s.EntityId == x.Id).SlugName, Title = x.Title }).ToList();
                model.recentPosts.Add(post);
            }

            return model;
        }
    }
}