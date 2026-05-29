

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
                    var newContact = _contactListUI.AddContactDisplay();
                    await _contactService.AddContact(newContact);
                    break;
                case Enums.MainMenuOptions.UpdateContact:
                    // Logic to update contact
                    break;
                case Enums.MainMenuOptions.DeleteContact:
                    // Logic to delete contact
                    break;
                case Enums.MainMenuOptions.ViewContacts:
                    // Logic to view contacts
                    break;
                case Enums.MainMenuOptions.Exit:
                    return; // Exit the application
            }
        }
    }
}
