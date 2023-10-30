using ELabel.Models;
using System.ComponentModel.DataAnnotations;

namespace ELabel.ViewModels
{

    public class WineProductDetailsDto : AuditableEntity
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
         * Image
         */

        public ImageDto? Image { get; set; }

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

        [Display(Name = "EAN/GTIN", Description = "Enter your European Article Number (EAN) or Global Trade Item Number (GTIN) of your product.")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        [RegularExpression("^(\\d{12,14})$", ErrorMessage = "Invalid GTIN (12, 13 or 14 digits)")]
        public ulong? Ean { get; set; }
    }
}
