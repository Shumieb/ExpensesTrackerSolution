using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Frequency
    {
        public int FrequencyId { get; set; }

        [Required(ErrorMessage="Please enter a name.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "The name should have between 5 and 50 characters")]
        public required string Name { get; set; }
    }
}
