using ELabel.Models;
using System.ComponentModel.DataAnnotations;

namespace ELabel.ViewModels
{

    public class WineProductEditDto
    {
        [Required]
        public required Guid Id { get; set; }

        [Required]
        [Display(Name = "Name", Description = "This is the name of the product as you have it in your bottle, without the vintage.")]
        [StringLength(100)]
        [DataType(DataType.Text)]
        public required string Name { get; set; }

        [Display(Name = "Brand", Description = "Brand, producer or product marketing name.")]
        [StringLength(100)]
        [DataType(DataType.Text)]
        public string? Brand { get; set; }

        [Display(Name = "Net volume", Description = "Enter the volume of the liquid in liters.")]
        [Range(float.Epsilon, float.MaxValue)]
        public float? Volume { get; set; }

        /*
         * Details
         */

        //public ImageDto? Image { get; set; }

        public required WineInformation WineInformation { get; set; } = new();

        /*
         * Ingredients
         */

        public List<ProductIngredientDto> ProductIngredients { get; } = new();

        [Display(Name = "Packaging gases", Description = "Select an option for botteling atmosphere.")]
        [EnumDataType(typeof(PackagingGases))]
        public required PackagingGases PackagingGases { get; set; }

        /*
         * Nutrition declaration
         */

        public required NutritionInformation NutritionInformation { get; set; } = new();

        public required ResponsibleConsumption ResponsibleConsumption { get; set; } = new();

        public required Certifications Certifications { get; set; } = new();

        public required FoodBusinessOperator FoodBusinessOperator { get; set; } = new();

        public required Logistics Logistics { get; set; } = new();

        public required Portability Portability { get; set; } = new();
    }
}
