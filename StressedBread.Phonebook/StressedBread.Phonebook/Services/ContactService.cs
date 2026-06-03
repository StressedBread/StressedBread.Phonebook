using Microsoft.EntityFrameworkCore;
using StressedBread.Phonebook.Data;
using StressedBread.Phonebook.Models;
using static StressedBread.Phonebook.Enums;

namespace StressedBread.Phonebook.Services;
public class ContactService
{
    private readonly PhonebookContext _context;
    public ContactService(PhonebookContext context)
    {
        _context = context;
    }

    internal async Task<Result> AddContactAsync((string Name, string PhoneNumber, string Email) newContact)
    {
        var contact = new Contact 
        { 
            Name = newContact.Name, 
            PhoneNumber = newContact.PhoneNumber, 
            Email = newContact.Email 
        };

        _context.Contacts.Add(contact);

        try
        {
            await _context.SaveChangesAsync();
            return Result.Success(ResultType.Success);
        }
        catch (DbUpdateException)
        {
            return Result.Failure(ResultType.DbUpdateError);
        }
    }

    internal async Task<Result<List<Contact>>> GetAllContactsAsync()
    {
        var contactsList = await _context.Contacts.ToListAsync();

        if (contactsList.Count == 0) return Result<List<Contact>>.Failure(ResultType.ContactsEmpty);
        return Result<List<Contact>>.Success(contactsList, ResultType.None);
    }

    internal async Task<Result> DeleteContactAsync(int contactId)
    {
        var contact = await _context.Contacts.FindAsync(contactId);
        if (contact == null) return Result.Failure(ResultType.ContactNotFound);

        _context.Contacts.Remove(contact);

        try
        {
            await _context.SaveChangesAsync();
            return Result.Success(ResultType.Success);
        }
        catch (DbUpdateException)
        {
            return Result.Failure(ResultType.DbUpdateError);
        }
    }

    internal async Task<Result> UpdateContactAsync(int contactId, Contact updatedContact)
    {
        var contact = await _context.Contacts.FindAsync(contactId);
        if (contact == null) return Result.Failure(ResultType.ContactNotFound);

        _context.Entry(contact).CurrentValues.SetValues(updatedContact);

        try
        {
            await _context.SaveChangesAsync();
            return Result.Success(ResultType.Success);
        }
        catch (DbUpdateException)
        {
            return Result.Failure(ResultType.DbUpdateError);
        }
    }
}
