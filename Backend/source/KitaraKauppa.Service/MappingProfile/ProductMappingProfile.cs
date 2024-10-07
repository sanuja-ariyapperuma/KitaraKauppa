using AutoMapper;
using KitaraKauppa.Core.Orders;
using KitaraKauppa.Core.Products;
using KitaraKauppa.Service.OrdersService.Dtos;
using KitaraKauppa.Service.ProductsServices.Dtos;
using KitaraKauppa.Service.ProductsServices.Dtos.Definition;
using KitaraKauppa.Service.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.MappingProfile
{
    public class ProductMappingProfile : Profile
    {
        
        public ProductMappingProfile()
        {
            CreateMap<CreateProductDto, Product>()
            .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.BrandId))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => ConvertToImage(src.ImageName, src.Extension)))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductTitle, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand!.Name))
                .ForMember(dest => dest.ImageAlt, opt => opt.MapFrom(src => src.Images.FirstOrDefault().ImageAlt));
                //.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => Helper.GetImageUrl(src.Images.FirstOrDefault())));

            CreateMap<Product, ReadProductDto>()
            .ForMember(dest => dest.ProductColors, opt => opt.MapFrom(src => src.Colors.Select(c => c.Id)))
            .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.BrandId))
            .ForMember(dest => dest.ProductOrientation, opt => opt.MapFrom(src =>src.Orientation));

            CreateMap<Image, ImageReadDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            //.ForMember(dest => dest.Url, opt => opt.MapFrom(src => Helper.GetImageUrl(src)));

            CreateMap<Brand, BrandDto>();
            CreateMap<Color, ColorDto>();
        }

        private ICollection<Image> ConvertToImage(string imageName, string imageExtension)
        {
            Image image = new Image
            {
                Id = Guid.Parse(imageName),
                Extention = imageExtension
            };

            return new List<Image> { image };
        }

    }

}
