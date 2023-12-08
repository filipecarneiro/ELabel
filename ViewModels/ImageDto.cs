namespace ELabel.ViewModels
{
    public class ImageDto
    {
        public required string DataUrl { get; set; }

        public required string MediaType { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }
    }
}
