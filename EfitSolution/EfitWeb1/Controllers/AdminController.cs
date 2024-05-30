using EfitWeb1.Attributes;
using EfitWeb1.Models;
using eUseControl.BusinessLogic;
using eUseControl.BusinessLogic.DB;
using eUseControl.BusinessLogic.DB.Seed;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using eUseControl.Domain.Entities.User;

namespace EfitWeb1.Controllers
{
     [AdminMod]
     public class AdminController : Controller
     {
          private readonly IAdmin _admin;
          private readonly EfitContext _user;
          private readonly ProductContext _product;

          public AdminController()
          {
               var bl = new BusinessLogic();
               _admin = bl.GetAdminBL();
               _user = new EfitContext();
               _product = new ProductContext();
          }

          // GET: Admin
          public ActionResult AdminPage()
          {
               var users = _user.Users.ToList();
               var products = _product.Products.ToList();
               var model = new AdminModel
               {
                    Users = users,
                    Products = products
               };
               return View(model);
          }

          public async Task<ActionResult> EditProductAdmin(int? id)
          {
               if (id == null)
               {
                    return HttpNotFound();
               }

               var product = await _product.Products.FindAsync(id);
               if (product == null)
               {
                    Debug.WriteLine($"Product with id = {id} not found.");
                    return HttpNotFound();
               }

               Debug.WriteLine($"Product with id = {id} found.");
               return View(product);
          }


          // POST: Trainer/EditProduct/{id}
          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> EditProductAdmin(int id, ProductTable product)
          {
               if (id != product.ProductId)
               {
                    return HttpNotFound();
               }

               if (ModelState.IsValid)
               {
                    try
                    {
                         var existingProduct = await _product.Products.FindAsync(id);
                         if (existingProduct == null)
                         {
                              Debug.WriteLine($"Existing product with id = {id} not found.");
                              return HttpNotFound();
                         }
                         UpdateProduct(existingProduct, product);

                         _product.Entry(existingProduct).State = EntityState.Modified;
                         await _product.SaveChangesAsync();
                         Debug.WriteLine($"Product with id = {id} updated.");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                         if (!ProductExists(product.ProductId))
                         {
                              Debug.WriteLine($"Product with id = {product.ProductId} not found.");
                              return HttpNotFound();
                         }
                         else
                         {
                              throw;
                         }
                    }

                    return RedirectToAction(nameof(AdminPage));
               }

               return View(product);
          }
          private void UpdateProduct(ProductTable existingProduct, ProductTable updatedProduct)
          {
               existingProduct.Name = updatedProduct.Name;
               existingProduct.Description = updatedProduct.Description;
               existingProduct.ProductCategory = updatedProduct.ProductCategory;
               existingProduct.ProductPrice = updatedProduct.ProductPrice;
               existingProduct.Level = updatedProduct.Level;
          }
          private bool ProductExists(int id)
          {
               return _product.Products.Any(e => e.ProductId == id);
          }

          public async Task<ActionResult> DeleteProductAdmin(int? id)
          {
               if (id == null)
               {
                    return HttpNotFound();
               }

               var product = await _product.Products.FirstOrDefaultAsync(m => m.ProductId == id);
               if (product == null)
               {
                    return HttpNotFound("Product not found");
               }

               return View(product);
          }

          [HttpPost, ActionName("DeleteProductAdmin")]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> DeleteProductConfirmed(int id)
          {
               try
               {
                    var product = await _product.Products.FindAsync(id);
                    if (product != null)
                    {
                         _product.Products.Remove(product);
                         await _product.SaveChangesAsync();
                    }
               }
               catch (Exception ex)
               {
                    Debug.WriteLine(ex.Message);
                    return HttpNotFound("Failed to delete product");
               }

               return RedirectToAction(nameof(AdminPage));
          }

          public async Task<ActionResult> EditLevelAdmin(int? id)
          {
               if (id == null)
               {
                    return HttpNotFound();
               }

               var user = await _user.Users.FindAsync(id);
               if (user == null)
               {
                    Debug.WriteLine($"User with id = {id} not found.");
                    return HttpNotFound();
               }

               Debug.WriteLine($"User with id = {id} found.");
               return View(user);
          }


          // POST: Trainer/EditProduct/{id}
          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> EditLevelAdmin(int id, UDBTable user)
          {
               if (id != user.UserId)
               {
                    return HttpNotFound();
               }

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
                    return View(user);
               }

               try
               {
                    var existingUser = await _user.Users.FindAsync(id);
                    if (existingUser == null)
                    {
                         Debug.WriteLine($"Existing User with id = {id} not found.");
                         return HttpNotFound();
                    }

                    UpdateUser(existingUser, user);

                    _user.Entry(existingUser).State = EntityState.Modified;
                    await _user.SaveChangesAsync();
                    Debug.WriteLine($"User with id = {id} updated.");
               }
               catch (DbUpdateConcurrencyException)
               {
                    if (!UserExists(user.UserId))
                    {
                         Debug.WriteLine($"User with id = {user.UserId} not found.");
                         return HttpNotFound();
                    }
                    else
                    {
                         throw;
                    }
               }

               return RedirectToAction(nameof(AdminPage));
          }


          private bool UserExists(int id)
          {
               return _user.Users.Any(e => e.UserId == id);
          }

          private void UpdateUser(UDBTable existingUser, UDBTable updatedUser)
          {
               existingUser.Name = updatedUser.Name;
               existingUser.Email = updatedUser.Email;
               existingUser.Password = updatedUser.Password;
               existingUser.level = updatedUser.level;
          }

          public async Task<ActionResult> DeleteUser(int? id)
          {
               if (id == null)
               {
                    return HttpNotFound();
               }

               var user = await _user.Users.FirstOrDefaultAsync(m => m.UserId == id);
               if (user == null)
               {
                    return HttpNotFound("User not found");
               }

               return View(user);
          }

          [HttpPost, ActionName("DeleteUser")]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> DeleteUserConfirmed(int id)
          {
               try
               {
                    var user = await _user.Users.FindAsync(id);
                    if (user != null)
                    {
                         _user.Users.Remove(user);
                         await _user.SaveChangesAsync();
                    }
                    else
                    {
                         return HttpNotFound("User not found");
                    }
               }
               catch (Exception ex)
               {
                    Debug.WriteLine(ex.Message);
                    return HttpNotFound("Failed to delete user");
               }

               return RedirectToAction(nameof(AdminPage));
          }


     }
}