using EfitWeb1.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EfitWeb1.Controllers
{
     [TrainerMod]
    public class TrainerController : Controller
    {
        // GET: Trainer
        public ActionResult TrainerPage()
        {
            return View();
        }
    }
}