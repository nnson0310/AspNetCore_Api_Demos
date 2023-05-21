using System.ComponentModel.DataAnnotations;

namespace AspCoreWebAPIDemos.Models
{
    public class RateForUpdate
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Guest name is required")]
        [MaxLength(20, ErrorMessage = "Guest name must be less than 20 characters")]
        public string? GuestName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Point is required")]
        [Range(0, 10, ErrorMessage = "Point must be in range from 0 to 10")]
        public int? Point { get; set; }
    }
}
