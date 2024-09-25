using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitaraKauppa.Core.Carts;
using KitaraKauppa.Service.CustomExceptions;
using KitaraKauppa.Service.Repositories.Carts;

namespace KitaraKauppa.Service.CartsService
{
    public class CartItemsManagement : ICartItemsManagement
    {
        private ICartItemsRepository _cartItemsRepository;

        public CartItemsManagement(ICartItemsRepository cartItemsRepository)
        {
            _cartItemsRepository = cartItemsRepository;
        }

        public async Task<bool> CreateCartItems(CartItem cartItem)
        {
            var result = await _cartItemsRepository.CreateCartItems(cartItem);
            if(result == false) throw new RecordNotCreatedException("cartItems");
            return result;
        }

        public async Task<bool> DeleteProductFromCart(Guid cartItemId)
        {
            var result = await _cartItemsRepository.DeleteProductFromCart(cartItemId);
            if(result == false) throw new RecordNotFoundException("cartItems");
            return result;
        }

        public async Task<CartItem> GetCartItem(Guid cartItemId)
        {
            var result = await _cartItemsRepository.GetCartItem(cartItemId);
            if(result == null) throw new RecordNotFoundException("cartItems");
            return result;
        }

        public async Task<bool> UpdateCart(Guid cartItemId, Guid productId, Guid variantId, int quantity)
        {
            var result = await _cartItemsRepository.UpdateCart(cartItemId, productId, variantId, quantity); 
            if(result == false) throw new RecordNotFoundException("cartItems");
            return result;
        }
    }
}