using System.Diagnostics;
using Youth.ViewModels.Interfaces;
using QRCoder;
using Youth.ViewModels.Base;

namespace Youth.ViewModels
{
    [QueryProperty(nameof(InputText), nameof(InputText))]
    internal class ReferralAccountQrCodeViewModel : BaseViewModel, IReferralAccountQrCodeViewModel
    {
        public ReferralAccountQrCodeViewModel()
        {
            Title = "QR Code";
        }

        List<QRCodeGenerator.ECCLevel> _eccLevels;
        public List<QRCodeGenerator.ECCLevel> ECCLevels
        {
            get => _eccLevels;
            set => SetProperty(ref _eccLevels, value);
        }

        QRCodeGenerator.ECCLevel _selectedECCLevel;
        public QRCodeGenerator.ECCLevel SelectedECCLevel
        {
            get => _selectedECCLevel;
            set => SetProperty(ref _selectedECCLevel, value);
        }

        ImageSource _qrCodeImage;
        public ImageSource QrCodeImage
        {
            get => _qrCodeImage;
            set => SetProperty(ref _qrCodeImage, value);
        }

        string _inputText;
        public string InputText
        {
            get => _inputText;
            set
            {
                SetProperty(ref _inputText, value);
                Convert(value);
            }
        }

        void Convert(string textValue)
        {
            Debug.WriteLine("ReferralAccountQrCodeViewModel: " + textValue);
            if (string.IsNullOrEmpty(textValue))
                textValue = "";

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(textValue, SelectedECCLevel);
            PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qRCode.GetGraphic(20);
            QrCodeImage = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
            OnPropertyChanged("QrCodeImage");

            IsBusy = false;
            OnPropertyChanged("IsBusy");
        }

        void PopulateECCLevels()
        {
            ECCLevels = Enum.GetValues(typeof(QRCodeGenerator.ECCLevel)).OfType<QRCodeGenerator.ECCLevel>().ToList();
        }
        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }
    }
}
