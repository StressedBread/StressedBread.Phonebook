using Spectre.Console;
using StressedBread.Phonebook.Models;

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
    internal void ViewContacts(List<Contact> contacts)
    {
        DisplayContacts(contacts);

        AnsiConsole.MarkupLine("Press any key to return to the main menu...");
        Console.ReadKey();
    }

    internal void DisplayContacts(List<Contact> contacts)
    {
        AnsiConsole.Clear();

        var table = new Table();

        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Phone Number");
        table.AddColumn("Email");

        int id = 1;

        foreach (var contact in contacts)
        {
            table.AddRow(id.ToString(), contact.Name, contact.PhoneNumber, contact.Email);
            id++;
        }

        AnsiConsole.Write(table);        
    }    
}
