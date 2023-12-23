namespace ELabel.Extensions
{
    public static class UrlHelper
    {
        public static string GetBaseUrl(HttpRequest request)
        {
            // TODO: Use Microsoft.AspNetCore.Mvc.Routing.UrlHelper?

            return $"{request.Scheme}://{request.Host}{request.PathBase}";
        }
    }
}
