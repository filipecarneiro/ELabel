using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELabel.Models
{
    /// <summary>
    /// Indication of the bottler, producer, importer and vendor.
    /// </summary>
    /// <remarks>
    /// Regulated by Article 46 of Commission Delegated Regulation (EU) 2019/33.
    /// </remarks>
    /// <see cref="https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX%3A02019R0033-20231208#art_46"/>
    public enum FoodBusinessOperatorType
    {
        [Display(Name = "(no title)", Description = "")]
        None = 0,

        [Display(Name = "Bottler", Description = "Bottled by")]
        Bottler = 1,

        [Display(Name = "Packager", Description = "Packaged by")]
        Packager = 2,

        [Display(Name = "Producer", Description = "Produced by")]
        Producer = 3,

        [Display(Name = "Vendor", Description = "Sold by")]
        Vendor = 4,

        [Display(Name = "Importer", Description = "Imported by")]
        Importer = 5,

        [Display(Name = "Producer and Bottler", Description = "Produced and Bottled by")]
        ProducerAndBottler = 6
    }
}
