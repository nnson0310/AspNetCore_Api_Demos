
using System.ComponentModel.DataAnnotations;

namespace AspCoreWebAPIDemos.Models
{
    public class Rate
    {
        public int Id { get; set; }

        public string? GuestName { get; set; }

        public int? Point { get; set; }
    }
}
