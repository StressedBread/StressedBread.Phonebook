using StressedBread.Phonebook.Models;

namespace StressedBread.Phonebook.Data;
public class DatabaseSeeding
{
    public static List<Contact> Seed()
    {
        return new List<Contact>
        {
            new Contact { Name = "Alice Johnson", PhoneNumber = "+1 650 253 0000", Email = "alice.johnson@gmail.com" },
            new Contact { Name = "Bob Smith", PhoneNumber = "+44 20 7946 0958", Email = "bob.smith@outlook.com" },
            new Contact { Name = "Marta Novák", PhoneNumber = "+421 902 123 456", Email = "marta.novak@gmail.com" },
            new Contact { Name = "James Lee", PhoneNumber = "+61 2 9374 4000", Email = "james.lee@yahoo.com" },
            new Contact { Name = "Sofia García", PhoneNumber = "+34 91 123 4567", Email = "sofia.garcia@hotmail.com" }
        };
    }
}
