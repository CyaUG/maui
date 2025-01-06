using Youth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youth.Views.Account
{

    public partial class NameEditorPage : ContentPage
    {
        public NameEditorPage()
        {
            InitializeComponent();
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSubmitName(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(NameValue.Text))
            {
                MessagingCenter.Send<NameEditorPage, String>(this, "name", NameValue.Text);
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await DisplayAlert("Message", "Name is required", "CLOSE");
            }
        }
    }
}