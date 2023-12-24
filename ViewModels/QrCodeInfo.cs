using Net.Codecrete.QrCodeGenerator;
using System.ComponentModel.DataAnnotations;

namespace ELabel.ViewModels
{
    public class QrCodeInfo
    {
        public QrCodeInfo(string content, string filename = "qr-code")
        {
            Content = content;
            Filename = filename;
            SvgString = QrCode.EncodeText(content, QrCode.Ecc.Medium).ToSvgString(0);
        }

        [Display(Name = "Label public link", Description = "This is the link that is encoded in the QR code.")]
        public string Content { get; private set; }

        public string Filename { get; private set; }

        public string SvgString { get; private set; }
    }
}
