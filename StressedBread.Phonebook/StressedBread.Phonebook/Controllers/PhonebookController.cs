

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
                    await _contactService.AddContactAsync(newContact);
                    break;

                case Enums.MainMenuOptions.UpdateContact:
                    // Logic to update contact
                    break;

                case Enums.MainMenuOptions.DeleteContact:
                    var contactsToDelete = await _contactService.GetAllContactsAsync();
                    var contactToDelete = _contactListUI.DeleteContactDisplay(contactsToDelete);

                    if (contactToDelete == -1)
                        break;

                    var result = await _contactService.DeleteContactAsync(contactToDelete);
                    _contactListUI.ShowResults(result);
                    break;

                case Enums.MainMenuOptions.ViewContacts:
                    var contactsToDisplay = await _contactService.GetAllContactsAsync();
                    _contactListUI.ViewContacts(contactsToDisplay);
                    break;

                case Enums.MainMenuOptions.Exit:
                    return; // Exit the application
            }
        }
    }
}
