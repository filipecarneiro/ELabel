using ELabel.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace ELabel.Models
{
    [Index(nameof(ProductId), nameof(PixelDensity), IsUnique = true)]
    public class Image : BaseEntity
    {
        public static int MinBiggerSideLenght = 500;
        public static int MaxBiggerSideLenght = MinBiggerSideLenght* 4;
        public static readonly List<(int SideLenght, string PixelDensity)> DefaultSizes = new()
        {
            (MinBiggerSideLenght / 4, "0.25x"),
            (MinBiggerSideLenght / 2, "0.5x"),
            (MinBiggerSideLenght, "1x"),
            (MinBiggerSideLenght * 2, "2x"),
            (MinBiggerSideLenght * 3, "3x"),
            (MinBiggerSideLenght * 4, "4x")
        };
        public static string DefaultMimeType = "image/webp";
        public static int DefaultQuality = 90;
        public static string StandardPixelDensity = "1x";
        public static string ExportPixelDensity = "1x";

        [Required]
        [MaxLength(20)]
        public required string ContentType { get; set; }

        [Required]
        public required byte[] Content { get; set; }

        [Required]
        public required int Width { get; set; }

        [Required]
        public required int Height { get; set; }

        [MaxLength(5)]
        public string? PixelDensity { get; set; }

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
