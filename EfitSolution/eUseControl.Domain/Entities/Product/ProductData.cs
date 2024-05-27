using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Product
{
     public class ProductData
     {
          public int ProductId { get; set; }
          public string Name { get; set; }
          public string Description { get; set; }
          public string ProductCategory { get; set;}
          public string ProductDescription { get; set; }
          public decimal ProductPrice { get; set; }
          public ProductRoles Level { get; set; }
     }
}
