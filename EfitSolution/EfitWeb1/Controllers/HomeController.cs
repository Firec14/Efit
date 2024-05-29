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
               if (ModelState.IsValid)
               {
                    _contactContext.Contacts.Add(contact);
                    await _contactContext.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Form submission successful!";
                    return RedirectToAction("Index");
               }

               TempData["ErrorMessage"] = "Error sending message! Please try again.";
               return RedirectToAction("Index");
          }
     }
}
