using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Youth.ViewModels.Base;


namespace Youth.ViewModels
{
    public class SettingsViewModel : BaseViewModel, ISettingsViewModel
    {
        public UserAccount userAccount { get; set; }
        public Command LoadMyAccountCommand { get; }
        public ObservableCollection<NotificationModule> Items { get; }

        public SettingsViewModel()
        {
            Title = "Settings";
            Items = new ObservableCollection<NotificationModule>();
            LoadMyAccountCommand = new Command(async () => await ExecuteLoadMyAccountCommand());
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
            LoadMyAccountCommand.Execute(userAccount);
        }

        async Task ExecuteLoadMyAccountCommand()
        {
            Debug.WriteLine("HomeViewModel: ExecuteLoadMyAccountCommand()");
            IsBusy = true;

            try
            {
                var serializer = new JsonSerializer();
                serializer.Error += (sender, args) =>
                {
                    if (args.ErrorContext.Error is JsonException)
                    {
                        args.ErrorContext.Handled = true;
                    }
                };

                JObject accObj = await UserAccount.LoadMyProfileAsync();
                userAccount = accObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("userAccount");
                IsBusy = false;
                OnPropertyChanged("IsBusy");


            }
            catch (Exception ex)
            {
                Debug.WriteLine("HomeViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
        }
    }
}
