using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EfitWeb1.Models
{
     public class ProductDataModel
     {
          public int ProductId { get; set; }
          public string Name { get; set; }
          public string Description { get; set; }
          public string ProductCategory { get; set; }
          public decimal ProductPrice { get; set; }
          public ProductRoles Level { get; set; }
     }
}