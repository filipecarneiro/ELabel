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

        // Auxiliary methods

        /// <summary>
        /// Gets the product title, based on it's name.
        /// </summary>
        /// <remarks>The contents of the title depends on product kind.</remarks>
        /// <returns>A string with the product title.7</returns>
        public string GetTitle()
        {
            if (Kind == ProductKind.Wine && WineInformation.Vintage is not null && WineInformation.Vintage > 0)
                return $"{Name} {WineInformation.Vintage}";

            return Name;
        }

        /// <summary>
        /// Gets the product code, based on SKU or EAN.
        /// </summary>
        /// <remarks>If both SKU and EAN are not provided, the product code is based on it's internal Id.</remarks>
        /// <returns>A string with the product code.</returns>
        public string GetCode()
        {
            if (Logistics is not null && !string.IsNullOrWhiteSpace(Logistics.Sku))
                return Logistics.Sku;

            if (Logistics is not null && Logistics.Ean is not null && Logistics.Ean > 0)
                return Logistics.Ean.ToString() ?? Id.ToString();

            return Id.ToString();
        }

        /// <summary>
        /// Gets the label relative link.
        /// </summary>
        /// <returns>A string with the link.</returns>
        public string GetRelativeLabelUrl()
        {
            return $"~/l/{GetCode()}";
        }

        /// <summary>
        /// Gets the public label link.
        /// </summary>
        /// <param name="baseUrl">The current app base link.</param>
        /// <returns>A string with the link.</returns>
        public string GetAbsoluteLabelUrl(string baseUrl)
        {
            return $"{baseUrl}/l/{GetCode()}";
        }

        /// <summary>
        /// Gets the public label link.
        /// </summary>
        /// <param name="baseUrl">The current app base link.</param>
        /// <param name="code">The product code.</param>
        /// <returns>A string with the link.</returns>
        public static string GetAbsoluteLabelUrl(string baseUrl, string code)
        {
            return $"{baseUrl}/l/{code}";
        }
    }
}
