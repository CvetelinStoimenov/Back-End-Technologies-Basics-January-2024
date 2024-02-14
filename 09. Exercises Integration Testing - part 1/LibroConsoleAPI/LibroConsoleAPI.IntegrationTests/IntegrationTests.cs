using LibroConsoleAPI.Business;
using LibroConsoleAPI.Business.Contracts;
using LibroConsoleAPI.Data.Models;
using LibroConsoleAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;

namespace LibroConsoleAPI.IntegrationTests
{
    public class IntegrationTests : IClassFixture<BookManagerFixture>
    {
        private readonly BookManagerFixture _fixture;
        private readonly BookManager _bookManager;
        private readonly TestLibroDbContext _dbContext;

        public IntegrationTests()
        {
            _fixture = new BookManagerFixture();
            _bookManager = _fixture.BookManager;
            _dbContext = _fixture.DbContext;
        }

        [Fact]
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
            await _bookManager.AddAsync(newBook);

            // Assert
            var bookInDb = await _dbContext.Books.FirstOrDefaultAsync(b => b.ISBN == newBook.ISBN);
            Assert.NotNull(bookInDb);
            Assert.Equal("Test Book", bookInDb.Title);
            Assert.Equal("John Doe", bookInDb.Author);
        }

        [Fact]
        public async Task AddBookAsync_TryToAddBookWithInvalidCredentials_ShouldThrowException()
        {
            // Arrange
            var invalidBook = new Book
            {
                Title = new string('A', 500),
                Author = "John Doe",
                ISBN = "1234567890123",
                YearPublished = 2021,
                Genre = "Fiction",
                Pages = 100,
                Price = 19.99
            };

            // Act & Assert
            var exception = Assert.ThrowsAsync<ValidationException>(() => _bookManager.AddAsync(invalidBook));
            Assert.Equal(("Book is invalid."), exception.Result.Message);
            await Assert.ThrowsAsync<ValidationException>(() => _bookManager.AddAsync(invalidBook));

        }

        [Fact]
        public async Task DeleteBookAsync_WithValidISBN_ShouldRemoveBookFromDb()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);

            // Act
            await _bookManager.DeleteAsync("9780385487256");

            // Assert
            var bookInDb = await _dbContext.Books.FirstOrDefaultAsync(b => b.ISBN == "9780385487256");
            Assert.Null(bookInDb);
            var bookInDbToList = await _dbContext.Books.ToListAsync();
            Assert.Equal(9, bookInDbToList.Count);
        }

        [Fact]
        public async Task DeleteBookAsync_TryToDeleteWithNullOrWhiteSpaceISBN_ShouldThrowException()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);

            // Act & Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(() => _bookManager.DeleteAsync(""));
            Assert.Equal(("ISBN cannot be empty."), exception.Result.Message);
            await Assert.ThrowsAsync<ArgumentException>(() => _bookManager.DeleteAsync(""));
            var bookInDbToList = await _dbContext.Books.ToListAsync();
            Assert.Equal(10, bookInDbToList.Count);
        }

        [Fact]
        public async Task GetAllAsync_WhenBooksExist_ShouldReturnAllBooks()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);

            // Act
            var bookInDb = await _bookManager.GetAllAsync();

            // Assert
            Assert.NotNull(bookInDb);
            Assert.NotEmpty(bookInDb);
            Assert.Contains(bookInDb, b => b.Title == "The Martian");
            Assert.Contains(bookInDb, b => b.Author == "Patrick Rothfuss");
            var bookInDbToList = await _dbContext.Books.ToListAsync();
            Assert.Equal(10, bookInDbToList.Count);

            // Optionally, you can also check specific properties of the first book in the collection
            var firstBook = bookInDb.FirstOrDefault();
            Assert.NotNull(firstBook);
            Assert.Equal(("To Kill a Mockingbird"), firstBook.Title);
            Assert.Equal(("Harper Lee"), firstBook.Author);
            Assert.Equal(("Novel"), firstBook.Genre);
            Assert.Equal((1960), firstBook.YearPublished);
            Assert.Equal((336), firstBook.Pages);
            Assert.Equal((10.99), firstBook.Price);
        }

        [Fact]
        public async Task GetAllAsync_WhenNoBooksExist_ShouldThrowKeyNotFoundException()
        {
            // Act & Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => _bookManager.GetAllAsync());
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _bookManager.GetAllAsync());
            Assert.Equal(("No books found."), exception.Result.Message);
        }

        [Fact]
        public async Task SearchByTitleAsync_WithValidTitleFragment_ShouldReturnMatchingBooks()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);

            // Act
            var searchBookInDb = await _bookManager.SearchByTitleAsync("1984");

            // Assert
            Assert.NotNull(searchBookInDb);
            Assert.NotEmpty(searchBookInDb);
            Assert.Contains(searchBookInDb, b => b.Author == "George Orwell");
            Assert.Contains(searchBookInDb, b => b.ISBN == "9780312857753");
            Assert.Contains(searchBookInDb, b => b.YearPublished == 1949);
            Assert.Contains(searchBookInDb, b => b.Genre == "Dystopian Fiction");
            Assert.Contains(searchBookInDb, b => b.Price == 9.99);
            Assert.Contains(searchBookInDb, b => b.Pages == 328);
        }

        [Fact]
        public async Task SearchByTitleAsync_WithInvalidTitleFragment_ShouldThrowKeyNotFoundException()
        {
            // Act & Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(() => _bookManager.SearchByTitleAsync(""));
            await Assert.ThrowsAsync<ArgumentException>(() => _bookManager.SearchByTitleAsync(""));
            Assert.Equal("Title fragment cannot be empty.", exception.Result.Message);

            try
            {
                await _bookManager.SearchByTitleAsync("   ");
            }
            catch (Exception ex)
            {
                Assert.Equal("Title fragment cannot be empty.", ex.Message);
            }
        }

        [Fact]
        public async Task GetSpecificAsync_WithValidIsbn_ShouldReturnBook()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);

            // Act
            var searchBookInDb = await _bookManager.GetSpecificAsync("9780312857753");

            // Assert
            Assert.NotNull(searchBookInDb);
            Assert.Equal("1984", searchBookInDb.Title);
            Assert.Equal("George Orwell", searchBookInDb.Author);
            Assert.Equal(1949, searchBookInDb.YearPublished);
            Assert.Equal("Dystopian Fiction", searchBookInDb.Genre);
            Assert.Equal(9.99, searchBookInDb.Price);
            Assert.Equal(328, searchBookInDb.Pages);
        }

        [Fact]
        public async Task GetSpecificAsync_WithInvalidIsbn_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);

            // Act & Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => _bookManager.GetSpecificAsync("sdasda"));
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _bookManager.GetSpecificAsync("sdasda"));
            Assert.Equal("No book found with ISBN: sdasda", exception.Result.Message);

            try
            {
                await _bookManager.GetSpecificAsync("   ");
            }
            catch (Exception ex)
            {
                Assert.Equal("ISBN cannot be empty.", ex.Message);
            }
        }

        [Fact]
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
            await _bookManager.UpdateAsync(newBook);
            newBook.Title = "UpdatedTitle";

            // Act
            await _bookManager.UpdateAsync(newBook);

            // Assert
            Assert.NotNull(newBook);
            Assert.Equal("UpdatedTitle", newBook.Title);
            var bookInDb = _dbContext.Books.FirstOrDefault(b => b.Title == "UpdatedTitle");
        }

        [Fact]
        public async Task UpdateAsync_WithInvalidBook_ShouldThrowValidationException()
        {
            // Act
            var newBook = new Book
            {

            };

            //Assert
            var exception = Assert.ThrowsAsync<ValidationException>(() => _bookManager.UpdateAsync(newBook));
            await Assert.ThrowsAsync<ValidationException>(() => _bookManager.UpdateAsync(newBook));
            Assert.Equal(("Book is invalid."), exception.Result.Message);
            try
            {
                await _bookManager.UpdateAsync(newBook);
            }
            catch (Exception ex)
            {
                Assert.Equal(("Book is invalid."), ex.Message);
            }
        }

    }
}
