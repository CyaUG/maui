using Youth.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Youth.ViewModels.Base;


namespace Youth.ViewModels
{
    internal class ScheduleApointmentViewModel : BaseViewModel, IScheduleApointmentViewModel
    {
        public bool isDateFilled { get; set; }
        public bool isTimeFilled { get; set; }
        private TimeSpan _myTime;
        private DateChangedEventArgs _myDate;
        public Command SetNewScheduleCommand { get; }
        public ScheduleApointmentViewModel()
        {
            Title = "Schedule";
            isDateFilled = false;
            isTimeFilled = false;
            SetNewScheduleCommand = new Command(async () => await ExecuteSetNewScheduleCommand());
        }
        public async Task ExecuteSetNewScheduleCommand()
        {
            try
            {
                string formattedDate;
                string formattedTime;
                DateTime currentDate = DateTime.Now;
                if (!isDateFilled)
                {
                    //use current date
                    formattedDate = currentDate.ToString("yyyy-MM-dd");
                }
                else
                {
                    formattedDate = _myDate.NewDate.ToString("yyyy-MM-dd");
                    Console.WriteLine("Selected date: " + formattedDate);
                }

                if (!isTimeFilled)
                {
                    //use current time
                    formattedTime = currentDate.ToString("HH:mm:ss");
                }
                else
                {
                    formattedTime = _myTime.ToString();
                    Console.WriteLine("Selected time: " + formattedTime);
                }

                string seletedDateAndTime = formattedDate + " " + formattedTime;
                Console.WriteLine("ScheduleApointmentViewModel: " + seletedDateAndTime);
                MessagingCenter.Send<ScheduleApointmentViewModel, String>(this, "dateAndTime", seletedDateAndTime);
                GoBack();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ScheduleApointmentViewModel: " + ex);
            }
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public TimeSpan MyTime
        {
            get
            {
                return _myTime;
            }
            set
            {
                SetProperty(ref _myTime, value);
                isTimeFilled = true;
                OnPropertyChanged("isTimeFilled");
                OnPropertyChanged("MyTime");
            }
        }
        public DateChangedEventArgs MyDate
        {
            get
            {
                return _myDate;
            }
            set
            {
                SetProperty(ref _myDate, value);
                isDateFilled = true;
                OnPropertyChanged("isDateFilled");
                OnPropertyChanged("MyDate");
            }
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }
    }
}
