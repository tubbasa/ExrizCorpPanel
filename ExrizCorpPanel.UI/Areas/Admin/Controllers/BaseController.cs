using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExrizCorpPanel.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExrizCorpPanel.UI.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        ILanguageRepository _language;
    
        public BaseController(ILanguageRepository language)
        {
            this._language = language;
            var list = _language.GetAll();
          
        }
        
    }
}