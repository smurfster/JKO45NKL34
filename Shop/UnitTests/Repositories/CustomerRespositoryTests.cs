using Domain.Entities.Customer;
using Domain.Persistence;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Repositories
{
    public class CustomerRespositoryTests
    {
        [Fact]
        public async Task GetCustomerById_OnSuccess_Return_Customer()
        {
            CustomerEntity item;
            IQueryable<CustomerEntity> data;
            SetupData(out item, out data);

            var mockSet = new Mock<DbSet<CustomerEntity>>();
            mockSet.As<IQueryable<CustomerEntity>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<CustomerEntity>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<CustomerEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<CustomerEntity>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var dbContextMock = new Mock<EFContext>();
            dbContextMock.Setup(c => c.Customers).ReturnsDbSet(mockSet.Object);

            var sut = new CustomerRepository(dbContextMock.Object);

            var result = await sut.GetCustomerById(item.Id);

            result.Email.Should().Be(item.Email);
            result.Phone.Should().Be(item.Phone);
            result.Name.Should().Be(item.Name);
        }

        [Fact]
        public async Task GetCustomerById_NotFound_Return_Null()
        {
            CustomerEntity item;
            IQueryable<CustomerEntity> data;
            SetupData(out item, out data);

            var mockSet = new Mock<DbSet<CustomerEntity>>();
            mockSet.As<IQueryable<CustomerEntity>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<CustomerEntity>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<CustomerEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<CustomerEntity>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var dbContextMock = new Mock<EFContext>();
            dbContextMock.Setup(c => c.Customers).ReturnsDbSet(mockSet.Object);

            var sut = new CustomerRepository(dbContextMock.Object);

            var result = await sut.GetCustomerById(item.Id+10);

            result.Should().BeNull();
        }

        private static void SetupData(out CustomerEntity item, out IQueryable<CustomerEntity> data)
        {
            item = new CustomerEntity("bob", "steve@something.com", "3242342");
            var type = item.GetType();
            var idProperty = type.GetProperty(nameof(CustomerEntity.Id));
            idProperty.SetValue(item, 1);

            data = new List<CustomerEntity>
            {
                item
            }.AsQueryable();
        }
    }
}