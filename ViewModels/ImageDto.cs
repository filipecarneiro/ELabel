namespace ELabel.ViewModels
{
    public class ImageDto
    {
        public required Guid Id { get; set; }

        public required string MediaType { get; set; }

        public required int Width { get; set; }

        public required int Height { get; set; }

        public string? PixelDensity { get; set; }

        public string Url
        {
            get
            {
                return "/Image/Download/" + Id.ToString();
            }
        }
    }
}
