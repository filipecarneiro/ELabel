using AutoMapper;
using ELabel.Models;
using ELabel.ViewModels;

namespace ScriptumDigital.WebApp.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Image, ImageDto>();

            CreateMap<Product, WineProductDetailsDto>()
                .ReverseMap()
                .ForPath(p => p.Image, opt => opt.Ignore());

            CreateMap<WineProductCreateDto, Product>();

            CreateMap<Product, WineProductEditDto>()
                .ReverseMap();
        }
    }
}
