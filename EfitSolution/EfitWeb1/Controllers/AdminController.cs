using EfitWeb1.Attributes;
using EfitWeb1.Models;
using eUseControl.BusinessLogic;
using eUseControl.BusinessLogic.DB;
using eUseControl.BusinessLogic.DB.Seed;
using eUseControl.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EfitWeb1.Controllers
{
     [AdminMod]
     public class AdminController : Controller
     {
          private readonly IAdmin _admin;
          private readonly EfitContext _user;
          private readonly ProductContext _product;

          public AdminController()
          {
               var bl = new BusinessLogic();
               _admin = bl.GetAdminBL();
               _user = new EfitContext();
               _product = new ProductContext();
          }
          // GET: Admin
          public ActionResult AdminPage()
          {
               var users = _user.Users.ToList();
               var products = _product.Products.ToList();
               var model = new AdminModel
               {
                    Users = users,
                    Products = products
               };
               return View(model);
          }


     }
}