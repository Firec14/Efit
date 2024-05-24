using System;
using System.Linq;
using System.Web;
using System.Data.Entity;
using eUseControl.BusinessLogic.DB;
using eUseControl.Domain.Entities.User;
using EntityState = System.Data.Entity.EntityState;
using Helpers;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.User;
using AutoMapper;
using eUseControl.Domain.Enums;

namespace eUseControl.BusinessLogic.Core
{
     public class UserApi
     {
          internal ULoginResp UserLoginLogic(ULoginData data)
          {
               UDBTable user;
               using (var context = new EfitContext())
               {
                    user = context.Users.FirstOrDefault(u => u.Name == data.Username && u.Password == data.Password);
                    if (user == null)
                    {
                         return new ULoginResp { Status = false, StatusMsg = "Wrong email or password" };
                    }
               }
               

               var apiCookie = Cookie(user.Name);

               HttpContext.Current.Response.Cookies.Add(apiCookie);

               return new ULoginResp { Status = true };
          }

          internal HttpCookie Cookie(string loginCredential)
          {
               var apiCookie = new HttpCookie("X-KEY")
               {
                    Value = CookieGenerator.Create(loginCredential)
               };

               using (var db = new SessionContext())
               {
                    // Initialize the database if it doesn't exist
                    Database.SetInitializer<EfitContext>(new CreateDatabaseIfNotExists<EfitContext>());

                    var session = db.Sessions.FirstOrDefault(s => s.Username == loginCredential);

                    if (session != null)
                    {
                         session.CookieString = apiCookie.Value;
                         session.ExpireTime = DateTime.Now.AddMinutes(60);
                         session.Username = loginCredential; // Store the username in the session
                         db.Entry(session).State = EntityState.Modified;
                    }
                    else
                    {
                         db.Sessions.Add(new Session
                         {
                              Username = loginCredential,
                              CookieString = apiCookie.Value,
                              ExpireTime = DateTime.Now.AddMinutes(60)
                         });
                    }

                    db.SaveChanges();
               }

               return apiCookie;
          }

          internal UserMini UserCookie(string cookie)
          {
               Session session;
               UDBTable curentUser;

               using (var db = new SessionContext())
               {
                    session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
               }

               if (session == null) return null;
               using (var db = new EfitContext())
               {
                    var validate = new EmailAddressAttribute();
                    if (validate.IsValid(session.Username))
                    {
                         curentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                    }
                    else
                    {
                         curentUser = db.Users.FirstOrDefault(u => u.Name == session.Username);
                    }
               }

               if (curentUser == null) return null;
               var config = new MapperConfiguration(cfg => cfg.CreateMap<UDBTable, UserMini>());
               var mapper = config.CreateMapper();
               var usermini = mapper.Map<UserMini>(curentUser);
               return usermini;
          }

          internal ULoginResp UserRegistrationLogic(URegisterData data)
          {
               UDBTable user;

               using (var db = new EfitContext())
               {
                    user = db.Users.FirstOrDefault(u => u.Email == data.Email);
               }

               // If the user already exists, return an appropriate response
               if (user != null)
               {
                    return new ULoginResp { Status = false, StatusMsg = "User already exists" };
               }

               // Create a new user
               user = new UDBTable
               {
                    Name = data.Username,
                    Password = data.Password,
                    Email = data.Email,
                    level = URoles.User
               };

               // Save the new user to the database
               using (var db = new EfitContext())
               {
                    Database.SetInitializer<EfitContext>(new CreateDatabaseIfNotExists<EfitContext>());
                    db.Users.Add(user);
                    db.SaveChanges();
               }

               // Succes
               return new ULoginResp { Status = true, StatusMsg = "Utilizatorul a fost înregistrat cu succes." };
          }
          }
}
