using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitaraKauppa.Core.Carts;
using KitaraKauppa.Service.CartsService;

namespace KitaraKauppa.Service.Repositories.Carts
{
    public interface ICartRepository
    {
        public Task <CartDto> GetCart(Guid cartId);
    }
}