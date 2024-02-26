using Microsoft.EntityFrameworkCore;
using ProductConsoleAPI.Business.Contracts;
using ProductConsoleAPI.Business;
using ProductConsoleAPI.Data.Models;
using ProductConsoleAPI.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ProductConsoleAPI.IntegrationTests.XUnit
{
    public class IntegrationTests : IClassFixture<ProductsManagerFixture>
    {
        private TestProductsDbContext dbContext;
        private IProductsManager productsManager;

        public IntegrationTests(ProductsManagerFixture fixture)
        {
            this.productsManager = fixture.ProductsManager;
            dbContext = fixture.DbContext;
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }


        //positive test
        [Fact] 
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
            Assert.Equal(newProduct.ProductName, dbProduct.ProductName);
            Assert.Equal(newProduct.Description, dbProduct.Description);
            Assert.Equal(newProduct.Price, dbProduct.Price);
            Assert.Equal(newProduct.Quantity, dbProduct.Quantity);
            Assert.Equal(newProduct.OriginCountry, dbProduct.OriginCountry);
            Assert.Equal(newProduct.ProductCode, dbProduct.ProductCode);
        }

        //Negative test
        [Fact]
        public async Task AddProductAsync_TryToAddProductWithInvalidCredentials_ShouldThrowException()
        {
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = -10m,
                Quantity = 100,
                Description = "Anything for description"
            };

            var ex = await Assert.ThrowsAsync<ValidationException>(async () => await productsManager.AddAsync(newProduct));
            var actual = await dbContext.Products.FirstOrDefaultAsync(c => c.ProductCode == newProduct.ProductCode);

            Assert.Null(actual);
            Assert.Equal("Invalid product!", ex?.Message);

        }

        [Fact] 
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

            Assert.Null(productInDb);

        }

        [Fact] 
        public async Task DeleteProductAsync_TryToDeleteWithNullOrWhiteSpaceProductCode_ShouldThrowException()
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

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await productsManager.DeleteAsync(null));
            Assert.Equal("Product code cannot be empty.", ex?.Message);
        }

        [Fact] 
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
            Assert.NotNull(products);
            Assert.Equal(2, products.Count());

            // Additional Asserts
            Assert.Contains(products, p => p.ProductCode == "AB12C"); // Check if product with code "AB12C" exists
            Assert.Contains(products, p => p.ProductCode == "AB13C"); // Check if product with code "AB13C" exists
            Assert.DoesNotContain(products, p => p.ProductCode == "XYZ"); // Ensure product with code "XYZ" doesn't exist
            Assert.All(products, p => Assert.NotNull(p)); // Ensure all products are not null
            Assert.Equal(150, products.Sum(p => p.Quantity)); // Ensure total quantity is correct

        }

        [Fact] 
        public async Task GetAllAsync_WhenNoProductsExist_ShouldThrowKeyNotFoundException()
        {
            // Act

            //Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => productsManager.GetAllAsync());
            Assert.Equal("No product found.", exception.Message);

            try
            {
                await productsManager.GetAllAsync();
            }
            catch (Exception ex)
            {
                Assert.Equal("No product found.", ex.Message);
            }

            //var ex = await Assert.ThrowsAsync<KeyNotFoundException>(async () => await productsManager.GetAllAsync());
            //Assert.Equal("No product found.", ex.Message);

        }

        [Fact] 
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
            Assert.Single(searchProduct);

            // Additional Asserts
            Assert.Contains(searchProduct, p => p.ProductCode == "AB12C"); // Ensure product with code "AB12C" exists
            Assert.DoesNotContain(searchProduct, p => p.ProductCode == "XYZ"); // Ensure product with code "XYZ" doesn't exist
            Assert.Single(searchProduct); // Ensure only one product is returned
            Assert.True(searchProduct.All(p => p.OriginCountry == "BG")); // Ensure all products have the correct origin country
            Assert.All(searchProduct, p => Assert.NotNull(p)); // Ensure all products are not null

        }

        [Fact] 
        public async Task SearchByOriginCountryAsync_WithNonExistingOriginCountry_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act & Assert
            var ex = await Assert.ThrowsAsync<KeyNotFoundException>(async () => await productsManager.SearchByOriginCountry("Not existing"));
            Assert.Equal("No product found with the given first name.", ex.Message);

            try
            {
                await productsManager.SearchByOriginCountry("Not existing");
            }
            catch (Exception expect)
            {
                Assert.Equal("No product found with the given first name.", expect.Message);
            }
        }

        [Fact] 
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
            Assert.Equal("AB13C", searchProduct.ProductCode);
            Assert.Equal("FR", searchProduct.OriginCountry);
            Assert.Equal("TestProduct2", searchProduct.ProductName);
            Assert.Equal(1.50m, searchProduct.Price);
            Assert.Equal(50, searchProduct.Quantity);
            Assert.Equal("Anything for description for TestProduct2", searchProduct.Description);

        }

        [Fact] 
        public async Task GetSpecificAsync_WithInvalidProductCode_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act and Assert
            var ex = await Assert.ThrowsAsync<KeyNotFoundException>(async () => await productsManager.GetSpecificAsync("InvalidProductCode"));
            Assert.Equal("No product found with product code: InvalidProductCode", ex?.Message);

            try
            {
                await productsManager.GetSpecificAsync("InvalidProductCode");
            }
            catch (Exception expect)
            {
                Assert.Equal("No product found with product code: InvalidProductCode", expect.Message);
            }
        }

        [Fact] 
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
            Assert.Equal("GB", newProductFirst.OriginCountry);
            Assert.Equal("Updated name TestProduct1", newProductFirst.ProductName);
            Assert.Equal("Updated name", newProductSecond.ProductName);
            Assert.Equal(200, newProductFirst.Quantity);
            Assert.Equal(200, newProductSecond.Quantity);
        }

        [Fact] 
        public async Task UpdateAsync_WithInvalidProduct_ShouldThrowValidationException()
        {
            // Act and Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(async () => await productsManager.UpdateAsync(new Product()));
            Assert.Equal("Invalid prduct!", ex?.Message);

            try
            {
                await productsManager.UpdateAsync(new Product());
            }
            catch (Exception expect)
            {
                Assert.Equal("Invalid prduct!", expect.Message);
            }
        }
    }
}
