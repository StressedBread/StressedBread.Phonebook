using PhoneNumbers;

namespace StressedBread.Phonebook.Validation;
public class PhoneNumberValidation
{
    private readonly PhoneNumberUtil _phoneNumberUtil;
    public PhoneNumberValidation(PhoneNumberUtil phoneNumberUtil)
    {
        _phoneNumberUtil = phoneNumberUtil;
    }

    internal (bool isValid, string message) IsValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return (false, "Phone number cannot be empty.");

        try
        {
            var parsedPhoneNumber = _phoneNumberUtil.Parse(phoneNumber, null);
            var isValid = _phoneNumberUtil.IsValidNumber(parsedPhoneNumber);

            if (isValid) return (true, "");
            return (false, "Invalid phone number.");
        }
        catch (NumberParseException ex)
        {
            return (false, $"Invalid phone number format: {ex.Message}");
        }        
    }
}
