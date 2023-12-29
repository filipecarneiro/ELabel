using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELabel.Models
{
    [Owned]
    [Display(Name = "Portability")]
    public class Portability
    {
        [Column("ExternalShortUrl")]
        [Display(Name = "External short link", Description = "Current link already printed in your label from an external URL shortening service, like Bitly, so you can manage all QR Codes in Open E-Label.")]
        [StringLength(50)]
        [DataType(DataType.Url)]
        [MaxLength(50)]
        public string? ExternalShortUrl { get; set; }

        [Column("RedirectUrl")]
        [Display(Name = "Redirect link", Description = "Redirect/forward this label page to a different e-label site (for portability).")]
        [StringLength(100)]
        [DataType(DataType.Url)]
        [MaxLength(100)]
        public string? RedirectUrl { get; set; }

        public Portability DeepCopy()
        {
            return (Portability)this.MemberwiseClone();
        }
    }
}
