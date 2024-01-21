using API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using Service;

namespace UnitTests.Controllers
{
    public class CustomerControllerTests
    {
        const int id = 1;
        const string name = "Name";
        const string email = "some@one.com";
        const string phoneNumber = "093242343";
        
        CreateUpdateCustomerRequestModel createCustomerRequestModel = new CreateUpdateCustomerRequestModel()
            {
                Email = email,
                Name = name,
                Phone = phoneNumber
            };

        [Fact]
        public async Task Update_OnSuccess_ReturnStatusCode204()
        {            
            Mock<ICustomerService> customerServiceMock = SetupCustomerServiceMock();
            customerServiceMock.Setup(x => x.Update(createCustomerRequestModel));

            var sut = new CustomerController(customerServiceMock.Object);

            var result = await sut.Update(createCustomerRequestModel) as NoContentResult;
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Update_OnSuccess_InvokesCustomerServiceExactlyOnce()
        {
            Mock<ICustomerService> customerServiceMock = SetupCustomerServiceMock();
            customerServiceMock.Setup(x => x.Update(createCustomerRequestModel));

            var sut = new CustomerController(customerServiceMock.Object);

            var result = await sut.Update(createCustomerRequestModel);

            customerServiceMock.Verify(x => x.Update(createCustomerRequestModel), Times.Once());
        }

        [Fact]
        public async Task Create_OnSuccess_ReturnStatusCode201()
        {
            int id = 23;
            Mock<ICustomerService> customerServiceMock = SetupCustomerServiceMock();
            customerServiceMock.Setup(x => x.CreateCustomer(It.IsAny<CreateUpdateCustomerRequestModel>())).ReturnsAsync(id);

            var sut = new CustomerController(customerServiceMock.Object);

            var result = await sut.Create(createCustomerRequestModel) as CreatedAtActionResult;
            result.StatusCode.Should().Be(201);
            result.Value.Should().Be(id);
        }

        [Fact]
        public async Task Create_OnSuccess_InvokesCustomerServiceExactlyOnce()
        {
            Mock<ICustomerService> customerServiceMock = SetupCustomerServiceMock();

            var sut = new CustomerController(customerServiceMock.Object);

            var result = await sut.Create(createCustomerRequestModel);

            customerServiceMock.Verify(x => x.CreateCustomer(createCustomerRequestModel), Times.Once());
        }


        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode200()
        {
            Mock<ICustomerService> customerServiceMock = SetupCustomerServiceMock();

            var sut = new CustomerController(customerServiceMock.Object);

            var result = await sut.Get(id) as OkObjectResult;

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnSuccess_InvokesCustomerServiceExactlyOnce()
        {
            Mock<ICustomerService> customerServiceMock = SetupCustomerServiceMock();

            var sut = new CustomerController(customerServiceMock.Object);

            var result = await sut.Get(id);

            customerServiceMock.Verify(x => x.GetCustomer(id), Times.Once());
        }

        [Fact]
        public async Task Get_OnSuccess_Return_Customer()
        {
            Mock<ICustomerService> customerServiceMock = SetupCustomerServiceMock();

            var sut = new CustomerController(customerServiceMock.Object);

            var result = await sut.Get(id);

            result.Should().BeOfType<OkObjectResult>();
            var resultObj = (OkObjectResult)result;
            resultObj.Value.Should().BeOfType<GetCustomerResponseModel>();
            var customer = resultObj.Value as GetCustomerResponseModel;
            customer.Should().NotBeNull();
            customer.Email.Should().Be(email);
            customer.Id.Should().Be(id);
            customer.Name.Should().Be(name);
            customer.Phone.Should().Be(phoneNumber);
        }        

        [Fact]
        public async Task Get_OnNoCustomerFound_ReturnStatusCode404()
        {
            var customerServiceMock = new Mock<ICustomerService>();

            customerServiceMock.Setup(service => service.GetCustomer(id));

            var sut = new CustomerController(customerServiceMock.Object);

            var result = await sut.Get(id);

            result.Should().BeOfType<NotFoundResult>();
            var resultObj = (NotFoundResult)result;
            resultObj.StatusCode.Should().Be(404);            
        }

        private Mock<ICustomerService> SetupCustomerServiceMock()
        {
            var customerServiceMock = new Mock<ICustomerService>();

            customerServiceMock
                .Setup(service => service.GetCustomer(id))
                .ReturnsAsync(new GetCustomerResponseModel()
                {
                    Id = id,
                    Name = name,
                    Email = email,
                    Phone = phoneNumber
                }
                );

            customerServiceMock
                .Setup(service => service.CreateCustomer(createCustomerRequestModel));

            return customerServiceMock;
        }
    }
}