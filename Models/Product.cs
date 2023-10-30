using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELabel.Models
{
    [Index(nameof(Name), nameof(Volume), nameof(WineVintage), IsUnique = true)]
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

        /*
         * Other details
         */

        // TODO: Country of provenance

        // TODO: Analytics (Alcohol, Production method)

        // TODO: Producer (Name, logo and website)

        // TODO: List of ingredients (Mandatory)

        // TODO: Nutrition declaration per 100 ml (Mandatory)

        // TODO: Responsible consumption (pictogram warning against drinking during pregnanc and a generic message are Mandatory)

        // TODO: Sustainability & other certifications (Certifications, Sustainability message)

        // TODO: Packaging & Recycling (Mandatory in Italy)

        // TODO: Food business operators

        /*
         * Logistics
         */

        public ulong? Ean { get; set; }

        // Navigation properties
        // https://docs.microsoft.com/en-us/ef/core/modeling/relationships

        [InverseProperty("Product")]
        public virtual Image? Image { get; set; }
    }
}
