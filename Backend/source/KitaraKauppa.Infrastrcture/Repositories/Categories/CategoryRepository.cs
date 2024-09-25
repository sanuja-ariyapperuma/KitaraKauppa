using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitaraKauppa.Core.Products;
using KitaraKauppa.Infrastrcture.Database;
using KitaraKauppa.Service.CategoriesService;
using KitaraKauppa.Service.Repositories.Categories;
using Microsoft.EntityFrameworkCore;

namespace KitaraKauppa.Infrastrcture.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private KitaraKauppaDbContext _context;

        public CategoryRepository(KitaraKauppaDbContext context)
        {
            _context = context;
        }
        public async Task<Category?> CreateCategory(Category category)
        {
            var newCategory = (await _context.Categories.AddAsync(category)).Entity;
            _context.SaveChanges();
            return newCategory;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<bool> UpdateCategory(Guid categoryId, UpdateCategoryDto updateCategoryDto)
        {
            Category category = await _context.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefaultAsync<Category>();
            if (category is null) return false;
            category.CategoryName = updateCategoryDto.CategoryName;
            category.ParentId = updateCategoryDto.ParentId;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCategory(Guid categoryId)
        {
            Category category = await _context.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefaultAsync<Category>();
            if (category is null) return false;
            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}