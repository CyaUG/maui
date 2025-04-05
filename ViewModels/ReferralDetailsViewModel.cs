using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views;
using Youth.Views.Main;
using Youth.Views.ReferralProgram;
using Youth.Views.Shopping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Youth.ViewModels.Base;

namespace Youth.ViewModels
{
    [QueryProperty(nameof(ReferralId), nameof(ReferralId))]
    public class ReferralDetailsViewModel : BaseViewModel, IReferralDetailsViewModel
    {
        private int _referralId;
        private int _healthCenterId;
        private string appointmentDate;
        private int _secondaryUserId;
        public SystemSettings systemSettings { get; set; }
        public UserAccount userAccount { get; set; }
        public ReferralAccount referralAccount { get; set; }
        public ReferralService referralService { get; set; }
        public Referral referral { get; set; }
        public CustomerCareChatGroup customerCareChatGroup { get; set; }
        public ObservableCollection<ReferralService> ReferralServices { get; }
        //public Command RunReferralDetailsMainCommand { get; }
        public Command OpenHealthCenterMap { get; }
        public Command OpenContactUsPage { get; }
        public Command OpenContactAssigneePage { get; }
        public Command OpenHealthCenterProviderPage { get; }
        public Command OpenHealthCenterStaffMemberProviderPage { get; }
        public Command OpenScheduleApointmentPage { get; }
        public Command OpenReferralServicesProviderPage { get; }
        public Command<Gender> GenderNavTap { get; }
        public ReferralDetailsViewModel()
        {
            Title = "Referral Details";
            ReferralServices = new ObservableCollection<ReferralService>();
            //RunReferralDetailsMainCommand = new Command(async () => await LoadReferralDetails(ReferralId));
            OpenHealthCenterMap = new Command(async () => await ExecuteHealthCenterMap());
            OpenContactUsPage = new Command(async () => await ExecuteOpenContactUsPage());
            OpenContactAssigneePage = new Command(async () => await ExecuteOpenContactAssigneePage());
            OpenHealthCenterProviderPage = new Command(async () => await ExecuteOpenHealthCenterProviderPage());
            OpenHealthCenterStaffMemberProviderPage = new Command(async () => await ExecuteOpenHealthCenterStaffMemberProviderPage());
            OpenScheduleApointmentPage = new Command(async () => await ExecuteOpenScheduleApointmentPage());
            OpenReferralServicesProviderPage = new Command(async () => await ExecuteOpenReferralServicesProviderPage());
            GenderNavTap = new Command<Gender>(OnGenderSelected);
            _ = LoadReferralDetails(ReferralId);
        }

        async void OnGenderSelected(Gender gender)
        {
            if (gender == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            MessagingCenter.Send<ReferralDetailsViewModel, Gender>(this, "gender", gender);
            GoBack();
        }

        async Task ExecuteHealthCenterMap()
        {
            if (referral == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            Debug.WriteLine("JobDetailsViewModel: OnJobAddressSelected()");
            await Map.OpenAsync(referral.ch_address_latitude, referral.hc_address_longitude, new MapLaunchOptions { Name = referral.ch_address_label });
        }

        async Task ExecuteOpenContactUsPage()
        {
            if (customerCareChatGroup == null)
                return;

            //open contact us page
            await Shell.Current.GoToAsync($"{nameof(MessagesPage)}?{nameof(MessageViewModel.ChatId)}={customerCareChatGroup.id}&{nameof(MessageViewModel.UserId)}={userAccount.id}");
        }

        async Task ExecuteOpenContactAssigneePage()
        {
            if (referral == null)
                return;

            //open contact assignee (health worker)
            await Shell.Current.GoToAsync($"{nameof(MessagesPage)}?{nameof(MessageViewModel.ChatId)}={referral.chatId}&{nameof(MessageViewModel.UserId)}={referral.assigneeId}");
        }

        async Task ExecuteOpenHealthCenterStaffMemberProviderPage()
        {
            //open HealthCenterProviderPage
            await Shell.Current.GoToAsync($"{nameof(HealthCenterStaffMemberProviderPage)}?{nameof(HealthCenterStaffMemberProviderViewModel.HealthCenterId)}={referral.hc_id}");
        }

        async Task ExecuteOpenReferralServicesProviderPage()
        {
            //open ReferralServicesProviderPage
            await Shell.Current.GoToAsync($"{nameof(ReferralServicesProviderPage)}?{nameof(ReferralServicesProviderViewModel.ReferralId)}={ReferralId}");
        }

        async Task ExecuteOpenHealthCenterProviderPage()
        {
            //open HealthCenterProviderPage
            await Shell.Current.GoToAsync($"{nameof(HealthCenterProviderPage)}");
        }

        async Task ExecuteOpenScheduleApointmentPage()
        {
            //open HealthCenterProviderPage
            await Shell.Current.GoToAsync($"{nameof(ScheduleApointmentPage)}");
        }

        public int ReferralId
        {
            get
            {
                return _referralId;
            }
            set
            {
                _referralId = value;
                LoadReferralDetails(value);
            }
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
                UpdateReferralHealthCenter(value);
            }
        }

        public String AppointmentDate
        {
            get
            {
                return appointmentDate;
            }
            set
            {
                appointmentDate = value;
                UpdateReferralAppointment(value);
            }
        }

        public int SecondaryUserId
        {
            get
            {
                return _secondaryUserId;
            }
            set
            {
                _secondaryUserId = value;
                UpdateSecondaryReferral(value);
            }
        }

        public async void UpdateSecondaryReferral(int secondaryUserId)
        {
            Debug.WriteLine("ReferralDetailsViewModel: UpdateSecondaryReferral()");
            IsBusy = true;

            try
            {
                await Referral.submitSecondaryReferral(secondaryUserId, referralAccount.userId, ReferralId);
                await LoadReferralDetails(ReferralId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ReferralDetailsViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
        }

        public async void UpdateReferralAppointment(String _appointmentDate)
        {
            Debug.WriteLine("ReferralDetailsViewModel: UpdateReferralAppointment()");
            IsBusy = true;

            try
            {
                await Referral.setReferralAppointment(_appointmentDate, ReferralId);
                await LoadReferralDetails(ReferralId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ReferralDetailsViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
        }

        public async void UpdateReferralHealthCenter(int healthCenterId)
        {
            Debug.WriteLine("ReferralDetailsViewModel: UpdateReferralHealthCenter()");
            IsBusy = true;

            try
            {
                await Referral.updateReferralHealthCenter(ReferralId, healthCenterId);
                await LoadReferralDetails(ReferralId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ReferralDetailsViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
        }

        public async Task LoadReferralDetails(int referralId)
        {
            Debug.WriteLine("ReferralDetailsViewModel: LoadReferralDetails()");
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

                systemSettings = await SystemSettings.fetchSystemSettings();
                Debug.WriteLine("ReferralDetailsViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject accObj = await UserAccount.LoadMyProfileAsync();
                userAccount = accObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("userAccount");

                JObject refAccObj = await ReferralAccount.fetchMyReferralAccount();
                try
                {
                    JObject refAccJsonObj = (JObject)refAccObj.GetValue("data");
                    referralAccount = refAccJsonObj.ToObject<ReferralAccount>(serializer);
                    OnPropertyChanged("referralAccount");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("ReferralDetailsViewModel Err: " + ex);
                }

                JObject referralObj = await Referral.fetchReferralDetails(referralId);
                JObject refJsonObj = (JObject)referralObj.GetValue("data");
                referral = refJsonObj.ToObject<Referral>(serializer);

                string age = "";
                //Enter date of birth in YYYY-MM-DD format
                try
                {
                    DateTime dateOfBirth = DateTime.ParseExact(referral.dateOfBirth, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    DateTime now = DateTime.Now;
                    int years = now.Year - dateOfBirth.Year;
                    if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                    {
                        years--;
                    }
                    age = $" ({years})";
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                referral.name = referral.name + age;
                OnPropertyChanged("referral");
                Debug.WriteLine("MessageViewModel: LoadChatMessages(), referral: " + referral);


                JObject servicesObj = await ReferralService.fetchReferralOfferedServices(referralId);
                JArray servicesArray = (JArray)servicesObj.GetValue("data");
                ReferralServices.Clear();

                foreach (JToken token in servicesArray)
                {
                    JObject chatObj = (JObject)token;
                    ReferralService referralService = chatObj.ToObject<ReferralService>(serializer);
                    Debug.WriteLine("ReferralDetailsViewModel: " + referralService.label);
                    referralService.currency = systemSettings.currency;
                    ReferralServices.Add(referralService);
                }
                OnPropertyChanged("ReferralServices");

                JObject ccObj = await CustomerCareChatGroup.getCustomerCareChatGroupId();
                customerCareChatGroup = ccObj.ToObject<CustomerCareChatGroup>(serializer);

                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ReferralDetailsViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }

        public void OnHealthCenterId(int healthCenterId)
        {
            HealthCenterId= healthCenterId;
        }
    }
}
