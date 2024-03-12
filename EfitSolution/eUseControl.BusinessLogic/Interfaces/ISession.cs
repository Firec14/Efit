using eUseControl.BusinessLogic.Core;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Interfaces
{
     public interface ISession
     {
          UserLogin Userlogin(ULoginData data);
     }
}
