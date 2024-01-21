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

        [Fact]
        public async Task Create_OnSuccess_ReturnStatusCode201()
        {
            var customer = new CreateCustomerRequestModel()
            {
                Email = email,
                Name = name,
                Phone = phoneNumber
            };

            Mock<ICustomerService> customerServiceMock = SetupGetCustomerServiceMock();

            var sut = new CustomerController(customerServiceMock.Object);

            var result = await sut.Create(customer) as OkObjectResult;

            result.StatusCode.Should().Be(201);
        }


        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode200()
        {
            Mock<ICustomerService> customerServiceMock = SetupGetCustomerServiceMock();

            var sut = new CustomerController(customerServiceMock.Object);

            var result = await sut.Get(id) as OkObjectResult;

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnSuccess_InvokesCustomerServiceExactlyOnce()
        {
            Mock<ICustomerService> customerServiceMock = SetupGetCustomerServiceMock();

            var sut = new CustomerController(customerServiceMock.Object);

            var result = await sut.Get(id);

            customerServiceMock.Verify(x => x.GetCustomer(id), Times.Once());
        }

        [Fact]
        public async Task Get_OnSuccess_Return_Customer()
        {
            Mock<ICustomerService> customerServiceMock = SetupGetCustomerServiceMock();

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

        private static Mock<ICustomerService> SetupGetCustomerServiceMock()
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

            return customerServiceMock;
        }
    }
}