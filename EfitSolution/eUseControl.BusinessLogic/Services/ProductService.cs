using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Services
{
     public class ProductService : ProductApi, IProduct
     {
          public List<ProductTable> GetProductList()
          {
               return AllProducts();
          }
          public ProductTable GetProductById(int id)
          {
               return ProductById(id);
          }
          public ProductTable GetProductByName(string productName)
          {
               return ProductByName(productName);
          }
          public ServiceResponse ValidateEditProduct(ProductTable product)
          {
               return ReturnEditProductStatus(product);
          }
          public ServiceResponse ValidateCreateProduct(ProductData product)
          {
               return ReturnCreateProductStatus(product);
          }
     }
}
