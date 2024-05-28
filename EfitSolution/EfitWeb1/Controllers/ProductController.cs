using eUseControl.BusinessLogic.DB;
using eUseControl.BusinessLogic.DB.Seed;
using eUseControl.Domain.Entities.Product;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EfitWeb1.Controllers
{
     public class ProductController : Controller
     {
          private readonly ProductContext _product;

          public ProductController(ProductContext product)
          {
               _product = product ?? throw new ArgumentNullException(nameof(product));
          }

          // GET: Product
          public async Task<ActionResult> ProductPage()
          {
               var products = await _product.Products.ToListAsync();
               return View(products);
          }
          public ActionResult Create()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> Create(ProductTable product)
          {
               if (ModelState.IsValid)
               {
                    _product.Products.Add(product);
                    await _product.SaveChangesAsync();
                    return RedirectToAction(nameof(ProductPage));
               }
               return View(product);
          }

          public async Task<ActionResult> Edit(int? id)
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
          public async Task<ActionResult> Edit(int id, ProductTable product)
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
                         existingProduct.ProductDescription = product.ProductDescription;
                         existingProduct.ProductPrice = product.ProductPrice;

                         // Mark the product as modified
                         _product.Entry(existingProduct).State = EntityState.Modified;

                         // Save the changes to the database
                         await _product.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
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
                    return RedirectToAction(nameof(ProductPage));
               }
               return View(product);
          }
          public async Task<ActionResult> Delete(int? id)
          {
               if (id == null)
               {
                    return HttpNotFound();
               }

               var product = await _product.Products
                   .FirstOrDefaultAsync(m => m.ProductId == id);
               if (product == null)
               {
                    return HttpNotFound();
               }

               return View(product);
          }

          [HttpPost, ActionName("Delete")]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> DeleteConfirmed(int id)
          {
               var product = await _product.Products.FindAsync(id);
               _product.Products.Remove(product);
               await _product.SaveChangesAsync();
               return RedirectToAction(nameof(ProductPage));
          }


     }
}