using Spectre.Console;
using StressedBread.Phonebook.Converters;
using static StressedBread.Phonebook.Enums;

namespace StressedBread.Phonebook.UI;
public class MainMenuUI
{
    public static MainMenuOptions DisplayMainMenu()
    {
        AnsiConsole.Clear();

        return AnsiConsole.Prompt(
            new SelectionPrompt<MainMenuOptions>()
                .Title("Select an option:")
                .UseConverter(EnumToStringFormatAndConvert.Convert)
                .AddChoices(Enum.GetValues<MainMenuOptions>()));
    }
}
