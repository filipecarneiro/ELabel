using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ELabel.Models
{
    [Index(nameof(Name), nameof(Category), IsUnique = true)] // TODO: Protect UI for this constraint on Create, Edit and Import
    public class Ingredient : BaseEntity
    {
        [MaxLength(100)]
        public required string Name { get; set; }

        public required IngredientCategory Category { get; set; }

        public required bool Allergen {  get; set; }

        public required bool Custom { get; set; }

        //public JsonObject? Translations { get; set; }

        public List<Product> Products { get; } = new();
        public List<ProductIngredient> ProductIngredients { get; } = new();
    }
}
