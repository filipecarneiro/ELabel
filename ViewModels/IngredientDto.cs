using ELabel.Models;
using System.ComponentModel.DataAnnotations;

namespace ELabel.ViewModels
{
    public class IngredientDto
    {
        [Required]
        public required Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Text)]
        public required string Name { get; set; }

        [Required]
        [EnumDataType(typeof(IngredientCategory))]
        public required IngredientCategory Category { get; set; }

        [Display(Name = "E number", Description = "Europe number for substances used as food additives.")]
        [DisplayFormat(DataFormatString = "E{0:D}")]
        [Range(100, 1599)]
        public ushort? ENumber { get; set; }

        [Required]
        public required bool Allergen { get; set; }

        [Required]
        public required bool Custom { get; set; }

        [Required]
        public required LocalizableStrings LocalizableStrings { get; set; } = new();
    }
}
