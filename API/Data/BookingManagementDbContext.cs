using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class BookingManagementDbContext : DbContext
{
    public BookingManagementDbContext(DbContextOptions<BookingManagementDbContext> options) : base(options) { }

    // Add Models to migrate
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<University> Universities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>().HasIndex(e => e.Nik).IsUnique();
        modelBuilder.Entity<Employee>().HasIndex(e => e.Email).IsUnique();
        modelBuilder.Entity<Employee>().HasIndex(e => e.PhoneNumber).IsUnique();

        // One Role has many AccountRoles
        modelBuilder.Entity<Role>()
                    .HasMany(ar => ar.AccountRoles)
                    .WithOne(r => r.Role)
                    .HasForeignKey(ac => ac.RoleGuid)
                    .OnDelete(DeleteBehavior.Restrict);
        // Many AccountRoles have one Account
        modelBuilder.Entity<AccountRole>()
                    .HasOne(a => a.Account)
                    .WithMany(ar => ar.AccountRoles)
                    .HasForeignKey(ac => ac.AccountGuid)
                    .OnDelete(DeleteBehavior.Restrict);
        // One Account has one Employee
        modelBuilder.Entity<Account>()
                    .HasOne(em => em.Employee)
                    .WithOne(a => a.Account)
                    .HasForeignKey<Account>(a => a.Guid)
                    .OnDelete(DeleteBehavior.Restrict);
        // One Employee has one Education
        modelBuilder.Entity<Employee>()
                    .HasOne(ed => ed.Education)
                    .WithOne(em => em.Employee)
                    .HasForeignKey<Education>(ed => ed.Guid)
                    .OnDelete(DeleteBehavior.Restrict);
        // Many Educations have one University
        modelBuilder.Entity<Education>()
                    .HasOne(u => u.University)
                    .WithMany(ed => ed.Educations)
                    .HasForeignKey(ed => ed.UniversityGuid)
                    .OnDelete(DeleteBehavior.Restrict);
        // One Employee has many Bookings
        modelBuilder.Entity<Employee>()
                    .HasMany(b => b.Bookings)
                    .WithOne(em => em.Employee)
                    .HasForeignKey(b => b.EmployeeGuid)
                    .OnDelete(DeleteBehavior.Restrict);
        // Many Bookings have one Room
        modelBuilder.Entity<Booking>()
                    .HasOne(r => r.Room)
                    .WithMany(b => b.Bookings)
                    .HasForeignKey(b => b.RoomGuid)
                    .OnDelete(DeleteBehavior.Restrict);
    }
}
