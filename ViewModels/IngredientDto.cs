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

        [Required]
        public required bool Allergen { get; set; }

        [Required]
        public required bool Custom { get; set; }

    }
}
