using Spectre.Console;
using StressedBread.Phonebook.Models;
using StressedBread.Phonebook.Validation;
using static StressedBread.Phonebook.Enums;

namespace StressedBread.Phonebook.UI;
public class ContactListUI
{
    private readonly PhoneNumberValidation _phoneNumberValidation;
    private readonly EmailValidation _emailValidation;

    private static readonly Dictionary<ResultType, string> ResultMessages = new()
    {
        { ResultType.ContactNotFound, "Contact not found." },
        { ResultType.DbUpdateError, "An error occurred while updating the database." },
        { ResultType.None, string.Empty },
        { ResultType.ContactsEmpty, "No contacts found." }
    };

    public ContactListUI(PhoneNumberValidation phoneNumberValidation, EmailValidation emailValidation)
    {
        _phoneNumberValidation = phoneNumberValidation;
        _emailValidation = emailValidation;
    }

    internal (string Name, string PhoneNumber, string Email) AddContactDisplay()
    {
        AnsiConsole.Clear();

        var name = AnsiConsole.Ask<string>("Enter contact name:");
        var phoneNumber = PhoneNumberUIValidation();
        var email = EmailUIValidation();

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

    internal void ShowResults(Result result, string message = "")
    {
        if (result.IsSuccess)
            AnsiConsole.MarkupLine($"[green]{message}[/]");
        else
        {
            if (ResultMessages.ContainsKey(result.ResultType))
                AnsiConsole.MarkupLine($"[red]{ResultMessages[result.ResultType]}[/]");
            else
                AnsiConsole.MarkupLine($"[red]An unknown error occurred.[/]");
        }

        AnsiConsole.MarkupLine("Press any key to return to the main menu...");
        Console.ReadKey();
    }

    private string PhoneNumberUIValidation()
    {
        var phoneNumber = string.Empty;
        var isValidNumber = false;

        do
        {
            phoneNumber = AnsiConsole.Ask<string>("Enter contact phone number in international format:");
            var validationResult = _phoneNumberValidation.IsValidPhoneNumber(phoneNumber);
            isValidNumber = validationResult.isValid;

            if (!isValidNumber)
                AnsiConsole.MarkupLine($"[red]{validationResult.message} Please try again.[/]");

        } while (!isValidNumber);

        return phoneNumber;
    }

    private string EmailUIValidation()
    {
        var email = string.Empty;
        var isValidEmail = false;

        do
        {
            email = AnsiConsole.Ask<string>("Enter contact email:");
            var validationResult = _emailValidation.IsValidEmail(email);
            isValidEmail = validationResult.isValid;

            if (!isValidEmail)
                AnsiConsole.MarkupLine($"[red]{validationResult.message} Please try again.[/]");
        } while (!isValidEmail);

        return email;
    }
}