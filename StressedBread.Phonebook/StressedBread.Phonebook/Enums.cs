namespace StressedBread.Phonebook;
public class Enums
{
    public enum MainMenuOptions
    {
        AddContact,
        UpdateContact,
        DeleteContact,
        ViewContacts,
        SendEmail,
        Exit
    }

    public enum ResultType
    {
        Success,
        None,
        DbUpdateError,
        ContactNotFound,
        ContactsEmpty,
    }
}
