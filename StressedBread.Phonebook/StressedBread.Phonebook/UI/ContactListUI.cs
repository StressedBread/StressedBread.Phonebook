using Spectre.Console;
using StressedBread.Phonebook.Models;
using StressedBread.Phonebook.Validation;

namespace StressedBread.Phonebook.UI;
public class ContactListUI
{
    private readonly PhoneNumberValidation _phoneNumberValidation;

    public ContactListUI(PhoneNumberValidation phoneNumberValidation)
    {
        _phoneNumberValidation = phoneNumberValidation;
    }

    internal (string Name, string PhoneNumber, string Email) AddContactDisplay()
    {
        AnsiConsole.Clear();

        var name = AnsiConsole.Ask<string>("Enter contact name:");
        
        var phoneNumber = string.Empty;
        var isValid = false;

        do
        {
            phoneNumber = AnsiConsole.Ask<string>("Enter contact phone number in international format:");
            var validationResult = _phoneNumberValidation.IsValidPhoneNumber(phoneNumber);
            isValid = validationResult.isValid;

            if (!isValid)
                AnsiConsole.MarkupLine($"[red]{validationResult.message} Please try again.[/]");

        } while (!isValid);

        var email = AnsiConsole.Ask<string>("Enter contact email:");

        return (name, phoneNumber, email);
    }

    internal void ViewContacts(List<Contact> contacts)
    {
        DisplayContacts(contacts);

        AnsiConsole.MarkupLine("Press any key to return to the main menu...");
        Console.ReadKey();
    }

    internal int SelectContactDisplayById(List<Contact> contacts)
    {
        DisplayContacts(contacts);

        while (true)
        {
            var displayContactId = AnsiConsole.Ask<int>("Enter the ID of the contact or 0 to return to menu:");
            if (displayContactId == 0)
                return -1;

            var contactId = displayContactId - 1;

            if (contactId < 0 || contactId >= contacts.Count)
                AnsiConsole.MarkupLine("[red]Invalid contact ID. Please try again.[/]");
            else
                return contacts[contactId].Id;
        }
    }

    internal Contact UpdateContactDisplay(Contact contact)
    {
        AnsiConsole.Clear();
        contact.Name = AnsiConsole.Ask($"Enter new name or press Enter to keep current (current: {contact.Name}):", contact.Name);
        contact.PhoneNumber = AnsiConsole.Ask($"Enter new phone number or press Enter to keep current (current: {contact.PhoneNumber}):", contact.PhoneNumber);
        contact.Email = AnsiConsole.Ask($"Enter new email or press Enter to keep current (current: {contact.Email}):", contact.Email);
        return contact;
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

    internal void ShowResults((bool isSuccess, string message) result)
    {
        if (result.isSuccess)
            AnsiConsole.MarkupLine($"[green]{result.message}[/]");
        else
            AnsiConsole.MarkupLine($"[red]{result.message}[/]");
        AnsiConsole.MarkupLine("Press any key to return to the main menu...");
        Console.ReadKey();
    }
}
