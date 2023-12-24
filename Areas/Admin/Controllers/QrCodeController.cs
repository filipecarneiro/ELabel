using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net.Codecrete.QrCodeGenerator;
using System.Text;

namespace ELabel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize()]
    public class QrCodeController : Controller
    {
        // GET: QrCode/Download
        public IActionResult Download([FromQuery] string content, [FromQuery] string filename = "qrcode", [FromQuery] string format = "svg")
        {
            if (content == null || string.IsNullOrWhiteSpace(content))
            {
                return NotFound();
            }

            // Generate QrCode
            var qr = QrCode.EncodeText(content, QrCode.Ecc.Medium);

            byte[] byteArray;

            // PNG

            if (format.ToLower() == "png")
            {
                byteArray = qr.ToPng(10, 4);

                return File(byteArray, "image/png", filename + ".png");
            }

            // JPEG

            if (format.ToLower() == "jpeg" || format.ToLower() == "jpg")
            {
                byteArray = qr.ToJpeg(10, 4);

                return File(byteArray, "image/jpeg", filename + ".jpeg");
            }

            // SVG

            byteArray = Encoding.UTF8.GetBytes(qr.ToSvgString(4));

            return File(byteArray, "text/svg", filename + ".svg");
        }

    }
}
