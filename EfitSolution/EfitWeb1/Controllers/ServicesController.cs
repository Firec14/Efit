using EfitWeb1.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EfitWeb1.Controllers
{
     [UserMod]
     public class ServicesController : Controller
     {
          // GET: Services
          public ActionResult Services()
          {
               return View();
          }
     }
}