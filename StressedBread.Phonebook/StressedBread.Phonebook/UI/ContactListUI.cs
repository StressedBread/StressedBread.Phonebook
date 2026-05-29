using Spectre.Console;

namespace StressedBread.Phonebook.UI;
public class ContactListUI
{
    internal (string Name, string PhoneNumber, string Email) AddContactDisplay()
    {
        AnsiConsole.Clear();

        var name = AnsiConsole.Ask<string>("Enter contact name:");
        var phoneNumber = AnsiConsole.Ask<string>("Enter contact phone number:");
        var email = AnsiConsole.Ask<string>("Enter contact email:");

        return (name, phoneNumber, email);
    }
}
