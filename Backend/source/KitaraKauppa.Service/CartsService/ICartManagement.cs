using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitaraKauppa.Core.Carts;

namespace KitaraKauppa.Service.CartsService
{
    public interface ICartManagement
    {
        public Task <CartDto> GetCart(Guid cartId);
    }
}