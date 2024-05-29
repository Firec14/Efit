using EfitWeb1.Attributes;
using eUseControl.BusinessLogic.DB.Seed;
using eUseControl.Domain.Entities.Product;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

          // GET: Trainer/EditProduct/{id}
          public async Task<ActionResult> EditProduct(int? id)
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

                    return RedirectToAction(nameof(TrainerPage));
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

          public ActionResult TrainerPage()
          {
               var products = _product.Products.ToList();
               return View(products);
          }

          public async Task<ActionResult> DeleteProduct(int? id)
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

          [HttpPost, ActionName("DeleteProduct")]
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

               return RedirectToAction(nameof(TrainerPage));
          }
     }
}
