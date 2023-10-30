using System.ComponentModel.DataAnnotations;

namespace ELabel.ViewModels
{
    public class ImageFileUpload
    {
        [Required]
        [Display(Name = "Image File", Description = "Image to upload", Prompt = "image.jpeg")]
        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        public static int MinWidth = 100;
        public static int MaxWidth = 500;
        public static int MinHeight = 100;
        public static int MaxHeight = 500;
        public static string MimeType = "image/webp";
        public static int Quality = 75;
    }
}
