using AspCoreWebAPIDemos.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspCoreWebAPIDemos.DBContexts
{
    public class CityContext: DbContext
    {
        public DbSet<CityEntity> City { get; set; }

        public DbSet<RateEntity> Rate { get; set; } 

        public CityContext(DbContextOptions<CityContext> options): base(options)
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
                     Description = "This is Thai Lan capital"
                 },
                  new CityEntity("Beijing")
                  {
                      Id = 3,
                      Description = "This is a very big city of China"
                  });
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite();
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
