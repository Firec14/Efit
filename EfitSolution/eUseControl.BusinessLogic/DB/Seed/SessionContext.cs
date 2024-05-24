using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.User;

namespace eUseControl.BusinessLogic.DB
{
     internal class SessionContext : DbContext
     {
          public SessionContext() : base("EFitDB") {
               Database.SetInitializer(new CreateDatabaseIfNotExists<SessionContext>());
          }
          public virtual DbSet<Session> Sessions { get; set;}
     }
}
