namespace ELabel.Extensions
{
    public static class UrlHelper
    {
        public static string GetBaseUrl(HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}{request.PathBase}";
        }

        public static string GetShortUrl(string baseUrl, string code)
        {
            return $"{baseUrl}/l/{code}";
        }

        public static string GetQrCodeUrl(string baseUrl, string code, string format = "svg")
        {
            string url = $"{baseUrl}/Label/QRCode/{code}";
            
            if(format.ToLower() != "svg")
                url += $"?format={format}";

            return url;
        }
    }
}
