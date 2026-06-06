using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneNumbers;
using Spectre.Console;
using StressedBread.Phonebook.Controllers;
using StressedBread.Phonebook.Data;
using StressedBread.Phonebook.Helpers;
using StressedBread.Phonebook.Services;
using StressedBread.Phonebook.UI;
using StressedBread.Phonebook.Validation;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var (isValid, message) = ConnectionStringCheck.IsValidConnectionString(connectionString);

if (!isValid)
{
    AnsiConsole.MarkupLine($"[red]{message}[/]");
    return;
}

builder.Services.AddDbContext<PhonebookContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<PhonebookController>();
builder.Services.AddScoped<ContactListUI>();
builder.Services.AddScoped<ContactService>();
builder.Services.AddScoped<PhoneNumberValidation>();
builder.Services.AddScoped<PhoneNumberFormatter>();
builder.Services.AddScoped<EmailUI>();
builder.Services.AddSingleton(PhoneNumberUtil.GetInstance());

var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<PhonebookContext>();
var controller = scope.ServiceProvider.GetRequiredService<PhonebookController>();

try
{
    if (await context.Database.CanConnectAsync())
    {
        context.Database.Migrate();

        if (!context.Contacts.Any())
        {
            context.Contacts.AddRange(DatabaseSeeding.Seed());
            await context.SaveChangesAsync();
        }

        await controller.Run();
    }
    else
        AnsiConsole.MarkupLine("[red]Unable to connect to the database.[/]");
}
catch (Exception ex)
{
    AnsiConsole.MarkupLine($"[red]An error occurred: {ex.Message}[/]");
}