
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
                    Description = "Viet Nam capital",
                    Districts = new List<District>()
                    {
                        new District()
                        {
                            Id = 1,
                            Name = "Long Bien",
                            Address = "South East"
                        },
                        new District()
                        {
                            Id = 2,
                            Name = "Hoan Kiem",
                            Address = "City central"
                        }
                    }
                },
                new City()
                {
                    Id = 2,
                    Name = "Ho Chi Minh city",
                    Description = "Biggest city of Viet Nam",
                    Districts = new List<District>()
                    {
                        new District()
                        {
                            Id = 1,
                            Name = "Quan 1",
                            Address = "City Central"
                        }
                    }
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
