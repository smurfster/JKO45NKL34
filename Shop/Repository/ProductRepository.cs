﻿using Domain.Entities.Customer;
using Domain.Entities.Product;
using Domain.Persistence;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await dbContext.Products.FindAsync(id);

            if (product == null) { return false; }

            dbContext.Products.Remove(product);
            return true;
        }

        public async Task<ProductEntity> GetProduct(int id)
        {
            var result = await dbContext.Products.SingleOrDefaultAsync(o => o.Id == id);
            return result;
        }

        public async Task<ErrorOr<ProductEntity>> UpdateProduct(int id, ProductEntity productEntity)
        {
            var product = await dbContext.Products.FindAsync(id);
            
            if (product == null) return Error.NotFound();

            product.UpdateDescription(productEntity.Description);
            product.UpdateSKU(productEntity.Sku.Value);
            product.UpdateName(productEntity.Name);

            return product;
        }
    }
}