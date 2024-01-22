using Domain.Entities.Customer;
using Domain.Entities.Product;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mappers
{
    internal static class MappingExtensions
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

        internal static CustomerEntity CreateUpdateCustomerRequestModelToCustomerEntity(this CreateUpdateCustomerRequestModel model)
        {
            return new CustomerEntity(model.Name, model.Email, model.Phone);
        }

        internal static ProductEntity CreateUpdateProductRequestModelToProductEntity(this CreateUpdateProductRequestModel model)
        {
            return new ProductEntity(model.Name, model.Description, SKU.Create(model.Sku)!);
        }

        internal static GetProductResponseModel ProductEntityToGetProductResponseModel(this ProductEntity entity)
        {
            return new GetProductResponseModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Sku = entity.Sku.Value
            };
        }
    }
}
