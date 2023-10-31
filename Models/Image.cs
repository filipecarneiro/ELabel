using ELabel.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace ELabel.Models
{
    [Index(nameof(ProductId), IsUnique = true)]
    public class Image : BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public required string ContentType { get; set; }

        [Required]
        public required byte[] Content { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }


        // Auxiliary read-only properties

        public string DataUrl
        {
            get
            {
                return ImageHelper.ConvertToDataUrl(MediaType, Content);
            }
        }

        public int Length
        {
            get
            {
                return Content.Length;
            }
        }

        public string Size
        {
            get
            {
                if (Length < 1024)
                    return $"{Length} B";
                else
                    return $"{Length / 1024} KB";
            }
        }

        public string MediaType
        {
            get
            {
                return ContentType;
            }
        }

        public string SHA256
        {
            get
            {
                using (var sha256 = System.Security.Cryptography.SHA256.Create())
                {
                    byte[] hash = sha256.ComputeHash(Content);

                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        // Navigation properties
        // https://docs.microsoft.com/en-us/ef/core/modeling/relationships

        [Required]
        public required Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
