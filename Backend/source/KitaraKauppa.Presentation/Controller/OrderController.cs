using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitaraKauppa.Core.Shared;
using KitaraKauppa.Service.CustomExceptions;
using KitaraKauppa.Service.OrdersService;
using KitaraKauppa.Service.OrdersService.Dtos;
using KitaraKauppa.Service.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KitaraKauppa.Presentation.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManagement _orderManagement;
        public OrderController(IOrderManagement orderManagement)
        {
            _orderManagement = orderManagement;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto newOrder)
        {

            var validateOrderResult = ValidateOrder(newOrder);

            if (!validateOrderResult.Succeeded)
                return BadRequest(validateOrderResult);
                
                var result = await _orderManagement.CreateOrder(newOrder);

                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateOrder(Guid orderId, OrderStatus orderStatus)
        {
            try
            {
                var success = await _orderManagement.UpdateOrder(orderId, orderStatus);

                if (success)
                {
                    return Ok(new { Message = "Order updated successfully." });
                }
                else
                {
                    throw new Exception("Something went wrong!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new KKResult<string>().Fail("Order id cannot be null"));
            

            var order = await _orderManagement.GetOrder(id);

            if (!order.Succeeded)
                return BadRequest(order);

            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderManagement.GetAllOrders();
            return Ok(orders);
        }

        private KKResult<string> ValidateOrder(CreateOrderDto order) 
        {
            if(order.UserId == Guid.Empty)
                return new KKResult<string>().Fail("User Id is required");

            if (order.ShippingAddressId == Guid.Empty)
                return new KKResult<string>().Fail("Shipping address is required");

            if (order.OrderItems.Count == 0)
                return new KKResult<string>().Fail("Order must have at least one item");

            foreach (var orderItem in order.OrderItems)
            {
                if (orderItem.ProductId == Guid.Empty)
                    return new KKResult<string>().Fail("Product Id is required");

                if (orderItem.ColorId == Guid.Empty)
                    return new KKResult<string>().Fail("Color Id is required");

                if (orderItem.Units <= 0)
                    return new KKResult<string>().Fail("Quantity must be greater than 0");

            }

            return new KKResult<string>().SucceededWithValue("Order is valid");
        }

    }
}