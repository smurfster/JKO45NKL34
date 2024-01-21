using API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Controllers
{
    public class CustomerControllerTests
    {
        [Fact]
        public async void Get_OnSuccess_ReturnStatusCode200()
        {
            var sut = new CustomerController();

            var result = (StatusCodeResult)await sut.Get(1);

            result.StatusCode.Should().Be(200);
        }
    }
}