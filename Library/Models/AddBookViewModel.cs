using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class AddBookViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [StringLength(50, MinimumLength = 10)]
        public string Title { get; set; } = null!;
        [System.ComponentModel.DataAnnotations.Required]
        [StringLength(5000, MinimumLength = 5)]
        public string Description { get; set; } = null!;
        [System.ComponentModel.DataAnnotations.Required]
        [StringLength(50, MinimumLength = 5)]
        public string Author { get; set; } = null!;
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings =false)]
        
        public string Url { get; set; } = null!;
        [System.ComponentModel.DataAnnotations.Required]
        public string Rating { get; set; } = null!;
         
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
        public ICollection<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}