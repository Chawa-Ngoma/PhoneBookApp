using API.Models;

namespace API.Managers
{
    /// <summary>
    /// Interface for the contact manager
    /// </summary>
    public interface IContactManager
    {
        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <returns><see cref="List<ContactModel>"></returns>
        List<ContactModel> GetAllContacts();

        /// <summary>
        /// Get all contacts by phoneNumber
        /// </summary>
        /// <param name="id" cref="int"></param>
        /// <returns><see cref="ContactModel?"/></returns>
        ContactModel? GetContactById(int id);

        /// <summary>
        /// Search for a contact by name or phone number
        /// </summary>
        /// <param name="name" cref="string"></param>
        /// <param name="phoneNumber" cref="string"></param>
        /// <returns><see cref="List<ContactModel>"></returns>
        List<ContactModel>? SearchContact(string? name, string? phoneNumber);

        /// <summary>
        /// Create a contact
        /// </summary>
        /// <param name="request" cref="CreateContactRequest"></param>
        /// <returns><see cref="ContactModel?"/></returns>
        ContactModel? CreateContact(CreateContactRequest request);

        /// <summary>
        /// Update a contact
        /// </summary>
        /// <param name="request" cref="UpdateContactRequest"></param>
        /// <returns><see cref="ContactModel?"/></returns>
        ContactModel? UpdateContact(UpdateContactRequest request);

        /// <summary>
        /// Delete a contact by an id
        /// </summary>
        /// <param name="phoneNumber" <see cref="string"/>></param>
        /// <returns></returns>
        bool DeleteContact(int id);

        /// <summary>
        /// Check if a contact exists by phone number
        /// </summary>
        /// <param name="phoneNumber" cref="string">></param>
        /// <returns></returns>
        bool ContactExists(string phoneNumber);

        /// <summary>
        /// Check if a contact exists by phone number and excludes Id
        /// </summary>
        /// <param name="phoneNumber" cref="string">></param>
        /// <param name="id" cref="int">></param>
        /// <returns></returns>
        bool ContactExists(string phoneNumber, int id);

    }
}
