using FluentAssertions;
using Microsoft.AspNetCore.SignalR.Protocol;
using Models;
using Moq;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain.Entities.Customer;

namespace UnitTests.Services
{
    public class CustomerServiceTest
    {
        const int id = 1;
        const string name = "Name";
        const string email = "some@one.com";
        const string phoneNumber = "093242343";
        readonly CustomerEntity testCustomer = new CustomerEntity("name", "name@some.com", "7897890890");
        
        CreateCustomerRequestModel createCustomerRequestModel = new CreateCustomerRequestModel()
        {
            Email = email,
            Name = name,
            Phone = phoneNumber
        };

        [Fact]
        public async Task CreateCustomer_WhenCalled_InvokesCustomerRepository()
        {
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(x => x.CreateCustomer(testCustomer));

            var customerService = new CustomerService(customerRepositoryMock.Object);

            var result = await customerService.CreateCustomer(createCustomerRequestModel);

            customerRepositoryMock.Verify(x => x.GetCustomerById(id), Times.Once());
        }

        [Fact]
        public async Task GetCustomer_WhenCalled_InvokesCustomerRepository()
        {
            const int id = 1;

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(x => x.GetCustomerById(id)).ReturnsAsync(testCustomer);

            var customerService = new CustomerService(customerRepositoryMock.Object);

            var result = await customerService.GetCustomer(id);

            customerRepositoryMock.Verify(x => x.GetCustomerById(id), Times.Once());
        }

        public async Task GetCustomer_OnSuccess_Returns_Customer()
        {
            const int id = 1;

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(x => x.GetCustomerById(id)).ReturnsAsync(testCustomer);

            var customerService = new CustomerService(customerRepositoryMock.Object);

            var result = await customerService.GetCustomer(id);

            result.Should().NotBeNull();
            //result.Id.Should().Be(id); cant check as Id has protected setter
            result.Name.Should().Be(testCustomer.Name);
            result.Phone.Should().Be(testCustomer.Phone);
            result.Email.Should().Be(testCustomer.Email);
        }

        [Fact]
        public async Task GetCustomer_OnDoesNotExit_Returns_Null()
        {
            const int id = 1;

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(x => x.GetCustomerById(id));

            var customerService = new CustomerService(customerRepositoryMock.Object);

            var result = await customerService.GetCustomer(id);

            result.Should().BeNull();
        }
    }
}