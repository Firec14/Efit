using eUseControl.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUseControl.Domain.Entities.User
{
     public class UDBTable
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int UserId { get; set; }

          [Required]
          [Display(Name = "Username")]
          [StringLength(50,MinimumLength =5, ErrorMessage = "Username cannot be longer than 30 characters")]
          public string Name { get; set; }

          [Required]
          [Display(Name ="Password")]
          [StringLength(50, MinimumLength = 8, ErrorMessage = "Password cannot be shorter than 8 characters")]
          public string Password { get; set; }

          [Required]
          [Display(Name = "email Address")]
          public string Email { get; set; }

          public URoles level { get; set; }
     }
}
