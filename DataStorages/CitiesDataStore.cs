
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
                    },
                    PointRate = new List<Rate>()
                    {
                        new Rate()
                        {
                            Id = 1,
                            GuestName = "Micheal Truong",
                            Point = 10
                        },
                        new Rate()
                        {
                            Id = 2,
                            GuestName = "Sarah Nguyen",
                            Point = 7
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
                    },
                    PointRate = new List<Rate>()
                    {
                        new Rate()
                        {
                            Id = 1,
                            GuestName = "Mario Duc",
                            Point = 3
                        },
                        new Rate()
                        {
                            Id = 2,
                            GuestName = "Thor",
                            Point = 8
                        }
                    }
                },
                new City()
                {
                    Id = 3,
                    Name = "Bangkok",
                    Description = "Thailand capital",
                    PointRate = new List<Rate>()
                    {
                        new Rate()
                        {
                            Id = 1,
                            GuestName = "Scarlet witch",
                            Point = 5
                        },
                        new Rate()
                        {
                            Id = 2,
                            GuestName = "Captain America",
                            Point = 6
                        }
                    }
                }
            };
        }
    }
}
