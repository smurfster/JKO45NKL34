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
using Domain.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Services
{
    public class CustomerServiceTest
    {
        const int id = 1;
        const string name = "Name";
        const string email = "some@one.com";
        const string phoneNumber = "093242343";
        readonly CustomerEntity testCustomer = new CustomerEntity("name", "name@some.com", "7897890890");

        Mock<EFContext> dbContextMock = new Mock<EFContext>();

        CreateUpdateCustomerRequestModel createCustomerRequestModel = new CreateUpdateCustomerRequestModel()
        {
            Email = email,
            Name = name,
            Phone = phoneNumber
        };

        [Fact]
        public async Task Delete_WhenCalled_InvokesCustomerRepositoryAndDbContextSave()
        {
            const int id = 1;

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(x => x.DeleteCustomer(id)).ReturnsAsync(true);

            var customerService = new CustomerService(customerRepositoryMock.Object, dbContextMock.Object);

            var result = await customerService.Delete(id);

            customerRepositoryMock.Verify(x => x.DeleteCustomer(id), Times.Once());
            dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public async Task Delete_OnSuccess_ReturnsTrue()
        {
            const int id = 1;

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(x => x.DeleteCustomer(id)).ReturnsAsync(true);

            var customerService = new CustomerService(customerRepositoryMock.Object, dbContextMock.Object);

            var result = await customerService.Delete(id);

            result.Should().Be(true);
        }

        [Fact]
        public async Task Delete_NotFound_ReturnsFalse()
        {
            const int id = 1;

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(x => x.DeleteCustomer(id)).ReturnsAsync(false);

            var customerService = new CustomerService(customerRepositoryMock.Object, dbContextMock.Object);

            var result = await customerService.Delete(id);

            result.Should().Be(false);
        }

        [Fact]
        public async Task UpdateCustomer_WhenCalled_InvokesCustomerRepositoryAndDbContextMock()
        {
            var customerRepositoryMock = new Mock<ICustomerRepository>();

            customerRepositoryMock.Setup(x => x.UpdateCustomer(id, It.IsAny<CustomerEntity>()));

            var customerService = new CustomerService(customerRepositoryMock.Object, dbContextMock.Object);

            await customerService.Update(id, createCustomerRequestModel);

            customerRepositoryMock.Verify(x => x.UpdateCustomer(id, It.IsAny<CustomerEntity>()), Times.Once());
            dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public async Task CreateCustomer_WhenCalled_InvokesCustomerRepositoryAndDbContextMock()
        {
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            
            customerRepositoryMock.Setup(x => x.CreateCustomer(It.IsAny<CustomerEntity>())).ReturnsAsync(new CustomerEntity(name, email, phoneNumber));

            var customerService = new CustomerService(customerRepositoryMock.Object, dbContextMock.Object);

            var result = await customerService.CreateCustomer(createCustomerRequestModel);

            customerRepositoryMock.Verify(x => x.CreateCustomer(It.IsAny<CustomerEntity>()), Times.Once());
            dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
         
        [Fact]
        public async Task GetCustomer_WhenCalled_InvokesCustomerRepository()
        {
            const int id = 1;

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(x => x.GetCustomerById(id)).ReturnsAsync(testCustomer);

            var customerService = new CustomerService(customerRepositoryMock.Object, dbContextMock.Object);

            var result = await customerService.GetCustomer(id);

            customerRepositoryMock.Verify(x => x.GetCustomerById(id), Times.Once());
        }

        public async Task GetCustomer_OnSuccess_Returns_Customer()
        {
            const int id = 1;

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(x => x.GetCustomerById(id)).ReturnsAsync(testCustomer);

            var customerService = new CustomerService(customerRepositoryMock.Object, dbContextMock.Object);

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

            var customerService = new CustomerService(customerRepositoryMock.Object, dbContextMock.Object);

            var result = await customerService.GetCustomer(id);

            result.Should().BeNull();
        }
    }
}