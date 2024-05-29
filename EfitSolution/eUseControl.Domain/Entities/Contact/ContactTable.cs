using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Contact
{
     public class ContactTable
     {
          [Key]
          public int Id { get; set; }

          [Required]
          [StringLength(100)]
          public string Name { get; set; }

          [Required]
          [StringLength(100)]
          [EmailAddress]
          public string Email { get; set; }

          [Required]
          [StringLength(15)]
          public string Phone { get; set; }

          [Required]
          [StringLength(500)]
          public string Message { get; set; }

          public DateTime SubmittedAt { get; set; } = DateTime.Now;
     }
}
