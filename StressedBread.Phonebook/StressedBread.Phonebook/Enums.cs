namespace StressedBread.Phonebook;
public class Enums
{
    public enum MainMenuOptions
    {
        AddContact,
        UpdateContact,
        DeleteContact,
        ViewContacts,
        Exit
    }

    public enum ErrorResultType
    {
        None,
        ValidationError,
        DbUpdateError,
        NotFound
    }
}
