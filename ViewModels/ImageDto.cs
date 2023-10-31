using Ganss.Excel;

namespace ELabel.ViewModels
{
    public class ImageDto
    {
        [Column("Image")]
        public required string DataUrl { get; set; }

        [Ignore]
        public required string MediaType { get; set; }

        [Ignore]
        public int? Width { get; set; }

        [Ignore]
        public int? Height { get; set; }
    }
}
