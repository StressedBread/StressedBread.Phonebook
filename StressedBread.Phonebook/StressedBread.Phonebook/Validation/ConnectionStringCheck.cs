namespace StressedBread.Phonebook.Validation;
public class ConnectionStringCheck
{
    public static (bool IsValid, string Message) IsValidConnectionString(string? connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
            return (false, "Connection string is missing. Please check your configuration.");
        return (true, "");
    }
}
