using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ELabel.Models
{
    [Index(nameof(Name), nameof(Category), IsUnique = true)] // TODO: Protect UI for this constraint on Create, Edit and Import
    //[Index(nameof(ENumber), IsUnique = true)] // TODO: Protect UI for this constraint on Create, Edit and Import
    public class Ingredient : BaseEntity
    {
        [MaxLength(100)]
        public required string Name { get; set; }

        public required IngredientCategory Category { get; set; }

        public ushort? ENumber { get; set; }

        public required bool Allergen {  get; set; }

        public required bool Custom { get; set; }

        public required LocalizableStrings LocalizableStrings { get; set; } = new();

        public List<Product> Products { get; } = new();
        public List<ProductIngredient> ProductIngredients { get; } = new();
    }
}
