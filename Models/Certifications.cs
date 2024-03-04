using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELabel.Models
{
    // Sustainability & other certifications (Certifications, Sustainability message)

    [Owned]
    public class Certifications
    {
        [Display(Name = "Organic", Description = "Certified organic based on EU-Guidelines")]
        public bool Organic { get; set; } = false;

        [Display(Name = "Vegetarian", Description = "Certified Vegetarian by V-Label")]
        public bool Vegetarian { get; set; } = false;

        [Display(Name = "Vegan", Description = "Certified Vegan by V-Label")]
        public bool Vegan { get; set; } = false;

        public bool HasAny()
        {
            if(Organic || Vegetarian || Vegan)
                return true;

            return false;
        }

        public Certifications DeepCopy()
        {
            return (Certifications)this.MemberwiseClone();
        }
    }
}
