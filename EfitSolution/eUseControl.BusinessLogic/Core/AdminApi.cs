using eUseControl.BusinessLogic.DB;
using eUseControl.Domain.Entities.User;
using System.Collections.Generic;
using System.Linq;

namespace eUseControl.BusinessLogic.Core
{
     public class AdminApi
     {
          internal List<UDBTable> GetAllUsersFromDatabase()
          {
               List<UDBTable> users;

               using (var db = new EfitContext())
               {
                    users = db.Users.ToList();
               }

               return users;
          }

          internal void UpdateUserInDatabase(UDBTable user)
          {
               using (var db = new EfitContext())
               {
                    var dbu = db.Users.FirstOrDefault(u => u.Name == user.Name);
                    if (dbu == null) return;

                    dbu.Email = user.Email;
                    dbu.level = user.level;

                    db.SaveChanges();
               }
          }

          internal void DeleteUserFromDatabase(int userId)
          {
               using (var db = new EfitContext())
               {
                    var user = db.Users.Find(userId);
                    if (user == null) return;

                    db.Users.Remove(user);
                    db.SaveChanges();
               }
          }

     }
}
