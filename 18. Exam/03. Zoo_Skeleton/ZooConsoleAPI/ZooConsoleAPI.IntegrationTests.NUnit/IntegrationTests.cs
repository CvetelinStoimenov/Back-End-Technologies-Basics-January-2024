using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using ZooConsoleAPI.Business;
using ZooConsoleAPI.Business.Contracts;
using ZooConsoleAPI.Data.Model;
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
            var newAnimal = new Animal()
            {
                Name = "Laura",
                CatalogNumber = "01HNTWXTR0E5",
                Type = "Mammal",
                Age = 10,
                Breed = "Oriental short-clawed otter",
                Gender = "Female",
                IsHealthy = false,
            };

            // Act
            await animalsManager.AddAsync(newAnimal);

            // Assert
            var dbAnimal = await this.dbContext.Animals.FirstOrDefaultAsync(a => a.Name == newAnimal.Name);
            Assert.NotNull(dbAnimal);
            Assert.That(dbAnimal.Name, Is .EqualTo(newAnimal.Name));
            Assert.That(dbAnimal.Id, Is .EqualTo(newAnimal.Id));
            Assert.That(dbAnimal.CatalogNumber, Is .EqualTo(newAnimal.CatalogNumber));
            Assert.That(dbAnimal.Type, Is .EqualTo(newAnimal.Type));
            Assert.That(dbAnimal.Age, Is .EqualTo(newAnimal.Age));
            Assert.That(dbAnimal.Breed, Is .EqualTo(newAnimal.Breed));
            Assert.That(dbAnimal.Gender, Is .EqualTo(newAnimal.Gender));
            Assert.That(dbAnimal.IsHealthy, Is .EqualTo(newAnimal.IsHealthy));
        }

        //Negative test
        [Test]
        public async Task AddAnimalAsync_TryToAddAnimalWithInvalidCredentials_ShouldThrowException()
        {
            // Arrange
            var newAnimal = new Animal()
            {
                Name = "Laura",
                CatalogNumber = "01HNTWXTR0E5",
                Type = null,
                Age = -10,
                Breed = "Oriental short-clawed otter",
                Gender = "Female",
                IsHealthy = false,
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await animalsManager.AddAsync(newAnimal));
            var actual = await this.dbContext.Animals.FirstOrDefaultAsync(c => c.CatalogNumber == newAnimal.CatalogNumber);

            Assert.IsNull(actual);
            Assert.That(ex?.Message, Is.EqualTo("Invalid animal!"));
        }

        [Test]
        public async Task DeleteAnimalAsync_WithValidCatalogNumber_ShouldRemoveAnimalFromDb()
        {
            // Arrange
            var newAnimal = new Animal()
            {
                Name = "Laura",
                CatalogNumber = "01HNTWXTR0E5",
                Type = "Mammal",
                Age = 10,
                Breed = "Oriental short-clawed otter",
                Gender = "Female",
                IsHealthy = false,
            };

            await animalsManager.AddAsync(newAnimal);

            // Act
            await animalsManager.DeleteAsync("01HNTWXTR0E5");
            var dbAnimal = await this.dbContext.Animals.FirstOrDefaultAsync(a => a.Name == newAnimal.Name);

            // Assert
            Assert.Null(dbAnimal);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public async Task DeleteAnimalAsync_TryToDeleteWithNullOrWhiteSpaceCatalogNumber_ShouldThrowException(string invalidCatalogNumber)
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await animalsManager.DeleteAsync(invalidCatalogNumber));
            Assert.That(ex?.Message, Is.EqualTo("Catalog number cannot be empty."));
        }

        [Test]
        public async Task GetAllAsync_WhenAnimalsExist_ShouldReturnAllAnimals()
        {
            // Arrange
            var firstAnimal = new Animal()
            {
                Name = "Laura",
                CatalogNumber = "01HNTWXTR0E5",
                Type = "Mammal",
                Age = 10,
                Breed = "Oriental short-clawed otter",
                Gender = "Female",
                IsHealthy = false,
            };

            var secondAnimal = new Animal()
            {
                Name = "Loris",
                CatalogNumber = "01HNTWXTQX03",
                Type = "Mammal",
                Age = 2,
                Breed = "Colobus, magistrate black",
                Gender = "Male",
                IsHealthy = true,
            };

            await animalsManager.AddAsync(firstAnimal);
            await animalsManager.AddAsync(secondAnimal);

            // Act
            var animalsInDb = await animalsManager.GetAllAsync();

            // Assert
            Assert.That(animalsInDb.Count(), Is.EqualTo(2));

            var firstAnimalInDb = animalsInDb.FirstOrDefault(x => x.Name == firstAnimal.Name);
            Assert.NotNull(firstAnimalInDb);
            Assert.That(firstAnimalInDb.Name, Is.EqualTo(firstAnimal.Name));
            Assert.That(firstAnimalInDb.Id, Is.EqualTo(firstAnimal.Id));
            Assert.That(firstAnimalInDb.CatalogNumber, Is.EqualTo(firstAnimal.CatalogNumber));
            Assert.That(firstAnimalInDb.Type, Is.EqualTo(firstAnimal.Type));
            Assert.That(firstAnimalInDb.Age, Is.EqualTo(firstAnimal.Age));
            Assert.That(firstAnimalInDb.Breed, Is.EqualTo(firstAnimal.Breed));
            Assert.That(firstAnimalInDb.Gender, Is.EqualTo(firstAnimal.Gender));
            Assert.That(firstAnimalInDb.IsHealthy, Is.EqualTo(firstAnimal.IsHealthy));

            var secondAnimalInDb = animalsInDb.FirstOrDefault(x => x.Name == secondAnimal.Name);
            Assert.NotNull(secondAnimalInDb);
            Assert.That(secondAnimalInDb.Name, Is.EqualTo(secondAnimal.Name));
            Assert.That(secondAnimalInDb.Id, Is.EqualTo(secondAnimal.Id));
            Assert.That(secondAnimalInDb.CatalogNumber, Is.EqualTo(secondAnimal.CatalogNumber));
            Assert.That(secondAnimalInDb.Type, Is.EqualTo(secondAnimal.Type));
            Assert.That(secondAnimalInDb.Age, Is.EqualTo(secondAnimal.Age));
            Assert.That(secondAnimalInDb.Breed, Is.EqualTo(secondAnimal.Breed));
            Assert.That(secondAnimalInDb.Gender, Is.EqualTo(secondAnimal.Gender));
            Assert.That(secondAnimalInDb.IsHealthy, Is.EqualTo(secondAnimal.IsHealthy));
        }

        [Test]
        public async Task GetAllAsync_WhenNoAnimalsExist_ShouldThrowKeyNotFoundException()
        {
            //Act & Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => animalsManager.GetAllAsync());
            Assert.That(exception.Message, Is.EqualTo(("No animal found.")));

            try
            {
                await animalsManager.GetAllAsync();
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(("No animal found.")));
            }
        }

        [Test]
        public async Task SearchByTypeAsync_WithExistingType_ShouldReturnMatchingAnimals()
        {
            // Arrange
            var firstAnimal = new Animal()
            {
                Name = "Laura",
                CatalogNumber = "01HNTWXTR0E5",
                Type = "Mammal",
                Age = 10,
                Breed = "Oriental short-clawed otter",
                Gender = "Female",
                IsHealthy = false,
            };

            var secondAnimal = new Animal()
            {
                Name = "Loris",
                CatalogNumber = "01HNTWXTQX03",
                Type = "Mammal",
                Age = 2,
                Breed = "Colobus, magistrate black",
                Gender = "Male",
                IsHealthy = true,
            };

            var thirdAnimal = new Animal()
            {
                Name = "Charly",
                CatalogNumber = "01HNTWXTQYTY",
                Type = "Birth",
                Age = 1,
                Breed = "Chilean flamingo",
                Gender = "Male",
                IsHealthy = true,

            };

            await animalsManager.AddAsync(firstAnimal);
            await animalsManager.AddAsync(secondAnimal);
            await animalsManager.AddAsync(thirdAnimal);

            // Act
            var searchAnimal = await animalsManager.SearchByTypeAsync("Birth");

            // Assert
            Assert.NotNull(searchAnimal);
            Assert.That(searchAnimal.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task SearchByTypeAsync_WithNonExistingType_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var firstAnimal = new Animal()
            {
                Name = "Laura",
                CatalogNumber = "01HNTWXTR0E5",
                Type = "Mammal",
                Age = 10,
                Breed = "Oriental short-clawed otter",
                Gender = "Female",
                IsHealthy = false,
            };
            await animalsManager.AddAsync(firstAnimal);

            // Act & Assert
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(() => animalsManager.SearchByTypeAsync("Not existing"));
            Assert.That(ex.Message, Is.EqualTo("No animal found with the given type."));

            try
            {
                await animalsManager.SearchByTypeAsync("Not existing");
            }
            catch (Exception expect)
            {
                Assert.That(expect.Message, Is.EqualTo(("No animal found with the given type.")));
            }
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public async Task SearchByTypeAsync_WhenTypeIsNullOrWhiteSpace_ShouldThrowArgumentException(string type)
        {
            // Arrange
            var firstAnimal = new Animal()
            {
                Name = "Laura",
                CatalogNumber = "01HNTWXTR0E5",
                Type = "Mammal",
                Age = 10,
                Breed = "Oriental short-clawed otter",
                Gender = "Female",
                IsHealthy = false,
            };
            await animalsManager.AddAsync(firstAnimal);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await animalsManager.SearchByTypeAsync(type));
            Assert.That(ex?.Message, Is.EqualTo("Animal type cannot be empty."));
        }

        [Test]
        public async Task GetSpecificAsync_WithValidCatalogNumber_ShouldReturnAnimal()
        {
            // Arrange
            var firstAnimal = new Animal()
            {
                Name = "Laura",
                CatalogNumber = "01HNTWXTR0E5",
                Type = "Mammal",
                Age = 10,
                Breed = "Oriental short-clawed otter",
                Gender = "Female",
                IsHealthy = false,
            };

            var secondAnimal = new Animal()
            {
                Name = "Loris",
                CatalogNumber = "01HNTWXTQX03",
                Type = "Mammal",
                Age = 2,
                Breed = "Colobus, magistrate black",
                Gender = "Male",
                IsHealthy = true,
            };

            var thirdAnimal = new Animal()
            {
                Name = "Charly",
                CatalogNumber = "01HNTWXTQYTY",
                Type = "Birth",
                Age = 1,
                Breed = "Chilean flamingo",
                Gender = "Male",
                IsHealthy = true,

            };

            await animalsManager.AddAsync(firstAnimal);
            await animalsManager.AddAsync(secondAnimal);
            await animalsManager.AddAsync(thirdAnimal);

            // Act
            var searchAnimal = await animalsManager.GetSpecificAsync("01HNTWXTQYTY");

            // Assert
            Assert.NotNull(searchAnimal);
            Assert.That(searchAnimal.Id, Is.EqualTo(thirdAnimal.Id));
            Assert.That(searchAnimal.Age, Is.EqualTo(thirdAnimal.Age));
            Assert.That(searchAnimal.Type, Is.EqualTo("Birth"));
            Assert.That(searchAnimal.Breed, Is.EqualTo(thirdAnimal.Breed));
            Assert.That(searchAnimal.Breed, Is.EqualTo("Chilean flamingo"));
        }

        [Test]
        public async Task GetSpecificAsync_WithInvalidCatalogNumber_ShouldThrowKeyNotFoundException()
        {
            // Act and Assert
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () => await animalsManager.GetSpecificAsync("InvalidProductCode"));
            Assert.That(ex?.Message, Is.EqualTo("No animal found with catalog number: InvalidProductCode"));

            try
            {
                await animalsManager.GetSpecificAsync("InvalidProductCode");
            }
            catch (Exception expect)
            {
                Assert.That(expect.Message, Is.EqualTo(("No animal found with catalog number: InvalidProductCode")));
            }
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public async Task GetSpecificAsync_WhenCatalogNumberIsNullOrWhiteSpace_ShouldThrowArgumentException(string catalogNumber)
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await animalsManager.GetSpecificAsync(catalogNumber));
            Assert.That(ex?.Message, Is.EqualTo("Catalog number cannot be empty."));
        }

        [Test]
        public async Task UpdateAsync_WithValidAnimal_ShouldUpdateAnimal()
        {
            // Arrange
            var firstAnimal = new Animal()
            {
                Name = "Laura",
                CatalogNumber = "01HNTWXTR0E5",
                Type = "Mammal",
                Age = 10,
                Breed = "Oriental short-clawed otter",
                Gender = "Female",
                IsHealthy = false,
            };

            var secondAnimal = new Animal()
            {
                Name = "Loris",
                CatalogNumber = "01HNTWXTQX03",
                Type = "Mammal",
                Age = 2,
                Breed = "Colobus, magistrate black",
                Gender = "Male",
                IsHealthy = true,
            };

            var thirdAnimal = new Animal()
            {
                Name = "Charly",
                CatalogNumber = "01HNTWXTQYTY",
                Type = "Birth",
                Age = 1,
                Breed = "Chilean flamingo",
                Gender = "Male",
                IsHealthy = true,

            };

            await animalsManager.AddAsync(firstAnimal);
            await animalsManager.AddAsync(secondAnimal);
            await animalsManager.AddAsync(thirdAnimal);

            firstAnimal.Name = "Updated name: Gosho";
            firstAnimal.Age = 11;
            secondAnimal.Name = "Updated name: Pesho";
            secondAnimal.Age = 3;
            thirdAnimal.Age = 2;

            // Act
            await animalsManager.UpdateAsync(firstAnimal);
            await animalsManager.UpdateAsync(secondAnimal);
            await animalsManager.UpdateAsync(thirdAnimal);

            // Assert
            var animals = await animalsManager.GetAllAsync();

            Assert.NotNull(animals);
            Assert.That(firstAnimal.Name, Is.EqualTo("Updated name: Gosho"));
            Assert.That(firstAnimal.Age, Is.EqualTo(11));
            Assert.That(secondAnimal.Name, Is.EqualTo("Updated name: Pesho"));
            Assert.That(secondAnimal.Age, Is.EqualTo(3));
            Assert.That(thirdAnimal.Age, Is.EqualTo(2));
        }

        [Test]
        public async Task UpdateAsync_WithInvalidAnimal_ShouldThrowValidationException()
        {
            // Act and Assert
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await animalsManager.UpdateAsync(new Animal()));
            Assert.That(ex?.Message, Is.EqualTo("Invalid animal!"));

            try
            {
                await animalsManager.UpdateAsync(new Animal());
            }
            catch (Exception expect)
            {
                Assert.That(expect.Message, Is.EqualTo(("Invalid animal!")));
            }
        }
    }
}

