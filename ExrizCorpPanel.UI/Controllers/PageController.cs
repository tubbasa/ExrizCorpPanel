using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
using ExrizCorpPanel.UI.Models.CustomModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExrizCorpPanel.UI.Controllers
{
    public class PageController : Controller
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
        private static IPageTypeRepository pageType;
        private static IPageDetailRepository pageDetail;
        private static ICategoryRepository category;
        private readonly IHttpContextAccessor requestContext;


        private static LayoutModel layoutModel;

        public PageController(IMenuRepository _menu, ISliderGalleryRepository _gallery, ISliderRepository _slider, IBannerRepository _banner, IReferenceJobRepository _job, IJobLanguageRepository _jobresource, IReferencesRepository _reference, ISiteDetailRepository _site, IMenuItemsRepository _menuItems, ISlugRepository _slug, IThemesRepository _theme, IImageRepository _image, ISocialMediaObjectRepository _socialObject, ISocialMediaTypesRepository _socialType, IStringResourceRepository _stringresource, IResourceVariableRepository _resourcevariables, IAreaDetailRepository _areaDetail, IOnePageAreaRepository _area, IAreaTypeRepository _areaType, ISlugRepository _slugRepo, IBlogRepository _blog, IBlogPostRepository _blogPost, IPageRepository _page, IPageDetailRepository _pageDetail, ICategoryRepository _category, IHttpContextAccessor _requestContext, IPageTypeRepository _pageType)
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
            pageType = _pageType;
            ViewBag.LayoutModel = layoutModel;
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
        public IActionResult Index()
        {
            return View();
        }

        [Route("{slug}")]
        public IActionResult Detail(string slugName)
        {
            GlobalUrls selectedSlug = slug.Get(x => x.SlugName == slugName);
            PageModel pageModel = generatePageModel(Convert.ToInt32(selectedSlug.EntityId));
            ViewBag.LayoutModel = layoutModel;
            return View(pageModel);

        }

        public PageModel generatePageModel(int DetailId)
        {
            var pageDetailModel = pageDetail.GetById(DetailId);
            var RelatedPageModel = page.GetById(pageDetailModel.Id);
            PageModel pageModel = new PageModel();
            pageModel.Content = pageDetailModel.Content;
            pageModel.ImageId = pageDetailModel.ImageId;
            pageModel.ImageUrl = image.GetById(Convert.ToInt32(pageDetailModel.ImageId)).ImageUrl;
            pageModel.IsIndexable = RelatedPageModel.IsIndexable;
            pageModel.LangId = pageDetailModel.LangId;
            pageModel.PageDetailId = pageDetailModel.Id;
            pageModel.PageId = RelatedPageModel.Id;
            pageModel.PageName = RelatedPageModel.PageName;
            pageModel.SeoKeywords = pageDetailModel.SeoKeywords;
            pageDetailModel.SubTitle = pageDetailModel.SubTitle;
            pageDetailModel.Title = pageDetailModel.Title;

            return pageModel;
        }

        [Route("MultiPage/Pages")]
        public IActionResult Pages()
        {
            PagesModel model = GeneratePagesModel();
            ViewBag.LayoutModel = layoutModel;
            if (layoutModel.siteDetail.ThemeName != null)
            {
                var partialName = "~/Views/" + layoutModel.siteDetail.ThemeName + "/Page/_Pages.cshtml";
                ViewBag.LayoutModel = layoutModel;
                return View(partialName, model);
            }
            else
            {
                return View();
            }
        }

        [Route("MultiPage/Pages/{slugName}")]
        public IActionResult Page(string slugName)
        {
            var selectedSlug = slugRepo.Get(x=>x.SlugName == slugName && x.ControllerName == "MultiPage" && x.EntityName == "Pages");
            PageModel model = GeneratePageModel(selectedSlug.EntityId.Value);
            ViewBag.LayoutModel = layoutModel;
            if (layoutModel.siteDetail.ThemeName != null)
            {
                var partialName = "~/Views/" + layoutModel.siteDetail.ThemeName + "/Page/_Page.cshtml";
                ViewBag.LayoutModel = layoutModel;
                return View(partialName, model);
            }
            else
            {
                return View();
            }
        }

        [Route("MultiPage/Categories")]
        public IActionResult Categories()
        {
            MultiPageCategoriesModel model = GenerateCategoriesModel();
            ViewBag.LayoutModel = layoutModel;
            if (layoutModel.siteDetail.ThemeName != null)
            {
                var partialName = "~/Views/" + layoutModel.siteDetail.ThemeName + "/Page/_Categories.cshtml";
                ViewBag.LayoutModel = layoutModel;
                return View(partialName, model);
            }
            else
            {
                return View();
            }
        }

        [Route("MultiPage/Categories/{slugName}")]
        public IActionResult Category(string slugName)
        {
            var selectedSlug = slugRepo.Get(x => x.SlugName == slugName && x.ControllerName == "MultiPage" && x.EntityName == "Categories");
            MultiPageCategoryModel model = GenerateCategoryModel(selectedSlug.EntityId.Value);
            ViewBag.LayoutModel = layoutModel;
            if (layoutModel.siteDetail.ThemeName != null)
            {
                var partialName = "~/Views/" + layoutModel.siteDetail.ThemeName + "/Page/_Category.cshtml";
                ViewBag.LayoutModel = layoutModel;
                return View(partialName, model);
            }
            else
            {
                return View();
            }
        }

        public PageModel GeneratePageModel(int id)
        {
            var selectedPage = page.Get(x=>x.Id == id);
            var selectedDetail = pageDetail.Get(x=>x.PageId== selectedPage.Id && x.LangId == 3);
            PageModel model = new PageModel();
            model.Content = selectedDetail.Content;
            model.ImageId = selectedDetail.ImageId;
            if (model.ImageId !=null && model.ImageId != 0)
            {

                var url = (image.Get(x => x.Id == model.ImageId).ImageUrl).Split("\\");

                for (var i =0; i<url.Length; i++)
                {
                    if (i!=url.Length-1)
                    {
                        model.ImageUrl += url[i] + "/";
                    }
                    else
                    {
                        model.ImageUrl += url[i];
                    }
                
                }

            }
            model.IsIndexable = selectedPage.IsIndexable;
            model.LangId = selectedDetail.LangId;
            model.PageDetailId = selectedDetail.Id;
            model.PageId = selectedPage.Id;
            model.PageName = selectedPage.PageName;
            model.SeoKeywords = selectedDetail.SeoKeywords;
            model.SubTitle = selectedDetail.SubTitle;
            model.Title = selectedDetail.Title;
            return model;
        }

        public PagesModel GeneratePagesModel()
        {
            var selectedPage = page.GetAll().Take(10);
            PagesModel modelList = new PagesModel();
            foreach (var item in selectedPage)
            {
               
                    var selectedDetail = pageDetail.Get(x => x.PageId == item.Id && x.LangId == 3);
                if (selectedDetail.Title !=null && selectedDetail.Title != "")
                {
                    PageModel model = new PageModel();
                    model.Content = selectedDetail.Content;
                    model.ImageId = selectedDetail.ImageId;
                    if (model.ImageId != null && model.ImageId != 0)
                    {

                        model.ImageUrl = image.Get(x => x.Id == model.ImageId).ImageUrl;
                    }
                    model.IsIndexable = item.IsIndexable;
                    model.LangId = selectedDetail.LangId;
                    model.PageDetailId = selectedDetail.Id;
                    model.PageId = item.Id;
                    model.PageName = item.PageName;
                    model.SeoKeywords = selectedDetail.SeoKeywords;
                    model.SubTitle = selectedDetail.SubTitle;
                    model.Title = selectedDetail.Title;
                    var slug = slugRepo.Get(x => x.EntityId == item.Id && x.ControllerName == "MultiPage" && x.EntityName == "Pages");
                    if (slug != null)
                    {
                        model.Url = "/" + slug.ControllerName + "/" + slug.EntityName + "/" + slug.SlugName;
                    }

                    modelList.pageList.Add(model);
                }
                    
                
               
            }
          
            return modelList;
        }

        public MultiPageCategoriesModel GenerateCategoriesModel()
        {
            MultiPageCategoriesModel pageModel = new MultiPageCategoriesModel();
            pageModel.CategoryName = "Kategoriler";
            var selectedTypes = pageType.GetAll();
            List<PageModel> modelList = new List<PageModel>();
            foreach (var item in selectedTypes)
            {
                
               
                PageModel model = new PageModel();
               model.PageName = item.PageName;
                model.SubTitle = "(" + page.GetMany(x => x.TypeId == item.Id).Count() + ") sayfa mevcut";
                model.Url = slug.Get(x=>x.EntityName== "Categories"&& x.ControllerName == "MultiPage" && x.EntityId ==  item.Id).SlugName;
                modelList.Add(model);
            }
            pageModel.pages = modelList;
            return pageModel;
        }

        public MultiPageCategoryModel GenerateCategoryModel(int id)
        {
            MultiPageCategoryModel model = new MultiPageCategoryModel();
            var selectedPage = page.GetMany(x=>x.TypeId==id);
          //  var selectedPages = pageDetail.GetMany(x => x.PageId == selectedPage.Id && x.LangId == 3);
            model.pages = new List<PageModel>();
            var selectedTypes = pageType.GetAll();
            List<PageModel> categoryList = new List<PageModel>();
            foreach (var item in selectedTypes)
            {


                PageModel category = new PageModel();
                category.PageName = item.PageName;
                category.SubTitle = "(" + page.GetMany(x => x.TypeId == item.Id).Count() + ") sayfa mevcut";
                category.Url = slug.Get(x => x.EntityName == "Categories" && x.ControllerName == "MultiPage" && x.EntityId == item.Id).SlugName;
                categoryList.Add(category);
            }
            model.categories = categoryList;
            foreach (var item in selectedPage)
            {

                var selectedDetail = pageDetail.Get(x => x.PageId == item.Id && x.LangId == 3);
                PageModel pageModel = new PageModel();
                pageModel.Content = selectedDetail.Content;
                pageModel.ImageId = selectedDetail.ImageId;
                if (pageModel.ImageId != null && pageModel.ImageId != 0)
                {

                    pageModel.ImageUrl = image.Get(x => x.Id == pageModel.ImageId).ImageUrl;
                }
                pageModel.LangId = selectedDetail.LangId;
                pageModel.PageDetailId = selectedDetail.Id;
                pageModel.PageId = item.Id;
                pageModel.PageName = item.PageName;
                pageModel.SeoKeywords = selectedDetail.SeoKeywords;
                pageModel.SubTitle = selectedDetail.SubTitle;
                var pageSlug = slug.Get(x=>x.EntityId== item.Id && x.EntityName == "Pages");
                pageModel.Url = pageSlug.SlugName;
                model.pages.Add(pageModel);
            }
            return model;
        }

    }
}