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
    }
}
