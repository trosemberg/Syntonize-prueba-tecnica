using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace TechTest.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class TEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public virtual string Name { get; set; }
    }
}