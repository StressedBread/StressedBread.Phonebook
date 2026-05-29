using Microsoft.EntityFrameworkCore;
using StressedBread.Phonebook.Models;

namespace StressedBread.Phonebook.Data;
public class PhonebookContext : DbContext
{ 
    public DbSet<Contact> Contacts { get; set; }

    public PhonebookContext() { }

    public PhonebookContext(DbContextOptions<PhonebookContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name);
            entity.Property(e => e.PhoneNumber);
            entity.Property(e => e.Email);
        });
    }
}
