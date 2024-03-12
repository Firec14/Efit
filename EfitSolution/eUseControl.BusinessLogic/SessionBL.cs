using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic
{
     public class SessionBL : ISession
     {
          public UserLogin Userlogin(ULoginData data)
          {
               UserLogin userLogin = new UserLogin();
               return userLogin;
          }
     }
}
