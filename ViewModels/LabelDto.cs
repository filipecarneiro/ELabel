using ELabel.Models;
using System.ComponentModel.DataAnnotations;

namespace ELabel.ViewModels
{

    public class LabelDto : BaseEntity
    {
        [Display(Name = "Name", Description = "This is the name of the product as you have it in your bottle, without the vintage.")]
        [DataType(DataType.Text)]
        public required string Name { get; set; }

        [Display(Name = "Brand", Description = "Brand, producer or product marketing name.")]
        [DataType(DataType.Text)]
        public string? Brand { get; set; }

        [Display(Name = "Net volume", Description = "Enter the volume of the liquid in liters.")]
        [DisplayFormat(DataFormatString = "{0:G} l")]
        public float? Volume { get; set; }

        /*
         * Details
         */

        public List<ImageDto>? Images { get; set; }

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

        public PackagingGases? PackagingGases { get; set; }

        /*
         * Nutrition declaration
         */

        public NutritionInformation? NutritionInformation { get; set; }

        public ResponsibleConsumption? ResponsibleConsumption { get; set; }

        public Certifications? Certifications { get; set; }

        public FoodBusinessOperator? FoodBusinessOperator { get; set; }

        public Logistics? Logistics { get; set; }

        public Portability? Portability { get; set; }

        // Auxiliary properties

        public string? Title { get; set; }

        public string? LabelUrl { get; set; }

        public string? ShareImageUrl { get; set; }
    }
}
