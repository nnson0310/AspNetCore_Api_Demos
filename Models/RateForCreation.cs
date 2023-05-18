
using System.ComponentModel.DataAnnotations;

namespace AspCoreWebAPIDemos.Models
{
    public class RateForCreation
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Guest name must be provided")]
        [MaxLength(20, ErrorMessage = "Quest name must be less than 20 characters")]
        public string? GuestName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Rate is required")]
        [Range(0, 10, ErrorMessage = "Rate must be in range from 0 to 10")]
        public int? Point { get; set; }
    }
}
