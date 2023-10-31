using SkiaSharp;
using System.Text.RegularExpressions;

namespace ELabel.Extensions
{
    public class OptimizedImage
    {
        public OptimizedImage(string contentType, byte[] content, int? width, int? height)
        {
            ContentType = contentType;
            Content = content;
            Width = width;
            Height = height;
        }

        public string ContentType { get; }

        public byte[] Content { get; }

        public int? Width { get; }

        public int? Height { get; }
    }

    public static class ImageHelper
    {
        /*
         * https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/Data_URLs
         */

        public static string ConvertToDataUrl(string mediaType, byte[] data)
        {
            string base64String = Convert.ToBase64String(data);

            return $"data:{mediaType};base64,{base64String}";
        }

        public static byte[]? ConvertFromDataUrl(string dataUrl)
        {
            var r = new Regex(@"^data:([^;]+)*;base64,([a-zA-Z0-9+\/+=]+)*$", RegexOptions.IgnoreCase);
            var match = r.Match(dataUrl);

            if (!match.Success || match.Groups.Count != 3)
            {
                return null;
            }

            //string contentType = match.Groups[1].Value;
            byte[] content = Convert.FromBase64String(match.Groups[2].Value);

            return content;
        }

        public static OptimizedImage? Optimize(byte[] content, int maxWidth = int.MaxValue, int maxHeight = int.MaxValue, int quality = 75)
        {
            int width, height;
            bool resize = false;

            SKBitmap bitmap = SKBitmap.Decode(content);

            if (bitmap.IsEmpty)
            {
                bitmap.Dispose();
                return null;
            }

            width = bitmap.Width; height = bitmap.Height;

            if (width > maxWidth)
            {
                double ratio = height / (double)width;
                width = maxWidth;
                height = (int)(width * ratio);
                resize = true;
            }
            if (height > maxHeight)
            {
                double ratio = width / (double)height;
                height = maxHeight;
                width = (int)(height * ratio);
                resize = true;
            }

            if (resize)
            {
                bitmap = bitmap.Resize(new SKImageInfo(width, height), SKFilterQuality.High);
            }

            SKData data = bitmap.Encode(SKEncodedImageFormat.Webp, quality);
            bitmap.Dispose();

            OptimizedImage image = new OptimizedImage("image/webp", data.ToArray(), width, height);
            data.Dispose();

            return image;
        }
    }
}
