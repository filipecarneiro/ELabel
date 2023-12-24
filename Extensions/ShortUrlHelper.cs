using AutoMapper.Internal.Mappers;
using NPOI.SS.Formula.Functions;
using System.ComponentModel.DataAnnotations;

namespace ELabel.Extensions
{
    public class ShortUrlHelper
    {
        public static string AbsoluteUrl(string baseUrl, string code)
        {
            return $"{baseUrl}/l/{code}";
        }

        public static string RelativeUrl(string code)
        {
            return $"~/l/{code}";
        }
    }
}
