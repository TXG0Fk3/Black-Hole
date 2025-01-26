using QRCoder;
using System.Drawing;

namespace Black_Hole.Helpers
{
    static class QrCodeHelper
    {
        public static Bitmap GenerateQrCode(string text)
        {
            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

            return new QRCode(qrCodeData).GetGraphic(20);
        }
    }
}
