using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELabel.Models
{
    [Index(nameof(Name), nameof(Volume), nameof(WineVintage), IsUnique = true)] // TODO: Protect UI for this constraint on Create, Edit and Import
    public class Product : AuditableEntity
    {
        [MaxLength(100)]
        public required string Name { get; set; }

        public float? Volume { get; set; }

        public float? Weight { get; set; }

        public required ProductKind Kind { get; set; }

        /*
         * Wine Details
         * ProductKind = 3 (Wine, Table wine)
         */

        public ushort? WineVintage { get; set; }

        public WineType? WineType { get; set; }

        public WineStyle? WineStyle { get; set; }

        [MaxLength(100)]
        public string? WineAppellation { get; set; }

        // TODO: Grape varieties

        // TODO: Alcohol on label

        // TODO: Production method (Traditional method, Barrel aged, Stainless steel fermented, Cask aged, etc.)

        /*
         * Other details
         */

        // TODO: Responsible consumption (pictogram warning against drinking during pregnanc and a generic message are Mandatory)

        // TODO: Producer (Name, logo and website)

        // TODO: Sustainability & other certifications (Certifications, Sustainability message)

        // TODO: Packaging & Recycling (Mandatory in Italy)

        // TODO: Food business operators

        /*
         * Nutrition declaration
         */

        public required NutritionInformation NutritionInformation { get; set; } = new();

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
