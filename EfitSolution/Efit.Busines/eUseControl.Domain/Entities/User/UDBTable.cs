using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.User
{
     public class UDBTable
     {
          [Key]
          public int UserId { get; set; }
          [Required]
          public string Name { get; set; }
          [Required]
          public string Password { get; set; }
          [Required]
          public string Email { get; set; }
     }
}
