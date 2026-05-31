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

    internal async Task AddContactAsync((string Name, string PhoneNumber, string Email) newContact)
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

    internal async Task<(bool, string)> DeleteContactAsync(int contactId)
    {
        var contact = await _context.Contacts.FindAsync(contactId);
        if (contact == null) return (false, "Contact not found.");

        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();

        return (true, "Contact deleted successfully.");
    }

    internal async Task<(bool, string)> UpdateContactAsync(int contactId, Contact updatedContact)
    {
        var contact = await _context.Contacts.FindAsync(contactId);
        if (contact == null) return (false, "Contact not found.");

        _context.Entry(contact).CurrentValues.SetValues(updatedContact);
        await _context.SaveChangesAsync();

        return (true, "Contact updated successfully.");
    }
}
