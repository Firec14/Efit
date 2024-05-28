using EfitWeb1.Models;
using eUseControl.BusinessLogic;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Domain.Entities.User;

namespace EfitWeb1.Controllers
{
     public class LoginController : Controller
     {
          private readonly ISession _session;

          public LoginController()
          {
               var bl = new BusinessLogic();
               _session = bl.GetSessionBL();
          }
          public ActionResult Login()
          {
               return View();
          }
          // GET: Login
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult LogInUser(UserLogin login)
          {
               if (ModelState.IsValid)
               {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<UserLogin, ULoginData>());
                    var mapper = config.CreateMapper();
                    var data = mapper.Map<ULoginData>(login);

                    var userLogin = _session.UserLogin(data);

                    if (userLogin == null)
                    {
                         throw new AuthenticationException("ERROR. No login response!");
                    }

                    if (userLogin.Status)
                    {
                         // Generarea cookie pentru sesiunea actuală
                         HttpCookie cookie = _session.GenCookie(login.Username);
                         ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                         return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                         // Autentificare nereusita
                         ModelState.AddModelError("", userLogin.StatusMsg);
                         return View("Login", login); ;
                    }
               }


               return RedirectToAction("Index", "Home");
          }
          public UserMini GetUserDetails(string authToken)
          {
               return _session.GetUserByCookie(authToken);
          }


     }
}