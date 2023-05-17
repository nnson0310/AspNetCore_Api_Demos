
using AspCoreWebAPIDemos.Models;

namespace AspCoreWebAPIDemos.DataStorages
{
    public class CitiesDataStore 
    {
        public List<City> Cities { get; set; }

        public static CitiesDataStore CitiesData { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new List<City>()
            {
                new City()
                {
                    Id = 1,
                    Name = "Hanoi",
                    Description = "Viet Nam capital"
                },
                new City()
                {
                    Id = 2,
                    Name = "Ho Chi Minh city",
                    Description = "Biggest city of Viet Nam"
                },
                new City()
                {
                    Id = 3,
                    Name = "Bangkok",
                    Description = "Thailand capital"
                }
            };
        }
    }
}
