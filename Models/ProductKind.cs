using System.ComponentModel.DataAnnotations;

namespace ELabel.Models
{
    public enum ProductKind
    {
        [Display(Name = "General", Description = "General product.")]
        General = 0,

        [Display(Name = "Food")]
        Food = 1,

        [Display(Name = "Drink")]
        Drink = 2,

        [Display(Name = "Wine", Description = "Table wine.")]
        Wine = 3
    }
}
