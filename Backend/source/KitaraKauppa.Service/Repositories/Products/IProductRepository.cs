using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitaraKauppa.Core.Products;
using KitaraKauppa.Service.ProductsServices.Dtos;
using KitaraKauppa.Service.Shared;
using KitaraKauppa.Service.Shared_Dtos;

namespace KitaraKauppa.Service.Repositories.Products
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<PaginatedResults<Product>> GetAllProductsAsync(ProductQueryOptions filtration);
        public Task<bool> IsAProductWithTheTitleAlreadyExists(Guid brandId, string title);
        public Task<Product?> GetProductById(Guid productId);
    }
}