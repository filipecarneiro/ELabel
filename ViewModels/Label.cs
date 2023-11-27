using ELabel.Models;
using System.ComponentModel.DataAnnotations;

namespace ELabel.ViewModels
{

    public class LabelDto : BaseEntity
    {
        [Display(Name = "Name", Description = "This is the name of the product as you have it in your bottle, without the vintage.")]
        [DataType(DataType.Text)]
        public required string Name { get; set; }

        [Display(Name = "Net volume", Description = "Enter the volume of the liquid in liters.")]
        [DisplayFormat(DataFormatString = "{0:G} l")]
        public float? Volume { get; set; }

        /*
         * Details
         */

        public ImageDto? Image { get; set; }

        public WineInformation? WineInformation { get; set; }

        /*
         * Ingredients
         */

        public List<ProductIngredientDto>? ProductIngredients { get; set; }

        public string? Ingredients
        {
            get
            {
                if (ProductIngredients == null)
                    return null;

                List<string?> ingredients = ProductIngredients.Select(pi => pi.Ingredient?.Name).ToList();
                return String.Join(", ", ingredients.ToArray());
            }
        }

        /*
         * Nutrition declaration
         */

        public NutritionInformation? NutritionInformation { get; set; }

        public ResponsibleConsumption? ResponsibleConsumption { get; set; }

        public Certifications? Certifications { get; set; }

        /*
         * Logistics
         */

        [Display(Name = "Country of origin", Description = "Enter the ISO 3166-1 two-letter contry code.", Prompt = "PT")]
        public string? Country { get; set; }

        [Display(Name = "SKU Code", Description = "Enter your internal Stock Keeping Unit (SKU) text code.")]
        public string? Sku { get; set; }

        [Display(Name = "EAN/GTIN", Description = "Enter your European Article Number (EAN) or Global Trade Item Number (GTIN) of your product.")]
        [DisplayFormat(DataFormatString = "{0:G0}")]
        public ulong? Ean { get; set; }
    }
}
