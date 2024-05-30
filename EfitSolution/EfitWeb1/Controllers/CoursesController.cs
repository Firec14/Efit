using EfitWeb1.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EfitWeb1.Controllers
{
     [UserMod]
     public class CoursesController : Controller
     {
          public ActionResult Courses()
          {
               return View();
          }
          public ActionResult NOT()
          {
               return View();
          }
          public ActionResult BeginnerCo()
          {
               return View();
          }
     }

}