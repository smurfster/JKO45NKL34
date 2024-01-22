using Domain.Entities.Product;
using Domain.Persistence;
using Models;
using Repository;
using Service.Mappers;
using System.Runtime.InteropServices;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly EFContext dbContext;
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository, EFContext dbContext)
        {
            this.dbContext = dbContext;
            this.productRepository = productRepository;
        }

        public async Task<int> CreateProduct(CreateUpdateProductRequestModel model)
        {
            var entity = model.CreateUpdateProductRequestModelToProductEntity();
            var product = await productRepository.CreateProduct(entity);

            dbContext.SaveChanges();
            return product.Id;
        }

        public async Task<GetProductResponseModel> GetProduct(int id)
        {
            var result = await productRepository.GetProduct(id);

            if (result == null)
            {
                return null;
            }

            return result.ProductEntityToGetProductResponseModel();
        }

        public void Update(int id, CreateUpdateProductRequestModel model)
        {
            throw new NotImplementedException();
        }
    }
}