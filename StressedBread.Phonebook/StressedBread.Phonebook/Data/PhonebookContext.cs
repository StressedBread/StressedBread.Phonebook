using Microsoft.EntityFrameworkCore;

namespace StressedBread.Phonebook.Data;
public class PhonebookContext : DbContext
{ 
    public PhonebookContext(DbContextOptions<PhonebookContext> options) : base(options)
    {
    }
}
