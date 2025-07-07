using API.Models;

namespace API.Repositories
{
    /// <summary>
    /// The interface for the Contact Repository
    /// </summary>
    public interface IContactRepository
    {
        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <returns><see cref="List<ContactModel>"></returns>
        List<ContactModel> GetAllContacts();

        /// <summary>
        /// Get all contacts by phoneNumber
        /// </summary>
        /// <param name="id" cref="string"></param>
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
        /// <param name="contact" cref="ContactModel"></param>
        /// <returns><see cref="ContactModel?"/></returns>
        ContactModel? CreateContact(ContactModel contact);

        /// <summary>
        /// Update a contact
        /// </summary>
        /// <param name="contact" cref="ContactModel"></param>
        /// <returns><see cref="ContactModel?"/></returns>
        ContactModel? UpdateContact(ContactModel contact);

        /// <summary>
        /// Delete a contact by an id
        /// </summary>
        /// <param name="phoneNumber" <see cref="string"/>></param>
        /// <returns></returns>
        bool DeleteContact(ContactModel phoneNumber);

        /// <summary>
        /// Check if a contact exists by phone number
        /// </summary>
        /// <param name="phoneNumber" cref="string">></param>
        /// <returns></returns>
        bool contactExists(string phoneNumber);
    }
}
