using Microsoft.EntityFrameworkCore;

namespace SoftwareMind.Models
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext()
        {

        }

        public BookingDbContext(DbContextOptions<BookingDbContext> options)
            : base(options)
        {

        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Desk> Desks { get; set; }
        public DbSet<Location> Locations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Location>(opt =>
            {
                opt.HasKey(e => e.idLocation);
                opt.Property(e => e.idLocation).ValueGeneratedOnAdd();
                opt.Property(e => e.city).HasMaxLength(50);
                opt.Property(e => e.street).HasMaxLength(40);
                
            });

            modelBuilder.Entity<User>(opt =>
            {
                opt.HasKey(e => e.idUser);
                opt.Property(e => e.idUser).ValueGeneratedOnAdd();
                opt.Property(e => e.firstName).HasMaxLength(50);
                opt.Property(e => e.lastName).HasMaxLength(50);
                opt.Property(e => e.login).HasMaxLength(50);
                opt.Property(e => e.password).HasMaxLength(50);
                opt.Property(e => e.Role).HasMaxLength(50);
                opt.Property(e => e.RefreshToken).HasMaxLength(300);
                opt.Property(e => e.RefreshTokenExpiry);
            });

            modelBuilder.Entity<Booking>(opt =>
            {
                opt.HasKey(e => e.idBooking);
                opt.Property(e => e.idBooking).ValueGeneratedOnAdd();
                opt.Property(e => e.startDate);
                opt.Property(e => e.endDate);
                opt.HasOne(e => e.desk).WithMany(e => e.Bookings).HasForeignKey(e => e.idDesk);
                opt.HasOne(e => e.user).WithMany(e => e.Bookings).HasForeignKey(e => e.idUser);

            });

            modelBuilder.Entity<Desk>(opt =>
            {
                opt.HasKey(e => e.idDesk);
                opt.Property(e => e.idDesk).ValueGeneratedOnAdd();
                opt.Property(e => e.name).HasMaxLength(50);
                opt.HasOne(e => e.Location).WithMany(e => e.Desks).HasForeignKey(e => e.idLocation);

            });
        }
    }
}
