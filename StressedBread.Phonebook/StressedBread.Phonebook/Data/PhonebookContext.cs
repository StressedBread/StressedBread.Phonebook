using Microsoft.EntityFrameworkCore;
using StressedBread.Phonebook.Models;

namespace StressedBread.Phonebook.Data;
public class PhonebookContext : DbContext
{ 
    public DbSet<Contact> Contacts { get; set; }

    public PhonebookContext() { }

    public PhonebookContext(DbContextOptions<PhonebookContext> options) : base(options) { }
}
