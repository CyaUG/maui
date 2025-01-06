using Youth.Utils;
using Youth.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Youth.ViewModels.Base;
public partial class BaseViewModel : ObservableObject, INotifyPropertyChanged
{

    public ICommand GoBackCommand { get; protected set; }

    public BaseViewModel()
    {
        GoBackCommand = new AsyncCommand(async () =>
        {
        });
        UpdateAuthStatus();
    }

    public void GoBack()
    {
        Shell.Current.GoToAsync("..");
    }

    bool isBusy = false;
    public bool IsBusy
    {
        get { return isBusy; }
        set { SetProperty(ref isBusy, value); }
    }

    bool isUserAuthenticated = false;
    public bool IsUserAuthenticated
    {
        get { return isUserAuthenticated; }
        set { SetProperty(ref isUserAuthenticated, value);
            OnPropertyChanged("IsUserAuthenticated");
        }
    }

    string title = string.Empty;
    public string Title
    {
        get { return title; }
        set { SetProperty(ref title, value); }
    }

    protected bool SetProperty<T>(ref T backingStore, T value,
        [CallerMemberName] string propertyName = "",
        Action onChanged = null)
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        onChanged?.Invoke();
        OnPropertyChanged(propertyName);
        return true;
    }

    public virtual void Initialize(object navigationData) { }
    public virtual void OnStart() { }
    public virtual void OnResume() { }
    public virtual bool OnBackButtonPressed() => false;
    public virtual void OnPause() { }
    public virtual void OnStop() { }

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        var changed = PropertyChanged;
        if (changed == null)
            return;

        changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    protected async void UpdateAuthStatus()
    {
        var access_token = await SecureStorage.GetAsync(Constants.AUTH_TOCKEN_REF);

        if (access_token != null)
        {
            IsUserAuthenticated = true;
        }
        else
        {
            IsUserAuthenticated = false;
        }
        Debug.WriteLine("BaseViewModel: UpdateAuthStatus()");
    }
}
