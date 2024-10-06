using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KitaraKauppa.Core.Shared;

namespace KitaraKauppa.Core.Products
{
    public class Product : BaseEntity
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; } = String.Empty;
        [Required]
        public string Description { get; set; } = String.Empty;
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public VarientType VarientType { get; set; }
        [Required]
        public Orientation Orientation { get; set; }
        public Guid BrandId { get; set; }
        public Brand? Brand { get; set; }
        public ICollection<Image>? Images { get; set; }
        public ICollection<Color>? Colors { get; set; } = new List<Color>();
    }
}