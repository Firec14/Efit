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
          [Display(Name = "Name")]
          public string Name { get; set; }

          [Required]
          [StringLength(100)]
          [EmailAddress]
          [Display(Name = "Email")]
          public string Email { get; set; }

          [Required]
          [StringLength(15)]
          [Display(Name = "Phone")]
          public string Phone { get; set; }

          [Required]
          [StringLength(500)]
          [Display(Name = "Message")]
          public string Message { get; set; }

          public DateTime SubmittedAt { get; set; } = DateTime.Now;
     }
}
