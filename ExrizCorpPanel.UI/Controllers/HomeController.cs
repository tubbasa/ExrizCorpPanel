using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExrizCorpPanel.UI.Models;
using ExrizCorpPanel.UI.Models.CustomModels;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;

namespace ExrizCorpPanel.UI.Controllers
{
    public class HomeController : Controller
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
        private static IPageTypeRepository pageType;

        private static LayoutModel layoutModel;

        public HomeController(IMenuRepository _menu, ISliderGalleryRepository _gallery, ISliderRepository _slider, IBannerRepository _banner, IReferenceJobRepository _job, IJobLanguageRepository _jobresource, IReferencesRepository _reference, ISiteDetailRepository _site, IMenuItemsRepository _menuItems, ISlugRepository _slug, IThemesRepository _theme, IImageRepository _image, ISocialMediaObjectRepository _socialObject, ISocialMediaTypesRepository _socialType, IStringResourceRepository _stringresource, IResourceVariableRepository _resourcevariables, IAreaDetailRepository _areaDetail, IOnePageAreaRepository _area, IAreaTypeRepository _areaType, ISlugRepository _slugRepo, IBlogRepository _blog, IBlogPostRepository _blogPost, IPageRepository _page, IPageDetailRepository _pageDetail, ICategoryRepository _category, IPageTypeRepository _pageType)
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
            pageType = _pageType;
            ViewBag.LayoutModel = layoutModel;
        }



        public LayoutModel generateLayoutModel()
        {
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
            if (detail.LogoImageId != null && detail.LogoImageId != 0)
            {
                detail.LogoUrl = image.GetById(Convert.ToInt32(sitedetail.LogoImageId)).ImageUrl;
            }
            if (detail.SmallLogoImageId != null && detail.SmallLogoImageId != 0)
            {
                detail.SmallLogoImageUrl = image.GetById(Convert.ToInt32(sitedetail.SmallLogoImageId)).ImageUrl;
            }
            detail.MenuBranches = sitedetail.MenuBranches;
            detail.SiteAddress = sitedetail.SiteAddress;
            detail.SiteDesc = sitedetail.SiteDesc;
            detail.SiteName = sitedetail.SiteName;
            detail.SiteTagLine = sitedetail.SiteTagLine;
            detail.SiteTags = sitedetail.SiteTags;
            detail.SiteTitle = sitedetail.SiteTitle;
            detail.TelNo = sitedetail.TelNo;
            detail.SiteEmail = sitedetail.SiteEmail;
            detail.LogoImageId = sitedetail.LogoImageId;
            detail.SmallLogoImageId = sitedetail.SmallLogoImageId;
            if (detail.LogoImageId != null && detail.SmallLogoImageId != 0)
            {
                detail.LogoUrl = image.GetById(detail.LogoImageId.Value).ImageUrl; ;
            }

            if (detail.SmallLogoImageId != null && detail.SmallLogoImageId != 0)
            {
                detail.SmallLogoImageUrl = image.GetById(detail.SmallLogoImageId.Value).ImageUrl; ;
            }

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
        public IActionResult Index()
        {

            MainModel MainModel = generateMainModel();


            if (layoutModel.siteDetail.ThemeName != null)
            {
                var partialName = "~/Views/" + layoutModel.siteDetail.ThemeName + "/Home/_Index.cshtml";
                ViewBag.LayoutModel = layoutModel;
                return View(partialName, MainModel);
            }
            else
            {
                return View();
            }


        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //public IActionResult checkGlobalUrl(string slug)
        //{
        //    var selected = slugRepo.Get(x=>x.SlugName==slug);

        //    if (selected.EntityName=="BlogPost")
        //    {
        //        DetailPagesModel model = new DetailPagesModel();
        //        BlogModel blogModel = generateBlogModel(Convert.ToInt32(selected.EntityId));
        //        model.BlogModel = blogModel;
        //        model.Type = selected.EntityTitle;
        //        if (layoutModel.siteDetail.ThemeName != null)
        //        {
        //            var partialName = "~/Views/" + layoutModel.siteDetail.ThemeName + "/Blog/_BlogPost.cshtml";
        //            ViewBag.LayoutModel = layoutModel;
        //            return View(partialName, model.BlogModel);
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }
        //  else if(selected.EntityName=="Page")
        //    {
        //        DetailPagesModel model = new DetailPagesModel();
        //        PageModel pageModel = generatePageModel(Convert.ToInt32(selected.EntityId));
        //        model.PageModel = pageModel;
        //        model.Type = selected.EntityTitle;
        //        if (layoutModel.siteDetail.ThemeName != null)
        //        {

        //            var partialName = "~/Views/" + layoutModel.siteDetail.ThemeName + "/Home/_Page.cshtml";
        //            ViewBag.LayoutModel = layoutModel;
        //            return View(partialName, model.PageModel);
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }
        //    return View();
        //}




        #region

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
        public MainModel generateMainModel()
        {
            MainModel model = new MainModel();
            model.banners = banner.GetAll().Select(x => new Models.Components.Banner { Id = x.Id, BannerContent = x.BannerContent, BannerLink = x.BannerLink, BannerName = x.BannerName, ImageUrl = image.GetById(Convert.ToInt32(x.Image)).ImageUrl }).ToList();
            model.jobresources = jobresource.GetAll().Select(x => new Models.Components.JobLanguage { Id = x.Id, Content = x.Content, JobId = x.JobId, JobName = x.JobName, LangId = x.LangId }).ToList();
            model.jobs = job.GetAll().Select(x => new Models.Components.Job { Id = x.Id, JobSystemName = x.JobSystemName, ReferenceId = x.ReferenceId, ReferenceName = reference.GetById(Convert.ToInt32(x.ReferenceId)).ReferenceName }).ToList();
            model.references = reference.GetAll().Select(x => new Models.Components.Reference { Id = x.Id, ReferenceName = x.ReferenceName, Url = x.Url, ReferenceImageUrl = image.GetById(Convert.ToInt32(x.ReferenceImage)).ImageUrl }).ToList();
            var sliderGalleryId = gallery.Get(x => x.IsActive == true).Id;
            model.sliders = slider.GetMany(x => x.GalleryId == sliderGalleryId).Select(x => new Models.Components.Slider { Id = x.Id, GalleryId = x.GalleryId, LangId = x.LangId, RawNumber = x.RawNumber, Url = x.Url, SliderName = x.SliderName, SliderContent = x.SliderContent, ImageUrl = image.GetById(Convert.ToInt32(x.ImageId)).ImageUrl.Replace("\\", "/") }).ToList();
            model.stringResources = resourcevariables.GetAll().Select(x => new ExrizCorpPanel.UI.Models.Components.Resources { Id = x.Id, LangId = x.LangId, ResourceContent = x.ResourceContent, ResourceId = x.ResourceId, ResourceSystemName = stringresource.GetById(Convert.ToInt32(x.ResourceId)).ResourceName }).ToList();
            model.areas = areaDetail.GetMany(x => x.LangId == 3).Select(x => new Models.Components.AreaDetail { Id = x.Id, AreaId = x.AreaId, Content = x.Content, IconName = x.IconName, ImageId = x.ImageId, LangId = x.LangId, rowNumber = Convert.ToInt32(area.GetById(Convert.ToInt32(x.AreaId)).RowNumber), imageUrl = x.ImageId != null ? image.GetById(Convert.ToInt32(x.ImageId)).ImageUrl : "", Title = x.Title, typeId = Convert.ToInt32(area.GetById(Convert.ToInt32(x.AreaId)) != null ? area.GetById(Convert.ToInt32(x.AreaId)).TypeId : 0), typeName = areaType.GetById(Convert.ToInt32(area.GetById(Convert.ToInt32(x.AreaId)) != null ? area.GetById(Convert.ToInt32(x.AreaId)).TypeId.Value : 0)) != null ? areaType.GetById(Convert.ToInt32(area.GetById(Convert.ToInt32(x.AreaId)) != null ? area.GetById(Convert.ToInt32(x.AreaId)).TypeId.Value : 0)).AreaSystemTypeName : "" }).ToList();
            model.pages = new List<PageModel>();
            var allPages = page.GetAll();
            model.blogPosts = new List<BlogPostModel>();
            var BlogPosts = blogPost.takeLast(5);
            foreach (var item in page.GetAll())
            {
                PageModel obj = new PageModel();
                if (item.TypeId != 0)
                {
                    var typeName = item.TypeId != null ? pageType.GetById(item.TypeId.Value) : null;
                    if (typeName != null)
                    {
                        obj.PageTypeName = typeName != null ? typeName.PageName : "";
                        obj.PageTypeId = item.TypeId.Value;
                    }

                    var detail = pageDetail.Get(x => x.PageId == item.Id && x.LangId == 3);
                    obj.Content = detail.Content;
                    obj.ImageUrl = detail.ImageId != null ? image.GetById(detail.ImageId.Value).ImageUrl : "/img/no-image.png";
                    obj.IsIndexable = item.IsIndexable;
                    obj.LangId = detail.LangId;
                    obj.PageDetailId = detail.Id;
                    obj.PageId = item.Id;
                    obj.PageName = item.PageName;
                    obj.SubTitle = detail.SubTitle;
                    obj.Content = detail.Content;
                    obj.Title = detail.Title;
                    obj.Url = slug.Get(x => x.EntityName == "Pages" && x.EntityId == item.Id)!=null?slug.Get(x => x.EntityName == "Pages" && x.EntityId == item.Id).SlugName:"";
                }
                model.pages.Add(obj);
            }

            foreach (var item in BlogPosts)
            {
                BlogPostModel post = new BlogPostModel();
                post.Content = item.Content;
                post.Date = item.Date!=null ? item.Date.Value:item.Date;
                post.ImageUrl = item.ImageId != null ? image.GetById(item.ImageId.Value).ImageUrl : "no-image.png";
                post.IsIndexable = item.IsIndexable;
                post.LangId = item.LangId;
                post.PostId = item.Id;
                post.SubTitle = item.SubTitle;
                post.Title = item.Title;
                post.Url = slug.Get(x=>x.EntityId==item.Id&&x.EntityName== "Posts").SlugName;
                model.blogPosts.Add(post);
            }
            


            return model;
        }
        #endregion
    }
}
