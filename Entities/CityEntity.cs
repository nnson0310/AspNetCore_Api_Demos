using AspCoreWebAPIDemos.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspCoreWebAPIDemos.Entities
{
    public class CityEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        public List<District>? Districts { get; set; } = new List<District>();

        public List<RateEntity>? Rates { get; set; }

        public CityEntity(string name)
        {
            Name = name;
        }
    }
}
