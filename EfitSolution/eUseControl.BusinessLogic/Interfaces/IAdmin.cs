using eUseControl.Domain.Entities.User;
using System.Collections.Generic;

namespace eUseControl.BusinessLogic.Interfaces
{
     public interface IAdmin
     {
          List<UDBTable> GetAllUsers();

          void UpdateUser(UDBTable user);

          void DeleteUser(int userId);

     }
}
