using Microsoft.EntityFrameworkCore;
using StressedBread.Phonebook.Data;
using StressedBread.Phonebook.Models;

namespace StressedBread.Phonebook.Services;
public class ContactService
{
    private readonly PhonebookContext _context;
    public ContactService(PhonebookContext context)
    {
        _context = context;
    }

    internal async Task AddContact((string Name, string PhoneNumber, string Email) newContact)
    {
        var contact = new Contact 
        { 
            Name = newContact.Name, 
            PhoneNumber = newContact.PhoneNumber, 
            Email = newContact.Email 
        };

        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
    }

    internal async Task<List<Contact>> GetAllContactsAsync()
    {
        return await _context.Contacts.ToListAsync();
    }
}
