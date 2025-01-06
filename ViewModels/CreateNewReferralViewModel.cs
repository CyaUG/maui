using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.jobs;
using Youth.Views.ReferralProgram;
using Youth.Views.Shopping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Youth.ViewModels.Base;



namespace Youth.ViewModels
{
    [QueryProperty(nameof(Token), nameof(Token))]
    internal class CreateNewReferralViewModel : BaseViewModel, ICreateNewReferralViewModel
    {
        private String token;
        public Command LoadReferralAccountCommand { get; set; }
        public Command PickHealthCenterCommand { get; set; }
        public ReferralAccount referralAccount { get; set; }
        public Command PickPictureAttachmentCommand { get; }
        public Command SubmitReferralCommand { get; }
        public string pictureName { get; set; }
        public string picturePath { get; set; }
        public bool picSelected { get; set; }

        private string _referralReason;
        public string ReferralReason
        {
            get { return _referralReason; }
            set
            {
                _referralReason = value;
                OnPropertyChanged("ReferralReason");
            }
        }

        private FileResult mPicture;
        public FileResult Picture
        {
            get { return mPicture; }
            set
            {
                mPicture = value;
                OnPropertyChanged("HighSchoolFile");
            }
        }

        public HealthCenter _healthCenter;
        public HealthCenter mHealthCenter
        {
            get
            {
                return _healthCenter;
            }
            set
            {
                _healthCenter = value;
                OnPropertyChanged("mHealthCenter");
            }
        }

        public CreateNewReferralViewModel()
        {
            Title = "Create New Referral";
            LoadReferralAccountCommand = new Command(async () => await FilterReferralAccount(Token));
            PickHealthCenterCommand = new Command(async () => await PickHealthCenter());
            PickPictureAttachmentCommand = new Command(async () => await PickPictureAttachment());
            SubmitReferralCommand = new Command(async () => await SubmitReferral());
        }

        async Task PickHealthCenter()
        {
            await Shell.Current.GoToAsync($"{nameof(HealthCenterProviderPage)}");
        }

        async Task SubmitReferral()
        {
            try
            {
                bool goOn = true;

                if (string.IsNullOrEmpty(Token))
                {
                    goOn = false;
                }

                if (string.IsNullOrEmpty(RemindDate))
                {
                    goOn = false;
                }

                if (string.IsNullOrEmpty(ReferralReason))
                {
                    goOn = false;
                }

                if (mHealthCenter == null)
                {
                    goOn = false;
                }

                if (goOn)
                {
                    MessagingCenter.Send<CreateNewReferralViewModel, bool>(this, "isSeekerRunning", true);
                    if (Picture == null)
                    {
                        await Referral.SubmitReferral(Token, mHealthCenter.id, RemindDate, ReferralReason);
                    }
                    else
                    {
                        await Referral.SubmitReferral(Token, mHealthCenter.id, RemindDate, ReferralReason, Picture);
                    }
                    MessagingCenter.Send<CreateNewReferralViewModel, bool>(this, "isSeekerRunning", false);
                    GoBack();
                }
                else
                {
                    MessagingCenter.Send<CreateNewReferralViewModel, string>(this, "message", "Fillout all the questions");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("CreateReferralProfileViewModel:" + e);
                MessagingCenter.Send<CreateNewReferralViewModel, string>(this, "message", "Provide all the details to apply");
            }
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        async Task PickPictureAttachment()
        {


            try
            {
                FileResult result = await MediaPicker.PickPhotoAsync();

                if (result != null)
                {
                    pictureName = result.FileName;
                    picturePath = result.FullPath;
                    Picture = result;
                    picSelected = true;
                    OnPropertyChanged("Picture");
                    OnPropertyChanged("pictureName");
                    OnPropertyChanged("picturePath");
                    OnPropertyChanged("picSelected");
                }
            }
            catch (Exception ex) { }
        }

        public String Token
        {
            get
            {
                return token;
            }
            set
            {
                token = value;
                FilterReferralAccount(value);
            }
        }

        private string _remindDate;
        public string RemindDate
        {
            get { return _remindDate; }
            set
            {
                _remindDate = value;
                OnPropertyChanged("RemindDate");
            }
        }

        public async Task FilterReferralAccount(String token)
        {
            Debug.WriteLine("ShoppingOrderProductsViewModel: ExecuteLoadShoppingOrdersCommand()");
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

                JObject serverObj = await ReferralAccount.fetchReferralAccountByTocken(token);
                try
                {
                    JObject refAccJsonObj = (JObject)serverObj.GetValue("data");
                    referralAccount = refAccJsonObj.ToObject<ReferralAccount>(serializer);
                    OnPropertyChanged("referralAccount");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("MainReferralViewModel Err: " + ex);
                }

                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ShoppingOrderProductsViewModel: " + ex);
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
    }
}
