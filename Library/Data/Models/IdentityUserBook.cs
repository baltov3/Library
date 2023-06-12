using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Models
{
    public class IdentityUserBook
    {
        [Required]
        
        public string CategoryId { get; set; } = null!;

        [ForeignKey(nameof(CategoryId))]
        public IdentityUser Collector { get; set; }=null!;
        [Required]
        
        public int BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; } = null!;
    }
}