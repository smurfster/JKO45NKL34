using Domain.Entities.Product;
using ErrorOr;

namespace Repository
{
    public interface IProductRepository
    {
        Task<ProductEntity> CreateProduct(ProductEntity productEntity);
        Task<ProductEntity> GetProduct(int id);
        Task<ErrorOr<ProductEntity>> UpdateProduct(int id, ProductEntity productEntity);
    }
}
