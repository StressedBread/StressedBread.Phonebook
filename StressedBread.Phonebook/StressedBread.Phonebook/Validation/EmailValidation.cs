namespace StressedBread.Phonebook.Validation;
using System.Net.Mail;
public class EmailValidation
{
    internal (bool isValid, string message) IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return (false, "Email cannot be empty.");

        try
        {
            var mailAddress = new MailAddress(email);
            var isValidAdress = mailAddress.Address == email.Trim();
            if (!isValidAdress)
                return (false, "Email format is invalid.");
            return (true, "");
        }
        catch (FormatException)
        {
            return (false, "Email format is invalid.");
        }
    }
}
