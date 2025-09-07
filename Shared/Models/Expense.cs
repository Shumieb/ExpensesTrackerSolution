using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }

        [Required(ErrorMessage ="Please enter a name")]
        [StringLength(100, MinimumLength = 5, ErrorMessage ="The name should have between 5 and 100 characters")]
        public required string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Required(ErrorMessage = "The Amount is required.")]
        [Range(1, 1000, ErrorMessage = "Please enter an amount between 1 and 1,000")]
        public decimal Amount { get; set; } = 0;

        [Required(ErrorMessage = "The Category is required.")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public bool IsPaid { get; set; } = false;

        [Required(ErrorMessage = "The Frequency is required.")]
        public int? FrequencyId { get; set; }

        public Frequency? Frequency { get; set; }

        public int? DueDate { get; set; }
    }
}
