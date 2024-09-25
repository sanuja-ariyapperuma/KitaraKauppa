using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using KitaraKauppa.Core.Products;
using KitaraKauppa.Infrastrcture.Database;
using KitaraKauppa.Service.ProductsServices.Dtos;
using KitaraKauppa.Service.Repositories.Products;
using KitaraKauppa.Service.Shared;
using KitaraKauppa.Service.Shared_Dtos;
using Microsoft.EntityFrameworkCore;

namespace KitaraKauppa.Infrastrcture.Repositories.Products
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {

        public ProductRepository(KitaraKauppaDbContext dbContext) : base(dbContext) {}

        public async Task<PaginatedResults<Product>> GetAllProductsAsync(ProductQueryOptions filtration)
        {
            var searchQuery = filtration.Search;

            IQueryable<Product> query = _dbSet
                .Include(e => e.Brand)
                .Include(e => e.Images)
                .AsNoTracking();

            if (!String.IsNullOrEmpty(filtration.Search)) 
            {
                query = query.Where(p => 
                    EF.Functions.ILike(p.Title, $"%{searchQuery}%") ||
                    EF.Functions.ILike(p.Description, $"%{searchQuery}%") ||
                    EF.Functions.ILike(p.Brand!.Name, $"%{searchQuery}%")
                    );
            }

            if (filtration.VarientType != null) 
            {
                query = query.Where(p => p.VarientType == filtration.VarientType);
            }

            query = filtration.OrderWith switch
            {
                ProductOrderOptions.ProductTitle => filtration.OrderBy switch
                {
                    OrderBy.ASC => query.OrderBy(p => p.Title),
                    OrderBy.DESC => query.OrderByDescending(p => p.Title),
                    _ => query.OrderBy(p => p.Title)
                },
                ProductOrderOptions.Price => filtration.OrderBy switch
                {
                    OrderBy.ASC => query.OrderBy(p => p.UnitPrice),
                    OrderBy.DESC => query.OrderByDescending(p => p.UnitPrice),
                    _ => query.OrderBy(p => p.Title)
                },
                _ => query.OrderBy(p => p.Title)
            };

            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecords / filtration.PageSize);

            query = query
            .Skip((filtration.PageNo) * filtration.PageSize)
            .Take(filtration.PageSize);

            var results = await query.ToListAsync();

            return new PaginatedResults<Product>
            {
                Results = results,
                TotalPages = totalPages,
                TotalRecords = totalRecords,
                PageNo = filtration.PageNo,
                PageSize = filtration.PageSize
            };

        }

        public async Task<bool> IsAProductWithTheTitleAlreadyExists(Guid brandId, string title) => 
            await _dbSet.AnyAsync(a => a.BrandId == brandId && a.Title == title && !a.IsDeleted);
        public async Task<Product?> GetProductById(Guid productId)
        {
            var products = await _dbSet
            .Include(p => p.Images)
            .Include(p => p.Brand)
            .Include(p => p.Colors)
            .FirstOrDefaultAsync(a => !a.IsDeleted && a.Id == productId);
            
            return products;
        }

    }
}