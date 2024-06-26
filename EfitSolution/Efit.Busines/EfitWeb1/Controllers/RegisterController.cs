﻿using EfitWeb1.Models;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.BusinessLogic;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EfitWeb1.Controllers
{
     public class RegisterController : Controller
     {
          private readonly ISession _session;
          public RegisterController()
          {
               var bl = new BussinessLogic();
               _session = bl.GetSessionBL();
          }
          // GET: Register
          public ActionResult Index()
          {
               return View();
          }

          [HttpPost]
          public ActionResult Index(UserRegister register)
          {
               if (ModelState.IsValid)
               {
                    URegisterData data = new URegisterData()
                    {
                         Username = register.Username,
                         Password = register.Password,
                         Email = register.Email
                    };
                    var userRegister = _session.UserRegistration(data);
                    return View();
               }
               return View();
          }
     }
}