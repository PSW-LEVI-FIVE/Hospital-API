using HospitalLibrary.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Settings
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasData(
                new Room() { Id = 1, Number = "101A", Floor = 1 },
                new Room() { Id = 2, Number = "204", Floor = 2 },
                new Room() { Id = 3, Number = "305B", Floor = 3 }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
