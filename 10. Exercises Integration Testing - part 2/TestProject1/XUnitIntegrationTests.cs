using MongoDB.Driver;
using MoviesLibraryAPI.Controllers;
using MoviesLibraryAPI.Controllers.Contracts;
using MoviesLibraryAPI.Data.Models;
using MoviesLibraryAPI.Services;
using MoviesLibraryAPI.Services.Contracts;
using System.ComponentModel.DataAnnotations;

namespace MoviesLibraryAPI.XUnitTests
{
    public class XUnitIntegrationTests : IClassFixture<DatabaseFixture>
    {
        private readonly MoviesLibraryXUnitTestDbContext _dbContext;
        private readonly IMoviesLibraryController _controller;
        private readonly IMoviesRepository _repository;

        public XUnitIntegrationTests(DatabaseFixture fixture)
        {
            _dbContext = fixture.DbContext;
            _repository = new MoviesRepository(_dbContext.Movies);
            _controller = new MoviesLibraryController(_repository);

            InitializeDatabaseAsync().GetAwaiter().GetResult();
        }

        private async Task InitializeDatabaseAsync()
        {
            await _dbContext.ClearDatabaseAsync();
        }

        [Fact]
        public async Task AddMovieAsync_WhenValidMovieProvided_ShouldAddToDatabase()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };

            // Act
            await _controller.AddAsync(movie);

            // Assert
            var resultMovie = await _dbContext.Movies.Find(m => m.Title == "Test Movie").FirstOrDefaultAsync();
            Assert.NotNull(resultMovie);
            Assert.Equal("Test Movie", resultMovie.Title);
            Assert.Equal("Test Director", resultMovie.Director);
            Assert.Equal(2022, resultMovie.YearReleased);
            Assert.Equal("Action", resultMovie.Genre);
            Assert.Equal(120, resultMovie.Duration);
            Assert.Equal(7.5, resultMovie.Rating);
        }

        [Fact]
        public async Task AddMovieAsync_WhenInvalidMovieProvided_ShouldThrowValidationException()
        {
            // Arrange
            var invalidMovie = new Movie
            {
                Title = "",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
                // Provide an invalid movie object, e.g., without a title or other required fields
            };

            // Act and Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _controller.AddAsync(invalidMovie));
            Assert.Equal("Movie is not valid.", exception.Message);
        }

        [Fact]
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
            await _controller.DeleteAsync(movie.Title);

            // Assert
            // The movie should no longer exist in the database
            var resultMovie = await _dbContext.Movies.Find(m => m.Title == "Test Movie").FirstOrDefaultAsync();
            Assert.Null(resultMovie);
        }


        [Fact]
        public async Task DeleteAsync_WhenTitleIsNull_ShouldThrowArgumentException()
        {
            // Act and Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _controller.DeleteAsync(null));
            Assert.Equal("Title cannot be empty.", exception.Message);
        }

        [Fact]
        public async Task DeleteAsync_WhenTitleIsEmpty_ShouldThrowArgumentException()
        {
            // Act
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

            // Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _controller.DeleteAsync(""));
            Assert.Equal("Title cannot be empty.", exception.Message);
        }

        [Fact]
        public async Task DeleteAsync_WhenTitleDoesNotExist_ShouldThrowInvalidOperationException()
        {
            // Act
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

            // Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _controller.DeleteAsync("Not Exist Movie"));
            Assert.Equal("Movie with title 'Not Exist Movie' not found.", exception.Message);
        }

        [Fact]
        public async Task GetAllAsync_WhenNoMoviesExist_ShouldReturnEmptyList()
        {
            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
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
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
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
            Assert.Equal(result.Title, firstMovie.Title);
            Assert.Equal(result.Director, firstMovie.Director);
        }

        [Fact]
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
            var result = await _controller.GetByTitle("Does Not Exist");

            // Assert
            Assert.Null(result);
        }

        [Fact]
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
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(result.ToList(), result.Where(x => x.Title == "Test Movie"));
            Assert.Equal(result.ToList(), result.Where(x => x.Director == "Test Director"));
        }

        [Fact]
        public async Task SearchByTitleFragmentAsync_WhenNoMatchingTitleFragment_ShouldThrowKeyNotFoundException()
        {
            // Act and Assert
            var result = await Assert.ThrowsAsync<KeyNotFoundException>(() => _controller.SearchByTitleFragmentAsync("KO"));
            Assert.Equal("No movies found.", result.Message);
        }

        [Fact]
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
            Assert.NotNull(movie);
            Assert.Equal("Updated Title", movie.Title);
            var bookInDb = _dbContext.Movies.Find(b => b.Title == "UpdatedTitle").FirstOrDefaultAsync();
        }

        [Fact]
        public async Task UpdateAsync_WhenInvalidMovieProvided_ShouldThrowValidationException()
        {
            // Act
            var newMovie = new Movie
            {

            };

            //Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _controller.UpdateAsync(newMovie));
            await Assert.ThrowsAsync<ValidationException>(() => _controller.UpdateAsync(newMovie));
            Assert.Equal(("Movie is not valid."), exception.Message);
            try
            {
                await _controller.UpdateAsync(newMovie);
            }
            catch (Exception ex)
            {
                Assert.Equal(("Movie is not valid."), ex.Message);
            }
        }

    }
}
