using StressedBread.Phonebook.Models;
using StressedBread.Phonebook.Services;
using StressedBread.Phonebook.UI;

namespace StressedBread.Phonebook.Controllers;
public class PhonebookController
{
    private readonly MainMenuUI _mainMenuUI;
    private readonly ContactService _contactService;
    private readonly ContactListUI _contactListUI;

    public PhonebookController(MainMenuUI mainMenuUI, ContactService contactService, ContactListUI contactListUI)
    {
        _mainMenuUI = mainMenuUI;
        _contactService = contactService;
        _contactListUI = contactListUI;
    }

    internal async Task Run()
    {
        while (true)
        {
            var selectedOption = _mainMenuUI.DisplayMainMenu();

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
                case Enums.MainMenuOptions.Exit: 
                    return;
            }
        }
    }

    private async Task AddContact()
    {
        var newContact = _contactListUI.AddContactDisplay();
        await _contactService.AddContactAsync(newContact);
    }

    private async Task UpdateContact()
    {
        var contact = await SelectContact();
        if (contact == null) return;

        var updatedContact = _contactListUI.UpdateContactDisplay(contact);

        var updateResult = await _contactService.UpdateContactAsync(contact.Id, updatedContact);
        _contactListUI.ShowResults(updateResult);
    }

    private async Task DeleteContact()
    {
        var contact = await SelectContact();
        if (contact == null) return;

        var deleteResult = await _contactService.DeleteContactAsync(contact.Id);
        _contactListUI.ShowResults(deleteResult);
    }

    private async Task ViewContacts()
    {
        var contacts = await _contactService.GetAllContactsAsync();
        if (contacts.Data != null)
            _contactListUI.ViewContacts(contacts.Data);
    }

    private async Task<Contact?> SelectContact()
    {
        var contacts = await _contactService.GetAllContactsAsync();
        if (contacts.Data != null)
        {
            var contactId = _contactListUI.SelectContactDisplayById(contacts.Data);
            if (contactId == -1) return null;
            return contacts.Data.FirstOrDefault(c => c.Id == contactId);
        }
        return null;
    }
}
