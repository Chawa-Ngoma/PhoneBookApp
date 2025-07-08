using API.Data;
using API.Models;

namespace API.Repositories
{
    /// <summary>
    /// The DB logic 
    /// </summary>
    public sealed class ContactRepository : IContactRepository
    {
        private readonly ContactDbContext _dbContext;

        public ContactRepository(ContactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ContactModel? CreateContact(ContactModel contact)
        {
            _dbContext.Contacts.Add(contact);
            _dbContext.SaveChanges();

            return contact; 
        }

        public bool DeleteContact(ContactModel contact)
        {
            _dbContext.Remove(contact);
            _dbContext.SaveChanges();

            return true;
        }

        public List<ContactModel> GetAllContacts()
        {
           return _dbContext.Contacts
            .OrderByDescending(c => c.Id).ToList();
        }

        public ContactModel? GetContactById(int id)
        {
            return _dbContext.Contacts.Find(id);
        }

        public List<ContactModel> SearchContact(string? name, string? phoneNumber)
        {
            var contacts = _dbContext.Contacts.ToList();

            if (!string.IsNullOrWhiteSpace(name))
            {
                contacts = contacts.Where(c => c.Name.Contains(name)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                contacts = contacts.Where(c => c.PhoneNumber.Contains(phoneNumber)).ToList();
            }

            return contacts;
        }

        public ContactModel? UpdateContact(ContactModel contact)
        {
            _dbContext.SaveChanges();

            return contact;
        }

        public bool ContactExists(string phoneNumber)
        {
            return _dbContext.Contacts.Any(c => c.PhoneNumber == phoneNumber);
        }

        public bool ContactExists(string phoneNumber, int id)
        {
            return _dbContext.Contacts.Any(c => c.PhoneNumber == phoneNumber && c.Id != id);
        }

       
    }
}
