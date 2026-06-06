using Spectre.Console;

namespace StressedBread.Phonebook.UI;
public class EmailUI
{
    public (string Subject, string Body) EmailMessageDisplay()
    {
        AnsiConsole.Clear();

        var subject = AnsiConsole.Ask<string>("Enter subject:");
        AnsiConsole.MarkupLine("Enter message in lines. Write END in new line to finish:");

        var lines = new List<string>();

        while (true)
        {
            var line = Console.ReadLine();
            if (string.Equals(line, "END", StringComparison.OrdinalIgnoreCase))
                break;
            lines.Add(line ?? "");
        }

        var emailBody = string.Join(Environment.NewLine, lines);

        return (subject, emailBody);
    }
}
