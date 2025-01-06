using System;
using Youth.Models;
using Youth.ViewModels;
using System.Collections.ObjectModel;

using Youth.Views.Account;

namespace Youth.Views.Main
{

    public partial class MessagesPage : ContentPage
    {
        ObservableCollection<Conversation> conversationList = new ObservableCollection<Conversation>();
        MessageViewModel _viewModel;

        public MessagesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MessageViewModel();
            NavigationPage.SetHasNavigationBar(this, false);

            MessagingCenter.Subscribe<LocationPickerPage, LocationModule>(this, "location", (sender, location) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.mLocation = location;
            });
        }

        private async void GoBack(object sender, EventArgs e)
        {
            //MessagingCenter.Send(new ChatsPage(), "RefreshMainPage");
            await Shell.Current.GoToAsync("..");
        }
        private async void ShowAttachmentOptions(object sender, EventArgs e)
        {
            if (AttachmentView.IsVisible)
                AttachmentView.IsVisible = false;
            else
                AttachmentView.IsVisible = true;
        }

        private void ToggleSendButton(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MessageEntry.Text))
            {
                SendButton.Source = "send_disabled";
            }
            else
            {
                SendButton.Source = "send_enabled";
            }

            if (conversationList != null && conversationList.Count > 0)
            {
                //conversationListView.ScrollTo(conversationList[conversationList.Count - 1], ScrollToPosition.End, false);
                var conv = conversationListView.ItemsSource.Cast<object>().LastOrDefault();
                //conversationListView.ScrollTo(conv, ScrollToPosition.End, false);
            }
        }

        protected override void OnDisappearing()
        {
            //MessagingCenter.Send(new ChatsPage(), "RefreshMainPage");
        }

        private async void SendMessage(object sender, EventArgs e)
        {

            MessageEntry.Text = string.Empty;
        }

        private async void OnSelectImage(object sender, EventArgs e)
        {
            AttachmentView.IsVisible = false;
            try
            {
                FileResult result = await MediaPicker.PickPhotoAsync();

                if (result != null)
                {
                    _viewModel.PictureFile = result;
                }
            }
            catch (Exception ex) { }
        }

        private async void OnOpenAddress(object sender, EventArgs e)
        {
            try
            {
                AttachmentView.IsVisible = false;
                await Shell.Current.GoToAsync($"{nameof(LocationPickerPage)}");
            }
            catch (Exception ex) { }
        }

        private async void OnOpenFile(object sender, EventArgs e)
        {
            AttachmentView.IsVisible = false;
            try
            {
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>> {
                    { DevicePlatform.iOS, new[] { "com.adobe.pdf", "com.microsoft.word.doc", "org.openxmlformats.wordprocessingml.document", "com.microsoft.excel.xls", "org.openxmlformats.spreadsheetml.sheet", "public.plain-text" } },
                    { DevicePlatform.Android, new[] { "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "text/plain" } }
                });

                var options = new PickOptions
                {
                    FileTypes = customFileType
                };

                var result = await FilePicker.PickAsync(options);

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    _viewModel.DocumentFile = result;
                    // Use the stream to process the selected file
                }
            }
            catch (Exception ex)
            {
                // Handle exception if picking the file fails
            }
        }

        private async void OnOpenContact(object sender, EventArgs e)
        {
            AttachmentView.IsVisible = false;
            //try
            //{
            //    var contact = await Contacts.PickContactAsync();

            //    if (contact != null)
            //    {
            //        // Use the contact's information here
            //        var name = contact.DisplayName;
            //        var email = contact.Emails.FirstOrDefault()?.EmailAddress;
            //        var contactEmails = "";

            //        int contactEmailsCount = 0;
            //        int contactPhonesCount = 0;

            //        ObservableCollection<ContactEmail> emails = new ObservableCollection<ContactEmail>();
            //        foreach (var contactEmail in contact.Emails)
            //        {
            //            contactEmails += $"{contactEmail.EmailAddress}\n";
            //            emails.Add(contactEmail);
            //            contactEmailsCount++;
            //        }

            //            // Access all phone numbers
            //            var phoneNumbers = "";
            //        ObservableCollection<ContactPhone> phones = new ObservableCollection<ContactPhone>();
            //        foreach (var phone in contact.Phones)
            //        {
            //            phoneNumbers += $"{phone.PhoneNumber}\n";
            //            phones.Add(phone);
            //            contactPhonesCount++;
            //        }

            //        PhoneContact phoneContact = new PhoneContact();
            //        phoneContact.firstName = contact.DisplayName;
            //        phoneContact.lastName = contact.GivenName;

            //        if (contactEmailsCount > 0)
            //        {
            //            if (contactEmailsCount == 1)
            //            {
            //                phoneContact.custom_email = emails[0].EmailAddress;
            //            }
            //            if (contactEmailsCount == 2)
            //            {
            //                phoneContact.custom_email = emails[0].EmailAddress;
            //                phoneContact.home_email = emails[1].EmailAddress;
            //            }
            //            if (contactEmailsCount == 3)
            //            {
            //                phoneContact.custom_email = emails[0].EmailAddress;
            //                phoneContact.home_email = emails[1].EmailAddress;
            //                phoneContact.work_email = emails[2].EmailAddress;
            //            }
            //            if (contactEmailsCount == 4)
            //            {
            //                phoneContact.custom_email = emails[0].EmailAddress;
            //                phoneContact.home_email = emails[1].EmailAddress;
            //                phoneContact.work_email = emails[2].EmailAddress;
            //                phoneContact.other_email = emails[3].EmailAddress;
            //            }
            //            if (contactEmailsCount > 4)
            //            {
            //                phoneContact.custom_email = emails[0].EmailAddress;
            //                phoneContact.home_email = emails[1].EmailAddress;
            //                phoneContact.work_email = emails[2].EmailAddress;
            //                phoneContact.other_email = emails[3].EmailAddress;
            //                phoneContact.mobile_email = emails[4].EmailAddress;
            //            }
            //        }

            //        if (contactPhonesCount > 0)
            //        {
            //            if (contactPhonesCount == 1) {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //            }
            //            if (contactPhonesCount == 2) {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //            }
            //            if (contactPhonesCount == 3) {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //                phoneContact.mobile_phone = phones[2].PhoneNumber;
            //            }
            //            if (contactPhonesCount == 4)
            //            {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //                phoneContact.mobile_phone = phones[2].PhoneNumber;
            //                phoneContact.work_phone = phones[3].PhoneNumber;
            //            }
            //            if (contactPhonesCount == 5)
            //            {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //                phoneContact.mobile_phone = phones[2].PhoneNumber;
            //                phoneContact.work_phone = phones[3].PhoneNumber;
            //                phoneContact.fax_work_phone = phones[4].PhoneNumber;
            //            }
            //            if (contactPhonesCount == 6)
            //            {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //                phoneContact.mobile_phone = phones[2].PhoneNumber;
            //                phoneContact.work_phone = phones[3].PhoneNumber;
            //                phoneContact.fax_work_phone = phones[4].PhoneNumber;
            //                phoneContact.fax_home_phone = phones[5].PhoneNumber;
            //            }
            //            if (contactPhonesCount == 7)
            //            {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //                phoneContact.mobile_phone = phones[2].PhoneNumber;
            //                phoneContact.work_phone = phones[3].PhoneNumber;
            //                phoneContact.fax_work_phone = phones[4].PhoneNumber;
            //                phoneContact.fax_home_phone = phones[5].PhoneNumber;
            //                phoneContact.pager_phone = phones[6].PhoneNumber;
            //            }
            //            if (contactPhonesCount == 8)
            //            {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //                phoneContact.mobile_phone = phones[2].PhoneNumber;
            //                phoneContact.work_phone = phones[3].PhoneNumber;
            //                phoneContact.fax_work_phone = phones[4].PhoneNumber;
            //                phoneContact.fax_home_phone = phones[5].PhoneNumber;
            //                phoneContact.pager_phone = phones[6].PhoneNumber;
            //                phoneContact.other_phone = phones[7].PhoneNumber;
            //            }
            //            if (contactPhonesCount == 9)
            //            {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //                phoneContact.mobile_phone = phones[2].PhoneNumber;
            //                phoneContact.work_phone = phones[3].PhoneNumber;
            //                phoneContact.fax_work_phone = phones[4].PhoneNumber;
            //                phoneContact.fax_home_phone = phones[5].PhoneNumber;
            //                phoneContact.pager_phone = phones[6].PhoneNumber;
            //                phoneContact.other_phone = phones[7].PhoneNumber;
            //                phoneContact.callback_phone = phones[8].PhoneNumber;
            //            }
            //            if (contactPhonesCount == 10)
            //            {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //                phoneContact.mobile_phone = phones[2].PhoneNumber;
            //                phoneContact.work_phone = phones[3].PhoneNumber;
            //                phoneContact.fax_work_phone = phones[4].PhoneNumber;
            //                phoneContact.fax_home_phone = phones[5].PhoneNumber;
            //                phoneContact.pager_phone = phones[6].PhoneNumber;
            //                phoneContact.other_phone = phones[7].PhoneNumber;
            //                phoneContact.callback_phone = phones[8].PhoneNumber;
            //                phoneContact.car_phone = phones[9].PhoneNumber;
            //            }
            //            if (contactPhonesCount == 11)
            //            {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //                phoneContact.mobile_phone = phones[2].PhoneNumber;
            //                phoneContact.work_phone = phones[3].PhoneNumber;
            //                phoneContact.fax_work_phone = phones[4].PhoneNumber;
            //                phoneContact.fax_home_phone = phones[5].PhoneNumber;
            //                phoneContact.pager_phone = phones[6].PhoneNumber;
            //                phoneContact.other_phone = phones[7].PhoneNumber;
            //                phoneContact.callback_phone = phones[8].PhoneNumber;
            //                phoneContact.car_phone = phones[9].PhoneNumber;
            //                phoneContact.company_main_phone = phones[10].PhoneNumber;
            //            }
            //            if (contactPhonesCount == 12)
            //            {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //                phoneContact.mobile_phone = phones[2].PhoneNumber;
            //                phoneContact.work_phone = phones[3].PhoneNumber;
            //                phoneContact.fax_work_phone = phones[4].PhoneNumber;
            //                phoneContact.fax_home_phone = phones[5].PhoneNumber;
            //                phoneContact.pager_phone = phones[6].PhoneNumber;
            //                phoneContact.other_phone = phones[7].PhoneNumber;
            //                phoneContact.callback_phone = phones[8].PhoneNumber;
            //                phoneContact.car_phone = phones[9].PhoneNumber;
            //                phoneContact.company_main_phone = phones[10].PhoneNumber;
            //                phoneContact.isdn_phone = phones[11].PhoneNumber;
            //            }
            //            if (contactPhonesCount == 13)
            //            {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //                phoneContact.mobile_phone = phones[2].PhoneNumber;
            //                phoneContact.work_phone = phones[3].PhoneNumber;
            //                phoneContact.fax_work_phone = phones[4].PhoneNumber;
            //                phoneContact.fax_home_phone = phones[5].PhoneNumber;
            //                phoneContact.pager_phone = phones[6].PhoneNumber;
            //                phoneContact.other_phone = phones[7].PhoneNumber;
            //                phoneContact.callback_phone = phones[8].PhoneNumber;
            //                phoneContact.car_phone = phones[9].PhoneNumber;
            //                phoneContact.company_main_phone = phones[10].PhoneNumber;
            //                phoneContact.isdn_phone = phones[11].PhoneNumber;
            //                phoneContact.main_phone = phones[12].PhoneNumber;
            //            }
            //            if (contactPhonesCount == 14)
            //            {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //                phoneContact.mobile_phone = phones[2].PhoneNumber;
            //                phoneContact.work_phone = phones[3].PhoneNumber;
            //                phoneContact.fax_work_phone = phones[4].PhoneNumber;
            //                phoneContact.fax_home_phone = phones[5].PhoneNumber;
            //                phoneContact.pager_phone = phones[6].PhoneNumber;
            //                phoneContact.other_phone = phones[7].PhoneNumber;
            //                phoneContact.callback_phone = phones[8].PhoneNumber;
            //                phoneContact.car_phone = phones[9].PhoneNumber;
            //                phoneContact.company_main_phone = phones[10].PhoneNumber;
            //                phoneContact.isdn_phone = phones[11].PhoneNumber;
            //                phoneContact.main_phone = phones[12].PhoneNumber;
            //                phoneContact.other_fax_phone = phones[13].PhoneNumber;
            //            }
            //            if (contactPhonesCount == 15)
            //            {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //                phoneContact.mobile_phone = phones[2].PhoneNumber;
            //                phoneContact.work_phone = phones[3].PhoneNumber;
            //                phoneContact.fax_work_phone = phones[4].PhoneNumber;
            //                phoneContact.fax_home_phone = phones[5].PhoneNumber;
            //                phoneContact.pager_phone = phones[6].PhoneNumber;
            //                phoneContact.other_phone = phones[7].PhoneNumber;
            //                phoneContact.callback_phone = phones[8].PhoneNumber;
            //                phoneContact.car_phone = phones[9].PhoneNumber;
            //                phoneContact.company_main_phone = phones[10].PhoneNumber;
            //                phoneContact.isdn_phone = phones[11].PhoneNumber;
            //                phoneContact.main_phone = phones[12].PhoneNumber;
            //                phoneContact.other_fax_phone = phones[13].PhoneNumber;
            //                phoneContact.radio_phone = phones[14].PhoneNumber;
            //            }
            //            if (contactPhonesCount == 16)
            //            {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //                phoneContact.mobile_phone = phones[2].PhoneNumber;
            //                phoneContact.work_phone = phones[3].PhoneNumber;
            //                phoneContact.fax_work_phone = phones[4].PhoneNumber;
            //                phoneContact.fax_home_phone = phones[5].PhoneNumber;
            //                phoneContact.pager_phone = phones[6].PhoneNumber;
            //                phoneContact.other_phone = phones[7].PhoneNumber;
            //                phoneContact.callback_phone = phones[8].PhoneNumber;
            //                phoneContact.car_phone = phones[9].PhoneNumber;
            //                phoneContact.company_main_phone = phones[10].PhoneNumber;
            //                phoneContact.isdn_phone = phones[11].PhoneNumber;
            //                phoneContact.main_phone = phones[12].PhoneNumber;
            //                phoneContact.other_fax_phone = phones[13].PhoneNumber;
            //                phoneContact.radio_phone = phones[14].PhoneNumber;
            //                phoneContact.telex_phone = phones[15].PhoneNumber;
            //            }
            //            if (contactPhonesCount == 17)
            //            {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //                phoneContact.mobile_phone = phones[2].PhoneNumber;
            //                phoneContact.work_phone = phones[3].PhoneNumber;
            //                phoneContact.fax_work_phone = phones[4].PhoneNumber;
            //                phoneContact.fax_home_phone = phones[5].PhoneNumber;
            //                phoneContact.pager_phone = phones[6].PhoneNumber;
            //                phoneContact.other_phone = phones[7].PhoneNumber;
            //                phoneContact.callback_phone = phones[8].PhoneNumber;
            //                phoneContact.car_phone = phones[9].PhoneNumber;
            //                phoneContact.company_main_phone = phones[10].PhoneNumber;
            //                phoneContact.isdn_phone = phones[11].PhoneNumber;
            //                phoneContact.main_phone = phones[12].PhoneNumber;
            //                phoneContact.other_fax_phone = phones[13].PhoneNumber;
            //                phoneContact.radio_phone = phones[14].PhoneNumber;
            //                phoneContact.telex_phone = phones[15].PhoneNumber;
            //                phoneContact.tty_tdd_phone = phones[16].PhoneNumber;
            //            }
            //            if (contactPhonesCount == 18)
            //            {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //                phoneContact.mobile_phone = phones[2].PhoneNumber;
            //                phoneContact.work_phone = phones[3].PhoneNumber;
            //                phoneContact.fax_work_phone = phones[4].PhoneNumber;
            //                phoneContact.fax_home_phone = phones[5].PhoneNumber;
            //                phoneContact.pager_phone = phones[6].PhoneNumber;
            //                phoneContact.other_phone = phones[7].PhoneNumber;
            //                phoneContact.callback_phone = phones[8].PhoneNumber;
            //                phoneContact.car_phone = phones[9].PhoneNumber;
            //                phoneContact.company_main_phone = phones[10].PhoneNumber;
            //                phoneContact.isdn_phone = phones[11].PhoneNumber;
            //                phoneContact.main_phone = phones[12].PhoneNumber;
            //                phoneContact.other_fax_phone = phones[13].PhoneNumber;
            //                phoneContact.radio_phone = phones[14].PhoneNumber;
            //                phoneContact.telex_phone = phones[15].PhoneNumber;
            //                phoneContact.tty_tdd_phone = phones[16].PhoneNumber;
            //                phoneContact.work_mobile_phone = phones[17].PhoneNumber;
            //            }
            //            if (contactPhonesCount == 19)
            //            {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //                phoneContact.mobile_phone = phones[2].PhoneNumber;
            //                phoneContact.work_phone = phones[3].PhoneNumber;
            //                phoneContact.fax_work_phone = phones[4].PhoneNumber;
            //                phoneContact.fax_home_phone = phones[5].PhoneNumber;
            //                phoneContact.pager_phone = phones[6].PhoneNumber;
            //                phoneContact.other_phone = phones[7].PhoneNumber;
            //                phoneContact.callback_phone = phones[8].PhoneNumber;
            //                phoneContact.car_phone = phones[9].PhoneNumber;
            //                phoneContact.company_main_phone = phones[10].PhoneNumber;
            //                phoneContact.isdn_phone = phones[11].PhoneNumber;
            //                phoneContact.main_phone = phones[12].PhoneNumber;
            //                phoneContact.other_fax_phone = phones[13].PhoneNumber;
            //                phoneContact.radio_phone = phones[14].PhoneNumber;
            //                phoneContact.telex_phone = phones[15].PhoneNumber;
            //                phoneContact.tty_tdd_phone = phones[16].PhoneNumber;
            //                phoneContact.work_mobile_phone = phones[17].PhoneNumber;
            //                phoneContact.work_pager_phone = phones[18].PhoneNumber;
            //            }
            //            if (contactPhonesCount == 20)
            //            {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //                phoneContact.mobile_phone = phones[2].PhoneNumber;
            //                phoneContact.work_phone = phones[3].PhoneNumber;
            //                phoneContact.fax_work_phone = phones[4].PhoneNumber;
            //                phoneContact.fax_home_phone = phones[5].PhoneNumber;
            //                phoneContact.pager_phone = phones[6].PhoneNumber;
            //                phoneContact.other_phone = phones[7].PhoneNumber;
            //                phoneContact.callback_phone = phones[8].PhoneNumber;
            //                phoneContact.car_phone = phones[9].PhoneNumber;
            //                phoneContact.company_main_phone = phones[10].PhoneNumber;
            //                phoneContact.isdn_phone = phones[11].PhoneNumber;
            //                phoneContact.main_phone = phones[12].PhoneNumber;
            //                phoneContact.other_fax_phone = phones[13].PhoneNumber;
            //                phoneContact.radio_phone = phones[14].PhoneNumber;
            //                phoneContact.telex_phone = phones[15].PhoneNumber;
            //                phoneContact.tty_tdd_phone = phones[16].PhoneNumber;
            //                phoneContact.work_mobile_phone = phones[17].PhoneNumber;
            //                phoneContact.work_pager_phone = phones[18].PhoneNumber;
            //                phoneContact.assistant_phone = phones[19].PhoneNumber;
            //            }
            //            if (contactPhonesCount > 20)
            //            {
            //                phoneContact.custom_phone = phones[0].PhoneNumber;
            //                phoneContact.home_phone = phones[1].PhoneNumber;
            //                phoneContact.mobile_phone = phones[2].PhoneNumber;
            //                phoneContact.work_phone = phones[3].PhoneNumber;
            //                phoneContact.fax_work_phone = phones[4].PhoneNumber;
            //                phoneContact.fax_home_phone = phones[5].PhoneNumber;
            //                phoneContact.pager_phone = phones[6].PhoneNumber;
            //                phoneContact.other_phone = phones[7].PhoneNumber;
            //                phoneContact.callback_phone = phones[8].PhoneNumber;
            //                phoneContact.car_phone = phones[9].PhoneNumber;
            //                phoneContact.company_main_phone = phones[10].PhoneNumber;
            //                phoneContact.isdn_phone = phones[11].PhoneNumber;
            //                phoneContact.main_phone = phones[12].PhoneNumber;
            //                phoneContact.other_fax_phone = phones[13].PhoneNumber;
            //                phoneContact.radio_phone = phones[14].PhoneNumber;
            //                phoneContact.telex_phone = phones[15].PhoneNumber;
            //                phoneContact.tty_tdd_phone = phones[16].PhoneNumber;
            //                phoneContact.work_mobile_phone = phones[17].PhoneNumber;
            //                phoneContact.work_pager_phone = phones[18].PhoneNumber;
            //                phoneContact.assistant_phone = phones[19].PhoneNumber;
            //                phoneContact.mms_phone = phones[20].PhoneNumber;
            //            }
            //        }

            //        // Show the contact's information in a message box
            //        _viewModel.mPhoneContact = phoneContact;
            //        //await DisplayAlert("Selected Contact", $"Name: {name}\nEmails: {contactEmailsCount}\nPhone Numbers:\n{contactPhonesCount}", "OK");
            //    }
            //}
            //catch (FeatureNotSupportedException ex)
            //{
            //    // Handle not supported on device exception
            //}
            //catch (Exception ex)
            //{
            //    // Handle other exceptions
            //}
        }
    }
}