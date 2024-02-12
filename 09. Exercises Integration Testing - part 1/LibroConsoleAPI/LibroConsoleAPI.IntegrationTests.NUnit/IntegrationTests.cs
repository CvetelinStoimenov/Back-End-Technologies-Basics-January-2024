﻿using LibroConsoleAPI.Business;
using LibroConsoleAPI.Business.Contracts;
using LibroConsoleAPI.Data.Models;
using LibroConsoleAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Assert.AreEqual("Test Book", bookInDb.Title);
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
            var newBook = new Book
            {
                Title = new string("Test Book"),
                Author = "John Doe",
                ISBN = "1234567890123",
                YearPublished = 2021,
                Genre = "Fiction",
                Pages = 100,
                Price = 19.99
            };
            await bookManager.AddAsync(newBook);

            // Act
            await bookManager.DeleteAsync("1234567890123");

            // Assert
            var bookInDb = await dbContext.Books.FirstOrDefaultAsync(b => b.ISBN == newBook.ISBN);
            Assert.Null(bookInDb);
        }

        [Test]
        public async Task DeleteBookAsync_TryToDeleteWithNullOrWhiteSpaceISBN_ShouldThrowException()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }

        [Test]
        public async Task GetAllAsync_WhenBooksExist_ShouldReturnAllBooks()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }

        [Test]
        public async Task GetAllAsync_WhenNoBooksExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }

        [Test]
        public async Task SearchByTitleAsync_WithValidTitleFragment_ShouldReturnMatchingBooks()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }

        [Test]
        public async Task SearchByTitleAsync_WithInvalidTitleFragment_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }

        [Test]
        public async Task GetSpecificAsync_WithValidIsbn_ShouldReturnBook()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }

        [Test]
        public async Task GetSpecificAsync_WithInvalidIsbn_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }

        [Test]
        public async Task UpdateAsync_WithValidBook_ShouldUpdateBook()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }

        [Test]
        public async Task UpdateAsync_WithInvalidBook_ShouldThrowValidationException()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }
    }
}
