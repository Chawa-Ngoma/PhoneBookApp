using API.Managers;
using API.Models;
using API.Repositories;
using Moq;

namespace API.Tests.Managers
{
    /// <summary>
    /// Tests for the Contact Manager
    /// </summary>
    public class ContactManagerTests
    {
        private readonly Mock<IContactRepository> _contactRepositoryMock;
        private readonly ContactManager _contactManager;

        public ContactManagerTests()
        {
            _contactRepositoryMock = new Mock<IContactRepository>();
            _contactManager = new ContactManager(_contactRepositoryMock.Object);
        }


        [Fact]
        public void AddContact_SavesNewContact()
        {
            //Arrange
            var contactRequest = new CreateContactRequest
            {
                Name = "Chawa",
                PhoneNumber = "0123456789",
                EmailAddress = "chawa@example.com",

            };

            _contactRepositoryMock.Setup(repo => repo.CreateContact(It.IsAny<ContactModel>()))
                          .Returns<ContactModel>(c => c);


            //Act
            var result = _contactManager.CreateContact(contactRequest);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Chawa", result.Name);
        }

        [Fact]
        public void DeleteContact_DeletesContactById()
        {
            var contact = new ContactModel { Name = "Chase", PhoneNumber = "12345678" };

            _contactRepositoryMock.Setup(repo => repo.GetContactById(contact.Id))
                         .Returns(contact);

            _contactRepositoryMock.Setup(repo => repo.DeleteContact(contact))
                             .Returns(true);

            //Act
            var result = _contactManager.DeleteContact(contact.Id);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void GetAllContacts_ReturnsContacts()
        {
            // Arrange
            var contacts = new List<ContactModel>
            {
                new ContactModel { Id = 1, Name = "Chase", PhoneNumber = "1234567890" },
                new ContactModel { Id = 2, Name = "Chant", PhoneNumber = "9876543210" }
            };

            _contactRepositoryMock.Setup(repo => repo.GetAllContacts())
                                  .Returns(contacts);

            // Act
            var result = _contactManager.GetAllContacts();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Chase", result[0].Name);
            Assert.Equal("Chant", result[1].Name);
        }

        [Fact]
        public void UpdateContact_ReturnsUpdatedContact()
        {
            // Arrange
            var existingContact = new ContactModel
            {
                Id = 1,
                Name = "Chawa",
                PhoneNumber = "0123456789",
                EmailAddress = "chawa@example.com",
            };

            var updateRequest = new UpdateContactRequest
            {
                Id = 1,
                Name = "Charles",
                PhoneNumber = "1234567890",
                EmailAddress = "new@example.com"
            };

            var updatedContact = new ContactModel
            {
                Id = 1,
                Name = updateRequest.Name,
                PhoneNumber = updateRequest.PhoneNumber,
                EmailAddress = updateRequest.EmailAddress
            };

            _contactRepositoryMock.Setup(repo => repo.GetContactById(updateRequest.Id))
                                  .Returns(existingContact);

            _contactRepositoryMock.Setup(repo => repo.UpdateContact(It.Is<ContactModel>(
                c => c.Id == updateRequest.Id &&
                     c.Name == updateRequest.Name &&
                     c.PhoneNumber == updateRequest.PhoneNumber &&
                     c.EmailAddress == updateRequest.EmailAddress)))
                .Returns(updatedContact);

            // Act
            var result = _contactManager.UpdateContact(updateRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Charles", result.Name);
            Assert.Equal("1234567890", result.PhoneNumber);
            Assert.Equal("new@example.com", result.EmailAddress);
        }

        [Fact]
        public void UpdateContact_ReturnsNull()
        {
            // Arrange
            var updateRequest = new UpdateContactRequest
            {
                Id = 1,
                Name = "Chawa",
                PhoneNumber = "1234567890",
                EmailAddress = "chawa@example.com"
            };

            _contactRepositoryMock.Setup(repo => repo.GetContactById(updateRequest.Id))
                                  .Returns((ContactModel?)null);

            // Act
            var result = _contactManager.UpdateContact(updateRequest);

            // Assert
            Assert.Null(result);
        }




    }
}
