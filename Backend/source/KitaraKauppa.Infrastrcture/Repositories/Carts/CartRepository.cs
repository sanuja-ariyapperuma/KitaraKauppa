using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitaraKauppa.Core.Carts;
using KitaraKauppa.Service.Repositories.Carts;
using KitaraKauppa.Infrastrcture.Database;
using Microsoft.EntityFrameworkCore;
using KitaraKauppa.Service.CartsService;

namespace KitaraKauppa.Infrastrcture.Repositories.Carts
{
    public class CartRepository : ICartRepository
    {
        private KitaraKauppaDbContext _context;

        public CartRepository(KitaraKauppaDbContext context)
        {
            _context = context;
        }
        public async Task<CartDto> GetCart(Guid cartId)
        {
            var cart = await _context.Carts
                         .Include(c => c.CartItems)
                            .ThenInclude(ci => ci.Variation)
                         .FirstOrDefaultAsync(c => c.CartId == cartId);

            if (cart == null)
            {
                return null;
            }

            // Map the Cart entity to CartDto
            var cartDto = new CartDto
            {
                Id = cart.CartId,
                CartItems = cart.CartItems.Select(ci => new CartItemDto
                {
                    CartId = ci.CartId,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity
                }).ToList()
            };

            return cartDto;
        }
    }
}