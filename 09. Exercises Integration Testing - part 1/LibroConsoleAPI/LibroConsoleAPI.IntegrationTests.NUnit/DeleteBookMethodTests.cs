using LibroConsoleAPI.Business;
using LibroConsoleAPI.DataAccess;
using LibroConsoleAPI.Repositories;

namespace LibroConsoleAPI.IntegrationTests.NUnit
{
    internal class DeleteBookMethodTests
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
        public async Task DeleteBookAsync_WithValidISBN_ShouldRemoveBookFromDb()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(dbContext, bookManager);

            // Act
            await bookManager.DeleteAsync("9780385487256");

            // Assert
            var bookInDb = dbContext.Books.ToList();
            Assert.That(bookInDb.Count, Is.EqualTo(9));
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
    }
}
