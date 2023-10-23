using Microsoft.EntityFrameworkCore;

namespace KickCraze.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        //public DbSet<AccountEntity> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<EventsEntity>()
            //    .HasOne(f => f.Type)
            //    .WithMany()
            //    .HasForeignKey(f => f.TypeID);

            //modelBuilder.Entity<RoleEntity>().HasData(
            //    new RoleEntity
            //    {
            //        RoleID = 1,
            //        Name = "admin",
            //    },
            //    new RoleEntity
            //    {
            //        RoleID = 2,
            //        Name = "user",
            //    },
            //    new RoleEntity
            //    {
            //        RoleID = 3,
            //        Name = "organiser",
            //    }
            //);
        }
    }
}
