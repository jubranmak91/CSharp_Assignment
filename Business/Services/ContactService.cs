using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class ContactService : IContactService
{
    private readonly IFileService _fileService;
    private List<Contact> _contacts = new();

    public ContactService(IFileService fileService)
    {
        _fileService = fileService;
    }

    public bool AddContact(Contact contact)
    {
        // Jag bestämt mig validerar bara e-post här eftersom den fullständiga valideringen redan görs i MenuDialog, enklare koder ! 
        if (string.IsNullOrEmpty(contact.Email))
            return false;  

        
        _contacts.Add(contact);

        
        _fileService.SaveListToFile(_contacts);

        return true;  
    }

    public IEnumerable<Contact> GetAllContacts()
    {
        _contacts = _fileService.LoadListFromFile();
        return _contacts;
    }
}
