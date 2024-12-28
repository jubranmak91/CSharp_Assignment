using Business.Models;
using Business.Services;
using Business.Interfaces;
using Xunit;

namespace Business.Tests.Services
{
    public class ContactService_Tests
    {
        private readonly ContactService _contactService;
        private readonly IFileService _fileService;

        public ContactService_Tests()
        {
            // Startar kontakt och filhantering.
            _fileService = new FileService();
            _contactService = new ContactService(_fileService);
        }

        [Fact]
        // Kollar att en giltig kontakt läggs till utan något problem typ.
        public void AddContact_ValidKontakt_ReturnsTrue()
        {
            var contact = new Contact
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Address = "123 Test St",
                PostalCode = "12345",
                City = "Test City"
            };

            bool result = _contactService.AddContact(contact);

            Assert.True(result);
        }

        [Fact]
        // Kollar att sparade kontakter går att hämta från listan.
        public void GetAllContacts_WithKontakter_ReturnList()
        {
            var contact = new Contact
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                PhoneNumber = "0987654321",
                Address = "456 Test Ave",
                PostalCode = "54321",
                City = "Test City"
            };
            _contactService.AddContact(contact);

            var result = _contactService.GetAllContacts();

            Assert.NotEmpty(result);
        }

        [Fact]
        // Kollar att en kontakt med tom e-post inte läggs till.
        public void AddContact_InvalidKontakt_ReturnFalse()
        {
            var contact = new Contact
            {
                FirstName = "Invalid",
                LastName = "Contact",
                Email = "", // här saknar e-post
                PhoneNumber = "1234567890",
                Address = "Test St",
                PostalCode = "12345",
                City = "Test City"
            };

            bool result = _contactService.AddContact(contact);

            Assert.False(result);
        }
    }
}
