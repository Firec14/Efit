using EfitWeb1.Attributes;
using eUseControl.BusinessLogic.DB;
using eUseControl.BusinessLogic.DB.Seed;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EfitWeb1.Controllers
{
     [TrainerMod]
     public class TrainerController : Controller
     {
          private readonly ProductContext _product;

          public TrainerController()
          {
               _product = new ProductContext();
          }

          public ActionResult TrainerPage()
          {
               var products = _product.Products.ToList();
               return View(products);
          }

          public ActionResult CreateProduct()
          {
               return View();
          }

          // POST: Trainer/CreateProduct
          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> CreateProduct(ProductTable product)
          {
               if (ModelState.IsValid)
               {
                    _product.Products.Add(product);
                    await _product.SaveChangesAsync();
                    return RedirectToAction("TrainerPage");
               }
               return View(product);
          }
          public async Task<ActionResult> EditProduct(int? id)
          {
               if (id == null)
               {
                    return HttpNotFound();
               }

               var product = await _product.Products.FindAsync(id);
               if (product == null)
               {
                    return HttpNotFound();
               }
               return View(product);
          }

          private bool ProductExists(int id)
          {
               return _product.Products.Any(p => p.ProductId == id);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> UpdateProduct(ProductTable product)
          {
               if (ModelState.IsValid)
               {
                    _product.Entry(product).State = EntityState.Modified;
                    await _product.SaveChangesAsync();
                    return RedirectToAction(nameof(TrainerPage));
               }
               return View(product);
          }


          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> EditProduct(int id, ProductTable product)
          {
               if (id != product.ProductId)
               {
                    return HttpNotFound();
               }

               if (ModelState.IsValid)
               {
                    try
                    {
                         // Retrieve the existing product from the database
                         var existingProduct = await _product.Products.FindAsync(id);
                         if (existingProduct == null)
                         {
                              return HttpNotFound();
                         }

                         // Update the properties of the existing product
                         existingProduct.Name = product.Name;
                         existingProduct.Description = product.Description;
                         existingProduct.ProductCategory = product.ProductCategory;
                         existingProduct.ProductPrice = product.ProductPrice;

                         // Mark the product as modified
                         _product.Entry(existingProduct).State = EntityState.Modified;

                         // Save the changes to the database
                         await _product.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                         // Reload the product from the database
                         var reloadedProduct = await _product.Products.FindAsync(id);

                         // Merge the changes
                         if (reloadedProduct != null)
                         {
                              reloadedProduct.Name = product.Name;
                              reloadedProduct.Description = product.Description;
                              reloadedProduct.ProductCategory = product.ProductCategory;
                              reloadedProduct.ProductPrice = product.ProductPrice;

                              // Mark the product as modified
                              _product.Entry(reloadedProduct).State = EntityState.Modified;

                              // Save the changes to the database
                              await _product.SaveChangesAsync();
                         }
                         else
                         {
                              if (!ProductExists(product.ProductId))
                              {
                                   return HttpNotFound();
                              }
                              else
                              {
                                   throw;
                              }
                         }
                    }
                    return RedirectToAction(nameof(TrainerPage), new { id = product.ProductId });
               }
               return View(product);
          }

          public async Task<ActionResult> DeleteProduct(int? id)
          {
               if (id == null)
               {
                    return HttpNotFound();
               }

               var product = await _product.Products
                  .FirstOrDefaultAsync(m => m.ProductId == id);
               if (product == null)
               {
                    return HttpNotFound("Product not found");
               }

               return View(product);
          }

          [HttpPost, ActionName("DeleteProduct")]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> DeleteProduct(int id)
          {
               try
               {
                    var product = await _product.Products.FindAsync(id);
                    _product.Products.Remove(product);
                    await _product.SaveChangesAsync();
               }
               catch (Exception ex)
               {
                    // Log the exception
                    Console.WriteLine(ex.Message);
                    return HttpNotFound("Failed to delete product");
               }

               return RedirectToAction(nameof(TrainerPage));
          }
     }
}