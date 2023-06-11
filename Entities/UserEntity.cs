using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspCoreWebAPIDemos.Entities
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string City { get; set; }

        public UserEntity(string name, string password, string city)
        {
            Name = name;
            Password = password;
            City = city;
        }
    }
}
