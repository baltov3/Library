using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace Library.Data.Models
{
    public class Category
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public List<Book> Books { get; set; } = new List<Book>();
    }
}