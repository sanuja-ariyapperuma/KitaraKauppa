using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitaraKauppa.Core.ProductReviews;
using KitaraKauppa.Core.Products;
using KitaraKauppa.Core.Shared;
using KitaraKauppa.Service.Shared;

namespace KitaraKauppa.Service.ProductsServices.Dtos
{
    public class CreateProductDto

    {
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public Guid BrandId { get; set; }
        public decimal UnitPrice { get; set; } 
        public VarientType VarientType { get; set; }
        public Orientation ProductOrientation { get; set; }
        public Guid[] ProductColors { get; set; } = [];

        public virtual KKResult<string> Validate() 
        {
            if(String.IsNullOrEmpty(this.Title.Trim()))
                return new KKResult<string>().Fail("Title is required");

            if (String.IsNullOrEmpty(this.Description.Trim()))
                return new KKResult<string>().Fail("Description is required");

            if (this.BrandId == Guid.Empty)
                return new KKResult<string>().Fail("Brand is required");

            if (this.UnitPrice <= 0)
                return new KKResult<string>().Fail("UnitPrice is required");

            if (ProductColors.Length == 0)
                return new KKResult<string>().Fail("At least one color is required");

            return new KKResult<string>().SucceededWithValue("Validated");
        }
    }

}