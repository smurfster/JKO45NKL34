using Domain.Entities.Product;
using Domain.Persistence;
using Models;
using Repository;

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

        public Task<int> CreateProduct(CreateUpdateProductRequestModel model)
        {
            throw new NotImplementedException();
        }
    }
}