using ELabel.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ELabel.ViewModels
{

    public class WineProductEditDto
    {
        [Required]
        public required Guid Id { get; set; }

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

        [Display(Name = "Vintage", Description = "The year that the wine was produced. Do not fill for non-vintage wines.")]
        [DisplayFormat(DataFormatString = "{0:D4}")]
        [Range(1, 2099)]
        public ushort? WineVintage { get; set; }

        [Display(Name = "Wine type", Description = "Wine classification by vinification process. Sometimes refered as wine 'colour'.")]
        [EnumDataType(typeof(WineType))]
        public WineType? WineType { get; set; }

        [Display(Name = "Wine style", Description = "Wine style, sweetness of wine or 'product type', according to EU Commission Regulation.")]
        [EnumDataType(typeof(WineStyle))]
        public WineStyle? WineStyle { get; set; }

        [Display(Name = "Appellation", Description = "Legally defined and protected geographical indication.")]
        [StringLength(100)]
        [DataType(DataType.Text)]
        public string? WineAppellation { get; set; }

        /*
         * Logistics
         */

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
        [DisplayFormat(DataFormatString = "{0:G0}")]
        [RegularExpression("^(\\d{12,14})$", ErrorMessage = "Invalid GTIN (12, 13 or 14 digits)")]
        public ulong? Ean { get; set; }
    }
}
