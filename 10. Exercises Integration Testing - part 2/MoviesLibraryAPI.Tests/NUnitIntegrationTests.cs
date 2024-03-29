using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MoviesLibraryAPI.Controllers;
using MoviesLibraryAPI.Controllers.Contracts;
using MoviesLibraryAPI.Data.Models;
using MoviesLibraryAPI.Services;
using MoviesLibraryAPI.Services.Contracts;
using NUnit.Framework.Legacy;
using System.ComponentModel.DataAnnotations;

namespace MoviesLibraryAPI.Tests
{
    [TestFixture]
    public class NUnitIntegrationTests
    {
        private MoviesLibraryNUnitTestDbContext _dbContext;
        private IMoviesLibraryController _controller;
        private IMoviesRepository _repository;
        IConfiguration _configuration;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        [SetUp]
        public async Task Setup()
        {
            string dbName = $"MoviesLibraryTestDb_{Guid.NewGuid()}";
            _dbContext = new MoviesLibraryNUnitTestDbContext(_configuration, dbName);

            _repository = new MoviesRepository(_dbContext.Movies);
            _controller = new MoviesLibraryController(_repository);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _dbContext.ClearDatabaseAsync();
        }

        [Test]
        public async Task AddMovieAsync_WhenValidMovieProvided_ShouldAddToDatabase()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 86,
                Rating = 7.5
            };

            // Act
            await _controller.AddAsync(movie);

            // Assert
            var resultMovie = await _dbContext.Movies.Find(m => m.Title == "Test Movie").FirstOrDefaultAsync();
            Assert.That(resultMovie, Is.Not.Null);
            Assert.That(resultMovie.Title, Is.EqualTo("Test Movie"));
            Assert.That(resultMovie.Director, Is.EqualTo("Test Director"));
        }

        [Test]
        public async Task AddMovieAsync_WhenInvalidMovieProvided_ShouldThrowValidationException()
        {
            // Arrange
            var invalidMovie = new Movie
            {
                // Provide an invalid movie object, for example, missing required fields like 'Title'
                // Assuming 'Title' is a required field, do not set it
                Title = "",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };

            // Act and Assert
            // Expect a ValidationException because the movie is missing a required field
            var exception = Assert.ThrowsAsync<ValidationException>(async () => await _controller.AddAsync(invalidMovie));
            Assert.That(exception.Message, Is.EqualTo("Movie is not valid."));
        }

        [Test]
        public async Task DeleteAsync_WhenValidTitleProvided_ShouldDeleteMovie()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 86,
                Rating = 7.5
            };

            // Act
            await _controller.AddAsync(movie);
            await _controller.DeleteAsync("Test Movie");

            // Assert
            // The movie should no longer exist in the database
            var resultMovie = await _dbContext.Movies.Find(m => m.Title == movie.Title).FirstOrDefaultAsync();
            Assert.That(resultMovie, Is.Null);
        }


        [Test]
        public async Task DeleteAsync_WhenTitleIsNull_ShouldThrowArgumentException()
        {
            // Act and Assert
            var movie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 86,
                Rating = 7.5
            };
            await _controller.AddAsync(movie);
            var result = Assert.ThrowsAsync<ArgumentException>(() => _controller.DeleteAsync(null));
            Assert.That(result.Message, Is.EqualTo("Title cannot be empty."));
        }

        [Test]
        public async Task DeleteAsync_WhenTitleIsEmpty_ShouldThrowArgumentException()
        {
            // Act and Assert
            var movie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 86,
                Rating = 7.5
            };
            await _controller.AddAsync(movie);
            var result = Assert.ThrowsAsync<ArgumentException>(() => _controller.DeleteAsync(""));
            Assert.That(result.Message, Is.EqualTo("Title cannot be empty."));
        }

        [Test]
        public async Task DeleteAsync_WhenTitleDoesNotExist_ShouldThrowInvalidOperationException()
        {
            // Act and Assert
            var movie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 86,
                Rating = 7.5
            };
            await _controller.AddAsync(movie);
            var exception = Assert.ThrowsAsync<InvalidOperationException>(() => _controller.DeleteAsync("TestMovie"));
            Assert.That(exception.Message, Is.EqualTo("Movie with title 'TestMovie' not found."));

        }

        [Test]
        public async Task GetAllAsync_WhenNoMoviesExist_ShouldReturnEmptyList()
        {
            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetAllAsync_WhenMoviesExist_ShouldReturnAllMovies()
        {
            // Arrange
            var firstMovie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 86,
                Rating = 7.5
            };

            var secondMovie = new Movie
            {
                Title = "Some Title",
                Director = firstMovie.Director,
                YearReleased = 2000,
                Genre = "Action",
                Duration = 90,
                Rating = 10
            };
            await _controller.AddAsync(firstMovie);
            await _controller.AddAsync(secondMovie);

            // Act
            var result = await _controller.GetAllAsync();


            // Assert
            // Ensure that all movies are returned
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetByTitle_WhenTitleExists_ShouldReturnMatchingMovie()
        {
            // Arrange
            var firstMovie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 86,
                Rating = 7.5
            };

            var secondMovie = new Movie
            {
                Title = "Some Title",
                Director = firstMovie.Director,
                YearReleased = 2000,
                Genre = "Action",
                Duration = 90,
                Rating = 10
            };
            await _controller.AddAsync(firstMovie);
            await _controller.AddAsync(secondMovie);

            // Act
            var result = await _controller.GetByTitle("Test Movie");

            // Assert
            Assert.That(result.Director, Is.EqualTo($"{firstMovie.Director}"));
            Assert.That(result.Title, Is.EqualTo($"{firstMovie.Title}"));
        }

        [Test]
        public async Task GetByTitle_WhenTitleDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var firstMovie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 86,
                Rating = 7.5
            };

            var secondMovie = new Movie
            {
                Title = "Some Title",
                Director = firstMovie.Director,
                YearReleased = 2000,
                Genre = "Action",
                Duration = 90,
                Rating = 10
            };
            await _controller.AddAsync(firstMovie);
            await _controller.AddAsync(secondMovie);

            // Act
            var resultMovie = await _controller.GetByTitle("Does Not Exist");

            // Assert
            Assert.That(resultMovie, Is.Null);
        }


        [Test]
        public async Task SearchByTitleFragmentAsync_WhenTitleFragmentExists_ShouldReturnMatchingMovies()
        {
            // Arrange
            var firstMovie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 86,
                Rating = 7.5
            };

            var secondMovie = new Movie
            {
                Title = "Some Title",
                Director = firstMovie.Director,
                YearReleased = 2000,
                Genre = "Action",
                Duration = 90,
                Rating = 10
            };
            await _controller.AddAsync(firstMovie);
            await _controller.AddAsync(secondMovie);

            // Act
            var result = await _controller.SearchByTitleFragmentAsync("Test");


            // Assert
            // Ensure that all movies are returned
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.ToList(), Is.EqualTo(result.Where(x => x.Title == "Test Movie")));
            Assert.That(result.ToList(), Is.EqualTo(result.Where(x => x.Director == "Test Director")));
        }

        [Test]
        public async Task SearchByTitleFragmentAsync_WhenNoMatchingTitleFragment_ShouldThrowKeyNotFoundException()
        {
            // Act and Assert
            var result = Assert.ThrowsAsync<KeyNotFoundException>(() => _controller.SearchByTitleFragmentAsync("KO"));
            Assert.That(result.Message, Is.EqualTo("No movies found."));
        }

        [Test]
        public async Task UpdateAsync_WhenValidMovieProvided_ShouldUpdateMovie()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 86,
                Rating = 7.5
            };
            await _controller.AddAsync(movie);

            // Modify the movie
            movie.Title = "Updated Title";

            // Act
            await _controller.UpdateAsync(movie);

            // Assert
            Assert.That(movie, Is.Not.Null);
            Assert.That(movie.Title, Is.EqualTo("Updated Title"));
            var bookInDb = _dbContext.Movies.Find(b => b.Title == "UpdatedTitle").FirstOrDefaultAsync();
        }

        [Test]
        public async Task UpdateAsync_WhenInvalidMovieProvided_ShouldThrowValidationException()
        {
            // Act
            var newMovie = new Movie
            {

            };

            //Assert
            var exception = Assert.ThrowsAsync<ValidationException>(() => _controller.UpdateAsync(newMovie));
            Assert.ThrowsAsync<ValidationException>(() => _controller.UpdateAsync(newMovie));
            Assert.That(exception.Message, Is.EqualTo(("Movie is not valid.")));
            try
            {
                await _controller.UpdateAsync(newMovie);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(("Movie is not valid.")));
            }
        }


        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await _dbContext.ClearDatabaseAsync();
        }
    }
}
