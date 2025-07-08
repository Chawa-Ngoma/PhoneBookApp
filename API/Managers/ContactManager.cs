using API.Models;
using API.Repositories;

namespace API.Managers
{
    /// <summary>
    /// The contact manager that has business logic
    /// </summary>
    public class ContactManager : IContactManager
    {
        private readonly IContactRepository _contactRepository;
        public ContactManager(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        ///<inheritdoc cref="IContactManager.CreateContact(CreateContactRequest)"/>
        public ContactModel? CreateContact(CreateContactRequest request)
        {
            
            var contact = new ContactModel
            {
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                EmailAddress = request.EmailAddress,
                CreatedDate = DateTime.Now
            };

           return _contactRepository.CreateContact(contact);
        }

        ///<inheritdoc cref="IContactManager.DeleteContact(int)"/>
        public bool DeleteContact(int id)
        {
            var contact = _contactRepository.GetContactById(id);

            if (contact == null)
            {
                return false;
            }

            return _contactRepository.DeleteContact(contact);
        }

        ///<inheritdoc cref="IContactManager.GetAllContacts"/>
        public List<ContactModel> GetAllContacts()
        {
            return _contactRepository.GetAllContacts();
        }

        ///<inheritdoc cref="IContactManager.GetContactById(int)"/>
        public ContactModel? GetContactById(int id)
        {
            return _contactRepository.GetContactById(id);
        }

        ///<inheritdoc cref="IContactManager.SearchContact(string?, string?)"/>
        public List<ContactModel>? SearchContact(string? name, string? phoneNumber)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                return _contactRepository.SearchContact(name, null);  
            }

            else if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                return _contactRepository.SearchContact(null, phoneNumber);
            }

            return null;
        }

        ///<inheritdoc cref="IContactManager.UpdateContact(UpdateContactRequest)"/>
        public ContactModel? UpdateContact(UpdateContactRequest request)
        {
            var contact = _contactRepository.GetContactById(request.Id);

            if(contact == null)
            {
                return null;
            }

            contact.Name = request.Name;
            contact.PhoneNumber = request.PhoneNumber;
            contact.EmailAddress = request.EmailAddress;

            return _contactRepository.UpdateContact(contact);
            
        }
        ///<inheritdoc cref="IContactManager.ContactExists(string)"/>
        public bool ContactExists(string phoneNumber)
        {
            if (_contactRepository.ContactExists(phoneNumber))
                return true;

            return false;
        }

        ///<inheritdoc cref="IContactManager.ContactExists(string, int)"/>
        public bool ContactExists(string phoneNumber, int id)
        {
            if (_contactRepository.ContactExists(phoneNumber, id))
                return true;

            return false;
        }

    }
}
