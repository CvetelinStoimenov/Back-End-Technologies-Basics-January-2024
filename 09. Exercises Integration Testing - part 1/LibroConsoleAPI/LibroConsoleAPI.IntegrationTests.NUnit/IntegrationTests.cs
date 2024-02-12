using LibroConsoleAPI.Business;
using LibroConsoleAPI.Business.Contracts;
using LibroConsoleAPI.Data.Models;
using LibroConsoleAPI.DataAccess;
using LibroConsoleAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibroConsoleAPI.IntegrationTests.NUnit
{
    public  class IntegrationTests
    {
        private TestLibroDbContext dbContext;
        private BookManager bookManager;

        [SetUp]
        public void SetUp()
        {
            string dbName = $"TestDb_{Guid.NewGuid()}";
            this.dbContext = new TestLibroDbContext(dbName);
            this.bookManager = new BookManager(new BookRepository(this.dbContext));
        }

        [TearDown]
        public void TearDown()
        {
            this.dbContext.Dispose();
        }

        [Test]
        public async Task AddBookAsync_ShouldAddBook()
        {
            // Arrange
            var newBook = new Book
            {
                Title = "Test Book",
                Author = "John Doe",
                ISBN = "1234567890123",
                YearPublished = 2021,
                Genre = "Fiction",
                Pages = 100,
                Price = 19.99
            };

            // Act
            await bookManager.AddAsync(newBook);

            // Assert
            var bookInDb = await dbContext.Books.FirstOrDefaultAsync(b => b.ISBN == newBook.ISBN);
            Assert.NotNull(bookInDb);
            Assert.That(bookInDb.Title, Is.EqualTo("Test Book"));
            Assert.AreEqual("John Doe", bookInDb.Author);
        }

        [Test]
        public async Task AddBookAsync_TryToAddBookWithInvalidCredentials_ShouldThrowException()
        {
            // Arrange
            var invalidBook = new Book
            {
                Title = new string ('A', 500),
                Author = "John Doe",
                ISBN = "1234567890123",
                YearPublished = 2021,
                Genre = "Fiction",
                Pages = 100,
                Price = 19.99
            };

            // Act and Assert
            var exception = Assert.ThrowsAsync<ValidationException>(() => bookManager.AddAsync(invalidBook));
            Assert.That(exception.Message, Is.EqualTo("Book is invalid."));

            // Using a method as a delegate
            {
                Assert.ThrowsAsync<ValidationException>(async () => await MethodThatThrows());
            }

            async Task MethodThatThrows()
            {
                await bookManager.AddAsync(invalidBook);
                throw new ValidationException("Book is invalid.");
            }
        }

        [Test]
        public async Task DeleteBookAsync_WithValidISBN_ShouldRemoveBookFromDb()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(dbContext, bookManager);

            // Act
            await bookManager.DeleteAsync("9780385487256");

            // Assert
            var bookInDb = await dbContext.Books.FirstOrDefaultAsync(b => b.ISBN == "9780385487256");
            Assert.Null(bookInDb);
            var booksInDbToList = dbContext.Books.ToList();
            Assert.That(booksInDbToList.Count, Is.EqualTo(9));
        }

        [Test]
        public async Task DeleteBookAsync_TryToDeleteWithNullOrWhiteSpaceISBN_ShouldThrowException()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(dbContext, bookManager);

            // Act & Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(() => bookManager.DeleteAsync(""));
            Assert.That(exception.Message, Is.EqualTo("ISBN cannot be empty."));
        }

        [Test]
        public async Task GetAllAsync_WhenBooksExist_ShouldReturnAllBooks()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(dbContext, bookManager);

            // Act
            var bookInDb = await bookManager.GetAllAsync();

            // Assert
            Assert.NotNull(bookInDb);
            Assert.IsNotEmpty(bookInDb);
            Assert.That(bookInDb.Count, Is.EqualTo(10));
            Assert.IsTrue(bookInDb.Any(b => b.Title == "The Martian"));
            Assert.IsTrue(bookInDb.Any(b => b.Author == "Patrick Rothfuss"));

            // Optionally, you can also check specific properties of the first book in the collection
            var firstBook = bookInDb.FirstOrDefault();
            Assert.NotNull(firstBook);
            Assert.That(firstBook.Title, Is.EqualTo("To Kill a Mockingbird"));
            Assert.That(firstBook.Author, Is.EqualTo("Harper Lee"));
            Assert.That(firstBook.Genre, Is.EqualTo("Novel"));
            Assert.That(firstBook.YearPublished, Is.EqualTo(1960));
            Assert.That(firstBook.Pages, Is.EqualTo(336));
            Assert.That(firstBook.Price, Is.EqualTo(10.99));
        }

        [Test]
        public async Task GetAllAsync_WhenNoBooksExist_ShouldThrowKeyNotFoundException()
        {
            // Act & Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => bookManager.GetAllAsync());
            Assert.ThrowsAsync<KeyNotFoundException>(() => bookManager.GetAllAsync());
            Assert.That(exception.Message, Is.EqualTo("No books found."));

            try
            {
                await bookManager.GetAllAsync();
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo("No books found."));
            }
        }

        [Test]
        public async Task SearchByTitleAsync_WithValidTitleFragment_ShouldReturnMatchingBooks()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(dbContext, bookManager);

            // Act
            var searchBookInDb = await bookManager.SearchByTitleAsync("1984");

            // Assert
            Assert.NotNull(searchBookInDb);
            Assert.IsNotEmpty(searchBookInDb);
            var firstBook = searchBookInDb.FirstOrDefault();
            Assert.Multiple(() =>
            {
                Assert.That(firstBook.ISBN, Is.EqualTo("9780312857753"));
                Assert.That(firstBook.Author, Is.EqualTo("George Orwell"));
                Assert.That(firstBook.Genre, Is.EqualTo("Dystopian Fiction"));
                Assert.That(firstBook.YearPublished, Is.EqualTo(1949));
                Assert.That(firstBook.Pages, Is.EqualTo(328));
                Assert.That(firstBook.Price, Is.EqualTo(9.99));
            });
        }

        [Test]
        public async Task SearchByTitleAsync_WithInvalidTitleFragment_ShouldThrowKeyNotFoundException()
        {
            // Act & Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(() => bookManager.SearchByTitleAsync(""));
            Assert.ThrowsAsync<ArgumentException>(() => bookManager.SearchByTitleAsync(""));
            Assert.That(exception.Message, Is.EqualTo("Title fragment cannot be empty."));

            try
            {
                await bookManager.SearchByTitleAsync("   ");
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Title fragment cannot be empty."));
            }
        }

        [Test]
        public async Task GetSpecificAsync_WithValidIsbn_ShouldReturnBook()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(dbContext, bookManager);

            // Act
            var searchBookInDb = await bookManager.GetSpecificAsync("9780312857753");

            // Assert
            Assert.NotNull(searchBookInDb);
            Assert.That(searchBookInDb.Title, Is.EqualTo("1984"));
            Assert.That(searchBookInDb.Author, Is.EqualTo("George Orwell"));
            Assert.That(searchBookInDb.YearPublished, Is.EqualTo(1949));
            Assert.That(searchBookInDb.Genre, Is.EqualTo("Dystopian Fiction"));
            Assert.That(searchBookInDb.Price, Is.EqualTo(9.99));
            Assert.That(searchBookInDb.Pages, Is.EqualTo(328));
        }

        [Test]
        public async Task GetSpecificAsync_WithInvalidIsbn_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(dbContext, bookManager);

            // Act & Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => bookManager.GetSpecificAsync("sdasda"));
            Assert.ThrowsAsync<KeyNotFoundException>(() => bookManager.GetSpecificAsync("sdasda"));
            Assert.That(exception.Message, Is.EqualTo("No book found with ISBN: sdasda"));

            try
            {
                await bookManager.GetSpecificAsync("   ");
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo("ISBN cannot be empty."));
            }
        }

        [Test]
        public async Task UpdateAsync_WithValidBook_ShouldUpdateBook()
        {
            var newBook = new Book
            {
                Title = "Test Book",
                Author = "John Doe",
                ISBN = "1234567890123",
                YearPublished = 2021,
                Genre = "Fiction",
                Pages = 100,
                Price = 19.99
            };
            await bookManager.UpdateAsync(newBook);
            newBook.Title = "UpdatedTitle";

            // Act
            await bookManager.UpdateAsync(newBook);

            // Assert
            Assert.NotNull(newBook);
            Assert.That(newBook.Title, Is.EqualTo("UpdatedTitle"));
            var bookInDb = dbContext.Books.FirstOrDefault(b => b.Title == "UpdatedTitle");
        }

        [Test]
        public async Task UpdateAsync_WithInvalidBook_ShouldThrowValidationException()
        {
            // Act
            var newBook = new Book
            {

            };

            //Assert
            var exception = Assert.ThrowsAsync<ValidationException>(() => bookManager.UpdateAsync(newBook));
            Assert.ThrowsAsync<ValidationException>(() => bookManager.UpdateAsync(newBook));
            Assert.That(exception.Message, Is.EqualTo("Book is invalid."));
            try
            {
                await bookManager.UpdateAsync(newBook);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Book is invalid."));
            }
        }
    }
}
