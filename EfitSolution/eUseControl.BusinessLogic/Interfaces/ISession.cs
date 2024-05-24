using Domain.Entities.User;
using eUseControl.Domain.Entities.User;
using System.Web;


namespace eUseControl.BusinessLogic.Interfaces
{
     public interface ISession
     {
          ULoginResp UserLogin(ULoginData uLoginData);

          ULoginResp UserRegister(URegisterData URegisterData);

          HttpCookie GenCookie(string loginCredential);

          UserMini GetUserByCookie(string apiCookieValue);

     }
}
