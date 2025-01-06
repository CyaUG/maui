using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using ZXing.Net.Maui.Controls;
using Youth.ViewModels.Interfaces;
using Youth.ViewModels;
using Youth.Views;
using Youth.Views.Account;
using Youth.Views.Auth;
using Youth.Views.Events;
using Youth.Views.jobs;
using Youth.Views.Main;
using Youth.Views.Plastics;
using Youth.Views.Quizzes;
using Youth.Views.ReferralProgram;
using Youth.Views.SafeSpace;
using Youth.Views.Shopping;
using Youth.Services;
using Microsoft.Maui.LifecycleEvents;
using Youth.Views.Tools;

namespace Youth
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMediaElement()
                .UseBarcodeReader() // Add this line to initialize ZXing.Net.Mau
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).ConfigureMauiHandlers(handlers =>
                {
#if ANDROID || IOS || MACCATALYST
                    handlers.AddHandler<Microsoft.Maui.Controls.Maps.Map, CustomMapHandler>();
#endif
                });

#if ANDROID || IOS || MACCATALYST
            builder.UseMauiMaps();
#endif

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.ConfigureLifecycleEvents(lifecycle =>
            {
#if WINDOWS
        //lifecycle
        //    .AddWindows(windows =>
        //        windows.OnNativeMessage((app, args) => {
        //            if (WindowExtensions.Hwnd == IntPtr.Zero)
        //            {
        //                WindowExtensions.Hwnd = args.Hwnd;
        //                WindowExtensions.SetIcon("Platforms/Windows/trayicon.ico");
        //            }
        //        }));

            lifecycle.AddWindows(windows => windows.OnWindowCreated((del) => {
                del.ExtendsContentIntoTitleBar = true;
            }));
#endif
            });

            var services = builder.Services;
#if WINDOWS
        services.AddSingleton<ITrayService, WinUI.TrayService>();
        services.AddSingleton<INotificationService, WinUI.NotificationService>();
#elif MACCATALYST
        services.AddSingleton<ITrayService, MacCatalyst.TrayService>();
        services.AddSingleton<INotificationService, MacCatalyst.NotificationService>();
#endif

            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<ChatsPage>();
            builder.Services.AddTransient<NotificationsPage>();
            builder.Services.AddTransient<WelcomePage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<ResetPassPage>();
            builder.Services.AddTransient<MessagesPage>();
            builder.Services.AddTransient<MainShoppingPage>();
            builder.Services.AddTransient<SafeSpaceMainPage>();
            builder.Services.AddTransient<MainJobsPage>();
            builder.Services.AddTransient<MainPlasticsPage>();
            builder.Services.AddTransient<MainEventsPage>();
            builder.Services.AddTransient<MainQuizzesPage>();
            builder.Services.AddTransient<MainReferralPage>();
            builder.Services.AddTransient<ChangePasswordPage>();
            builder.Services.AddTransient<MyAccountPage>();
            builder.Services.AddTransient<ProductDetailsPage>();
            builder.Services.AddTransient<ShoppingCategoriesPage>();
            builder.Services.AddTransient<ShoppingSubCategoriesPage>();
            builder.Services.AddTransient<SubCategoryItemsCustomPage>();
            builder.Services.AddTransient<SubCategoryItemsDefaultPage>();
            builder.Services.AddTransient<ChildSubCategoriesPage>();
            builder.Services.AddTransient<ShopingSubCategoryBrandsProviderPage>();
            builder.Services.AddTransient<ShoppingBrandModelsPage>();
            builder.Services.AddTransient<ShoppingWishListPage>();
            builder.Services.AddTransient<ShoppingOrdersPage>();
            builder.Services.AddTransient<ShoppingOrderProductsPage>();
            builder.Services.AddTransient<PostDetailsPage>();
            builder.Services.AddTransient<ComposePostPage>();
            builder.Services.AddTransient<JobCategoryJobsPage>();
            builder.Services.AddTransient<JobDetailsPage>();
            builder.Services.AddTransient<JobApplicationFormPage>();
            builder.Services.AddTransient<EventCategoryEventsPage>();
            builder.Services.AddTransient<EventDetailsPage>();
            builder.Services.AddTransient<QuizLeaderBoardPage>();
            builder.Services.AddTransient<QuizCategoryQuizzesPage>();
            builder.Services.AddTransient<QuizQuestionsPage>();
            builder.Services.AddTransient<JobAppCustomQnsPage>();
            builder.Services.AddTransient<SearchSymptomsPage>();
            builder.Services.AddTransient<ReferralDetailsHealthWorkerPage>();
            builder.Services.AddTransient<ReferralDetailsPeerEducatorPage>();
            builder.Services.AddTransient<ReferralDetailsPatientPage>();
            builder.Services.AddTransient<SymptomDetailsPage>();
            builder.Services.AddTransient<CreateReferralProfilePage>();
            builder.Services.AddTransient<LinkReferralProfilePage>();
            builder.Services.AddTransient<ReferralAccountCategoryPickerPage>();
            builder.Services.AddTransient<GenderPickerPage>();
            builder.Services.AddTransient<CitizenshipPickerPage>();
            builder.Services.AddTransient<ReferralDistrictProviderPage>();
            builder.Services.AddTransient<HealthCenterProviderPage>();
            builder.Services.AddTransient<HealthCenterStaffMemberProviderPage>();
            builder.Services.AddTransient<ScheduleApointmentPage>();
            builder.Services.AddTransient<ReferralServicesProviderPage>();
            builder.Services.AddTransient<ReferralAccountProviderPage>();
            builder.Services.AddTransient<CreateNewReferralPage>();
            builder.Services.AddTransient<ScanReferralCardPage>();
            builder.Services.AddTransient<CreateNewReferralAccountPage>();
            builder.Services.AddTransient<ReferralAccountQrCodePage>();
            builder.Services.AddTransient<ShoppingProductDiscussionRepliesPage>();
            builder.Services.AddTransient<EventDiscussionRepliesPage>();
            builder.Services.AddTransient<MyEventOrdersPage>();
            builder.Services.AddTransient<AddToCartPage>();
            builder.Services.AddTransient<ProductSizeProviderPage>();
            builder.Services.AddTransient<ProductColorProviderPage>();
            builder.Services.AddTransient<ShoppingCartPage>();
            builder.Services.AddTransient<SearchProductsPage>();
            builder.Services.AddTransient<CheckOutPage>();
            builder.Services.AddTransient<DeliveryInfoProviderPage>();
            builder.Services.AddTransient<EventApplicationPage>();
            builder.Services.AddTransient<EventTicketsProviderPage>();
            builder.Services.AddTransient<CartEventsPage>();
            builder.Services.AddTransient<CreateEventPage>();
            builder.Services.AddTransient<EventCategoryProviderPage>();
            builder.Services.AddTransient<MyActiveListedEventsPage>();
            builder.Services.AddTransient<MySavedEventsPage>();
            builder.Services.AddTransient<EventConfigPage>();
            builder.Services.AddTransient<EventMgmtPage>();
            builder.Services.AddTransient<EventPosPage>();
            builder.Services.AddTransient<EventOrderDetailsPage>();
            builder.Services.AddTransient<EventOrderTicetsPage>();
            builder.Services.AddTransient<TicketDetailsPage>();
            builder.Services.AddTransient<ContactsProviderPage>();
            builder.Services.AddTransient<SearchUsersPage>();
            builder.Services.AddTransient<UserProfilePage>();
            builder.Services.AddTransient<NameEditorPage>();
            builder.Services.AddTransient<PhoneEditorPage>();
            builder.Services.AddTransient<LanguageProviderPage>();
            builder.Services.AddTransient<LocationPickerPage>();
            builder.Services.AddTransient<ShoppingProductDiscussionPage>();
            builder.Services.AddTransient<JobDiscussionRepliesPage>();
            builder.Services.AddTransient<JobDiscussionPage>();
            builder.Services.AddTransient<EventDiscussionPage>();
            builder.Services.AddTransient<DeleteMyAccountPage>();
            builder.Services.AddTransient<CreateJobPage>();
            builder.Services.AddTransient<MyActiveListedJobsPage>();
            builder.Services.AddTransient<MyJobApplicationsPage>();
            builder.Services.AddTransient<MySavedJobsPage>();
            builder.Services.AddTransient<JobCategoryPickerPage>();
            builder.Services.AddTransient<PickJobTypePage>();
            builder.Services.AddTransient<AddCustomQuestionPage>();
            builder.Services.AddTransient<QuestionSuggestionsPage>();
            builder.Services.AddTransient<JobApplicantsPage>();
            builder.Services.AddTransient<JobApplicationDetailsPage>();
            builder.Services.AddTransient<ServiceCenterInfoPage>();
            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<CountryCodePickerPage>();

            builder.Services.AddTransient<IMainApplicationServices, MainApplicationServices>();
            builder.Services.AddTransient<IAddCustomQuestionViewModel, AddCustomQuestionViewModel>();
            builder.Services.AddTransient<IAddToCartViewModel, AddToCartViewModel>();
            builder.Services.AddTransient<ICartEventsViewModel, CartEventsViewModel>();
            builder.Services.AddTransient<IChatViewModel, ChatViewModel>();
            builder.Services.AddTransient<ICheckOutViewModel, CheckOutViewModel>();
            builder.Services.AddTransient<ICitizenshipPickeViewModel, CitizenshipPickeViewModel>();
            builder.Services.AddTransient<IComposePostViewModel, ComposePostViewModel>();
            builder.Services.AddTransient<IContactsProviderViewModel, ContactsProviderViewModel>();
            builder.Services.AddTransient<ICreateEventViewModel, CreateEventViewModel>();
            builder.Services.AddTransient<ICreateJobViewModel, CreateJobViewModel>();
            builder.Services.AddTransient<ICreateNewReferralAccountViewModel, CreateNewReferralAccountViewModel>();
            builder.Services.AddTransient<ICreateNewReferralViewModel, CreateNewReferralViewModel>();
            builder.Services.AddTransient<ICreateReferralProfileViewModel, CreateReferralProfileViewModel>();
            builder.Services.AddTransient<IDeliveryInfoProviderViewModel, DeliveryInfoProviderViewModel>();
            builder.Services.AddTransient<IEventApplicationViewModel, EventApplicationViewModel>();
            builder.Services.AddTransient<IEventCategoryEventsViewModel, EventCategoryEventsViewModel>();
            builder.Services.AddTransient<IEventCategoryProviderViewModel, EventCategoryProviderViewModel>();
            builder.Services.AddTransient<IEventConfigViewModel, EventConfigViewModel>();
            builder.Services.AddTransient<IEventDetailsViewModel, EventDetailsViewModel>();
            builder.Services.AddTransient<IEventDiscussionRepliesViewModel, EventDiscussionRepliesViewModel>();
            builder.Services.AddTransient<IEventDiscussionViewModel, EventDiscussionViewModel>();
            builder.Services.AddTransient<IEventMgmtViewModel, EventMgmtViewModel>();
            builder.Services.AddTransient<IEventOrderDetailsViewModel, EventOrderDetailsViewModel>();
            builder.Services.AddTransient<IEventOrderTicetsViewModel, EventOrderTicetsViewModel>();
            builder.Services.AddTransient<IEventPosViewModel, EventPosViewModel>();
            builder.Services.AddTransient<IEventTicketsProviderViewModel, EventTicketsProviderViewModel>();
            builder.Services.AddTransient<IGenderPickerViewModel, GenderPickerViewModel>();
            builder.Services.AddTransient<IHealthCenterProviderViewModel, HealthCenterProviderViewModel>();
            builder.Services.AddTransient<IHealthCenterStaffMemberProviderViewModel, HealthCenterStaffMemberProviderViewModel>();
            builder.Services.AddTransient<IHomeViewModel, HomeViewModel>();
            builder.Services.AddTransient<IJobAppCustomQnsViewModel, JobAppCustomQnsViewModel>();
            builder.Services.AddTransient<IJobApplicantsViewModel, JobApplicantsViewModel>();
            builder.Services.AddTransient<IJobApplicationDetailsViewModel, JobApplicationDetailsViewModel>();
            builder.Services.AddTransient<IJobApplicationFormViewModel, JobApplicationFormViewModel>();
            builder.Services.AddTransient<IJobCategoryJobsViewModel, JobCategoryJobsViewModel>();
            builder.Services.AddTransient<IJobCategoryPickerViewModel, JobCategoryPickerViewModel>();
            builder.Services.AddTransient<IJobDetailsViewModel, JobDetailsViewModel>();
            builder.Services.AddTransient<IJobDiscussionRepliesViewModel, JobDiscussionRepliesViewModel>();
            builder.Services.AddTransient<IJobDiscussionViewModel, JobDiscussionViewModel>();
            builder.Services.AddTransient<ILanguageProviderViewModel, LanguageProviderViewModel>();
            builder.Services.AddTransient<ILinkReferralProfileViewModel, LinkReferralProfileViewModel>();
            builder.Services.AddTransient<IMainEventsViewModel, MainEventsViewModel>();
            builder.Services.AddTransient<IMainJobsViewModel, MainJobsViewModel>();
            builder.Services.AddTransient<IMainPlasticsViewModel, MainPlasticsViewModel>();
            builder.Services.AddTransient<IMainQuizzesViewModel, MainQuizzesViewModel>();
            builder.Services.AddTransient<IMainReferralViewModel, MainReferralViewModel>();
            builder.Services.AddTransient<IMainShoppingViewModel, MainShoppingViewModel>();
            builder.Services.AddTransient<IMessageViewModel, MessageViewModel>();
            builder.Services.AddTransient<IMyAccountViewModel, MyAccountViewModel>();
            builder.Services.AddTransient<IMyActiveListedEventsViewModel, MyActiveListedEventsViewModel>();
            builder.Services.AddTransient<IMyActiveListedJobsViewModel, MyActiveListedJobsViewModel>();
            builder.Services.AddTransient<IMyEventOrdersViewModel, MyEventOrdersViewModel>();
            builder.Services.AddTransient<IMyJobApplicationsViewModel, MyJobApplicationsViewModel>();
            builder.Services.AddTransient<IMySavedEventsViewModel, MySavedEventsViewModel>();
            builder.Services.AddTransient<IMySavedJobsViewModel, MySavedJobsViewModel>();
            builder.Services.AddTransient<INotificationsViewModel, NotificationsViewModel>();
            builder.Services.AddTransient<IPickJobTypeViewModel, PickJobTypeViewModel>();
            builder.Services.AddTransient<IPostDetailsViewModel, PostDetailsViewModel>();
            builder.Services.AddTransient<IProductColorProviderViewModel, ProductColorProviderViewModel>();
            builder.Services.AddTransient<IProductDetailsViewModel, ProductDetailsViewModel>();
            builder.Services.AddTransient<IProductSizeProviderViewModel, ProductSizeProviderViewModel>();
            builder.Services.AddTransient<IQuestionSuggestionsViewModel, QuestionSuggestionsViewModel>();
            builder.Services.AddTransient<IQuizCategoryQuizzesViewModel, QuizCategoryQuizzesViewModel>();
            builder.Services.AddTransient<IQuizLeaderBoardViewModel, QuizLeaderBoardViewModel>();
            builder.Services.AddTransient<IQuizQuestionsViewModel, QuizQuestionsViewModel>();
            builder.Services.AddTransient<IReferralAccountCategoryPickerViewModel, ReferralAccountCategoryPickerViewModel>();
            builder.Services.AddTransient<IReferralAccountProviderViewModel, ReferralAccountProviderViewModel>();
            builder.Services.AddTransient<IReferralAccountQrCodeViewModel, ReferralAccountQrCodeViewModel>();
            builder.Services.AddTransient<IReferralDetailsViewModel, ReferralDetailsViewModel>();
            builder.Services.AddTransient<IReferralDistrictProviderViewModel, ReferralDistrictProviderViewModel>();
            builder.Services.AddTransient<IReferralServicesProviderViewModel, ReferralServicesProviderViewModel>();
            builder.Services.AddTransient<ISafeSpaceMainViewModel, SafeSpaceMainViewModel>();
            builder.Services.AddTransient<IScanReferralCardViewModel, ScanReferralCardViewModel>();
            builder.Services.AddTransient<IScheduleApointmentViewModel, ScheduleApointmentViewModel>();
            builder.Services.AddTransient<ISearchProductsViewModel, SearchProductsViewModel>();
            builder.Services.AddTransient<ISearchSymptomsViewModel, SearchSymptomsViewModel>();
            builder.Services.AddTransient<ISearchUsersViewModel, SearchUsersViewModel>();
            builder.Services.AddTransient<IServiceCenterInfoViewModel, ServiceCenterInfoViewModel>();
            builder.Services.AddTransient<ISettingsViewModel, SettingsViewModel>();
            builder.Services.AddTransient<IShopingSubCategoryBrandsProviderViewModel, ShopingSubCategoryBrandsProviderViewModel>();
            builder.Services.AddTransient<IShoppingBrandModelsViewModel, ShoppingBrandModelsViewModel>();
            builder.Services.AddTransient<IShoppingCartViewModel, ShoppingCartViewModel>();
            builder.Services.AddTransient<IShoppingOrderProductsViewModel, ShoppingOrderProductsViewModel>();
            builder.Services.AddTransient<IShoppingOrdersViewModel, ShoppingOrdersViewModel>();
            builder.Services.AddTransient<IShoppingProductDiscussionRepliesViewModel, ShoppingProductDiscussionRepliesViewModel>();
            builder.Services.AddTransient<IShoppingProductDiscussionViewModel, ShoppingProductDiscussionViewModel>();
            builder.Services.AddTransient<IShoppingSubCategoryViewModel, ShoppingSubCategoryViewModel>();
            builder.Services.AddTransient<IShoppingWishListViewModel, ShoppingWishListViewModel>();
            builder.Services.AddTransient<ISubCategoryItemsCustomViewModel, SubCategoryItemsCustomViewModel>();
            builder.Services.AddTransient<ISubCategoryItemsDefaultViewModel, SubCategoryItemsDefaultViewModel>();
            builder.Services.AddTransient<ISymptomDetailsViewModel, SymptomDetailsViewModel>();
            builder.Services.AddTransient<ITicketDetailsViewModel, TicketDetailsViewModel>();
            builder.Services.AddTransient<IUserProfileViewModel, UserProfileViewModel>();
            builder.Services.AddTransient<ICountryCodePickerViewModel, CountryCodePickerViewModel>();
            builder.Services.AddTransient<IPhoneEditorViewModel, PhoneEditorViewModel>();
            builder.Services.AddTransient<IShoppingCategoriesViewModel, ShoppingCategoriesViewModel>();

            return builder.Build();
        }
    }
}
