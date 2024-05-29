using System;
using System.ComponentModel.DataAnnotations;

namespace EfitWeb1.Models
{
     public class ContactModel
     {
          public int Id { get; set; }

          public string Name { get; set; }

          public string Email { get; set; }

          public string Phone { get; set; }

          public string Message { get; set; }

          public DateTime SubmittedAt { get; set; } = DateTime.Now;
     }
}