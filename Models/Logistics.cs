using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELabel.Models
{
    [Owned]
    [Display(Name = "Logistics")]
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
        [Ganss.Excel.DataFormat(1)] // Formats the Excel column as number without decimals ("0")
        public ulong? Ean { get; set; }

        public Logistics DeepCopy()
        {
            return (Logistics)this.MemberwiseClone();
        }
    }
}
