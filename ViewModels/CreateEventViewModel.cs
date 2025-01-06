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
    internal class CreateEventViewModel : BaseViewModel, ICreateEventViewModel
    {
        public Command PickPictureAttachmentCommand { get; }
        public Command SubmitEventCommand { get; }
        public Command LoadMainCommand { get; }
        public string pictureName { get; set; }
        public string picturePath { get; set; }
        public bool picSelected { get; set; }
        public CreateEventViewModel()
        {
            Title = "Create Event";
            RemindDate = "Pick Event Date";
            LocationAddress = "Current Address";
            mEventCategory = new EventCategory();
            mEventCategory.label = "Pick Event Category";
            mEventCategory.imageUrl = "images/logo.png";
            PickPictureAttachmentCommand = new Command(async () => await PickPictureAttachment());
            SubmitEventCommand = new Command(async () => await ExecuteSubmitEventCommand());
            LoadMainCommand = new Command(async () => await ExecuteLoadMainCommand());
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

        private EventCategory _eventCategory;
        public EventCategory mEventCategory
        {
            get { return _eventCategory; }
            set
            {
                _eventCategory = value;
                OnPropertyChanged("mEventCategory");
            }
        }

        private string _eventTitle;
        public string EventTitle
        {
            get { return _eventTitle; }
            set
            {
                _eventTitle = value;
                OnPropertyChanged("EventTitle");
            }
        }

        private string _eventDescription;
        public string EventDescription
        {
            get { return _eventDescription; }
            set
            {
                _eventDescription = value;
                OnPropertyChanged("EventDescription");
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
        public string LocationAddress { get; set; }
        public String SetLocationAddress
        {
            get { return LocationAddress; }
            set
            {
                LocationAddress = value;
                OnPropertyChanged("LocationAddress");
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

        private bool _isFreeEvent;
        public bool isFreeEvent
        {
            get { return _isFreeEvent; }
            set
            {
                _isFreeEvent = value;
                OnPropertyChanged("isFreeEvent");
            }
        }
        async Task ExecuteSubmitEventCommand()
        {
            try
            {
                bool goOn = true;
                String eventTitle = EventTitle;
                String description = EventDescription;
                String eventDate = RemindDate;

                if (string.IsNullOrEmpty(eventTitle))
                {
                    goOn = false;
                }

                if (string.IsNullOrEmpty(description))
                {
                    goOn = false;
                }

                if (string.IsNullOrEmpty(eventDate))
                {
                    goOn = false;
                }

                if (activeLocation == null)
                {
                    goOn = false;
                }

                if (mEventCategory == null)
                {
                    goOn = false;
                }

                if (goOn)
                {
                    Event mEvent = new Event();
                    mEvent.eventTitle = eventTitle;
                    mEvent.description = description;
                    mEvent.freeEntrance = isFreeEvent;
                    mEvent.eventDate = eventDate;
                    mEvent.event_address = LocationAddress;
                    mEvent.event_latitude = activeLocation.Latitude;
                    mEvent.event_longitude = activeLocation.Longitude;
                    mEvent.eventCategoryId = mEventCategory.id;


                    MessagingCenter.Send<CreateEventViewModel, bool>(this, "isSeekerRunning", true);
                    await Event.submitEvent(mEvent, Picture);
                    GoBack();
                }
                else
                {
                    MessagingCenter.Send<CreateEventViewModel, string>(this, "message", "Provide all the details to apply");
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send<CreateEventViewModel, bool>(this, "isSeekerRunning", false);
                MessagingCenter.Send<CreateEventViewModel, string>(this, "message", "Something went wrong");
                Debug.WriteLine("CreateEventViewModel: " + ex);
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

                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>> {
                    { DevicePlatform.iOS, new[] { "image/jpeg", "image/png", "image/gif" } },
                    { DevicePlatform.Android, new[] { "image/jpeg", "image/png", "image/gif" } }
                });

                var options = new PickOptions
                {
                    PickerTitle = "Please select a document",
                    FileTypes = customFileType,
                };

                var fileData = await FilePicker.PickAsync(options);

                if (fileData != null)
                {
                    // Use the fileData.FileName and fileData.Data properties to access the picked file
                    pictureName = fileData.FileName;
                    picturePath = fileData.FullPath;
                    Picture = fileData;
                    picSelected = true;
                    OnPropertyChanged("Picture");
                    OnPropertyChanged("pictureName");
                    OnPropertyChanged("picturePath");
                    OnPropertyChanged("picSelected");
                }
            }
            catch (Exception ex)
            {
                // Handle exception if picking the file fails
            }
        }

        async Task ExecuteLoadMainCommand()
        {
            IsBusy = false;
            OnPropertyChanged("IsBusy");
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
            /*Set defaults*/
        }
    }
}
