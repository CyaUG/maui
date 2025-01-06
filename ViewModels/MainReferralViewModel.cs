using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views;
using Youth.Views.Events;
using Youth.Views.jobs;
using Youth.Views.Main;
using Youth.Views.ReferralProgram;
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
using CommunityToolkit.Mvvm.Input;


namespace Youth.ViewModels
{
    internal partial class MainReferralViewModel : BaseViewModel, IMainReferralViewModel
    {
        public Command RunReferralMainCommand { get; }
        public Command SafeSpaceCardTappedCommand { get; }
        public Command RevealMyQrCodeTappedCommand { get; }
        public Command OpenCreateReferralProfile { get; }
        public Command OpenLinkReferralProfile { get; }
        public Command OpenContactUsPage { get; }
        public Command OpenReferralAccountProviderCommand { get; }
        public Command<Referral> RevealPatientTappedCommand { get; }
        public Command<Symptom> SymptomTappedCommand { get; }
        public Command<Referral> RevealDoctorTappedCommand { get; }
        public Command<Referral> RevealPeerTappedCommand { get; }
        public SystemSettings systemSettings { get; set; }
        public UserAccount userAccount { get; set; }
        public ReferralAccountCategory referralAccountCategory { get; set; }
        public ReferralAccount referralAccount { get; set; }
        public ObservableCollection<Symptom> Symptoms { get; set; }
        public ObservableCollection<Referral> Referrals { get; set; }
        public bool showReferralAccountNullError { get; set; }
        public bool showShimmer { get; set; }
        public bool showConsultations { get; set; }
        public bool showPeerEducatorReferrals { get; set; }
        public bool showDoctorReferralConsultations { get; set; }
        public bool showAccountUnderReview { get; set; }
        public CustomerCareChatGroup customerCareChatGroup { get; set; }
        public MainReferralViewModel()
        {
            Title = "Referral Program";
            Symptoms = new ObservableCollection<Symptom>();
            Referrals = new ObservableCollection<Referral>();
            RunReferralMainCommand = new Command(async () => await ExecuteRunReferralMainCommand());
            SafeSpaceCardTappedCommand = new Command(async () => await ExecuteSafeSpaceCardTappedCommand());
            RevealMyQrCodeTappedCommand = new Command(async () => await ExecuteRevealMyQrCodeTappedCommand());

            OpenCreateReferralProfile = new Command(async () => await OpenCreateReferralProfilePage());
            OpenLinkReferralProfile = new Command(async () => await OpenLinkReferralProfilePage());
            OpenContactUsPage = new Command(async () => await ExecuteOpenContactUsPage());
            OpenReferralAccountProviderCommand = new Command(async () => await ExecuteOpenReferralAccountProvider());

            RevealPatientTappedCommand = new Command<Referral>(OpenPatientTappedCommand);
            SymptomTappedCommand = new Command<Symptom>(OpenSymptonTappedCommand);
            RevealDoctorTappedCommand = new Command<Referral>(OpenDoctorTappedCommand);
            RevealPeerTappedCommand = new Command<Referral>(OpenPeerTappedCommand);

            showShimmer = true;
            showConsultations = false;
            showPeerEducatorReferrals = false;
            showDoctorReferralConsultations = false;
            showAccountUnderReview = false;
            showReferralAccountNullError = false;
        }

        async void OpenPatientTappedCommand(Referral mReferral)
        {
            if (mReferral == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ReferralDetailsPatientPage)}?{nameof(ReferralDetailsViewModel.ReferralId)}={mReferral.referralId}");
        }

        async void OpenSymptonTappedCommand(Symptom mSymptom)
        {
            if (mSymptom == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(SymptomDetailsPage)}?{nameof(SymptomDetailsViewModel.SymptomId)}={mSymptom.id}");
        }

        async void OpenDoctorTappedCommand(Referral mReferral)
        {
            if (mReferral == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ReferralDetailsHealthWorkerPage)}?{nameof(ReferralDetailsViewModel.ReferralId)}={mReferral.referralId}");
        }

        async void OpenPeerTappedCommand(Referral mReferral)
        {
            if (mReferral == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ReferralDetailsPeerEducatorPage)}?{nameof(ReferralDetailsViewModel.ReferralId)}={mReferral.referralId}");
        }

        async Task ExecuteOpenReferralAccountProvider()
        {
            await Shell.Current.GoToAsync($"{nameof(ReferralAccountProviderPage)}");
        }

        async Task OpenCreateReferralProfilePage()
        {
            await Shell.Current.GoToAsync($"{nameof(CreateReferralProfilePage)}");
        }

        async Task OpenLinkReferralProfilePage()
        {
            await Shell.Current.GoToAsync($"{nameof(LinkReferralProfilePage)}");
        }

        async Task ExecuteOpenContactUsPage()
        {
            //open contact us page
            await Shell.Current.GoToAsync($"{nameof(MessagesPage)}?{nameof(MessageViewModel.ChatId)}={customerCareChatGroup.id}&{nameof(MessageViewModel.UserId)}={userAccount.id}");
        }

        [RelayCommand]
        private async Task SearchGridTapped()
        {
            await Shell.Current.GoToAsync($"{nameof(SearchSymptomsPage)}");
        }
        async Task ExecuteSafeSpaceCardTappedCommand()
        {
            await Shell.Current.GoToAsync($"{nameof(SafeSpaceMainPage)}");
        }
        async Task ExecuteRevealMyQrCodeTappedCommand()
        {
            if (referralAccount == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ReferralAccountQrCodePage)}?{nameof(ReferralAccountQrCodeViewModel.InputText)}={referralAccount.token}");
        }
        async Task ExecuteRunReferralMainCommand()
        {
            Debug.WriteLine("MainReferralViewModel: ExecuteRunReferralMainCommand()");
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
                Debug.WriteLine("MainReferralViewModel: " + systemSettings.currency);
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
                    Debug.WriteLine("MainReferralViewModel Err: " + ex);
                }

                if (referralAccount == null)
                {
                    showReferralAccountNullError = true;
                    showShimmer = false;
                    showConsultations = false;
                    showPeerEducatorReferrals = false;
                    showDoctorReferralConsultations = false;
                    showAccountUnderReview = false;
                    OnPropertyChanged("showConsultations");
                    OnPropertyChanged("showPeerEducatorReferrals");
                    OnPropertyChanged("showDoctorReferralConsultations");
                    OnPropertyChanged("showReferralAccountNullError");
                    OnPropertyChanged("showShimmer");
                    OnPropertyChanged("showAccountUnderReview");
                }
                else
                {
                    JObject refAccCatObj = await ReferralAccountCategory.fetchReferralAccountCategory(referralAccount.accCategoryId);
                    JObject refAccCatJsonObj = (JObject)refAccCatObj.GetValue("data");
                    referralAccountCategory = refAccCatJsonObj.ToObject<ReferralAccountCategory>(serializer);
                    OnPropertyChanged("referralAccountCategory");

                    if (referralAccount.status == "CREATED" && (userAccount.id != referralAccount.userId))
                    {
                        showConsultations = false;
                        showPeerEducatorReferrals = false;
                        showDoctorReferralConsultations = false;
                        showAccountUnderReview = true;
                        showReferralAccountNullError = false;
                        showShimmer = false;
                        OnPropertyChanged("showConsultations");
                        OnPropertyChanged("showPeerEducatorReferrals");
                        OnPropertyChanged("showDoctorReferralConsultations");
                        OnPropertyChanged("showReferralAccountNullError");
                        OnPropertyChanged("showShimmer");
                        OnPropertyChanged("showAccountUnderReview");
                    }
                    else
                    {
                        showAccountUnderReview = false;
                        showReferralAccountNullError = false;
                        OnPropertyChanged("showReferralAccountNullError");
                        OnPropertyChanged("showAccountUnderReview");

                        if (referralAccountCategory.canPrimaryRefer && referralAccount.status == "APPROVED")
                        {
                            //peer educator
                            JObject refObj = await Referral.fetchAllMyPeerEducationReferrals();
                            Referrals.Clear();
                            JArray refArray = (JArray)refObj.GetValue("data");

                            foreach (JToken token in refArray)
                            {
                                JObject chatObj = (JObject)token;
                                Referral mReferral = chatObj.ToObject<Referral>(serializer);
                                Debug.WriteLine("MainReferralViewModel: " + mReferral.name);
                                Referrals.Add(mReferral);
                            }
                            OnPropertyChanged("Referrals");
                            showShimmer = false;
                            OnPropertyChanged("showShimmer");
                            showPeerEducatorReferrals = true;
                            OnPropertyChanged("showPeerEducatorReferrals");
                            showDoctorReferralConsultations = false;
                            OnPropertyChanged("showDoctorReferralConsultations");
                            showConsultations = false;
                            OnPropertyChanged("showConsultations");
                        }
                        else if (referralAccountCategory.canSecondaryRefer && referralAccount.status == "APPROVED")
                        {
                            //doctor
                            JObject refObj = await Referral.fetchAllMyAssigneeReferrals();
                            Referrals.Clear();
                            JArray refArray = (JArray)refObj.GetValue("data");

                            foreach (JToken token in refArray)
                            {
                                JObject chatObj = (JObject)token;
                                Referral mReferral = chatObj.ToObject<Referral>(serializer);
                                Debug.WriteLine("MainReferralViewModel: " + mReferral.name);
                                Referrals.Add(mReferral);
                            }
                            OnPropertyChanged("Referrals");
                            showShimmer = false;
                            OnPropertyChanged("showShimmer");
                            showDoctorReferralConsultations = true;
                            OnPropertyChanged("showDoctorReferralConsultations");
                            showConsultations = false;
                            OnPropertyChanged("showConsultations");
                            showPeerEducatorReferrals = false;
                            OnPropertyChanged("showPeerEducatorReferrals");
                        }
                        else
                        {
                            //general users
                            JObject refObj = await Referral.fetchAllMyReferrals();
                            Referrals.Clear();
                            JArray refArray = (JArray)refObj.GetValue("data");

                            foreach (JToken token in refArray)
                            {
                                JObject chatObj = (JObject)token;
                                Referral mReferral = chatObj.ToObject<Referral>(serializer);
                                Debug.WriteLine("MainReferralViewModel: " + mReferral.name);
                                Referrals.Add(mReferral);
                            }
                            OnPropertyChanged("Referrals");
                            showShimmer = false;
                            OnPropertyChanged("showShimmer");
                            showConsultations = true;
                            OnPropertyChanged("showConsultations");
                            showDoctorReferralConsultations = false;
                            OnPropertyChanged("showDoctorReferralConsultations");
                            showPeerEducatorReferrals = false;
                            OnPropertyChanged("showPeerEducatorReferrals");
                        }
                    }
                }

                JObject categoriesObj = await Symptom.fetchFeaturedSymptoms();
                Symptoms.Clear();
                JArray categoriesArray = (JArray)categoriesObj.GetValue("data");

                foreach (JToken token in categoriesArray)
                {
                    JObject chatObj = (JObject)token;
                    Symptom mSymptom = chatObj.ToObject<Symptom>(serializer);
                    Debug.WriteLine("MainReferralViewModel: " + mSymptom.label);
                    Symptoms.Add(mSymptom);
                }
                OnPropertyChanged("Symptoms");

                JObject ccObj = await CustomerCareChatGroup.getCustomerCareChatGroupId();
                customerCareChatGroup = ccObj.ToObject<CustomerCareChatGroup>(serializer);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MainReferralViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("Symptoms");
                OnPropertyChanged("Events");
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
