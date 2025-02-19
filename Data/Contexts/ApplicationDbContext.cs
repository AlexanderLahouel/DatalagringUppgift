using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

  
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<CustomerContacts> CustomerContacts { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<UnitType> UnitTypes { get; set; } = null!;
    public DbSet<StatusType> StatusTypes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       //mina many to many relationer. Ser till att customers, projectmanagers, services kan användas i mer än ett projekt. restriktiv borttagning så 
       //om en kund tas bort raderas inte projekt osv.

        modelBuilder.Entity<Project>()
            .HasOne(p => p.ProjectManager)
            .WithMany(e => e.Projects)
            .HasForeignKey(p => p.ProjectManagerId)
            .OnDelete(DeleteBehavior.Restrict); 

        
        modelBuilder.Entity<Project>()
            .HasOne(p => p.Customer)
            .WithMany(c => c.Projects)
            .HasForeignKey(p => p.CustomerId)
            .OnDelete(DeleteBehavior.Restrict); 

       
        modelBuilder.Entity<Project>()
            .HasOne(p => p.Service)
            .WithMany(s => s.Projects)
            .HasForeignKey(p => p.ServiceId)
            .OnDelete(DeleteBehavior.Restrict); 

       
        modelBuilder.Entity<Project>()
            .HasOne(p => p.StatusType)
            .WithMany()
            .HasForeignKey(p => p.StatusTypeId)
            .OnDelete(DeleteBehavior.Restrict); 

       
        modelBuilder.Entity<CustomerContacts>()
            .HasOne(cc => cc.Customer)
            .WithMany(c => c.CustomerContacts)
            .HasForeignKey(cc => cc.CustomerId)
            .OnDelete(DeleteBehavior.Restrict); 

       
        modelBuilder.Entity<Service>()
            .HasOne(s => s.UnitType)
            .WithMany()
            .HasForeignKey(s => s.UnitTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        
        modelBuilder.Entity<CustomerContacts>()
            .HasKey(cc => new { cc.FirstName, cc.LastName, cc.CustomerId });

        base.OnModelCreating(modelBuilder);
    }

}

