using Domain.Entities.Product;
using Domain.Persistence;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly EFContext dbContext;

        public ProductRepository(EFContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ProductEntity> CreateProduct(ProductEntity productEntity)
        {
            var result = await dbContext.Products.AddAsync(productEntity);
            return result.Entity;
        }

        public Task<ProductEntity> GetProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}