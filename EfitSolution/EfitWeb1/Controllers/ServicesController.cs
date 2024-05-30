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

          public ActionResult Buy(int productId)
          {
               var selectedProducts = Session["SelectedProducts"] as List<int> ?? new List<int>();
               selectedProducts.Add(productId);
               Session["SelectedProducts"] = selectedProducts;

               return RedirectToAction("Services");
          }


          public ActionResult ConfirmationPage(int productId)
          {
               var selectedProducts = Session["SelectedProducts"] as List<int>;
               if (selectedProducts == null || !selectedProducts.Any())
               {
                    return RedirectToAction("Services");
               }

               var products = _product.Products.Where(p => selectedProducts.Contains(p.ProductId)).ToList();

               return View(products);
          }

          [HttpPost]
          public ActionResult CompletePurchase()
          {
               var selectedProducts = Session["SelectedProducts"] as List<int>;
               if (selectedProducts == null || !selectedProducts.Any())
               {
                    return RedirectToAction("Services");
               }

               Session.Remove("SelectedProducts");

               return RedirectToAction("Services");
          }


     }
}