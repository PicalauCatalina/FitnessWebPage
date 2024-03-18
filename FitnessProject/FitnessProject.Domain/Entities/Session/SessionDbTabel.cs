using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitnessProject.Domain.Entities.Session
{
     public class SessionsDbTable
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Id { get; set; }

          public string Cookie { get; set; }
          public int UserId { get; set; }
          public DateTime ExpirationDate { get; set; }
     }
}
