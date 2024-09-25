using AutoMapper;
using KitaraKauppa.Core.Products;
using KitaraKauppa.Service.ProductsServices.Dtos;
using KitaraKauppa.Service.Repositories.Products;
using KitaraKauppa.Service.Shared;
using Microsoft.Extensions.Configuration;

namespace KitaraKauppa.Service.ProductsServices
{
    public class ProductManagement : IProductManagement
    {

        private readonly IProductRepository _productRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public ProductManagement(IProductRepository productRepository, IColorRepository colorRepository, IMapper mapper, IConfiguration configuration)
        {
            _productRepository = productRepository;
            _colorRepository = colorRepository;
            _mapper = mapper;
        }

        public async Task<KKResult<string>> DeleteProductById(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
                return new KKResult<string>().Fail("Product couldnt find");

            await _productRepository.DeleteAsync(product);

            return new KKResult<string>().SucceededWithValue("Product deleted successfully");

        }

        public async Task<KKResult<ReadProductDto>> CreateProduct(CreateProductDto product)
        {

            var isTitleExisting = await _productRepository.IsAProductWithTheTitleAlreadyExists(product.BrandId, product.Title);

            if (isTitleExisting)
                return new KKResult<ReadProductDto>().Fail("Product with the same title under same brand already exists");

            var newProduct = _mapper.Map<Product>(product);

            var colors = await _colorRepository.GetColorsByIdsAsync(product.ProductColors);

            if (colors.Count != product.ProductColors.Length)
                return new KKResult<ReadProductDto>().Fail("Some of the colors are not found");

            newProduct.Colors = colors;

            var result = await _productRepository.AddAsync(newProduct);

            var productRestult = _mapper.Map<ReadProductDto>(result);
            productRestult.ProductColors = product.ProductColors;

            return new KKResult<ReadProductDto>().SucceededWithValue(productRestult);
        }

        public async Task<KKResult<ReadProductDto>> GetProductById(Guid productId)
        {
            var product = await _productRepository.GetProductById(productId);
            if (product == null)
                return new KKResult<ReadProductDto>().Fail("Product not found");

            var productDto = _mapper.Map<ReadProductDto>(product);

            return new KKResult<ReadProductDto>().SucceededWithValue(productDto);
        }

        public async Task<KKResult<string>> UpdateProduct(Guid productId, UpdateProductDto productToUpdate)
        {
            var product = await _productRepository.GetProductById(productId);

            if(product == null)
                return new KKResult<string>().Fail("Product not found");

            product.Title = productToUpdate.Title;
            product.Description = productToUpdate.Description;
            product.UnitPrice = productToUpdate.UnitPrice;
            product.BrandId = productToUpdate.BrandId;

            await _productRepository.UpdateAsync(product);

            return new KKResult<string>().SucceededWithValue("Product updated successfully");
        }

        public async Task<KKResult<PaginatedResults<ProductDto>>> GetAllProducts(ProductQueryOptions pqdto)
        {
            var productsFromDb = await _productRepository.GetAllProductsAsync(pqdto);
            
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(productsFromDb.Results);

            var response = new PaginatedResults<ProductDto>
            {
                Results = productsDto,
                TotalPages = productsFromDb.TotalPages,
                TotalRecords = productsFromDb.TotalRecords,
                PageNo = productsFromDb.PageNo,
                PageSize = productsFromDb.PageSize
            };

            return new KKResult<PaginatedResults<ProductDto>>().SucceededWithValue(response);
        }
    }
}