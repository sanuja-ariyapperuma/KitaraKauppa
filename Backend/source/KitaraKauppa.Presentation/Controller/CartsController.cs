using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitaraKauppa.Core.Carts;
using KitaraKauppa.Service.CartsService;
using Microsoft.AspNetCore.Mvc;

namespace KitaraKauppa.Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private ICartManagement _cartManagement;

        public CartsController(ICartManagement cartManagement)
        {
            _cartManagement = cartManagement;
        }

        //[HttpGet("{cartId}")]
        //public async Task<CartDto> GetCart(Guid cartId)
        //{
        //    return await _cartManagement.GetCart(cartId);
        //}
    }
}