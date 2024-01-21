using Domain.Entities.Customer;
using Microsoft.EntityFrameworkCore;

namespace Domain.Persistence
{
    public interface IEFContext
    {
        DbSet<CustomerEntity> Customers { get; set; }
    }
}