using API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using Service;

namespace UnitTests.Controllers
{
    public class ProductControllerTests
    {
        CreateUpdateProductRequestModel model = new CreateUpdateProductRequestModel()
        {
            Description = "description",
            Id = 1,
            Name = "name",
            Sku = "sku"
        };

        [Fact]
        public async Task Create_OnSuccess_ReturnStatusCode201()
        {
            int id = 23;
            
            var sut = new ProductController();

            var result = await sut.Create(model) as CreatedAtActionResult;
            result.StatusCode.Should().Be(201);
        }
    }
}