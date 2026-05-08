using AutoMapper;
using Domain.Entities.ProductModule;
using Shared.Dtos.ProductModule;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand, BrandResultDto>();
            CreateMap<ProductType, TypeResultDto>();
            CreateMap<Product, ProductResultDto>()
                .ForMember(dest => dest.BrandName, option => option.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.TypeName, option => option.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, option => option.MapFrom<PictureUrlResolver>());
        }
    }
}
