using AutoMapper;
using KitaraKauppa.Core.Orders;
using KitaraKauppa.Service.OrdersService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.MappingProfile
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<CreateOrderDto, Order>()
            .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.ShippingAddressId))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<CreateOrderItems, OrderItem>()
            .ForMember(dest => dest.Price, opt => opt.Ignore()); // Prevent Price from being mapped directly

            CreateMap<Order, ReadOrderDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) 
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus))  
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.CreatedAt))  
            .ForMember(dest => dest.ShippingAddressId, opt => opt.MapFrom(src => src.AddressId))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderItem, CreateOrderItems>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => String.Concat(src.User.FirstName, " ", src.User.LastName)))
            .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => String.Concat(src.Address.AddressLine1, " ", src.Address.AddressLine2)))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Product.Title))
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Product.Brand.Name))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color.Name))
            .ForMember(dest => dest.Orientation, opt => opt.MapFrom(src => src.Orientation))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Units));

        }
    }
}
