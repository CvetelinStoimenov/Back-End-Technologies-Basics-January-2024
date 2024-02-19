using LibraryManagementSystem;

namespace LibraryManagementTest
{
    [TestFixture]
    public class LibraryTests
    {
        [Test]
        public void AddBook_ShouldAddANewBookToTheLibrary()
        {
            // Arrange
            Library library = new Library();
            var book = new Book
            {
                Id = 1,
                Title = "Test Title",
                Author = "Test Author",
                IsCheckedOut = false
            };

            // Act
            library.AddBook(book);

            // Assert
            var allBooks = library.GetAllBooks();
            Assert.That(allBooks.Count, Is.EqualTo(1));

            var singleBook = allBooks.First();
            Assert.That(singleBook.Id, Is.EqualTo(1));
            Assert.That(singleBook, Is.Not.Null);
            Assert.That(singleBook.Author, Is.EqualTo(book.Author));
            Assert.That(singleBook.Title, Is.EqualTo(book.Title));
            Assert.That(singleBook.IsCheckedOut, Is.EqualTo(false));
        }

        [Test]
        public void CheckOutBook_ShouldReturnTrue_IfWeCanCheckoutTheBook()
        {
            // Arrange
            Library library = new Library();
            var book = new Book
            {
                Id = 1,
                Title = "Test Title",
                Author = "Test Author",
                IsCheckedOut = false
            };
            library.AddBook(book);

            // Act
            var result = library.CheckOutBook(1);

            // Assert
            Assert.IsTrue(result);
            var allBooks = library.GetAllBooks();
            var singleBook = allBooks.First();
            Assert.IsTrue(singleBook.IsCheckedOut);
        }

        [Test]
        public void CheckOutBook_ShouldReturnFalse_IfBookDoesNotExist()
        {
            // Arrange
            Library library = new Library();
            var book = new Book
            {
                Id = 1,
                Title = "Test Title",
                Author = "Test Author",
                IsCheckedOut = false
            };
            library.AddBook(book);

            // Act
            var result = library.CheckOutBook(158);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CheckOutBook_ShouldReturnFalse_IfBookIsAlreadyCheckedOut()
        {
            // Arrange
            Library library = new Library();
            var book = new Book
            {
                Id = 1,
                Title = "Test Title",
                Author = "Test Author",
                IsCheckedOut = true
            };
            library.AddBook(book);

            // Act
            var result = library.CheckOutBook(1);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ReturnBook_ShouldReturnFalse_IfBookDoesNotExist()
        {
            // Arrange
            Library library = new Library();
            var book = new Book
            {
                Id = 1,
                Title = "Test Title",
                Author = "Test Author",
                IsCheckedOut = false
            };
            library.AddBook(book);

            // Act
            var result = library.ReturnBook(99);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ReturnBook_ShouldReturnFalse_IfBookIsNotCheckedOut()
        {
            // Arrange
            Library library = new Library();
            var book = new Book
            {
                Id = 1,
                Title = "Test Title",   
                Author = "Test Author",
                IsCheckedOut = false
            };
            library.AddBook(book);

            // Act
            var result = library.ReturnBook(1);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ReturnBook_ShouldReturnTrue_IfBookCanBeCheckedOut()
        {
            // Arrange
            var library = new Library();
            var newBook = new Book
            {
                Author = "Jane Doe",
                Id = 1,
                IsCheckedOut = true,
                Title = "Title",
            };

            library.AddBook(newBook);

            // Act
            var result = library.ReturnBook(1);

            // Assert
            Assert.IsTrue(result);
            var allBooks = library.GetAllBooks();
            var singleBook = allBooks.First();
            Assert.IsFalse(singleBook.IsCheckedOut);
        }
    }
}
