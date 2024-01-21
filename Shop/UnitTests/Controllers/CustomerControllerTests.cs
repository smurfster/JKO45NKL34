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
        [Fact]
        public async void Get_OnSuccess_ReturnStatusCode200()
        {
            var customerServiceMock = new Mock<ICustomerService>();
            var sut = new CustomerController(customerServiceMock.Object);

            var result = (StatusCodeResult)await sut.Get(1);

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void Get_OnSuccess_InvokesCustomerServiceExactlyOnce()
        {
            const int id = 1;
            var customerServiceMock = new Mock<ICustomerService>();
            customerServiceMock.Setup(service => service.GetCustomer(id)).ReturnsAsync(new GetUserResponseModel());

            var sut = new CustomerController(customerServiceMock.Object);

            var result = (StatusCodeResult)await sut.Get(id);

            customerServiceMock.Verify(x => x.GetCustomer(id), Times.Once());
        }
    }
}