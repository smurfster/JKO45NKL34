using API.Controllers;
using Castle.Core.Resource;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using Service;
using System.Xml.Linq;

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

        GetProductResponseModel modelresp = new GetProductResponseModel()
        {
            Id = id,
            Description = "description",
            Name = "name",
            Sku = "sku"
        };

        [Fact]
        public async Task Delete_OnSuccess_ReturnOK()
        {
            var productServiceMock = SetupProductServiceMock();

            var sut = new ProductController(productServiceMock.Object);

            var result = await sut.Delete(id) as OkObjectResult;
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Update_OnSuccess_ReturnStatusCode204()
        {
            var productServiceMock = SetupProductServiceMock();
            productServiceMock
                .Setup(x => x.Update(id, model))
                .ReturnsAsync(true);

            var sut = new ProductController(productServiceMock.Object);

            var result = await sut.Update(id, model) as NoContentResult;
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Update_OnSuccess_InvokesCustomerServiceExactlyOnce()
        {
            var productServiceMock = SetupProductServiceMock();           

            var sut = new ProductController(productServiceMock.Object);

            var result = await sut.Update(id, model);

            productServiceMock.Verify(x => x.Update(id, model), Times.Once());
        }

        [Fact]
        public async Task Update_Fail_ReturnStatusCode404()
        {
            var productServiceMock = SetupProductServiceMock();
            productServiceMock
                .Setup(x => x.Update(id, model))
                .ReturnsAsync(false); ; 

            var sut = new ProductController(productServiceMock.Object);
            
            var result = await sut.Update(id, model) as NotFoundResult;
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task Get_OnSuccess_Return_Product()
        {
            var productServiceMock = SetupProductServiceMock();
            productServiceMock
                .Setup(x => x.GetProduct(id))
                .ReturnsAsync(modelresp);

            var sut = new ProductController(productServiceMock.Object);

            var result = await sut.Get(id);

            result.Should().BeOfType<OkObjectResult>();
            var resultObj = (OkObjectResult)result;
            resultObj.Value.Should().BeOfType<GetProductResponseModel>();
            var customer = resultObj.Value as GetProductResponseModel;
            customer.Should().NotBeNull();
            customer.Sku.Should().Be(model.Sku);
            customer.Id.Should().Be(id);
            customer.Name.Should().Be(model.Name);
            customer.Sku.Should().Be(model.Sku);
        }

        [Fact]
        public async Task Get_OnNotFound_Return_NotFound404()
        {
            var productServiceMock = SetupProductServiceMock();
            productServiceMock
                .Setup(x => x.GetProduct(id));

            var sut = new ProductController(productServiceMock.Object);

            var result = await sut.Get(id);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode200()
        {
            var productServiceMock = SetupProductServiceMock();
            productServiceMock
                .Setup(x => x.GetProduct(id)).ReturnsAsync(modelresp); 
            
            var sut = new ProductController(productServiceMock.Object);

            var result = await sut.Get(id) as OkObjectResult;

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnSuccess_InvokesCustomerServiceExactlyOnce()
        {
            var productServiceMock = SetupProductServiceMock();

            var sut = new ProductController(productServiceMock.Object);

            var result = await sut.Get(id);

            productServiceMock.Verify(x => x.GetProduct(id), Times.Once());
        }



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