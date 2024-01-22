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