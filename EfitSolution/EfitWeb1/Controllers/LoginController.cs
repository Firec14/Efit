using eUseControl.BusinessLogic;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EfitWeb1.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISession _session;

        public LoginController()
          {
               var bl = new BussinesLogic();
               _session = bl.GetSessionBL();
          }

          // GET: Login
          [HttpPost]
          [ValidateAntiForgeryToken]
        public ActionResult Index(UserLogin login)
        {
               if(ModelState.IsValid)
               {
                    ULoginData data = new ULoginData
                    {
                         Credential = login.Credential,
                         Password = login.Password,
                         LoginIp = Request.UserHostAddress,
                         LoginDataTime = DateTime.Now
                    };

                    var userLogin = _session.Userlogin(data);
                    if (userLogin.Status)
                    {
                         //ADD COOKIE
                         return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                         ModelState.AddModelError("", userLogin.StatusMsg);
                         return View();
                    }
               }
            return View();
        }
    }
}