using hootel.Models;
using Microsoft.EntityFrameworkCore;

public class HotelManagementContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка связей и ограничений
        modelBuilder.Entity<Reservation>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(r => r.Id);

        modelBuilder.Entity<Reservation>()
            .HasOne<Room>()
            .WithMany()
            .HasForeignKey(r => r.RoomId);
    }
}
