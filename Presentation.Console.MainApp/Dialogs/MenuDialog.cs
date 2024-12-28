using Business.Interfaces;
using Business.Models;

namespace MainApp.Dialogs;

public class MenuDialog
{
    private readonly IContactService _contactService;

    public MenuDialog(IContactService contactService)
    {
        _contactService = contactService;
    }

    public void ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Hello And Welcome :)");
            Console.WriteLine("---------------------------");
            Console.WriteLine("1. List Of All Contacts");
            Console.WriteLine("2. Create a New Contact");
            Console.WriteLine("3. Quit");

            string option = Console.ReadLine() ?? string.Empty;

            switch (option)
            {
                case "1":
                    ListContacts();
                    break;
                case "2":
                    CreateContact();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Error, Choose one of the following options only!!");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private void ListContacts()
    {
        var contacts = _contactService.GetAllContacts();
        if (!contacts.Any())
        {
            Console.WriteLine("No contacts found.");
        }
        else
        {
            foreach (var contact in contacts)
            {
                Console.WriteLine($"Id: {contact.Id}");
                Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
                Console.WriteLine($"Email: {contact.Email}");
                Console.WriteLine($"Phone: {contact.PhoneNumber}");
                Console.WriteLine($"Address: {contact.Address}");
                Console.WriteLine($"Postal Code: {contact.PostalCode}");
                Console.WriteLine($"City: {contact.City}");
                Console.WriteLine("---------------------------");
            }
        }
        Console.ReadKey();
    }

    private void CreateContact()
    {
        Console.Clear();
        Console.WriteLine("Creating a new contact.");

        string firstName;
        do
        {
            Console.Write("Enter first name: ");
            firstName = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("Cannot be empty.");
            }
        } while (string.IsNullOrWhiteSpace(firstName));

        string lastName;
        do
        {
            Console.Write("Enter last name: ");
            lastName = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Cannot be empty.");
            }
        } while (string.IsNullOrWhiteSpace(lastName));

        string email;
        do
        {
            Console.Write("Enter email: ");
            email = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Cannot be empty.");
            }
        } while (string.IsNullOrWhiteSpace(email));

        string phoneNumber;
        do
        {
            Console.Write("Enter phone number: ");
            phoneNumber = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                Console.WriteLine("Cannot be empty.");
            }
        } while (string.IsNullOrWhiteSpace(phoneNumber));

        string address;
        do
        {
            Console.Write("Enter address: ");
            address = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(address))
            {
                Console.WriteLine("Cannot be empty.");
            }
        } while (string.IsNullOrWhiteSpace(address));

        string postalCode;
        do
        {
            Console.Write("Enter postal code: ");
            postalCode = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(postalCode))
            {
                Console.WriteLine("Cannot be empty.");
            }
        } while (string.IsNullOrWhiteSpace(postalCode));

        string city;
        do
        {
            Console.Write("Enter city: ");
            city = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(city))
            {
                Console.WriteLine("Cannot be empty.");
            }
        } while (string.IsNullOrWhiteSpace(city));

     
        var contact = new Contact
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = address,
            PostalCode = postalCode,
            City = city
        };

        bool success = _contactService.AddContact(contact);

        if (success)
        {
            Console.WriteLine($"Your ID is: {contact.Id}");
        }
        else
        {
            Console.WriteLine("Failed to add contact. Please check !");
        }

        Console.ReadKey();
    }
}
