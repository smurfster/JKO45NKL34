using Domain.Entities.Product;

namespace Repository
{
    public interface IProductRepository
    {
        Task<ProductEntity> CreateProduct(ProductEntity productEntity);
    }
}
