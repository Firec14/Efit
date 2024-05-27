using eUseControl.BusinessLogic.DB.Seed;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Core
{
     public class ProductApi
     {
          public struct ProductData
          {
               public string ProductName { get; set; }
               public string BrandName { get; set; }
               public string Description { get; set; }
               public float Price { get; set; }
               public string Category { get; set; }
          }

          public List<ProductTable> AllProducts()
          {
               List<ProductTable> products = null;
               using (var db = new ProductContext())
               {
                    products = db.Products.OrderByDescending(x => x.Name).ToList();
               }
               return products;
          }
          public ProductTable ProductById(int id)
          {
               using (var db = new ProductContext())
               {
                    var currentProduct = db.Products.FirstOrDefault(x => x.ProductId == id);
                    if (currentProduct == null)
                    {
                         return null;
                    }
                    return currentProduct;
               }
          }
          public ProductTable ProductByName(string productName)
          {
               using (var db = new ProductContext())
               {
                    var product = db.Products
                        .FirstOrDefault(p => p.Name.Replace(" ", "") == productName);
                    return product;
               }
          }
          public ServiceResponse ReturnEditProductStatus(ProductTable data)
          {
               var response = new ServiceResponse();
               using (var db = new ProductContext())
               {
                    try
                    {
                         var existingProduct = db.Products.FirstOrDefault(x => x.ProductId == data.ProductId);
                         if (existingProduct == null)
                         {
                              response.StatusMessage = "Product not found";
                              response.Status = false;
                              return response;
                         }

                         var duplicateProduct = db.Products.FirstOrDefault(x => (x.Name == data.Name) && x.ProductId != data.ProductId);
                         if (duplicateProduct != null)
                         {
                              response.StatusMessage = "Product with this name already exists";
                              response.Status = false;
                              return response;
                         }
                         else
                         {
                              existingProduct.Name = data.Name;
                              existingProduct.Description = data.Description;
                              existingProduct.ProductPrice = data.ProductPrice;

                              db.SaveChanges();

                              response.StatusMessage = "Product updated successfully";
                              response.Status = true;
                              return response;
                         }
                    }
                    catch (Exception)
                    {
                         response.StatusMessage = "An error occurred while updating the Product";
                         response.Status = false;
                    }
                    return response;
               }


          }

          public ServiceResponse ReturnCreateProductStatus(ProductData data)
          {
               var response = new ServiceResponse();
               using (var db = new ProductContext())
               {
                    try
                    {
                         var existingProduct = db.Products.FirstOrDefault(x => x.Name == data.ProductName);
                         if (existingProduct != null)
                         {
                              response.StatusMessage = "Product with this name already exists";
                              response.Status = false;
                              return response;
                         }

                         var product = new ProductTable
                         {
                              Name = data.ProductName,
                              Description = data.Description,
                              ProductPrice = (decimal)data.Price,
                         };

                         using (var dbProduct = new ProductContext())
                         {
                              dbProduct.Products.Add(product);
                              dbProduct.SaveChanges();
                         }
                         response.StatusMessage = "Product added successfully";
                         response.Status = true;
                    }
                    catch (Exception)
                    {
                         response.StatusMessage = "An error occurred while adding the product";
                         response.Status = false;
                    }
                    return response;
               }
          }
     }
}
