using System.ComponentModel.DataAnnotations;

namespace ELabel.ViewModels
{
    public class ProductIngredientDto
    {
        [Required]
        public required Guid Id { get; set; }

        [Required]
        public required Guid ProductId { get; set; }

        [Required]
        [Display(Name = "Ingredient")]
        public required Guid IngredientId { get; set; }

        [Required]
        //[Range(-255, 255)]
        public required short Order { get; set; }

        public bool ToDelete { get; set; }

        public IngredientDto? Ingredient { get; set; }
    }
}
