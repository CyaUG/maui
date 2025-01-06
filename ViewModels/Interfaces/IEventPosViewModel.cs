namespace Youth.ViewModels.Interfaces
{
    public interface IEventPosViewModel
    {
        void OnAppearing();
        void OnBarcodeDetected(string tocken);
        void OnTockenStatusUpdate(bool isTicketOpen);
    }
}
