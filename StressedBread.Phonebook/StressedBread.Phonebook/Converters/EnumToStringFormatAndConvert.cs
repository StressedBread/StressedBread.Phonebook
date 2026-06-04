using System.Text.RegularExpressions;

namespace StressedBread.Phonebook.Converters;

public static partial class EnumToStringFormatAndConvert
{
    public static string Convert<TEnum>(TEnum value) where TEnum : Enum
    {
        var name = value.ToString();
        var formattedName = MyRegex().Replace(name, " $1");
        return formattedName;
    }

    [GeneratedRegex("(?<!^)([A-Z])")]
    private static partial Regex MyRegex();
}