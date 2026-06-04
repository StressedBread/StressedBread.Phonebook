using Microsoft.EntityFrameworkCore;
using StressedBread.Phonebook.Data;
using StressedBread.Phonebook.Models;
using static StressedBread.Phonebook.Enums;

namespace StressedBread.Phonebook.Services;
public class ContactService(PhonebookContext context)
{
    public async Task<Result> AddContactAsync(Contact newContact)
    {
        context.Contacts.Add(newContact);

        try
        {
            await context.SaveChangesAsync();
            return Result.Success(ResultType.Success);
        }
        catch (DbUpdateException)
        {
            return Result.Failure(ResultType.DbUpdateError);
        }
    }

    public async Task<Result<List<Contact>>> GetAllContactsAsync()
    {
        var contactsList = await context.Contacts.ToListAsync();

        if (contactsList.Count == 0) return Result.Failure<List<Contact>>(ResultType.ContactsEmpty);
        return Result<List<Contact>>.Success(contactsList, ResultType.None);
    }

    public async Task<Result> DeleteContactAsync(int contactId)
    {
        var contact = await context.Contacts.FindAsync(contactId);
        if (contact == null) return Result.Failure(ResultType.ContactNotFound);

        context.Contacts.Remove(contact);

        try
        {
            await context.SaveChangesAsync();
            return Result.Success(ResultType.Success);
        }
        catch (DbUpdateException)
        {
            return Result.Failure(ResultType.DbUpdateError);
        }
    }

    public async Task<Result> UpdateContactAsync(int contactId, Contact updatedContact)
    {
        var contact = await context.Contacts.FindAsync(contactId);
        if (contact == null) return Result.Failure(ResultType.ContactNotFound);

        contact.Name = updatedContact.Name;
        contact.PhoneNumber = updatedContact.PhoneNumber;
        contact.Email = updatedContact.Email;

        try
        {
            await context.SaveChangesAsync();
            return Result.Success(ResultType.Success);
        }
        catch (DbUpdateException)
        {
            return Result.Failure(ResultType.DbUpdateError);
        }
    }
}
