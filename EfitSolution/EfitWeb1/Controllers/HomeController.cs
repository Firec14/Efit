using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Mvc;
using EfitWeb1.Models;
using eUseControl.BusinessLogic.DB.Seed;
using eUseControl.Domain.Entities.Contact;

namespace EfitWeb1.Controllers
{
     public class HomeController : Controller
     {
          private readonly ContactContext _contactContext;

          public HomeController()
          {
               _contactContext = new ContactContext();
          }

          public ActionResult Index()
          {
               return View();
          }

          public ActionResult About()
          {
               ViewBag.Message = "Your application description page.";
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> Submit(ContactTable contact)
          {
               if (!ModelState.IsValid)
               {
                    // Log ModelState errors
                    foreach (var state in ModelState)
                    {
                         foreach (var error in state.Value.Errors)
                         {
                              Debug.WriteLine($"Error in {state.Key}: {error.ErrorMessage}");
                         }
                    }

                    // Return the view with the current user object to display validation messages
                    return View("Index", contact);
               }

               try
               {
                    _contactContext.Contacts.Add(contact);
                    await _contactContext.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Form submission successful!";
                    return RedirectToAction("Index");
               }
               catch (Exception ex)
               {
                    // Log the exception
                    Debug.WriteLine($"Exception: {ex.Message}");
                    TempData["ErrorMessage"] = "Error sending message! Please try again.";
                    return RedirectToAction("Index");
               }
          }
     }
}
