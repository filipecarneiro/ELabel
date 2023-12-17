using ELabel.Models;
using System.ComponentModel.DataAnnotations;

namespace ELabel.ViewModels
{

    public class WineProductCreateDto
    {
        [Required]
        [Display(Name = "Product name", Description = "This is the name of the product as you have it in your bottle, without the vintage.")]
        [StringLength(100)]
        [DataType(DataType.Text)]
        public required string Name { get; set; }

        [Display(Name = "Net volume", Description = "Enter the volume of the liquid in liters.")]
        [Range(float.Epsilon, float.MaxValue)]
        public float? Volume { get; set; }

        /*
         * Wine Details
         */

        public required WineInformation WineInformation { get; set; } = new();
    }
}
