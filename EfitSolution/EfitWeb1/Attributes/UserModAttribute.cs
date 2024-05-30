using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EfitWeb1.Attributes
{
     public class UserModAttribute : ActionFilterAttribute
     {
          public override void OnActionExecuting(ActionExecutingContext filterContext)
          {
               var authCookie = filterContext.HttpContext.Request.Cookies["X-KEY"];

               if (authCookie == null)
               {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                        { "controller", "Login" },
                        { "action", "Login" },
                        { "errorMessage", "Please login first." }
                        });
                    return;
               }

               base.OnActionExecuting(filterContext);
          }
     }
}