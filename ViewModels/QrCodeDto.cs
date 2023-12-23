using ELabel.Models;
using NPOI.SS.Formula.Functions;
using System.ComponentModel.DataAnnotations;

namespace ELabel.ViewModels
{
    [Display(Name = "QR code", Description = "Links for quick-response code urls and images.")]
    public class QrCodeDto
    {
        public QrCodeDto(string baseUrl, string code, bool excel = false)
        {
            ShortUrl = $"{baseUrl}/l/{code}";

            QRCodeDownloadUrl = $"{baseUrl}/Label/QRCode/{code}";
            QRCodeDownloadUrlForPng = QRCodeDownloadUrl + "?format=png";
            QRCodeDownloadUrlForJpeg = QRCodeDownloadUrl + "?format=jpeg";

            if(excel)
            {
                ShortUrl = "HYPERLINK(\"" + ShortUrl + "\")";
                QRCodeDownloadUrl = "HYPERLINK(\"" + QRCodeDownloadUrl + "\")";
                QRCodeDownloadUrlForPng = "HYPERLINK(\"" + QRCodeDownloadUrlForPng + "\")";
                QRCodeDownloadUrlForJpeg = "HYPERLINK(\"" + QRCodeDownloadUrlForJpeg + "\")";
            }
        }

        [Display(Name = "Short link", Description = "This is the link that is encoded in the QR code.")]
        [DataType(DataType.Url)]
        [Ganss.Excel.Formula]
        [Ganss.Excel.Column("ShortUrl")]
        public string? ShortUrl { get; private set; }

        [Display(Name = "Download SVG", Description = "Download QR code in SVG format.")]
        [DataType(DataType.Url)]
        [Ganss.Excel.Formula]
        [Ganss.Excel.Column("QRCode")]
        public string? QRCodeDownloadUrl { get; private set; }

        [Display(Name = "Download PNG", Description = "Download QR code in PNG format.")]
        [DataType(DataType.Url)]
        [Ganss.Excel.Formula]
        [Ganss.Excel.Column("QRCodePNG")]
        public string? QRCodeDownloadUrlForPng { get; private set; }

        [Display(Name = "Download JPEG", Description = "Download QR code in JPEG format.")]
        [DataType(DataType.Url)]
        [Ganss.Excel.Formula]
        [Ganss.Excel.Column("QRCodeJPEG")]
        public string? QRCodeDownloadUrlForJpeg { get; private set; }
    }
}
