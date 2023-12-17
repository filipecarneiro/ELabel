using ELabel.Models;
using System.ComponentModel.DataAnnotations;

namespace ELabel.ViewModels
{

    public class WineProductDetailsDto : AuditableEntity
    {
        [Required]
        [Display(Name = "Name", Description = "This is the name of the product as you have it in your bottle, without the vintage.")]
        [StringLength(100)]
        [DataType(DataType.Text)]
        public required string Name { get; set; }

        [Display(Name = "Net volume", Description = "Enter the volume of the liquid in liters.")]
        [Range(float.Epsilon, float.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:G} l")]
        public float? Volume { get; set; }

        /*
         * Details
         */

        public ImageDto? Image { get; set; }

        public required WineInformation WineInformation { get; set; } = new();

        /*
         * Ingredients
         */

        public List<ProductIngredientDto> ProductIngredients { get; set; } = new();

        public required PackagingGases PackagingGases { get; set; }

        /*
         * Nutrition declaration
         */

        public required NutritionInformation NutritionInformation { get; set; } = new();

        public required ResponsibleConsumption ResponsibleConsumption { get; set; } = new();

        public required Certifications Certifications { get; set; } = new();

        /*
         * Logistics
         */

        [Display(Name = "Country of origin", Description = "Enter the ISO 3166-1 two-letter contry code.")]
        public string? Country { get; set; }

        [Display(Name = "SKU Code", Description = "Enter your internal Stock Keeping Unit (SKU) text code.")]
        public string? Sku { get; set; }

        [Display(Name = "EAN/GTIN", Description = "Enter your European Article Number (EAN) or Global Trade Item Number (GTIN) of your product.")]
        [DisplayFormat(DataFormatString = "{0:G0}")]
        public ulong? Ean { get; set; }
    }
}
