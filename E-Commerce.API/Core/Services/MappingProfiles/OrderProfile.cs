using AutoMapper;
using Domain.Entities.IdentityModule;
using Domain.Entities.OrderModule;
using Shared.Dtos.OrderModule;
using shippingAddress = Domain.Entities.OrderModule.Address;
using identityAddress = Domain.Entities.IdentityModule.Address;

namespace Services.MappingProfiles
{
    internal class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<shippingAddress, AddressDto>().ReverseMap();  
            CreateMap<identityAddress, AddressDto>().ReverseMap();  
            CreateMap<DeliveryMethod, DeliveryMethodResult>()
                .ForMember(dest => dest.Cost, option => option.MapFrom(src => src.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, option => option.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.ProductName, option => option.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, option => option.MapFrom(src => src.Product.PictureUrl));
            CreateMap<Order, OrderResult>()
                .ForMember(dest => dest.DeliveryMethod, option => option.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.paymentStatus, option => option.MapFrom(src => src.paymentStatus.ToString()))
                .ForMember(dest => dest.Total, option => option.MapFrom(src => src.SubTotal + src.DeliveryMethod.Price));
        }
    }
}
