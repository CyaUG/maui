using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.jobs;
using Youth.Views.SafeSpace;
using Youth.Views.Shopping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Youth.ViewModels.Base;



namespace Youth.ViewModels
{
    [QueryProperty(nameof(HealthCenterId), nameof(HealthCenterId))]
    internal class HealthCenterStaffMemberProviderViewModel : BaseViewModel, IHealthCenterStaffMemberProviderViewModel
    {
        private int _healthCenterId;
        public ObservableCollection<StaffMember> StaffMembers { get; }
        public Command RunReferralHCMainCommand { get; }
        public Command<StaffMember> StaffMemberNavTap { get; }

        public HealthCenterStaffMemberProviderViewModel()
        {
            Title = "Staff Members";
            StaffMembers = new ObservableCollection<StaffMember>();
            RunReferralHCMainCommand = new Command(async () => await LoadHealthCenterStaffMembers(HealthCenterId));
            StaffMemberNavTap = new Command<StaffMember>(OnStaffMemberSelected);
        }

        async void OnStaffMemberSelected(StaffMember mStaffMember)
        {
            if (mStaffMember == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            MessagingCenter.Send<HealthCenterStaffMemberProviderViewModel, StaffMember>(this, "staffMember", mStaffMember);
            GoBack();
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public int HealthCenterId
        {
            get
            {
                return _healthCenterId;
            }
            set
            {
                _healthCenterId = value;
                LoadHealthCenterStaffMembers(value);
            }
        }

        public async Task LoadHealthCenterStaffMembers(int healthCenterId)
        {
            Debug.WriteLine("JobDetailsViewModel: LoadHealthCenterStaffMembers()");
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

                JObject staffObj = await StaffMember.fetchAllHealthCenterStaffMembers(healthCenterId);
                StaffMembers.Clear();
                JArray staffArray = (JArray)staffObj.GetValue("data");

                foreach (JToken token in staffArray)
                {
                    JObject chatObj = (JObject)token;
                    StaffMember staffMember = chatObj.ToObject<StaffMember>(serializer);
                    Debug.WriteLine("JobDetailsViewModel: " + staffMember.name);
                    StaffMembers.Add(staffMember);
                }
                OnPropertyChanged("StaffMembers");
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("JobDetailsViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("StaffMembers");
                OnPropertyChanged("IsBusy");
            }
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }
    }
}
