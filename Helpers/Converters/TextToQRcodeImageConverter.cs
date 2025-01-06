using Youth.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;


namespace Youth.Helpers.Converters
{
    internal class TextToQRcodeImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            ImageSource QrCodeImage = "";
            if (value != null)
            {
                try
                {
                    string textValue = value as string;
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(textValue, SelectedECCLevel);
                    PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
                    byte[] qrCodeBytes = qRCode.GetGraphic(20);
                    QrCodeImage = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("TextToQRcodeImageConverter: " + ex);
                }
            }

            return QrCodeImage;
        }

        List<QRCodeGenerator.ECCLevel> _eccLevels;
        public List<QRCodeGenerator.ECCLevel> ECCLevels
        {
            get;
            set;
        }

        QRCodeGenerator.ECCLevel _selectedECCLevel;
        public QRCodeGenerator.ECCLevel SelectedECCLevel
        {
            get;
            set;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();

        }
    }
}
