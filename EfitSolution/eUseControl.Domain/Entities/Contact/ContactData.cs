using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Contact
{
     public class ContactData
     { 
          public int Id { get; set; }
          public string Name { get; set; }
          public string Email { get; set; }
          public string Phone { get; set; }
          public string Message { get; set; }
          public DateTime SubmittedAt { get; set; } = DateTime.Now;
     }

}
