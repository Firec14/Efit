using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Product
{
     public class ProductTable
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int ProductId { get; set; }
          [Required]
          [Display(Name = "Name")]
          public string Name { get; set; }
          [Required]
          [Display (Name = "Description")]
          public string Description { get; set; }
          [Required]
          [Display (Name = "ProductCategory")]
          public string ProductCategory { get; set; }
          [Required]
          [Display(Name = "ProductDescription")]
          public string ProductDescription { get; set; }
          [Required]
          [Display(Name = "ProductPrice")]
          public decimal ProductPrice { get; set; }
          public ProductRoles Level { get; set; }
     }
}
