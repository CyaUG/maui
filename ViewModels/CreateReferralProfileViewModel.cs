using Youth.Models;
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
using System.IO;
using System.Collections.Generic;
using Youth.Views.ReferralProgram;
using Youth.ViewModels.Interfaces;

namespace Youth.ViewModels
{
    internal class CreateReferralProfileViewModel : BaseViewModel, ICreateReferralProfileViewModel
    {
        public string profilePictureName { get; set; }
        public string nationalID { get; set; }
        public string LocationAddress { get; set; }
        public string profilePicturePath { get; set; }
        public string nationalIDPath { get; set; }
        public Command PickProfilePictureCommand { get; }
        public Command PickNationalIdCommand { get; }
        public Command PickReferralAccountCategory { get; }
        public Command PickCitizenship { get; }
        public Command PickGender { get; }
        public Command PickReferralDistrictProvider { get; }
        //public Command LoadEssentialsCommand { get; }
        public Command CreateReferralProfileCommand { get; }
        public bool profilePicSelected { get; set; }
        public bool nationalIdSelected { get; set; }
        public bool isGenderSelected { get; set; }
        public bool isReferralAccountCitizenshipSelected { get; set; }

        private string _nameValue;
        public string NameValue
        {
            get { return _nameValue; }
            set
            {
                _nameValue = value;
                OnPropertyChanged("NameValue");
            }
        }

        private string _emailValue;
        public string EmailValue
        {
            get { return _emailValue; }
            set
            {
                _emailValue = value;
                OnPropertyChanged("EmailValue");
            }
        }

        private string _phoneValue;
        public string PhoneValue
        {
            get { return _phoneValue; }
            set
            {
                _phoneValue = value;
                OnPropertyChanged("PhoneValue");
            }
        }

        private string _DirectionsValue;
        public string DirectionsValue
        {
            get { return _DirectionsValue; }
            set
            {
                _DirectionsValue = value;
                OnPropertyChanged("DirectionsValue");
            }
        }

        private string _aboutValue;
        public string AboutValue
        {
            get { return _aboutValue; }
            set
            {
                _aboutValue = value;
                OnPropertyChanged("_aboutValue");
            }
        }

        private string _SubCountyValue;
        public string SubCountyValue
        {
            get { return _SubCountyValue; }
            set
            {
                _SubCountyValue = value;
                OnPropertyChanged("_SubCountyValue");
            }
        }

        private string _NinValue;
        public string NinValue
        {
            get { return _NinValue; }
            set
            {
                _NinValue = value;
                OnPropertyChanged("_NinValue");
            }
        }

        private string _DobValue;
        public string DobValue
        {
            get { return _DobValue; }
            set
            {
                _DobValue = value;
                OnPropertyChanged("_DobValue");
            }
        }
        public CreateReferralProfileViewModel()
        {
            Title = "Create Profile";
            PickProfilePictureCommand = new Command(async () => await PickProfilePicture());
            //LoadEssentialsCommand = new Command(async () => await ExecuteLoadEssentialsCommand());
            PickNationalIdCommand = new Command(async () => await PickNationalID());
            PickReferralAccountCategory = new Command(async () => await OpenReferralAccountCategoryPickerPage());
            PickCitizenship = new Command(async () => await OpenCitizenshipPicker());
            PickReferralDistrictProvider = new Command(async () => await OpenReferralDistrictProvider());
            PickGender = new Command(async () => await OpenGenderPickerPage());
            CreateReferralProfileCommand = new Command(async () => await ExecureCreateReferralProfileCommand());
            nationalID = "National ID";
            activeReferralAccountCategory = new ReferralAccountCategory();
            activeReferralAccountCategory.label = "Account Category";
            activeGender = new Gender();
            activeGender.label = "Gender";
            LocationAddress = "Current Address";
            activeReferralAccountCitizenship = new ReferralAccountCitizenship();
            activeReferralAccountCitizenship.label = "Citizenship";
            activeReferralDistrict = new ReferralDistrict();
            activeReferralDistrict.label = "District";
            _ = ExecuteLoadEssentialsCommand();
        }

        private ReferralAccountCategory _referralAccountCategory;
        public ReferralAccountCategory activeReferralAccountCategory
        {
            get { return _referralAccountCategory; }
            set
            {
                _referralAccountCategory = value;
                OnPropertyChanged();
            }
        }

        private Gender _Gender;
        public Gender activeGender
        {
            get { return _Gender; }
            set
            {
                _Gender = value;
                OnPropertyChanged();
            }
        }

        private ReferralAccountCitizenship _ReferralAccountCitizenship;
        public ReferralAccountCitizenship activeReferralAccountCitizenship
        {
            get { return _ReferralAccountCitizenship; }
            set
            {
                _ReferralAccountCitizenship = value;
                OnPropertyChanged();
            }
        }

        private ReferralDistrict _ReferralDistrict;
        public ReferralDistrict activeReferralDistrict
        {
            get { return _ReferralDistrict; }
            set
            {
                _ReferralDistrict = value;
                OnPropertyChanged();
            }
        }
        public String SetLocationAddress
        {
            get { return LocationAddress; }
            set
            {
                LocationAddress = value;
                OnPropertyChanged("LocationAddress");
            }
        }

        private LocationModule _Location;
        public LocationModule activeLocation
        {
            get { return _Location; }
            set
            {
                _Location = value;
                OnPropertyChanged();
            }
        }

        private FileResult mProfilePicture;
        public FileResult ProfilePicture
        {
            get { return mProfilePicture; }
            set
            {
                mProfilePicture = value;
                OnPropertyChanged("HighSchoolFile");
            }
        }
        async Task OpenCitizenshipPicker()
        {
            await Shell.Current.GoToAsync($"{nameof(CitizenshipPickerPage)}");
        }
        async Task OpenReferralDistrictProvider()
        {
            await Shell.Current.GoToAsync($"{nameof(ReferralDistrictProviderPage)}");
        }
        async Task OpenReferralAccountCategoryPickerPage()
        {
            await Shell.Current.GoToAsync($"{nameof(ReferralAccountCategoryPickerPage)}");
        }
        async Task OpenGenderPickerPage()
        {
            await Shell.Current.GoToAsync($"{nameof(GenderPickerPage)}");
        }
        async Task ExecureCreateReferralProfileCommand()
        {
            try
            {
                bool goOn = true;

                String name = NameValue;
                String email = EmailValue;
                String phone = PhoneValue;
                int accCategoryId = activeReferralAccountCategory.id;
                int genderId = activeGender.id;
                String about = AboutValue;
                String birthDistrict = activeReferralDistrict.label;
                String birthSubCounty = SubCountyValue;
                String mAddress_label = LocationAddress;
                double mAddress_latitude = activeLocation.Latitude;
                double mAddress_longitude = activeLocation.Longitude;
                int citizenshipId = activeReferralAccountCitizenship.id;
                String dateOfBirth = DobValue;
                String nin = NinValue;

                if (string.IsNullOrEmpty(about))
                {
                    goOn = false;
                }

                if (string.IsNullOrEmpty(name))
                {
                    goOn = false;
                }

                if (string.IsNullOrEmpty(email))
                {
                    goOn = false;
                }

                if (string.IsNullOrEmpty(phone))
                {
                    goOn = false;
                }

                if (string.IsNullOrEmpty(birthSubCounty))
                {
                    goOn = false;
                }

                if (string.IsNullOrEmpty(nin))
                {
                    goOn = false;
                }

                if (string.IsNullOrEmpty(birthSubCounty))
                {
                    goOn = false;
                }

                if (activeReferralAccountCategory == null)
                {
                    goOn = false;
                }

                if (activeGender == null)
                {
                    goOn = false;
                }

                if (activeReferralDistrict == null)
                {
                    goOn = false;
                }

                if (string.IsNullOrEmpty(mAddress_label))
                {
                    goOn = false;
                }

                if (activeReferralAccountCitizenship == null)
                {
                    goOn = false;
                }

                if (NationalID == null)
                {
                    goOn = false;
                }

                if (ProfilePicture == null)
                {
                    goOn = false;
                }

                if (goOn)
                {
                    ReferralAccount referralAccount = new ReferralAccount();
                    referralAccount.name = name;
                    referralAccount.email = email;
                    referralAccount.phone = phone;
                    referralAccount.accCategoryId = accCategoryId;
                    referralAccount.genderId = genderId;
                    referralAccount.about = about;
                    referralAccount.birthDistrict = birthDistrict;
                    referralAccount.birthSubCounty = birthSubCounty;
                    referralAccount.address_label = mAddress_label;
                    referralAccount.address_latitude = mAddress_latitude;
                    referralAccount.address_longitude = mAddress_longitude;
                    referralAccount.citizenshipId = citizenshipId;
                    referralAccount.dateOfBirth = dateOfBirth;
                    referralAccount.nin = nin;

                    MessagingCenter.Send<CreateReferralProfileViewModel, bool>(this, "isSeekerRunning", true);
                    await ReferralAccount.createMyReferralAccount(referralAccount, NationalID, ProfilePicture);
                    GoBack();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("CreateReferralProfileViewModel:" + e);
                MessagingCenter.Send<CreateReferralProfileViewModel, string>(this, "message", "Provide all the details to apply");
            }
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        async Task PickProfilePicture()
        {
            try
            {
                FileResult result = await MediaPicker.PickPhotoAsync();

                if (result != null)
                {
                    profilePictureName = result.FileName;
                    profilePicturePath = result.FullPath;
                    ProfilePicture = result;
                    profilePicSelected = true;
                    OnPropertyChanged("ProfilePicture");
                    OnPropertyChanged("profilePictureName");
                    OnPropertyChanged("profilePicturePath");
                    OnPropertyChanged("profilePicSelected");
                }
            }
            catch (Exception ex) { }
        }

        private FileResult mNationalID;
        public FileResult NationalID
        {
            get { return mNationalID; }
            set
            {
                mNationalID = value;
                OnPropertyChanged("NationalID");
            }
        }
        async Task PickNationalID()
        {
            try
            {
                FileResult result = await MediaPicker.PickPhotoAsync();

                if (result != null)
                {
                    nationalID = result.FileName;
                    nationalIDPath = result.FullPath;
                    NationalID = result;
                    nationalIdSelected = true;
                    OnPropertyChanged("nationalID");
                    OnPropertyChanged("nationalIDPath");
                    OnPropertyChanged("NationalID");
                    OnPropertyChanged("nationalIdSelected");
                }
            }
            catch (Exception ex) { }
        }

        async Task ExecuteLoadEssentialsCommand()
        {
            Debug.WriteLine("CreateReferralProfileViewModel: ExecuteLoadEssentialsCommand()");
            IsBusy = false;
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }
    }
}
