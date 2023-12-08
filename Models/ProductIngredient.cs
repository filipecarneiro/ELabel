using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELabel.Models
{
    [Index(nameof(ProductId), nameof(IngredientId), IsUnique = true)]
    public class ProductIngredient : BaseEntity
    {
        public required Guid ProductId { get; set; }
        //[Display(Name = "Ingredient")]
        public required Guid IngredientId { get; set; }
        public short Order { get; set; } = 0;

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }

        [ForeignKey("IngredientId")]
        public virtual Ingredient? Ingredient { get; set; }
    }
}
