using eUseControl.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.DB.Seed
{
     public class ProductContext : DbContext
     {
          public ProductContext() : base("EfitDB")
          {
               Database.SetInitializer(new CreateDatabaseIfNotExists<ProductContext>());
          }

          public virtual DbSet<ProductTable> Products { get; set; }
     }
}
