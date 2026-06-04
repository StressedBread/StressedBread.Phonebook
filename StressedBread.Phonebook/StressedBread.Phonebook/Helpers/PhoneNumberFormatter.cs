using PhoneNumbers;

namespace StressedBread.Phonebook.Helpers;
public class PhoneNumberFormatter(PhoneNumberUtil phoneNumberUtil)
{
    public string FormatPhoneNumber(string phoneNumberInput)
    {
        var phoneNumber = phoneNumberUtil.Parse(phoneNumberInput, null);
        return phoneNumberUtil.Format(phoneNumber, PhoneNumberFormat.INTERNATIONAL);
    }
}
