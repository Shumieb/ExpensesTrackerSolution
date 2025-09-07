using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "The name should have between 5 and 100 characters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "The description should have between 5 and 100 characters")]
        public required string Description { get; set; }
    }
}
