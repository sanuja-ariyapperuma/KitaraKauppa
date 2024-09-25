using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitaraKauppa.Core.Products;

namespace KitaraKauppa.Service.CategoriesService
{
    public class CreateCategoryDto
    {
        public string CategoryName { get; set; }
        public Guid? ParentId { get; set; }

        public Category ConvertToCategory()
        {
            return new Category { CategoryId = Guid.NewGuid(), CategoryName = this.CategoryName, ParentId = this.ParentId };
        }

    }


    public class UpdateCategoryDto
    {
        public string CategoryName { get; set; }
        public Guid? ParentId { get; set; }
    }
}
