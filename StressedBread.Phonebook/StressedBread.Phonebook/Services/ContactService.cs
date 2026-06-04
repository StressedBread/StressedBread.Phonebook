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

    public async Task<Result> AddContactAsync(Contact newContact)
    {
        _context.Contacts.Add(newContact);

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

    public async Task<Result<List<Contact>>> GetAllContactsAsync()
    {
        var contactsList = await _context.Contacts.ToListAsync();

        if (contactsList.Count == 0) return Result.Failure<List<Contact>>(ResultType.ContactsEmpty);
        return Result<List<Contact>>.Success(contactsList, ResultType.None);
    }

    public async Task<Result> DeleteContactAsync(int contactId)
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

    public async Task<Result> UpdateContactAsync(int contactId, Contact updatedContact)
    {
        var contact = await _context.Contacts.FindAsync(contactId);
        if (contact == null) return Result.Failure(ResultType.ContactNotFound);

        contact.Name = updatedContact.Name;
        contact.PhoneNumber = updatedContact.PhoneNumber;
        contact.Email = updatedContact.Email;

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
