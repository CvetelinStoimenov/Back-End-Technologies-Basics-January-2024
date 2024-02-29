using Microsoft.EntityFrameworkCore;
using ProductConsoleAPI.Business;
using ProductConsoleAPI.Business.Contracts;
using ProductConsoleAPI.Data.Models;
using ProductConsoleAPI.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace ProductConsoleAPI.IntegrationTests.NUnit
{
    public  class IntegrationTests
    {
        private TestProductsDbContext dbContext;
        private IProductsManager productsManager;

        [SetUp]
        public void SetUp()
        {
            this.dbContext = new TestProductsDbContext();
            this.productsManager = new ProductsManager(new ProductsRepository(this.dbContext));
        }


        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.Dispose();
        }


        //positive test
        [Test]
        public async Task AddProductAsync_ShouldAddNewProduct()
        {
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };

            await productsManager.AddAsync(newProduct);

            var dbProduct = await this.dbContext.Products.FirstOrDefaultAsync(p => p.ProductCode == newProduct.ProductCode);

            Assert.NotNull(dbProduct);
            Assert.That(dbProduct.ProductName, Is.EqualTo(newProduct.ProductName));
            Assert.That(dbProduct.Description, Is.EqualTo(newProduct.Description));
            Assert.That(dbProduct.Price, Is.EqualTo(newProduct.Price));
            Assert.That(dbProduct.Quantity, Is.EqualTo(newProduct.Quantity));
            Assert.That(dbProduct.OriginCountry, Is.EqualTo(newProduct.OriginCountry));
            Assert.That(dbProduct.ProductCode, Is.EqualTo(newProduct.ProductCode));
        }

        //Negative test
        [Test]
        public async Task AddProductAsync_TryToAddProductWithInvalidCredentials_ShouldThrowException()
        {
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = -1m,
                Quantity = 100,
                Description = "Anything for description"
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await productsManager.AddAsync(newProduct));
            var actual = await dbContext.Products.FirstOrDefaultAsync(c => c.ProductCode == newProduct.ProductCode);

            Assert.IsNull(actual);
            Assert.That(ex?.Message, Is.EqualTo("Invalid product!"));

        }

        [Test]
        public async Task DeleteProductAsync_WithValidProductCode_ShouldRemoveProductFromDb()
        {
            // Arrange
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };

            await productsManager.AddAsync(newProduct);

            // Act
            await productsManager.DeleteAsync("AB12C");

            // Assert
            var productInDb = await dbContext.Products.FirstOrDefaultAsync(p => p.ProductCode == newProduct.ProductCode);

            Assert.IsNull(productInDb); 
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public async Task DeleteProductAsync_TryToDeleteWithNullOrWhiteSpaceProductCode_ShouldThrowException(string invalidCode)
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await productsManager.DeleteAsync(invalidCode));
            Assert.That(ex?.Message, Is.EqualTo("Product code cannot be empty."));
        }

        [Test]
        public async Task GetAllAsync_WhenProductsExist_ShouldReturnAllProducts()
        {
            // Arrange
            var newProductFirst = new Product()
            {
                OriginCountry = "BG",
                ProductName = "TestProduct1",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description for TestProduct1"
            };


            var newProductSecond = new Product()
            {
                OriginCountry = "FR",
                ProductName = "TestProduct2",
                ProductCode = "AB13C",
                Price = 1.50m,
                Quantity = 50,
                Description = "Anything for description for TestProduct2"
            };

            await productsManager.AddAsync(newProductFirst);
            await productsManager.AddAsync(newProductSecond);

            // Act
            var products = await productsManager.GetAllAsync();

            // Assert
            Assert.That(products.Count(), Is.EqualTo(2));

            Assert.That(products, Contains.Item(newProductFirst).And.Contains(newProductSecond));
            Assert.That(products, Has.Exactly(1).Matches<Product>(p => p.OriginCountry == "BG"));
            Assert.That(products, Has.Exactly(1).Matches<Product>(p => p.OriginCountry == "FR"));
            Assert.That(products, Has.Exactly(1).Matches<Product>(p => p.Price == 1.25m));
            Assert.That(products, Has.Exactly(1).Matches<Product>(p => p.Price == 1.50m));

            var firstItem = products.FirstOrDefault(x => x.ProductCode == newProductFirst.ProductCode);
            Assert.NotNull(firstItem);
            Assert.That(firstItem.ProductName, Is.EqualTo(newProductFirst.ProductName));
            Assert.That(firstItem.Description, Is.EqualTo(newProductFirst.Description));
            Assert.That(firstItem.Price, Is.EqualTo(newProductFirst.Price));
            Assert.That(firstItem.Quantity, Is.EqualTo(newProductFirst.Quantity));
            Assert.That(firstItem.OriginCountry, Is.EqualTo(newProductFirst.OriginCountry));
            Assert.That(firstItem.ProductCode, Is.EqualTo(newProductFirst.ProductCode));

            var secondItem = products.FirstOrDefault(x => x.ProductCode.Equals(newProductSecond.ProductCode));
            
            Assert.NotNull(secondItem);
            Assert.That(secondItem.ProductName, Is.EqualTo(newProductSecond.ProductName));
            Assert.That(secondItem.Description, Is.EqualTo(newProductSecond.Description));
            Assert.That(secondItem.Price, Is.EqualTo(newProductSecond.Price));
            Assert.That(secondItem.Quantity, Is.EqualTo(newProductSecond.Quantity));
            Assert.That(secondItem.OriginCountry, Is.EqualTo(newProductSecond.OriginCountry));
            Assert.That(secondItem.ProductCode, Is.EqualTo(newProductSecond.ProductCode));
        }

        [Test]
        public async Task GetAllAsync_WhenNoProductsExist_ShouldThrowKeyNotFoundException()
        {
            //Act & Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => productsManager.GetAllAsync());
            Assert.That(exception.Message, Is.EqualTo(("No product found.")));
           
            try
            {
                await productsManager.GetAllAsync();
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(("No product found.")));
            }
        }

        [Test]
        public async Task SearchByOriginCountry_WithExistingOriginCountry_ShouldReturnMatchingProducts()
        {
            // Arrange
            var newProductFirst = new Product()
            {
                OriginCountry = "BG",
                ProductName = "TestProduct1",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description for TestProduct1"
            };


            var newProductSecond = new Product()
            {
                OriginCountry = "FR",
                ProductName = "TestProduct2",
                ProductCode = "AB13C",
                Price = 1.50m,
                Quantity = 50,
                Description = "Anything for description for TestProduct2"
            };

            await productsManager.AddAsync(newProductFirst);
            await productsManager.AddAsync(newProductSecond);

            // Act
            var searchProduct = await productsManager.SearchByOriginCountry("BG");

            // Assert
            Assert.NotNull(searchProduct);
            Assert.That(searchProduct.Count(), Is.EqualTo(1));

            // Additional Asserts
            Assert.That(searchProduct, Has.Some.Matches<Product>(p => p.ProductCode == "AB12C")); // Ensure product with code "AB12C" exists
            Assert.That(searchProduct, Has.None.Matches<Product>(p => p.ProductCode == "XYZ")); // Ensure product with code "XYZ" doesn't exist
            Assert.That(searchProduct.Count, Is.EqualTo(1)); // Ensure only one product is returned
            Assert.That(searchProduct, Has.All.Property("OriginCountry").EqualTo("BG")); // Ensure all products have the correct origin country
            Assert.That(searchProduct, Has.All.Not.Null); // Ensure all products are not null
        }

        [Test]
        public async Task SearchByOriginCountryAsync_WithNonExistingOriginCountry_ShouldThrowKeyNotFoundException()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(() => productsManager.SearchByOriginCountry("Not existing"));
            Assert.That(ex.Message, Is.EqualTo("No product found with the given first name."));

            try
            {
                await productsManager.SearchByOriginCountry("Not existing");
            }
            catch (Exception expect)
            {
                Assert.That(expect.Message, Is.EqualTo(("No product found with the given first name.")));
            }
        }

        [Test]
        public async Task GetSpecificAsync_WithValidProductCode_ShouldReturnProduct()
        {
            // Arrange
            var newProductFirst = new Product()
            {
                OriginCountry = "BG",
                ProductName = "TestProduct1",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description for TestProduct1"
            };


            var newProductSecond = new Product()
            {
                OriginCountry = "FR",
                ProductName = "TestProduct2",
                ProductCode = "AB13C",
                Price = 1.50m,
                Quantity = 50,
                Description = "Anything for description for TestProduct2"
            };

            await productsManager.AddAsync(newProductFirst);
            await productsManager.AddAsync(newProductSecond);

            // Act
            var searchProduct = await productsManager.GetSpecificAsync("AB13C");

            // Assert
            Assert.NotNull(searchProduct);

            // Additional Asserts
            Assert.That(searchProduct.ProductCode, Is.EqualTo("AB13C"));
            Assert.That(searchProduct.OriginCountry, Is.EqualTo("FR"));
            Assert.That(searchProduct.ProductName, Is.EqualTo("TestProduct2"));
            Assert.That(searchProduct.Price, Is.EqualTo(1.50m));
            Assert.That(searchProduct.Quantity, Is.EqualTo(50));
            Assert.That(searchProduct.Description, Is.EqualTo("Anything for description for TestProduct2"));
        }

        [Test]
        public async Task GetSpecificAsync_WithInvalidProductCode_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act and Assert
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () => await productsManager.GetSpecificAsync("InvalidProductCode"));
            Assert.That(ex?.Message, Is.EqualTo("No product found with product code: InvalidProductCode"));

            try
            {
                await productsManager.GetSpecificAsync("InvalidProductCode");
            }
            catch (Exception expect)
            {
                Assert.That(expect.Message, Is.EqualTo(("No product found with product code: InvalidProductCode")));
            }
        }

        [Test]
        public async Task UpdateAsync_WithValidProduct_ShouldUpdateProduct()
        {
            // Arrange
            var newProductFirst = new Product()
            {
                OriginCountry = "BG",
                ProductName = "TestProduct1",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description for TestProduct1"
            };


            var newProductSecond = new Product()
            {
                OriginCountry = "FR",
                ProductName = "TestProduct2",
                ProductCode = "AB13C",
                Price = 1.50m,
                Quantity = 50,
                Description = "Anything for description for TestProduct2"
            };

            await productsManager.AddAsync(newProductFirst);
            await productsManager.AddAsync(newProductSecond);

            newProductFirst.OriginCountry = "GB";
            newProductFirst.ProductName = "Updated name TestProduct1";
            newProductSecond.ProductName = "Updated name";
            newProductFirst.Quantity = 200;
            newProductSecond.Quantity = 200;

            // Act
            await productsManager.UpdateAsync(newProductFirst);

            // Assert
            var products = await productsManager.GetAllAsync();

            Assert.NotNull(products);
            Assert.That(newProductFirst.OriginCountry, Is.EqualTo("GB"));
            Assert.That(newProductFirst.ProductName, Is.EqualTo("Updated name TestProduct1"));
            Assert.That(newProductSecond.ProductName, Is.EqualTo("Updated name"));
            Assert.That(newProductFirst.Quantity, Is.EqualTo(200));
            Assert.That(newProductSecond.Quantity, Is.EqualTo(200));

            Assert.That(products, Has.Exactly(1).Matches<Product>(p => p.OriginCountry == "GB"));
            Assert.That(products, Has.Exactly(1).Matches<Product>(p => p.ProductName == "Updated name TestProduct1"));
            Assert.That(products, Has.Exactly(1).Matches<Product>(p => p.ProductName == "Updated name"));
            Assert.That(products, Has.Exactly(2).Matches<Product>(p => p.Quantity == 200));
        }

        [Test]
        public async Task UpdateAsync_WithInvalidProduct_ShouldThrowValidationException()
        {
            // Act and Assert
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await productsManager.UpdateAsync(new Product()));
            Assert.That(ex?.Message, Is.EqualTo("Invalid prduct!"));

            try
            {
                await productsManager.UpdateAsync(new Product());
            }
            catch (Exception expect)
            {
                Assert.That(expect.Message, Is.EqualTo(("Invalid prduct!")));
            }
        }
    }
}
