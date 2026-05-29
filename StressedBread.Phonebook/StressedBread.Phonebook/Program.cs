using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StressedBread.Phonebook.Controllers;
using StressedBread.Phonebook.Data;
using StressedBread.Phonebook.Services;
using StressedBread.Phonebook.UI;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<PhonebookContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<MainMenuUI>();
builder.Services.AddSingleton<PhonebookController>();
builder.Services.AddSingleton<ContactService>();
builder.Services.AddSingleton<ContactListUI>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<PhonebookContext>();
context.Database.Migrate();

var controller = scope.ServiceProvider.GetRequiredService<PhonebookController>();
await controller.Run();