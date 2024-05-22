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
          public EfitContext() : base("name=Efit")
          {
               Database.SetInitializer(new CreateDatabaseIfNotExists<EfitContext>());
          }
          public virtual DbSet<UDBTable> User { get; set; }
          public virtual DbSet<UDBTable> Sesion { get; set; }
     }
}
