using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitaraKauppa.Core.Carts;
using KitaraKauppa.Service.CustomExceptions;
using KitaraKauppa.Service.Repositories.Carts;

namespace KitaraKauppa.Service.CartsService
{
    public class CartManagement : ICartManagement
    {
        private ICartRepository _cartRepository;
        public CartManagement(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<CartDto> GetCart(Guid cartId)
        {
            return await _cartRepository.GetCart(cartId);
        }
    }
}