using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

        // TODO: Analytics (Alcohol, Production method)

        // TODO: Producer

        // TODO: Ingredients

        // TODO: Nutrition (Energy, Nutrition information)

        // TODO: Sustainability & other certifications (Certifications, Sustainability message)

        // TODO: Packaging & Recycling

        // TODO: Responsible consumption

        // TODO: Food business operators

        /*
         * Logistics
         */

        public ulong? Ean { get; set; }
    }
}
