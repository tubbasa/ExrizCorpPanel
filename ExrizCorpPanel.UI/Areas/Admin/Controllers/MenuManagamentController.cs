using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.DisplayTypes;
using ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExrizCorpPanel.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class MenuManagamentController : Controller
    {
        IMenuRepository _menu;
        IMenuItemsRepository _menuItems;
        ILanguageRepository _lang;
        IPageDetailRepository _page;
        IBlogPostRepository _blogpost;
        ISlugRepository _slug;

        public MenuManagamentController(IMenuRepository menu, ILanguageRepository language, IPageDetailRepository page, IBlogPostRepository blogpost, IMenuItemsRepository menuItems, ISlugRepository slug)
        {
            _menu = menu;
            _lang = language;
            _page = page;
            _blogpost = blogpost;
            _menuItems = menuItems;
            _slug = slug;
        }
        #region InsertMenu
        public IActionResult AddMenu()
        {
            MenuRequest model = new MenuRequest();
            model.IsForMob = false;
            model.IsForWeb = false;
            return View(model);
        }

        [HttpPost]
        public ActionResult postaddMenu(MenuRequest menu)
        {
            Menu newMenu = new Menu();
            newMenu.Id = menu.Id;
            newMenu.IsActive = menu.IsActive;
            newMenu.IsForMob = menu.IsForMob;
            newMenu.IsForWeb = menu.IsForWeb;
            newMenu.Name = menu.Name;
            _menu.Insert(newMenu);
            _menu.Save();
            TempData["result"] = 1;
            return RedirectToAction("AddMenu");
        }


        #endregion

        #region UpdateMenu
        public IActionResult UpdateMenu(int id)
        {
            var menu = _menu.GetById(id);
            return View(menu);
        }

        [HttpPost]
        public int postupdateMenu([FromBody] Menu menu)
        {

            try
            {
                var selected = _menu.GetById(menu.Id);
                selected.Name = menu.Name;
                selected.Id = menu.Id;
                return 1;
            }
            catch (Exception)
            {
                return 2;

                throw;
            }
        }
        #endregion

        #region GetMenu
        public IActionResult GetMenuById(int id)
        {
            var selected = _menu.GetById(id);
            return View(selected);
        }


        public IActionResult GetMenu()
        {
            var list = _menu.GetAll();
            //dil idsi query string olarak gönderilecek ve kontorl edilecek
            return View(list);
        }

        #endregion

        #region DeleteMenu

        public IActionResult deleteMenu(int id)
        {
            try
            {
                _menu.Delete(id);
                _menu.Save();
                return RedirectToAction("GetMenu");
            }
            catch (Exception)
            {
                return RedirectToAction("GetMenu");
                throw;
            }
        }


        #endregion

        #region InsertMenuItem
        public IActionResult AddMenuItem()
        {
            //üst menü ana menü mobil menü vs
            var menus = _menu.GetAll();
            var liste = new List<SelectListItem>();
            liste.Add(new SelectListItem { Text = "Lütfen menu seçin", Value = "-1", Selected = true });
            foreach (var item in menus)
            {
                liste.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString(), Selected = false });
            }

            //dil seçimi 
            var languages = _lang.GetAll();
            var language = new List<SelectListItem>();
            language.Add(new SelectListItem { Text = "Lütfen dil seçin", Value = "-1", Selected = true });
            foreach (var item in languages)
            {
                language.Add(new SelectListItem { Text = item.LangName, Value = item.Id.ToString(), Selected = false });
            }

            //menu url
            var menuItems = new List<SelectListItem>();
            menuItems.Add(new SelectListItem { Text = "Lütfen menü'nün gideceği yeri seçin", Value = "-1", Selected = true });
            //menuItems.Add(new SelectListItem { Text = "--Sayfalar--", Value = "-1", Selected = false });
            //var pages = _page.GetAll();
            //foreach (var item in pages)
            //{
            //    menuItems.Add(new SelectListItem { Text = item.Title, Value = "s-"+ item.Id, Selected = false });
            //}
            //menuItems.Add(new SelectListItem { Text = "--Blog Gönderileri--", Value = "-1", Selected = false });
            //var blogposts = _blogpost.GetAll();
            //foreach (var item in blogposts)
            //{
            //    menuItems.Add(new SelectListItem { Text = item.Title, Value ="b-"+item.Id, Selected = false });
            //}
            var sluglist = _slug.GetAll();
            foreach (var item in sluglist)
            {
                menuItems.Add(new SelectListItem { Text = item.ControllerName+"-"+item.EntityTitle+"-"+item.EntityName, Value = item.Id.ToString(), Selected = false });
            }
            var mList = _menuItems.GetAll();
            var menuList = new List<SelectListItem>();
            menuList.Add(new SelectListItem { Text = "Üst Menü Seçiniz", Value = "-1", Selected = true });
            //foreach (var item in mList)
            //{
            //    menuList.Add(new SelectListItem { Text = item.Title, Value = item.Id.ToString(), Selected = false });
            //}

            menuList.AddRange(_menuItems.GetAll().Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString(), Selected = false }).ToList());
          
            TempData["lang"] = language;
            TempData["menuItems"] = menuItems;
            TempData["menus"] = liste;
            TempData["parentMenu"] = menuList;
            return View();
        }

        [HttpPost]
        public int postaddMenuItem(MenuItemsRequest menu)
        {

            //kodlar
            //-1 dil girilmemiş
            
            try
            {
        
                MenuItems menuItem = new MenuItems();
                menuItem.IsIndexable = menu.IsIndexable;
                menuItem.Keys = menu.Keys;
                if (menu.LangId!=-1)
                {
                    menuItem.LangId = menu.LangId;
                }
                else
                {
                    return -1; //dil girilmemiş
                }
                menuItem.MenuId = menu.MenuId;
                menuItem.RawNumber = menu.RawNumber;
                if (menu.RelatedMenuId!=-1)
                {
                    menuItem.RelatedMenuItemId = menu.RelatedMenuId;
                }
                menuItem.Title = menu.Title;
                menuItem.UrlId = menu.UrlId;
                _menuItems.Insert(menuItem);
                _menuItems.Save();
                return 1;
            }
            catch (Exception)
            {
                return 2;
            }

        }


        #endregion

        #region InsertGlobalUrl
        public IActionResult AddGlobalUrl()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGlobalUrl([FromForm] GlobalUrls urls)
        {
            try
            {
                _slug.Insert(urls);
                _slug.Save();
                return RedirectToAction("GetGlobalUrl");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion

        #region UpdateGlobalUrl
        public IActionResult UpdateGlobalUrl(int id)
        {
            var slug = _slug.GetById(id);
            return View(slug);
        }

        [HttpPost]
        public IActionResult UpdateGlobalUrl([FromForm] GlobalUrls urls)
        {
            _slug.Update(urls);
            _slug.Save();
            return RedirectToAction("GetGlobalUrl");
        }

        #endregion

        #region GetGlobalUrl
        public IActionResult GetGlobalUrl()
        {
            var list = _slug.GetAll();
            return View(list.ToList());
        }



        #endregion

        #region DeleteGlobalUrl
        public IActionResult DeleteGlobalUrl(int id)
        {
            _slug.Delete(id);
            _slug.Save();
            return RedirectToAction("GetGlobalUrl"); 
        }



        #endregion

        #region UpdateMenuItem
        public IActionResult UpdateMenuItem(int id)
        {
            var menus = _menu.GetAll();
            var liste = new List<SelectListItem>();
            liste.Add(new SelectListItem { Text = "Lütfen menu seçin", Value = "-1", Selected = true });
            foreach (var item in menus)
            {
                liste.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString(), Selected = false });
            }

            var languages = _lang.GetAll();
            var language = new List<SelectListItem>();
            language.Add(new SelectListItem { Text = "Lütfen dil seçin", Value = "-1", Selected = true });
            foreach (var item in languages)
            {
                language.Add(new SelectListItem { Text = item.LangName, Value = item.Id.ToString(), Selected = false });
            }
            var menuItems = new List<SelectListItem>();
            menuItems.Add(new SelectListItem { Text = "Lütfen menü'nün gideceği yeri seçin", Value = "-1", Selected = true });
            var sluglist = _slug.GetAll();
            foreach (var item in sluglist)
            {
                menuItems.Add(new SelectListItem { Text = item.EntityTitle + "-" + item.EntityName, Value = item.Id.ToString(), Selected = false });
            }
            //foreach (var item in pages)
            //{
            //    menuItems.Add(new SelectListItem { Text = item.Title, Value = item.Id.ToString(), Selected = false });
            //}
            //menuItems.Add(new SelectListItem { Text = "--Blog Gönderileri--", Value = "-1", Selected = false });
            //var blogposts = _blogpost.GetAll();
            //foreach (var item in blogposts)
            //{
            //    menuItems.Add(new SelectListItem { Text = item.Title, Value = item.SlugName, Selected = false });
            //}

         
        
            var selected = _menuItems.GetById(id);
            var releatedmenus = _menuItems.GetMany(x => x.MenuId ==selected.MenuId);
            var releatedList= new List<SelectListItem>();
            foreach (var item in releatedmenus)
            {
                releatedList.Add(new SelectListItem { Text =item.Title,Value=item.Id.ToString(), Selected=false });
            }


            var newMenuItem = new MenuItemsRequest();
            newMenuItem.Id = selected.Id;
            newMenuItem.IsIndexable = selected.IsIndexable;
            newMenuItem.Keys = selected.Keys;
            newMenuItem.LangId = selected.LangId;
            newMenuItem.MenuId = selected.MenuId;
            newMenuItem.RawNumber = selected.RawNumber;
            newMenuItem.RelatedMenuId = selected.RelatedMenuItemId;
            newMenuItem.Title = selected.Title;
            newMenuItem.UrlId = selected.UrlId;
            TempData["lang"] = language;
            TempData["menuItems"] = menuItems;
            TempData["menus"] = liste;
            TempData["releated"] = releatedList;
            return View(newMenuItem);
        }

        [HttpPost]
        public IActionResult postupdateMenuItem(MenuItemsRequest menuItems)
        {
            var selected = _menuItems.GetById(menuItems.Id);
            selected.IsIndexable = menuItems.IsIndexable;
            selected.Keys = menuItems.Keys;
            selected.LangId = menuItems.LangId;
            selected.MenuId = menuItems.MenuId;
            selected.RawNumber = menuItems.RawNumber;
            if (menuItems.RelatedMenuId!=-1)
            {
                selected.RelatedMenuItemId = menuItems.RelatedMenuId;
            }
            selected.Title = menuItems.Title;
            selected.UrlId = menuItems.UrlId;
            selected.Id = menuItems.Id;
            _menuItems.Update(selected);
            _menuItems.Save();
            return View();
        }
        #endregion

        #region GetMenuItem
        public IActionResult GetMenuItemById(int id)
        {
            var selected = _menuItems.GetById(id);
            return View(selected);
        }


        public IActionResult GetMenuItem()
        {
            var list = _menuItems.GetAll().Select(x=>new DisplayMenuItem {Id=x.Id,IsIndexable=x.IsIndexable,Keys=x.Keys,LangId=x.LangId,RawNumber=x.RawNumber,Title=x.Title,UrlId=x.UrlId.ToString(),RelatedMenuId=x.RelatedMenuItemId }).ToList();
            foreach (var item in list)
            {
                string menudividedstr = "";
                if (item.RelatedMenuId!=0&&item.RelatedMenuId!=null)
                {
                    menudividedstr = getMenuItemDivide(Convert.ToInt32(item.Id));
                }
                else
                {
                    menudividedstr = "-";
                }
                item.RelatedMenu = menudividedstr;
                if (item.UrlId =="0" )
                {
                    item.Url = "/";
                }
                else
                {
                    item.Url = _slug.GetById(Convert.ToInt32(item.UrlId)).SlugName;
                }
               
                item.Language = _lang.GetById(Convert.ToInt32(item.LangId)).LangName;
            }
       
            
            //dil idsi query string olarak gönderilecek ve kontorl edilecek
            return View(list);
        }

        #endregion

        #region DeleteMenuItem

        public IActionResult deleteMenuItem(int id)
        {
             _menuItems.Delete(id);
            _menuItems.Save();
            return RedirectToAction("GetMenuItem");
        }


        #endregion

        #region OtherActions

        public IActionResult countLanguage()
        {
            //dil idsi query string olarak gönderilecek ve kontorl edilecek
            return View();
        }

        public string getUrlName(int id)
        {
           return _slug.GetById(id).SlugName;
        }

        public string getMenuItemDivide(int id)
        {
            int escape = 0;
            string MenuDivide = "";
            var related = _menuItems.GetById(id).RelatedMenuItemId;
            if (related!=null)
            {
                MenuDivide = _menuItems.GetById(Convert.ToInt32(related)).Title + " > " + _menuItems.GetById(id).Title;
               
                while (escape==0)
                {
                    var relMenu = _menuItems.GetById(Convert.ToInt32(related)).RelatedMenuItemId;
                    if (relMenu!=null)
                    {
                        var menuName = _menuItems.GetById(Convert.ToInt32(relMenu)).Title;
                        MenuDivide= MenuDivide.Insert(0,menuName+" > ");
                    }
                    else
                    {
                        escape = 1;
                    }
                }
            }
            else
            {
                MenuDivide = "-";
            }

            return MenuDivide;
        }

        [HttpGet]
        public List<SelectListItem> getMenuItems( int menuId)
        {
            var list = _menuItems.GetMany(x => x.MenuId == menuId).Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString(), Selected = false }).ToList();
            return list;
        }
        #endregion 
    }
}