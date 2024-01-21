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

namespace UnitTests.Services
{
    public class CustomerServiceTest
    {
        [Fact]
        public async Task GetCustomer_WhenCalled_InvokesCustomerRepository()
        {
            const int id = 1;

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(x => x.GetCustomerById(id)).ReturnsAsync(new Domain.Entities.Customer.CustomerEntity() );

            var customerService = new CustomerService(customerRepositoryMock.Object);

            var result = await customerService.GetCustomer(id);

            customerRepositoryMock.Verify(x => x.GetCustomerById(id), Times.Once());
        }
    }
}