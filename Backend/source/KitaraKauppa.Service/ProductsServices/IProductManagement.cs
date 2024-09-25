using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitaraKauppa.Core.Products;
using KitaraKauppa.Service.ProductsServices.Dtos;
using KitaraKauppa.Service.Shared;

namespace KitaraKauppa.Service.ProductsServices
{
    public interface IProductManagement
    {
        public Task<KKResult<ReadProductDto>> CreateProduct(CreateProductDto product);
        public Task<KKResult<ReadProductDto>> GetProductById(Guid productId);
        public Task<KKResult<string>> UpdateProduct(Guid productId, UpdateProductDto productToUpdate);
        public Task<KKResult<string>> DeleteProductById(Guid productId);
        public Task<KKResult<PaginatedResults<ProductDto>>> GetAllProducts(ProductQueryOptions pqdto);
    }
}