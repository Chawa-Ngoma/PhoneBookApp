using API.Managers;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Contact Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactManager _contactManager;

        public ContactController(IContactManager contactManager)
        {
            _contactManager = contactManager;
        }

        /// <summary>
        /// A method to get all contacts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllContacts()
        {
            var contacts = _contactManager.GetAllContacts();
            return Ok(contacts);
        }

        /// <summary>
        /// A method to get a contact by an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetContactsById(int id)
        {
            var contact = _contactManager.GetContactById(id);
            if (contact == null) return NotFound();

            return Ok(contact);
        }

        /// <summary>
        /// A method to create a new contact
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateContact([FromBody] CreateContactRequest request)
        {
            if (_contactManager.ContactExists(request.PhoneNumber))
                return BadRequest("This number already exists");

            var newContact = _contactManager.CreateContact(request);

            return Ok(newContact);
        }

        /// <summary>
        /// A method to update a contact by an id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, [FromBody] UpdateContactRequest request)
        {
            if (request.Id != id)
                return NotFound();

            if (_contactManager.ContactExists(request.PhoneNumber, request.Id))
                return BadRequest("This number already exists");

            var updateContact = _contactManager.UpdateContact(request);
            if (updateContact == null) return NotFound();

            return Ok(updateContact);
        }

        /// <summary>
        /// A method to delete a contact by an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _contactManager.DeleteContact(id);

            if (!success) return NotFound();

            return Ok();
        }

        /// <summary>
        /// A method to search for a contact
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string? name, [FromQuery] string? phone)
        {
            var results = _contactManager.SearchContact(name, phone);
            return Ok(results);
        }
    }
}
