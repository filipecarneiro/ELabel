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

        public Certifications DeepCopy()
        {
            return (Certifications)this.MemberwiseClone();
        }
    }
}
