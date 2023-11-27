using AutoMapper;
using ELabel.Models;
using ELabel.ViewModels;

namespace ScriptumDigital.WebApp.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, LabelDto>();

            CreateMap<Image, ImageDto>();

            CreateMap<Product, WineProductDetailsDto>()
                .ReverseMap()
                .ForPath(p => p.Image, opt => opt.Ignore())
                .ForPath(p => p.Ingredients, opt => opt.Ignore())
                .ForPath(p => p.ProductIngredients, opt => opt.Ignore());

            CreateMap<WineProductCreateDto, Product>();

            CreateMap<Product, WineProductEditDto>()
                .ReverseMap()
                .ForPath(p => p.Image, opt => opt.Ignore())
                .ForPath(p => p.Ingredients, opt => opt.Ignore())
                .ForPath(p => p.ProductIngredients, opt => opt.Ignore());

            CreateMap<ProductIngredient, ProductIngredientDto>()
                .ReverseMap()
                .ForPath(p => p.Ingredient, opt => opt.Ignore());

            CreateMap<Ingredient, IngredientDto>()
                .ReverseMap();
        }
    }
}
