using AutoMapper;
using KitaraKauppa.Core.Shared;
using KitaraKauppa.Service.ProductsServices.Dtos.Definition;
using KitaraKauppa.Service.Repositories.Brand;
using KitaraKauppa.Service.Repositories.Products;
using KitaraKauppa.Service.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.ProductsServices
{
    public class ProductDefinitionManagement : IProductDefinitionManagement
    {
        private readonly IColorRepository _colorRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ProductDefinitionManagement(IColorRepository colorRepository, IBrandRepository brandRepository, IMapper mapper, IConfiguration configuration)
        {
            _colorRepository = colorRepository;
            _brandRepository = brandRepository;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<KKResult<ProductDefinitionDto>> GetDefinitions()
        {
            var colors = await _colorRepository.GetColorsAsync();
            var brands = await _brandRepository.GetBrandAsync();

            var result = new ProductDefinitionDto
            {
                Colors = _mapper.Map<IEnumerable<ColorDto>>(colors),
                Brands = _mapper.Map<IEnumerable<BrandDto>>(brands),
                Oriantations = Enum.GetNames(typeof(Orientation)).ToList(),
                Variants = Enum.GetNames(typeof(VarientType)).ToList()
            };

            return new KKResult<ProductDefinitionDto>().SucceededWithValue(result);

        }
    }
}
