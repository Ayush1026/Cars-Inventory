using CarsInventory.DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CarsInventory.DataAccessLayer.Repositories
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
           : base(options)
        {
        }

        public DbSet<CarsModel> Cars { get; set; }
        public DbSet<UserModel> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarsModel>()
                    .Property(x => x.Price)
                    .HasPrecision(10, 2);

            modelBuilder.Entity<CarsModel>(entity =>
            {
                entity.HasOne<UserModel>(p => p.UserModels)
                .WithMany(p => p.CarsModels)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserId");
            });


                
        }
    }
}
