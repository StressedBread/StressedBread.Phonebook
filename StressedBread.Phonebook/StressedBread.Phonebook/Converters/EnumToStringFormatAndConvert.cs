using System.Text.RegularExpressions;

namespace StressedBread.Phonebook.Converters;

public static class EnumToStringFormatAndConvert
{
    public static string Convert<TEnum>(TEnum value) where TEnum : Enum
    {
        var name = value.ToString();
        var formattedName = Regex.Replace(name, "(?<!^)([A-Z])", " $1");
        return formattedName;
    }
}