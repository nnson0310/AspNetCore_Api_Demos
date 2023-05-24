using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspCoreWebAPIDemos.Entities
{
    public class Rate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public string? GuestName { get; set; }

        public int? Point { get; set; }

        [ForeignKey("CityId")]
        public City? City { get; set; }  

        public int CityId { get; set; }
    }
}
