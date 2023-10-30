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

            CreateMap<Product, WineProductDetailsDto>();

            CreateMap<WineProductCreateDto, Product>();

            CreateMap<Product, WineProductEditDto>()
                .ReverseMap();
        }
    }
}
