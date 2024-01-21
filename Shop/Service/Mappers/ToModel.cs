using Domain.Entities.Customer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mappers
{
    internal static class ToModel
    {
        internal static GetCustomerResponseModel CustomerEntityToGetCustomerResponseModel
            (this CustomerEntity entity)
        {
            return new GetCustomerResponseModel()
            {
                Id = entity.Id,
                Email = entity.Email,
                Name = entity.Name,
                Phone = entity.Phone
            };
        }
    }
}
