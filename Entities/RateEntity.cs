using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspCoreWebAPIDemos.Entities
{
    public class RateEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public string? GuestName { get; set; }

        public int? Point { get; set; }

        [ForeignKey("CityId")]
        public CityEntity? City { get; set; }  

        public int CityId { get; set; }
    }
}
