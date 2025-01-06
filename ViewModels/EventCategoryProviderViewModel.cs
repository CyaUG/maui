using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.Events;
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


namespace Youth.ViewModels
{
    internal class EventCategoryProviderViewModel : BaseViewModel, IEventCategoryProviderViewModel
    {
        public ObservableCollection<EventCategory> eventCategories { get; set; }
        public Command LoadEventCategoriesCommand { get; }
        public Command<EventCategory> EventCategoryNavTap { get; }
        public EventCategoryProviderViewModel()
        {
            Title = "Event Categories";
            eventCategories = new ObservableCollection<EventCategory>();
            LoadEventCategoriesCommand = new Command(async () => await ExecuteLoadEventCategoriesCommand());
            EventCategoryNavTap = new Command<EventCategory>(ExecuteEventCategoryNavTapCommand);
        }

        async void ExecuteEventCategoryNavTapCommand(EventCategory eventCategory)
        {
            if (eventCategory == null)
                return;

            MessagingCenter.Send<EventCategoryProviderViewModel, EventCategory>(this, "eventCategory", eventCategory);
            GoBack();
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        async Task ExecuteLoadEventCategoriesCommand()
        {
            Debug.WriteLine("MainEventsViewModel: ExecuteLoadEventCategoriesCommand()");
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

                JObject categoriesObj = await EventCategory.fetchUserQueryParameterEventCategories();
                eventCategories.Clear();
                JArray categoriesArray = (JArray)categoriesObj.GetValue("data");

                foreach (JToken token in categoriesArray)
                {
                    JObject chatObj = (JObject)token;
                    EventCategory mEventCategory = chatObj.ToObject<EventCategory>(serializer);
                    Debug.WriteLine("MainEventsViewModel: " + mEventCategory.label);
                    eventCategories.Add(mEventCategory);
                }
                OnPropertyChanged("eventCategories");
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MainEventsViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("eventCategories");
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
