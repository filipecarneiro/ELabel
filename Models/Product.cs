using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELabel.Models
{
    //[Index(nameof(Name), nameof(Volume), "WineInformation_Vintage", IsUnique = true)] // TODO: Protect UI for this constraint on Create, Edit and Import
    //[Index(nameof(Sku), IsUnique = true)] // TODO: Protect UI for this constraint on Create, Edit and Import
    public class Product : AuditableEntity
    {
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(100)]
        public string? Brand { get; set; }

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

        public required Portability Portability { get; set; } = new();

        // Navigation properties
        // https://docs.microsoft.com/en-us/ef/core/modeling/relationships

        public List<Image> Images { get; private set; } = new();

        public List<Ingredient> Ingredients { get; } = new();
        public List<ProductIngredient> ProductIngredients { get; private set; } = new();

        // Auxiliary methods

        public Product DeepCopy()
        {
            Product other = (Product)this.MemberwiseClone();
            other.Id = Guid.NewGuid();

            other.WineInformation = this.WineInformation.DeepCopy();
            other.NutritionInformation = this.NutritionInformation.DeepCopy();
            other.ResponsibleConsumption = this.ResponsibleConsumption.DeepCopy();
            other.Certifications = this.Certifications.DeepCopy();
            other.FoodBusinessOperator = this.FoodBusinessOperator.DeepCopy();
            other.Logistics = this.Logistics.DeepCopy();
            other.Portability = this.Portability.DeepCopy();

            // Copy related images

            other.Images = new List<Image>();
            foreach (Image image in this.Images.OrderBy(i => i.Width).ToList())
            {
                Image otherImage = new Image()
                {
                    Id = Guid.NewGuid(),
                    ProductId = other.Id,
                    ContentType = image.ContentType,
                    Content = image.Content,
                    Width = image.Width,
                    Height = image.Height,
                    PixelDensity = image.PixelDensity
                };

                other.Images.Add(otherImage);
            }

            // Copy related Product Ingredients

            other.ProductIngredients = new List<ProductIngredient>();
            foreach (ProductIngredient productIngredient in this.ProductIngredients.OrderBy(p => p.Order).ToList())
            {
                ProductIngredient otherProductIngredient = new ProductIngredient()
                {
                    Id = Guid.NewGuid(),
                    ProductId = other.Id,
                    IngredientId = productIngredient.IngredientId,
                    Order = productIngredient.Order,
                };

                other.ProductIngredients.Add(otherProductIngredient);
            }

            return other;
        }

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
        /// <param name="useExternalShortUrl">If RedirectUrl is configured, returns RedirectUrl.</param>
        /// <param name="useExternalShortUrl">If ExternalShortUrl is configured, returns ExternalShortUrl.</param>
        /// <returns>A string with the link.</returns>
        public string GetAbsoluteLabelUrl(string baseUrl, bool useRedirectUrl = false, bool useExternalShortUrl = false)
        {
            if (useRedirectUrl && !string.IsNullOrWhiteSpace(Portability.RedirectUrl))
                return Portability.RedirectUrl;

            if (useExternalShortUrl && !string.IsNullOrWhiteSpace(Portability.ExternalShortUrl))
                return Portability.ExternalShortUrl;

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
