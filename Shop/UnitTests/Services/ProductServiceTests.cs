using Domain.Entities.Customer;
using Domain.Entities.Product;
using Domain.Persistence;
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
    }
}