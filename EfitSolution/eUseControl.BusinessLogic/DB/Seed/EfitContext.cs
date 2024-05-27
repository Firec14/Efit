using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.DB
{
     public class EfitContext : DbContext
     {
          public EfitContext() : base("EfitDB")
          {
               Database.SetInitializer(new CreateDatabaseIfNotExists<EfitContext>());
          }
          public virtual DbSet<UDBTable> Users { get; set; }
     }
}
