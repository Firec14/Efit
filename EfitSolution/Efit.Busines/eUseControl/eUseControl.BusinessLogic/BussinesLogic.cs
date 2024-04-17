using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic
{
     public interface ISession
     {

     }

     public class SessionBL : UserApi, ISession
     {

     }

     public class BusinessLogic
     {
          
          public ISession GetSessionBL()
          {
               return new SessionBL();
          }
     }

     public class UserApi
     {
          
     }

     public class LoginController : Controller
     {
          private readonly ISession _session;
          public LoginController()
          {
               var bl = new BusinessLogic();
               _session = bl.GetSessionBL();
          }
          //GET login
          public ActionResult Index()
          {
               return View();
          }

          private ActionResult View()
          {
               throw new NotImplementedException();
          }
     }
     
}
