using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KitaraKauppa.Core.Orders;
using KitaraKauppa.Core.Shared;
using KitaraKauppa.Service.OrdersService.Dtos;
using KitaraKauppa.Service.Repositories.Orders;
using KitaraKauppa.Service.Repositories.Products;
using KitaraKauppa.Service.Repositories.Users;
using KitaraKauppa.Service.Shared;

namespace KitaraKauppa.Service.OrdersService
{
    public class OrderManagement : IOrderManagement
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderManagement (IOrderRepository orderRepository, IUserRepository userRepository, IProductRepository productRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<KKResult<ReadOrderDto>> CreateOrder(CreateOrderDto newOrder)
        {

            var validOrder = await ValidateOrder(newOrder);

            if (!validOrder.Succeeded)
                return new KKResult<ReadOrderDto>().Fail(validOrder.Message);

            var order = _mapper.Map<Order>(newOrder);

            order = SafelyUpdatePrice(order);

            var savedOrder = await _orderRepository.CreateOrder(order);

            var readOrder = _mapper.Map<ReadOrderDto>(savedOrder);

            return new KKResult<ReadOrderDto>().SucceededWithValue(readOrder);
        }

        public async Task<KKResult<ReadOrderDto>> GetOrder(Guid orderId)
        {
            var order = await _orderRepository.GetOrder(orderId);
            
            if (order == null)
                return new KKResult<ReadOrderDto>().Fail("Order not found");

            var readOrder = _mapper.Map<ReadOrderDto>(order);
            return new KKResult<ReadOrderDto>().SucceededWithValue(readOrder);
        }

        public async Task<bool> UpdateOrder(Guid orderId, OrderStatus orderStatus) => await _orderRepository.UpdateOrder(orderId, orderStatus);

        private async Task<KKResult<bool>> ValidateOrder(CreateOrderDto newOrder) 
        {
            var user = await _userRepository.GetByIdAsync(newOrder.UserId);
            var userAddress = user.UserAddresses.FirstOrDefault(ua => ua.Id == newOrder.ShippingAddressId);

            if (user is null)
                return new KKResult<bool>().Fail("User not found");

            if (userAddress is null)
                return new KKResult<bool>().Fail("User address not found");

            foreach (var orderItem in newOrder.OrderItems)
            {
                var product = await _productRepository.GetProductById(orderItem.ProductId);

                if (product is null)
                    return new KKResult<bool>().Fail("User address not found");

                if (!product.Colors.Any(a => a.Id == orderItem.ColorId))
                    return new KKResult<bool>().Fail("Product color not found");

            }

            return new KKResult<bool>().SucceededWithValue(true);
        }

        private Order SafelyUpdatePrice(Order newOrder) 
        {
            foreach (var orderItem in newOrder.OrderItems)
            {
                var product = _productRepository.GetProductById(orderItem.ProductId).Result;
                orderItem.Price = product!.UnitPrice;
            }
            return newOrder;
        }

        public async Task<KKResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            var orders = await  _orderRepository.GetAllOrders();

            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders.Value);

            return new KKResult<IEnumerable<OrderDto>>().SucceededWithValue(orderDtos);

        }
    }
}