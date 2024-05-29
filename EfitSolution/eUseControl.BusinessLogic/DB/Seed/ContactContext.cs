using eUseControl.Domain.Entities.Contact;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.DB.Seed
{
     public class ContactContext : DbContext
     {
          public ContactContext() : base("EfitDB")
          {
               Database.SetInitializer(new CreateDatabaseIfNotExists<ContactContext>());
          }

          public DbSet<ContactTable> Contacts { get; set; }
     }
}
