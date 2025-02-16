using QRCoder;
using System.IO;

namespace Black_Hole.Helpers
{
    static class QrCodeHelper
    {
        public static string GenerateQrCode(string text, string? folderPath = null)
        {
            string filePath;

            if (folderPath != null)
            {
                Directory.CreateDirectory(folderPath);
                filePath = $"{folderPath}\\qrCode.png";
            }
            else
            {
                filePath = Path.GetTempPath() + "qrCode.png";
            }

            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            new QRCode(qrCodeData).GetGraphic(20).Save(filePath);

            return filePath;
        }
    }
}
