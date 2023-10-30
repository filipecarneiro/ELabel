using ELabel.Models;
using System.ComponentModel.DataAnnotations;

namespace ELabel.ViewModels
{
    public class ImageDto
    {
        public required string Source { get; set; }

        public required string MimeType { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }
    }
}
