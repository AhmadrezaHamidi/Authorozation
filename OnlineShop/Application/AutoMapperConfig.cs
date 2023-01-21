using Application.CQRS.Notifications;
using Application.CQRS.ProductCommandQuery.Query;
using AutoMapper;
using Core.Entities;
using Infrastructure.Dto;

namespace Application
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
 
            CreateMap<Product, ProductDto>().ReverseMap();
            //CreateMap<Source, Destination>().ReverseMap();

            CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.PriceWithComma, opt => opt.MapFrom(src => String.Format("{0:n0}", src.Price)))
            .ReverseMap();

            CreateMap<Product, GetProductQueryResponse>()
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.ProductName.ToUpper()))
               .ForMember(dest => dest.PriceWithComma, opt => opt.MapFrom(src => String.Format("{0:n0}", src.Price)));

            CreateMap<AddRefreshTokenNotification, UserRefreshToken>()
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.IsValid, opt => opt.MapFrom(src => true));
        }
    }
}
