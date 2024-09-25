using KitaraKauppa.Service.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.Repositories.Carts
{
    public class CreateCartItemsDto
    {
        [Required]
        public Guid CartId { get; set; }
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int? Quantity { get; set; }
    }
}