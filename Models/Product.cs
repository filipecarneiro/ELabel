using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELabel.Models
{
    //[Index(nameof(Name), nameof(Volume), "WineInformation_Vintage", IsUnique = true)] // TODO: Protect UI for this constraint on Create, Edit and Import
    public class Product : AuditableEntity
    {
        [MaxLength(100)]
        public required string Name { get; set; }

        public float? Volume { get; set; }

        public float? Weight { get; set; }

        public required ProductKind Kind { get; set; } = ProductKind.Wine;

        /*
         * Wine Details
         * ProductKind = 3 (Wine, Table wine)
         */

        public required WineInformation WineInformation { get; set; } = new();

        /*
         * Other details
         */

        // TODO: Packaging & Recycling (Mandatory in Italy)

        /*
         * Nutrition declaration
         */

        // Ingredient list (ProductIngredients bellow)

        public required PackagingGases PackagingGases { get; set; }

        public required NutritionInformation NutritionInformation { get; set; } = new();

        public required ResponsibleConsumption ResponsibleConsumption { get; set; } = new();

        public required Certifications Certifications { get; set; } = new();

        public required FoodBusinessOperator FoodBusinessOperator { get; set; } = new();

        public required Logistics Logistics { get; set; } = new();


        // Navigation properties
        // https://docs.microsoft.com/en-us/ef/core/modeling/relationships

        [InverseProperty("Product")]
        public virtual Image? Image { get; set; }

        public List<Ingredient> Ingredients { get; } = new();
        public List<ProductIngredient> ProductIngredients { get; } = new();
    }
}
