using Business.Models;
using Business.Services;
using Business.Interfaces;
using Xunit;


namespace Business.Tests.Services
{
    public class FileService_Tests
    {
        private readonly IFileService _fileService;

        public FileService_Tests()
        {
            // startar filhantering.
            _fileService = new FileService();
        }

        [Fact]
        // Kollar att en fil skapas när jag sparar contacter.
        public void SaveListToFile_ShouldCreateFile_WhenContactsAreSaved()
        {
            var contacts = new List<Contact>
            {
                new Contact
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    PhoneNumber = "1234567890",
                    Address = "123 Test St",
                    PostalCode = "12345",
                    City = "Test City"
                }
            };

            _fileService.SaveListToFile(contacts);

            Assert.True(File.Exists("Data/contacts.json"));
        }

        [Fact]
        // Kollar att kontakter kan läsas från filen.
        public void LoadListFromFile_ShouldReturnListOfContacts_WhenFileExists()
        {
            var contacts = new List<Contact>
            {
                new Contact
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane.doe@example.com",
                    PhoneNumber = "0987654321",
                    Address = "456 Test Ave",
                    PostalCode = "54321",
                    City = "Test City"
                }
            };
            _fileService.SaveListToFile(contacts);

            var result = _fileService.LoadListFromFile();

            Assert.NotEmpty(result);
        }

        [Fact]
        // Kollar att en tom lista hämtas om filen är tom. 
        public void LoadListFromFile_ShouldReturnEmptyList_WhenFileIsEmpty()
        {
            var fileService = new FileService("Data", "empty_contacts.json");
            File.WriteAllText("Data/empty_contacts.json", "[]");

            var result = fileService.LoadListFromFile();

            Assert.Empty(result);
        }

        [Fact]
        // Kollar att en korrupt fil ger tom lista.  

        public void LoadListFromFile_ShouldReturnEmptyList_WhenFileIsCorrupted()
        {
            var fileService = new FileService("Data", "corrupted_contacts.json");
            File.WriteAllText("Data/corrupted_contacts.json", "{Invalid JSON}");

            var result = fileService.LoadListFromFile();

            Assert.Empty(result);
        }

        [Fact]
        // Kollar att en ny mapp skapas automatiskt om den inte finns.
        public void SaveListToFile_ShouldCreateDirectory_WhenDirectoryDoesNotExist()
        {
            var directoryPath = "NonExistingDirectory";
            var fileService = new FileService(directoryPath);
            var contact = new Contact { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            var contacts = new List<Contact> { contact };

            fileService.SaveListToFile(contacts);

            Assert.True(Directory.Exists(directoryPath));
        }
    }
}
