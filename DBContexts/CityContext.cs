using AspCoreWebAPIDemos.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspCoreWebAPIDemos.DBContexts
{
    public class CityContext : DbContext
    {
        public DbSet<CityEntity> City { get; set; }

        public DbSet<RateEntity> Rate { get; set; }

        public CityContext(DbContextOptions<CityContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityEntity>().HasData(
                new CityEntity("Ha Noi")
                {
                    Id = 1,
                    Description = "This is Viet Nam capital"
                },
                 new CityEntity("Bangkok")
                 {
                     Id = 2,
                     Description = "Thai Lan capital where is a very attractive tourist place"
                 },
                  new CityEntity("Beijing")
                  {
                      Id = 3,
                      Description = "China captial with many Chinese traditional food you can taste"
                  },
                  new CityEntity("Okinawa")
                  {
                      Id = 3,
                      Description = "A beautiful city of Japan located in the South East"
                  },
                  new CityEntity("Paris")
                  {
                      Id = 3,
                      Description = "Kingdom of fashion and France capital. You should definitely visit it at least once."
                  });

            modelBuilder.Entity<RateEntity>().HasData(
                new RateEntity()
                {
                    Id = 1,
                    CityId = 1,
                    GuestName = "Nguyen Son",
                    Point = 10
                },
                new RateEntity()
                {
                    Id = 2,
                    CityId = 1,
                    GuestName = "Thu Huong",
                    Point = 7
                },
                new RateEntity()
                {
                    Id = 3,
                    CityId = 1,
                    GuestName = "Sarah Chalez",
                    Point = 4
                },
                new RateEntity()
                {
                    Id = 4,
                    CityId = 2,
                    GuestName = "David Micheal",
                    Point = 8
                },
                 new RateEntity()
                 {
                     Id = 5,
                     CityId = 2,
                     GuestName = "Mariah Ozawa",
                     Point = 6
                 },
                  new RateEntity()
                  {
                      Id = 6,
                      CityId = 3,
                      GuestName = "Okata Mutan",
                      Point = 9
                  });
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite();
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
