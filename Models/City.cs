
namespace AspCoreWebAPIDemos.Models
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public List<District>? Districts { get; set; }

        public List<Rate>? Rates { get; set; }
    }

    public class District
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Address { get; set; }
    }
}
