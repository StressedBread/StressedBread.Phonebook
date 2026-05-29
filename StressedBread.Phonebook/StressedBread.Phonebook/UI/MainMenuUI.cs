using Spectre.Console;
using StressedBread.Phonebook.Converters;
using static StressedBread.Phonebook.Enums;

namespace StressedBread.Phonebook.UI;
internal class MainMenuUI
{
    internal MainMenuOptions DisplayMainMenu()
    {
        AnsiConsole.Clear();

        return AnsiConsole.Prompt(
            new SelectionPrompt<MainMenuOptions>()
                .Title("Select an option:")
                .UseConverter(option => EnumToStringFormatAndConvert.Convert(option))
                .AddChoices(Enum.GetValues<MainMenuOptions>()));
    }
}
