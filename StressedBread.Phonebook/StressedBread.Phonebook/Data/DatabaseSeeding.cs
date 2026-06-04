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
            new Contact { Name = "Sofia García", PhoneNumber = "+34 91 123 4567", Email = "sofia.garcia@hotmail.com" },
            new Contact { Name = "Lukas Müller", PhoneNumber = "+49 30 12345678", Email = "lukas.mueller@web.de" },
            new Contact { Name = "Yuki Tanaka", PhoneNumber = "+81 3 1234 5678", Email = "yuki.tanaka@docomo.ne.jp" },
            new Contact { Name = "Fatima Al-Hassan", PhoneNumber = "+971 50 123 4567", Email = "fatima.alhassan@gmail.com" },
            new Contact { Name = "Carlos Mendoza", PhoneNumber = "+52 55 1234 5678", Email = "carlos.mendoza@hotmail.com" },
            new Contact { Name = "Priya Sharma", PhoneNumber = "+91 98765 43210", Email = "priya.sharma@yahoo.in" },
            new Contact { Name = "Tomáš Kováč", PhoneNumber = "+421 911 987 654", Email = "tomas.kovac@azet.sk" },
            new Contact { Name = "Elena Popescu", PhoneNumber = "+40 21 123 4567", Email = "elena.popescu@gmail.com" },
            new Contact { Name = "Omar Khalid", PhoneNumber = "+20 10 1234 5678", Email = "omar.khalid@outlook.com" },
            new Contact { Name = "Anna Kowalski", PhoneNumber = "+48 22 123 45 67", Email = "anna.kowalski@wp.pl" },
            new Contact { Name = "David Osei", PhoneNumber = "+233 24 123 4567", Email = "david.osei@gmail.com" }
        };
    }
}
