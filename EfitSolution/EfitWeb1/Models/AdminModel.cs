using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EfitWeb1.Models
{
     public class AdminModel
     {
          public List<UDBTable> Users { get; set; }
          public List<ProductTable> Products { get; set; }
     }
}