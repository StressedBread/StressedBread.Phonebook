using StressedBread.Phonebook.Models;
using StressedBread.Phonebook.Services;
using StressedBread.Phonebook.UI;

namespace StressedBread.Phonebook.Controllers;
public class PhonebookController(ContactService contactService, ContactListUI contactListUI, EmailUI emailUI)
{
    public async Task Run()
    {
        while (true)
        {
            var selectedOption = MainMenuUI.DisplayMainMenu();

            switch (selectedOption)
            {
                case Enums.MainMenuOptions.AddContact: 
                    await AddContact(); 
                    break;
                case Enums.MainMenuOptions.UpdateContact: 
                    await UpdateContact(); 
                    break;
                case Enums.MainMenuOptions.DeleteContact: 
                    await DeleteContact(); 
                    break;
                case Enums.MainMenuOptions.ViewContacts: 
                    await ViewContacts(); 
                    break;
                case Enums.MainMenuOptions.SendEmail: 
                    await SendEmail();
                    break;
                case Enums.MainMenuOptions.Exit: 
                    return;
            }
        }
    }

    private async Task AddContact()
    {
        var newContact = contactListUI.AddContactDisplay();
        var addResult = await contactService.AddContactAsync(newContact);
        
        ContactListUI.ShowResults(addResult, addResult.IsSuccess ? "Contact added successfully" : string.Empty);
    }

    private async Task UpdateContact()
    {
        var contact = await SelectContact();
        if (contact == null) return;

        var updatedContact = contactListUI.UpdateContactDisplay(contact);

        var updateResult = await contactService.UpdateContactAsync(contact.Id, updatedContact);

        ContactListUI.ShowResults(updateResult, updateResult.IsSuccess ? "Contact updated successfully" : string.Empty);
    }

    private async Task DeleteContact()
    {
        var contact = await SelectContact();
        if (contact == null) return;

        var deleteResult = await contactService.DeleteContactAsync(contact.Id);

        ContactListUI.ShowResults(deleteResult, deleteResult.IsSuccess ? "Contact deleted successfully" : string.Empty);
    }

    private async Task ViewContacts()
    {
        var contacts = await contactService.GetAllContactsAsync();

        if (contacts.Data == null)
            ContactListUI.ShowResults(contacts);
        else
            ContactListUI.ViewContacts(contacts.Data);
    }

    private async Task<Contact?> SelectContact()
    {
        var contacts = await contactService.GetAllContactsAsync();

        if (contacts.Data == null)
            ContactListUI.ShowResults(contacts);

        else
        { 
            var contactId = ContactListUI.SelectContactDisplayById(contacts.Data);
            if (contactId == -1) return null;
            return contacts.Data.FirstOrDefault(c => c.Id == contactId);
        }
        return null;
    }

    private async Task SendEmail()
    {
        var contact = await SelectContact();
        if (contact == null) return;
        var contactEmail = contact.Email;

        var (Subject, Body) = emailUI.EmailMessageDisplay();
    }
}
