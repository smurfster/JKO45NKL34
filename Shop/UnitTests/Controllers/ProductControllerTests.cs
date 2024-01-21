using API.Controllers;
using Castle.Core.Resource;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using Service;

namespace UnitTests.Controllers
{
    public class ProductControllerTests
    {
        const int id = 13;
        CreateUpdateProductRequestModel model = new CreateUpdateProductRequestModel()
        {
            Description = "description",
            Name = "name",
            Sku = "sku"
        };

        [Fact]
        public async Task Create_OnSuccess_ReturnStatusCode201()
        {
            var productServiceMock = SetupProductServiceMock();

            var sut = new ProductController(productServiceMock.Object);

            var result = await sut.Create(model) as CreatedAtActionResult;
            result.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task Create_OnSuccess_ReturnId()
        {
            var productServiceMock = SetupProductServiceMock();
            productServiceMock.Setup(x => x.CreateProduct(model)).ReturnsAsync(id);

            var sut = new ProductController(productServiceMock.Object);

            var result = await sut.Create(model) as CreatedAtActionResult;

            result.Value.Should().Be(id);
        }

        [Fact]
        public async Task Create_OnSuccess_InvokesProductServiceExactlyOnce()
        {
            var productServiceMock = SetupProductServiceMock();

            var sut = new ProductController(productServiceMock.Object);

            var result = await sut.Create(model);

            productServiceMock.Verify(x => x.CreateProduct(model), Times.Once());
        }

        private Mock<IProductService> SetupProductServiceMock()
        {
            var mock = new Mock<IProductService>();

            return mock;
        }
    }
}