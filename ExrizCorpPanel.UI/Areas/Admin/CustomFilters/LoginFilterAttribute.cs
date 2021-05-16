using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace ExrizCorpPanel.UI.Areas.Admin.CustomFilter
{
    public class LoginFilterAttribute : Attribute, IActionFilter
    {
        private IUserRepository _authrepo = new UserRepository();
        public void OnActionExecuted(ActionExecutedContext context)
        {
          
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var usermail = context.HttpContext.Session.GetString("email");
            var userpassword = context.HttpContext.Session.GetString("password");
            var finded = _authrepo.Get(x => x.Password == userpassword && x.Email == usermail);
            if (finded==null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "area","admin" }, { "controller", "Auth" }, { "action", "Login" } });
            }

        }
    }
}
