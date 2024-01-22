using Domain.Entities.Customer;
using Domain.Entities.Product;
using Domain.Persistence;
using FluentAssertions;
using Models;
using Moq;
using Repository;
using Service;
using System.Xml.Linq;

namespace UnitTests.Services
{
    public class ProductServiceTests
    {
        ProductEntity entity = new("name", "desc", SKU.Create("sku123")!);
        Mock<EFContext> dbContextMock = new Mock<EFContext>();
        CreateUpdateProductRequestModel model = new CreateUpdateProductRequestModel()
        {
            Description = "description",
            Name = "name",
            Sku = "sku"
        };

        [Fact]
        public async Task GetCustomer_OnDoesNotExit_Returns_Null()
        {
            const int id = 1;

            var repositoryMock = new Mock<IProductRepository>();
            repositoryMock.Setup(x => x.GetProduct(id));

            var customerService = new ProductService(repositoryMock.Object, dbContextMock.Object);

            var result = await customerService.GetProduct(id);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetProduct_WhenCalled_InvokesProductRepository()
        {
            const int id = 1;

            var repositoryMock = new Mock<IProductRepository>();
            repositoryMock.Setup(x => x.GetProduct(id));

            var customerService = new ProductService(repositoryMock.Object, dbContextMock.Object);

            var result = await customerService.GetProduct(id);

            repositoryMock.Verify(x => x.GetProduct(id), Times.Once());
        }

        [Fact]
        public async Task CreateProduct_WhenCalled_InvokesCustomerRepositoryAndDbContextMock()
        {
            var repositoryMock = new Mock<IProductRepository>();

            repositoryMock
                .Setup(x => x.CreateProduct(It.IsAny<ProductEntity>()))
                .ReturnsAsync(entity);

            var customerService = new ProductService(repositoryMock.Object, dbContextMock.Object);

            var result = await customerService.CreateProduct(model);

            repositoryMock.Verify(x => x.CreateProduct(It.IsAny<ProductEntity>()), Times.Once());
            dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        public async Task GetCustomer_OnSuccess_Returns_Customer()
        {
            const int id = 1;

            var repositoryMock = new Mock<IProductRepository>();

            repositoryMock
                .Setup(x => x.CreateProduct(It.IsAny<ProductEntity>()))
                .ReturnsAsync(entity);

            var customerService = new ProductService(repositoryMock.Object, dbContextMock.Object);

            var result = await customerService.GetProduct(id);

            result.Should().NotBeNull();
            //result.Id.Should().Be(id); cant check as Id has protected setter
            result.Name.Should().Be(entity.Name);
            result.Description.Should().Be(entity.Description);
            result.Sku.Should().Be(entity.Sku.Value);
        }

        [Fact]
        public async Task CreateProduct_OnSuccess_ReturnId()
        {
            var repositoryMock = new Mock<IProductRepository>();
            var productEntity = new ProductEntity("bike", "big bike", SKU.Create("sku125")!);
            var type = productEntity.GetType();
            var idProperty = type.GetProperty(nameof(ProductEntity.Id));
            idProperty!.SetValue(productEntity, 34);

            repositoryMock
                .Setup(x => x.CreateProduct(It.IsAny<ProductEntity>()))
                .ReturnsAsync(productEntity);

            var customerService = new ProductService(repositoryMock.Object, dbContextMock.Object);

            var result = await customerService.CreateProduct(model);
            result.Should().Be(productEntity.Id);
        }
    }
}