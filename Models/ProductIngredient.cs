using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;

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
