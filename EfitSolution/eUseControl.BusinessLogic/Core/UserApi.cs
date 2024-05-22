using System;
using System.Linq;
using System.Web;
using System.Data.Entity;
using eUseControl.BusinessLogic.DB;
using eUseControl.Domain.Entities.User;
using EntityState = System.Data.Entity.EntityState;
using Helpers;
using eUseControl.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.User;
using AutoMapper;

namespace eUseControl.BusinessLogic.Core
{
     public class UserApi
     {
          internal ULoginResp UserLoginLogic(ULoginData data)
          {
               UDBTable result;
               using (var context = new EfitContext())
               {
                    result = context.User.FirstOrDefault(u => u.Name == data.Credential && u.Password == data.Password);
                    if (result == null)
                    {
                         return new ULoginResp { Status = false, StatusMsg = "Wrong email or password" };
                    }
               }
               using (var todo = new EfitContext())
               {
                    result.lastLogin = data.LoginDateTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
               }

               var apiCookie = Cookie(result.Name);

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

                    var session = db.Session.FirstOrDefault(s => s.UserName == loginCredential);

                    if (session != null)
                    {
                         session.CookieString = apiCookie.Value;
                         session.ExpireTime = DateTime.Now.AddMinutes(60);
                         session.UserName = loginCredential; // Store the username in the session
                         db.Entry(session).State = EntityState.Modified;
                    }
                    else
                    {
                         db.Session.Add(new Session
                         {
                              UserName = loginCredential,
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
                    session = db.Session.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
               }

               if (session == null) return null;
               using (var db = new EfitContext())
               {
                    var validate = new EmailAddressAttribute();
                    if (validate.IsValid(session.UserName))
                    {
                         curentUser = db.User.FirstOrDefault(u => u.Email == session.UserName);
                    }
                    else
                    {
                         curentUser = db.User.FirstOrDefault(u => u.Name == session.UserName);
                    }
               }

               if (curentUser == null) return null;
               var config = new MapperConfiguration(cfg => cfg.CreateMap<UDBTable, UserMini>());
               var mapper = config.CreateMapper();
               var usermini = mapper.Map<UserMini>(curentUser);
               return usermini;
          }

          internal URegisterResp UserRegistrationLogic(URegisterData data)
          {
               using (var context = new EfitContext())
               {
                    UDBTable result;
                    result = context.User.FirstOrDefault(u => u.Name == data.Username);
                    if (result != null)
                    {
                         return new URegisterResp { Status = false, StatusMsg = "User with this name already exists" };
                    }

                    var newUser = new UDBTable
                    {
                         Name = data.Username,
                         Password = data.Password,
                         Email = data.Email,
                         level = URoles.User,
                         lastLogin = data.RegisterDateTime
                    };
                    context.User.Add(newUser);
                    context.SaveChanges();
                    return new URegisterResp { Status = true };
               }
          }
     }
}
