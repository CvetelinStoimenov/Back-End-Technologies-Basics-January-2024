using ContactsConsoleAPI.Business;
using ContactsConsoleAPI.Business.Contracts;
using ContactsConsoleAPI.Data.Models;
using ContactsConsoleAPI.DataAccess;
using ContactsConsoleAPI.DataAccess.Contrackts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsConsoleAPI.IntegrationTests.NUnit
{
    public class IntegrationTests
    {
        private TestContactDbContext dbContext;
        private IContactManager contactManager;

        [SetUp]
        public void SetUp()
        {
            this.dbContext = new TestContactDbContext();
            this.contactManager = new ContactManager(new ContactRepository(this.dbContext));
        }


        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.Dispose();
        }


        //positive test
        [Test]
        public async Task AddContactAsync_ShouldAddNewContact()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            var dbContact = await dbContext.Contacts.FirstOrDefaultAsync(c => c.Contact_ULID == newContact.Contact_ULID);

            Assert.NotNull(dbContact);
            Assert.That(dbContact.FirstName, Is.EqualTo(newContact.FirstName));
            Assert.That(dbContact.LastName, Is.EqualTo(newContact.LastName));
            Assert.That(dbContact.Phone, Is.EqualTo(newContact.Phone));
            Assert.That(dbContact.Email, Is.EqualTo(newContact.Email));
            Assert.That(dbContact.Address, Is.EqualTo(newContact.Address));
            Assert.That(dbContact.Contact_ULID, Is.EqualTo(newContact.Contact_ULID));
        }

        //Negative test
        [Test]
        public async Task AddContactAsync_TryToAddContactWithInvalidCredentials_ShouldThrowException()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "invalid_Mail", //invalid email
                Gender = "Male",
                Phone = "0889933779"
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await contactManager.AddAsync(newContact));
            var actual = await dbContext.Contacts.FirstOrDefaultAsync(c => c.Contact_ULID == newContact.Contact_ULID);

            Assert.IsNull(actual);
            Assert.That(ex?.Message, Is.EqualTo("Invalid contact!"));

        }

        [Test]
        public async Task DeleteContactAsync_WithValidULID_ShouldRemoveContactFromDb()
        {
            // Arrange
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", // Must be minimum 10 symbols - numbers or Upper case letters
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };
            await contactManager.AddAsync(newContact);

            // Act
            await contactManager.DeleteAsync(newContact.Contact_ULID);

            // Assert
            var dbContact = await dbContext.Contacts.FirstOrDefaultAsync(c => c.Contact_ULID == newContact.Contact_ULID);
            Assert.Null(dbContact);
        }

        [Test]
        public async Task DeleteContactAsync_TryToDeleteWithNullOrWhiteSpaceULID_ShouldThrowException()
        {
            // Arrange
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", // Must be minimum 10 symbols - numbers or Upper case letters
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };
            await contactManager.AddAsync(newContact);

            // Act and Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await contactManager.DeleteAsync(null));
            Assert.That(ex?.Message, Is.EqualTo("ULID cannot be empty."));
        }

        [Test]
        public async Task GetAllAsync_WhenContactsExist_ShouldReturnAllContacts()
        {
            // Arrange
            var newContact1 = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", // Must be minimum 10 symbols - numbers or Upper case letters
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            var newContact2 = new Contact()
            {
                FirstName = "SecondTestFirstName",
                LastName = "SecondTestLastName",
                Address = "Second Anything for testing address",
                Contact_ULID = "1ADERBC23456HH", // Must be minimum 10 symbols - numbers or Upper case letters
                Email = "secondtest@gmail.com",
                Gender = "Male",
                Phone = "0987933779"
            };
            await contactManager.AddAsync(newContact1);
            await contactManager.AddAsync(newContact2);

            // Act
            var contacts = await contactManager.GetAllAsync();

            // Assert
            Assert.NotNull(contacts);
            Assert.That(contacts.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllAsync_WhenNoContactsExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act and Assert
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () => await contactManager.GetAllAsync());
            Assert.That(ex?.Message, Is.EqualTo("No contact found."));
        }

        [Test]
        public async Task SearchByFirstNameAsync_WithExistingFirstName_ShouldReturnMatchingContacts()
        {
            // Arrange
            var newContact1 = new Contact() 
            { 
                FirstName = "TestFirstName1",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };
            var newContact2 = new Contact() 
            { 
                FirstName = "TestFirstName2",
                LastName = "SecondTestLastName",
                Address = "Second Anything for testing address",
                Contact_ULID = "1ADERBC23456HH",
                Email = "secondtest@gmail.com",
                Gender = "Male",
                Phone = "0987933779"
            };
            await contactManager.AddAsync(newContact1);
            await contactManager.AddAsync(newContact2);

            // Act
            var contacts = await contactManager.SearchByFirstNameAsync("TestFirstName1");

            // Assert
            Assert.NotNull(contacts);
            Assert.That(contacts.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task SearchByFirstNameAsync_WithNonExistingFirstName_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act and Assert
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () => await contactManager.SearchByFirstNameAsync("NonExistingName"));
            Assert.That(ex?.Message, Is.EqualTo("No contact found with the given first name."));

        }

        [Test]
        public async Task SearchByLastNameAsync_WithExistingLastName_ShouldReturnMatchingContacts()
        {
            // Arrange
            var newContact1 = new Contact() 
            { 
                FirstName = "TestFirstName1",
                LastName = "TestLastName1",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };
            var newContact2 = new Contact() 
            { 
                FirstName = "TestFirstName2",
                LastName = "TestLastName2",
                Address = "Second Anything for testing address",
                Contact_ULID = "1ADERBC23456HH",
                Email = "secondtest@gmail.com",
                Gender = "Male",
                Phone = "0987933779"
            };
            await contactManager.AddAsync(newContact1);
            await contactManager.AddAsync(newContact2);

            // Act
            var contacts = await contactManager.SearchByLastNameAsync("TestLastName1");

            // Assert
            Assert.NotNull(contacts);
            Assert.That(contacts.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task SearchByLastNameAsync_WithNonExistingLastName_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act and Assert
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () => await contactManager.SearchByLastNameAsync("NonExistingName"));
            Assert.That(ex?.Message, Is.EqualTo("No contact found with the given last name."));

        }

        [Test]
        public async Task GetSpecificAsync_WithValidULID_ShouldReturnContact()
        {
            // Arrange
            var newContact = new Contact()
            {
                FirstName = "TestFirstName1",
                LastName = "TestLastName1",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"// Must be minimum 10 symbols - numbers or Upper case letters
            };
            await contactManager.AddAsync(newContact);

            // Act
            var contact = await contactManager.GetSpecificAsync("1ABC23456HH");

            // Assert
            Assert.NotNull(contact);
            Assert.That(contact.Contact_ULID, Is.EqualTo("1ABC23456HH"));
        }

        [Test]
        public async Task GetSpecificAsync_WithInvalidULID_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act and Assert
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () => await contactManager.GetSpecificAsync("InvalidULID"));
            Assert.That(ex?.Message, Is.EqualTo("No contact found with ULID: InvalidULID"));

        }

        [Test]
        public async Task UpdateAsync_WithValidContact_ShouldUpdateContact()
        {
            // Arrange
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", // Must be minimum 10 symbols - numbers or Upper case letters
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };
            await contactManager.AddAsync(newContact);

            // Update contact details
            newContact.FirstName = "UpdatedFirstName";
            newContact.LastName = "UpdatedLastName";
            newContact.Phone = "UpdatedPhone";
            newContact.Email = "updated@gmail.com";

            // Act
            await contactManager.UpdateAsync(newContact);

            // Assert
            var updatedContact = await dbContext.Contacts.FirstOrDefaultAsync(c => c.Contact_ULID == newContact.Contact_ULID);
            Assert.NotNull(updatedContact);
            Assert.That(updatedContact.FirstName, Is.EqualTo("UpdatedFirstName"));
            Assert.That(updatedContact.LastName, Is.EqualTo("UpdatedLastName"));
            Assert.That(updatedContact.Phone, Is.EqualTo("UpdatedPhone"));
            Assert.That(updatedContact.Email, Is.EqualTo("updated@gmail.com"));
        }

        [Test]
        public async Task UpdateAsync_WithInvalidContact_ShouldThrowValidationException()
        {
            // Arrange
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", // Must be minimum 10 symbols - numbers or Upper case letters
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };
            await contactManager.AddAsync(newContact);

            // Attempt to update contact with invalid email
            newContact.Email = "invalid_Email";

            // Act and Assert
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await contactManager.UpdateAsync(newContact));
            Assert.That(ex?.Message, Is.EqualTo("Invalid contact!"));
        }
    }
}
