using NUnit.Framework;
using ZooConsoleAPI.Business;
using ZooConsoleAPI.Business.Contracts;
using ZooConsoleAPI.DataAccess;

namespace ZooConsoleAPI.IntegrationTests.NUnit
{
    public class IntegrationTests
    {
        private TestAnimalDbContext dbContext;
        private IAnimalsManager animalsManager;

        [SetUp]
        public void SetUp()
        {
            this.dbContext = new TestAnimalDbContext();
            this.animalsManager = new AnimalsManager(new AnimalRepository(this.dbContext));
        }


        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.Dispose();
        }


        //positive test
        [Test]
        public async Task AddAnimalAsync_ShouldAddNewAnimal()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }

        //Negative test
        [Test]
        public async Task AddAnimalAsync_TryToAddAnimalWithInvalidCredentials_ShouldThrowException()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");

        }

        [Test]
        public async Task DeleteAnimalAsync_WithValidCatalogNumber_ShouldRemoveAnimalFromDb()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }

        [Test]
        public async Task DeleteAnimalAsync_TryToDeleteWithNullOrWhiteSpaceCatalogNumber_ShouldThrowException()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }

        [Test]
        public async Task GetAllAsync_WhenAnimalsExist_ShouldReturnAllAnimals()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }

        [Test]
        public async Task GetAllAsync_WhenNoAnimalsExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }

        [Test]
        public async Task SearchByTypeAsync_WithExistingType_ShouldReturnMatchingAnimals()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }

        [Test]
        public async Task SearchByTypeAsync_WithNonExistingType_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }

        [Test]
        public async Task GetSpecificAsync_WithValidCatalogNumber_ShouldReturnAnimal()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }

        [Test]
        public async Task GetSpecificAsync_WithInvalidCatalogNumber_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }

        [Test]
        public async Task UpdateAsync_WithValidAnimal_ShouldUpdateAnimal()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }

        [Test]
        public async Task UpdateAsync_WithInvalidAnimal_ShouldThrowValidationException()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive("Test not implemented yet.");
        }
    }
}

