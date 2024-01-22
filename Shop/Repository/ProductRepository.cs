using Domain.Entities.Product;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        public Task<ProductEntity> CreateProduct(ProductEntity productEntity)
        {
            throw new NotImplementedException();
        }
    }
}