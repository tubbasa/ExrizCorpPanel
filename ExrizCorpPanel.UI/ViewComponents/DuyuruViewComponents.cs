using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.ViewComponents
{
    [ViewComponent]
    public class DuyuruViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var list = new List<string> { "adfasd", "adsfasfdsf", "dfadsfadsf" };
            return View("~/Views/Decor/Home/Components/Duyuru/Default.cshtml",list);
        }
    }
}
