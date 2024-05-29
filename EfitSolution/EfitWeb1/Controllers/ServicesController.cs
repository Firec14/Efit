using EfitWeb1.Attributes;
using eUseControl.BusinessLogic.DB.Seed;
using eUseControl.BusinessLogic.Interfaces;
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
        private readonly ProductContext _product;

        // Constructor with dependency injection
        public ServicesController()
        {
               _product = new ProductContext();
        }

          public ActionResult Services()
          {
               var products = _product.Products.ToList();
               return View(products);
          }
     }
}