using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.UI.Models.CustomModels;
using Microsoft.AspNetCore.Mvc;

namespace ExrizCorpPanel.UI.Controllers
{
    public class AboutController : Controller
    {
        private static IMenuRepository menu;
        private static IMenuItemsRepository menuItems;
        private static ISliderGalleryRepository gallery;
        private static ISliderRepository slider;
        private static IBannerRepository banner;
        private static IReferenceJobRepository job;
        private static IJobLanguageRepository jobresource;
        private static IReferencesRepository reference;
        private static ISiteDetailRepository site;
        private static ISlugRepository slug;
        private static IPageRepository page;
        private static IPageDetailRepository pageDetail;
        private static IThemesRepository theme;
        private static IImageRepository image;
        private static ISocialMediaObjectRepository socialObject;
        private static ISocialMediaTypesRepository socialType;
        private static IStringResourceRepository stringresource;
        private static IResourceVariableRepository resourcevariables;
        private static IAreaDetailRepository areaDetail;
        private static IOnePageAreaRepository area;
        private static IAreaTypeRepository areaType;

        public AboutController(IMenuRepository _menu, ISliderGalleryRepository _gallery, ISliderRepository _slider, IBannerRepository _banner, IReferenceJobRepository _job, IJobLanguageRepository _jobresource, IReferencesRepository _reference, ISiteDetailRepository _site, IMenuItemsRepository _menuItems, ISlugRepository _slug, IThemesRepository _theme, IImageRepository _image, ISocialMediaObjectRepository _socialObject, ISocialMediaTypesRepository _socialType, IStringResourceRepository _stringresource, IResourceVariableRepository _resourcevariables, IAreaDetailRepository _areaDetail, IOnePageAreaRepository _area, IAreaTypeRepository _areaType, IPageRepository _page, IPageDetailRepository _pageDetail)
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
            page = _page;
            pageDetail = _pageDetail;

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
        public LayoutModel generateLayoutModel()
        {
            var menuId = menu.Get(x => x.IsForWeb == true && x.IsActive == true).Id;
            var items = menuItems.GetMany(x => x.MenuId == menuId && x.RelatedMenuItemId == null).Select(x => new ExrizCorpPanel.UI.Models.Components.Menu { Id = x.Id, IsIndexable = x.IsIndexable, Keys = x.Keys, LangId = x.LangId, MenuId = x.MenuId, RawNumber = x.RawNumber, Title = x.Title, Url = slug.GetById(x.UrlId).ControllerName + "/" + slug.GetById(x.UrlId).SlugName }).ToList();
            var test = getRelations(items, Convert.ToInt32(site.GetAll().FirstOrDefault().MenuBranches));
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
        public IActionResult About()
        {
            var Layoutmodel = generateLayoutModel();
            ViewBag.LayoutModel = Layoutmodel;
            //var model = page.Get(x=>x.PageName="aboutUs");

            if (Layoutmodel.siteDetail.ThemeName != null)
            {
                var partialName = "~/Views/" + Layoutmodel.siteDetail.ThemeName + "/About/_About.cshtml";
                return View(partialName, new AboutModel { AboutPage=new Data.Models.DB.PageDetail { Content="içerik"} });
            }
            else
            {
                return View();
            }
        }
    }
}