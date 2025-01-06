using Youth.Models;
using Youth.Utils;
using Youth.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;




namespace Youth.Views.Auth
{

    public partial class ResetPassPage : ContentPage
    {
        public ResetPassPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void Btn_Reset(object sender, EventArgs e)
        {
            try
            {
                string email = EmailEntry.Text;

                if (string.IsNullOrEmpty(email))
                {
                    await DisplayAlert("Message", "Email is empty", "OKAY");
                }
                else
                {
                    ActivityIndicator.IsRunning = true;
                    HttpStatusCode statusCode = await UserAccount.ForgotPassword(email);
                    if (statusCode == HttpStatusCode.OK)
                    {
                        await DisplayAlert("Message", "We Have sent an email for you to continue", "OKAY");
                        ActivityIndicator.IsRunning = false;
                        await Shell.Current.GoToAsync("..");
                    }
                    else
                    {
                        await DisplayAlert("Message", "Something went wrong", "CLOSE");
                        ActivityIndicator.IsRunning = false;
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Fill out all the fields", "CLOSE");
                ActivityIndicator.IsRunning = false;
            }
        }

        private void Focused_Email(object sender, EventArgs e)
        {
            emailFrame.BorderColor = Color.FromRgb(189, 189, 189);
        }
    }
}