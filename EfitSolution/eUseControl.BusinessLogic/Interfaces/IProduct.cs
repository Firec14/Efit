using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Interfaces
{
     public interface IProduct
     {
          ProductTable GetProductById(int id);
          ProductTable GetProductByName(string productName);
          List<ProductTable> GetProductList();
          ServiceResponse ValidateEditProduct(ProductTable product);
     }
}
