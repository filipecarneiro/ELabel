using AutoMapper;

using ELabel.Models;
using ELabel.ViewModels;

namespace ELabel.Data
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

            CreateMap<Product, ProductExcelDto>()
                .ForMember(dest => dest.ImageDataUrl, opt => opt.MapFrom(src => src.Image != null ? src.Image.DataUrl : null))
                .ForMember(dest => dest.IngredientIdList, opt => opt.MapFrom(
                    src => src.ProductIngredients.OrderBy(p => p.Order).Select(pi => pi.IngredientId).ToList()))
                //.ForMember(dest => dest.ShortUrl, opt => opt.MapFrom<UrlResolver>())
                .ForMember(dest => dest.ShortUrl, opt => opt.MapFrom(src => src.GetCode()))
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

            CreateMap<Ingredient, IngredientExcelDto>()
                .ReverseMap()
                .ForPath(p => p.Products, opt => opt.Ignore())
                .ForPath(p => p.ProductIngredients, opt => opt.Ignore());
        }
    }
}
