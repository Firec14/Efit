using EfitWeb1.Controllers;
using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EfitWeb1.Attributes
{
     public class AdminModAttribute : ActionFilterAttribute
     {
          public override void OnActionExecuting(ActionExecutingContext filterContext)
          {
               var authCookie = filterContext.HttpContext.Request.Cookies["X-KEY"];

               if (authCookie != null)
               {
                    string authToken = authCookie.Value;
                    var login = new LoginController();
                    var currentUser = login.GetUserDetails(authToken);

                    if (currentUser == null || currentUser.Level != URoles.Admin)
                    {
                         filterContext.Result = new RedirectToRouteResult(
                             new RouteValueDictionary
                             {
                            { "controller", "Login" },
                            { "action", "Login" },
                            { "errorMessage", "You are not authorized to access this page." }
                             });
                         return;
                    }
               }
               else
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
