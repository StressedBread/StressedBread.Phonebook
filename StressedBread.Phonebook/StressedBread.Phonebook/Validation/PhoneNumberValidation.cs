using PhoneNumbers;

namespace StressedBread.Phonebook.Validation;
public class PhoneNumberValidation(PhoneNumberUtil phoneNumberUtil)
{
    public (bool IsValid, string Message) IsValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return (false, "Phone number cannot be empty.");

        try
        {
            var parsedPhoneNumber = phoneNumberUtil.Parse(phoneNumber, null);
            var isValid = phoneNumberUtil.IsValidNumber(parsedPhoneNumber);

            if (isValid) return (true, "");
            return (false, "Invalid phone number.");
        }
        catch (NumberParseException ex)
        {
            return (false, $"Invalid phone number format: {ex.Message}");
        }        
    }
}
