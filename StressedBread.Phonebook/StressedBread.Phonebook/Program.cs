using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneNumbers;
using StressedBread.Phonebook.Controllers;
using StressedBread.Phonebook.Data;
using StressedBread.Phonebook.Services;
using StressedBread.Phonebook.UI;
using StressedBread.Phonebook.Validation;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<PhonebookContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<PhonebookController>();
builder.Services.AddScoped<ContactListUI>();
builder.Services.AddScoped<ContactService>();
builder.Services.AddScoped<PhoneNumberValidation>();
builder.Services.AddSingleton(PhoneNumberUtil.GetInstance());

var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<PhonebookContext>();
context.Database.Migrate();

var controller = scope.ServiceProvider.GetRequiredService<PhonebookController>();
await controller.Run();