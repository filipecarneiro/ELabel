using ELabel.Models;
using System.ComponentModel.DataAnnotations;

namespace ELabel.ViewModels
{

    public class WineProductCreateDto
    {
        [Required]
        [Display(Name = "Product name", Description = "This is the name of the product as you have it in your bottle, without the vintage.")]
        [StringLength(100)]
        [DataType(DataType.Text)]
        public required string Name { get; set; }

        [Display(Name = "Net volume", Description = "Enter the volume of the liquid in liters.")]
        [Range(float.Epsilon, float.MaxValue)]
        public float? Volume { get; set; }

        /*
         * Wine Details
         */

        public required WineInformation WineInformation { get; set; } = new();

        /*
         * Logistics
         */

        /*
        [Display(Name = "Country of origin", Description = "Enter the ISO 3166-1 two-letter contry code.", Prompt = "PT")]
        [RegularExpression("^[A-Z]{2}$", ErrorMessage = "Please use, exactly, 2 capital letters (A-Z)")]
        [StringLength(2)]
        [DataType(DataType.Text)]
        public string? Country { get; set; }

        [Display(Name = "SKU Code", Description = "Enter your internal Stock Keeping Unit (SKU) text code.")]
        [RegularExpression("^[A-Z0-9/-]{3,}$", ErrorMessage = "Please use only capital letters (A-Z), numbers (0-9) and hyphen")]
        [StringLength(20)]
        [DataType(DataType.Text)]
        public string? Sku { get; set; }

        [Display(Name = "EAN/GTIN", Description = "Enter your European Article Number (EAN) or Global Trade Item Number (GTIN) of your product.")]
        [RegularExpression("^(\\d{12,14})$", ErrorMessage = "Invalid GTIN (12, 13 or 14 digits)")]
        public ulong? Ean { get; set; }
        */
    }
}
