using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELabel.Models
{
    [Owned]
    [Display(Name = "Logistics", Description = ".")]
    public class Logistics
    {
        [Column("Country")]
        [Display(Name = "Country of origin", Description = "Enter the ISO 3166-1 two-letter contry code.")]
        [RegularExpression("^[A-Z]{2}$", ErrorMessage = "Please use, exactly, 2 capital letters (A-Z)")]
        [StringLength(2)]
        [DataType(DataType.Text)]
        [MaxLength(2)]
        public string? Country { get; set; }

        [Column("Sku")]
        [Display(Name = "SKU Code", Description = "Enter your internal Stock Keeping Unit (SKU) text code.")]
        [RegularExpression("^[A-Z0-9/-]{3,}$", ErrorMessage = "Please use only capital letters (A-Z), numbers (0-9) and hyphen")]
        [StringLength(20)]
        [DataType(DataType.Text)]
        [MaxLength(20)]
        public string? Sku { get; set; }

        [Column("Ean")]
        [Display(Name = "EAN/GTIN", Description = "Enter your European Article Number (EAN) or Global Trade Item Number (GTIN) of your product.")]
        [DisplayFormat(DataFormatString = "{0:G0}")]
        [RegularExpression("^(\\d{12,14})$", ErrorMessage = "Invalid GTIN (12, 13 or 14 digits)")]
        public ulong? Ean { get; set; }

        /* TODO: ExternalShortUrl
        [Column("ExternalShortUrl")]
        [Display(Name = "External short link", Description = "Current link already printed in your label from an external URL shortening service, like Bitly, so you manage all QR Codes in Open E-Label.")]
        [StringLength(50)]
        [DataType(DataType.Url)]
        [MaxLength(50)]
        public string? ExternalShortUrl { get; set; }
        */

        /* TODO: RedirectUrl
        [Column("RedirectUrl")]
        [Display(Name = "Redirect link", Description = "Redirect/forward this label page to a different e-label site (for portability).")]
        [StringLength(100)]
        [DataType(DataType.Url)]
        [MaxLength(100)]
        public string? RedirectUrl { get; set; }
        */

        // Auxiliary methods

        public string? GetCode()
        {
            if (Sku is not null && !string.IsNullOrWhiteSpace(Sku))
                return Sku;

            if (Ean is not null && Ean > 0)
                return Ean.Value.ToString();

            return null;
        }
    }
}
