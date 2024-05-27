using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.User;
using System.Collections.Generic;

namespace eUseControl.BusinessLogic
{
     public class AdminBL : AdminApi, IAdmin
     {
          public List<UDBTable> GetAllUsers()
          {
               return GetAllUsersFromDatabase();
          }

          public void UpdateUser(UDBTable user)
          {
               UpdateUserInDatabase(user);
          }

          public void DeleteUser(int userId)
          {
               DeleteUserFromDatabase(userId);
          }
     }
}
