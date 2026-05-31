

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
        // update logic
    }

    private async Task DeleteContact()
    {
        var contacts = await _contactService.GetAllContactsAsync();
        var contactId = _contactListUI.DeleteContactDisplay(contacts);
        if (contactId == -1) return;

        var result = await _contactService.DeleteContactAsync(contactId);
        _contactListUI.ShowResults(result);
    }

    private async Task ViewContacts()
    {
        var contacts = await _contactService.GetAllContactsAsync();
        _contactListUI.ViewContacts(contacts);
    }
}
