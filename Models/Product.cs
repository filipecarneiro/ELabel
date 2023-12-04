using Microsoft.EntityFrameworkCore;
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

        // TODO: Food business operators

        /*
         * Nutrition declaration
         */

        // Ingredient list (ProductIngredients bellow)

        public required PackagingGases PackagingGases { get; set; }

        public required NutritionInformation NutritionInformation { get; set; } = new();

        public required ResponsibleConsumption ResponsibleConsumption { get; set; } = new();

        public required Certifications Certifications { get; set; } = new();

        /*
         * Logistics
         */

        [MaxLength(2)]
        public string? Country { get; set; }

        [MaxLength(20)]
        public string? Sku { get; set; }

        public ulong? Ean { get; set; }

        // Navigation properties
        // https://docs.microsoft.com/en-us/ef/core/modeling/relationships

        [InverseProperty("Product")]
        public virtual Image? Image { get; set; }

        public List<Ingredient> Ingredients { get; } = new();
        public List<ProductIngredient> ProductIngredients { get; } = new();
    }
}
