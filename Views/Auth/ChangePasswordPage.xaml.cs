using Youth.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;




namespace Youth.Views.Auth
{

    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
        }

        private async void Btn_Reset(object sender, EventArgs e)
        {
            try
            {
                string old_password = OldPasswordEntry.Text;
                string new_password = NewPasswordEntry.Text;
                string confirm_password = RepeatPasswordEntry.Text;

                if (new_password != confirm_password)
                {
                    await DisplayAlert("Message", "Passwords Don't Match", "OKAY");
                }
                else
                {
                    ActivityIndicator.IsRunning = true;
                    HttpStatusCode statusCode = await UserAccount.UpdatePassword(old_password, new_password, confirm_password);
                    if (statusCode == HttpStatusCode.OK)
                    {
                        await DisplayAlert("Message", "Your Password Has Been Updated", "OKAY");
                        ActivityIndicator.IsRunning = false;
                        await Shell.Current.GoToAsync("..");
                    }
                    else
                    {
                        await DisplayAlert("Message", "The Password You provided is wrong", "CLOSE");
                        ActivityIndicator.IsRunning = false;
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Fill out all the fields", "CLOSE");
            }
        }

        private void Focused_Old_Password(object sender, EventArgs e)
        {
            oldPasswordFrame.BorderColor = Color.FromRgb(189, 189, 189);
        }

        private void Focused_New_Password(object sender, EventArgs e)
        {
            newPasswordFrame.BorderColor = Color.FromRgb(189, 189, 189);
        }

        private void Focused_Repeat_Password(object sender, EventArgs e)
        {
            repeatPasswordFrame.BorderColor = Color.FromRgb(189, 189, 189);
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}