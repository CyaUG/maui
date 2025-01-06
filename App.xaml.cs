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
using Youth.Views.Tools;

namespace Youth
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DefineRautes();

            if (DeviceInfo.Idiom == DeviceIdiom.Phone)
                Shell.Current.CurrentItem = PhoneTabs;
        }

        public static void DefineRautes()
        {
            Routing.RegisterRoute(nameof(WelcomePage), typeof(WelcomePage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(ResetPassPage), typeof(ResetPassPage));

            Routing.RegisterRoute(nameof(MessagesPage), typeof(MessagesPage));
            Routing.RegisterRoute(nameof(MainShoppingPage), typeof(MainShoppingPage));
            Routing.RegisterRoute(nameof(SafeSpaceMainPage), typeof(SafeSpaceMainPage));
            Routing.RegisterRoute(nameof(MainJobsPage), typeof(MainJobsPage));
            Routing.RegisterRoute(nameof(MainPlasticsPage), typeof(MainPlasticsPage));
            Routing.RegisterRoute(nameof(MainEventsPage), typeof(MainEventsPage));
            Routing.RegisterRoute(nameof(MainQuizzesPage), typeof(MainQuizzesPage));
            Routing.RegisterRoute(nameof(MainReferralPage), typeof(MainReferralPage));
            Routing.RegisterRoute(nameof(ChangePasswordPage), typeof(ChangePasswordPage));
            Routing.RegisterRoute(nameof(MyAccountPage), typeof(MyAccountPage));
            Routing.RegisterRoute(nameof(ProductDetailsPage), typeof(ProductDetailsPage));
            Routing.RegisterRoute(nameof(ShoppingCategoriesPage), typeof(ShoppingCategoriesPage));
            Routing.RegisterRoute(nameof(ShoppingSubCategoriesPage), typeof(ShoppingSubCategoriesPage));
            Routing.RegisterRoute(nameof(SubCategoryItemsCustomPage), typeof(SubCategoryItemsCustomPage));
            Routing.RegisterRoute(nameof(SubCategoryItemsDefaultPage), typeof(SubCategoryItemsDefaultPage));
            Routing.RegisterRoute(nameof(ChildSubCategoriesPage), typeof(ChildSubCategoriesPage));
            Routing.RegisterRoute(nameof(ShopingSubCategoryBrandsProviderPage), typeof(ShopingSubCategoryBrandsProviderPage));
            Routing.RegisterRoute(nameof(ShoppingBrandModelsPage), typeof(ShoppingBrandModelsPage));
            Routing.RegisterRoute(nameof(ShoppingWishListPage), typeof(ShoppingWishListPage));
            Routing.RegisterRoute(nameof(ShoppingOrdersPage), typeof(ShoppingOrdersPage));
            Routing.RegisterRoute(nameof(ShoppingOrderProductsPage), typeof(ShoppingOrderProductsPage));
            Routing.RegisterRoute(nameof(PostDetailsPage), typeof(PostDetailsPage));
            Routing.RegisterRoute(nameof(ComposePostPage), typeof(ComposePostPage));
            Routing.RegisterRoute(nameof(JobCategoryJobsPage), typeof(JobCategoryJobsPage));
            Routing.RegisterRoute(nameof(JobDetailsPage), typeof(JobDetailsPage));
            Routing.RegisterRoute(nameof(JobApplicationFormPage), typeof(JobApplicationFormPage));
            Routing.RegisterRoute(nameof(EventCategoryEventsPage), typeof(EventCategoryEventsPage));
            Routing.RegisterRoute(nameof(EventDetailsPage), typeof(EventDetailsPage));
            Routing.RegisterRoute(nameof(QuizLeaderBoardPage), typeof(QuizLeaderBoardPage));
            Routing.RegisterRoute(nameof(QuizCategoryQuizzesPage), typeof(QuizCategoryQuizzesPage));
            Routing.RegisterRoute(nameof(QuizQuestionsPage), typeof(QuizQuestionsPage));
            Routing.RegisterRoute(nameof(JobAppCustomQnsPage), typeof(JobAppCustomQnsPage));
            Routing.RegisterRoute(nameof(SearchSymptomsPage), typeof(SearchSymptomsPage));
            Routing.RegisterRoute(nameof(ReferralDetailsHealthWorkerPage), typeof(ReferralDetailsHealthWorkerPage));
            Routing.RegisterRoute(nameof(ReferralDetailsPeerEducatorPage), typeof(ReferralDetailsPeerEducatorPage));
            Routing.RegisterRoute(nameof(ReferralDetailsPatientPage), typeof(ReferralDetailsPatientPage));
            Routing.RegisterRoute(nameof(SymptomDetailsPage), typeof(SymptomDetailsPage));
            Routing.RegisterRoute(nameof(CreateReferralProfilePage), typeof(CreateReferralProfilePage));
            Routing.RegisterRoute(nameof(LinkReferralProfilePage), typeof(LinkReferralProfilePage));
            Routing.RegisterRoute(nameof(ReferralAccountCategoryPickerPage), typeof(ReferralAccountCategoryPickerPage));
            Routing.RegisterRoute(nameof(GenderPickerPage), typeof(GenderPickerPage));
            Routing.RegisterRoute(nameof(CitizenshipPickerPage), typeof(CitizenshipPickerPage));
            Routing.RegisterRoute(nameof(ReferralDistrictProviderPage), typeof(ReferralDistrictProviderPage));
            Routing.RegisterRoute(nameof(HealthCenterProviderPage), typeof(HealthCenterProviderPage));
            Routing.RegisterRoute(nameof(HealthCenterStaffMemberProviderPage), typeof(HealthCenterStaffMemberProviderPage));
            Routing.RegisterRoute(nameof(ScheduleApointmentPage), typeof(ScheduleApointmentPage));
            Routing.RegisterRoute(nameof(ReferralServicesProviderPage), typeof(ReferralServicesProviderPage));
            Routing.RegisterRoute(nameof(ReferralAccountProviderPage), typeof(ReferralAccountProviderPage));
            Routing.RegisterRoute(nameof(CreateNewReferralPage), typeof(CreateNewReferralPage));
            Routing.RegisterRoute(nameof(ScanReferralCardPage), typeof(ScanReferralCardPage));
            Routing.RegisterRoute(nameof(CreateNewReferralAccountPage), typeof(CreateNewReferralAccountPage));
            Routing.RegisterRoute(nameof(ReferralAccountQrCodePage), typeof(ReferralAccountQrCodePage));
            Routing.RegisterRoute(nameof(ShoppingProductDiscussionRepliesPage), typeof(ShoppingProductDiscussionRepliesPage));
            Routing.RegisterRoute(nameof(EventDiscussionRepliesPage), typeof(EventDiscussionRepliesPage));
            Routing.RegisterRoute(nameof(MyEventOrdersPage), typeof(MyEventOrdersPage));
            Routing.RegisterRoute(nameof(AddToCartPage), typeof(AddToCartPage));
            Routing.RegisterRoute(nameof(ProductSizeProviderPage), typeof(ProductSizeProviderPage));
            Routing.RegisterRoute(nameof(ProductColorProviderPage), typeof(ProductColorProviderPage));
            Routing.RegisterRoute(nameof(ShoppingCartPage), typeof(ShoppingCartPage));
            Routing.RegisterRoute(nameof(SearchProductsPage), typeof(SearchProductsPage));
            Routing.RegisterRoute(nameof(CheckOutPage), typeof(CheckOutPage));
            Routing.RegisterRoute(nameof(DeliveryInfoProviderPage), typeof(DeliveryInfoProviderPage));
            Routing.RegisterRoute(nameof(EventApplicationPage), typeof(EventApplicationPage));
            Routing.RegisterRoute(nameof(EventTicketsProviderPage), typeof(EventTicketsProviderPage));
            Routing.RegisterRoute(nameof(CartEventsPage), typeof(CartEventsPage));
            Routing.RegisterRoute(nameof(CreateEventPage), typeof(CreateEventPage));
            Routing.RegisterRoute(nameof(EventCategoryProviderPage), typeof(EventCategoryProviderPage));
            Routing.RegisterRoute(nameof(MyActiveListedEventsPage), typeof(MyActiveListedEventsPage));
            Routing.RegisterRoute(nameof(MySavedEventsPage), typeof(MySavedEventsPage));
            Routing.RegisterRoute(nameof(EventConfigPage), typeof(EventConfigPage));
            Routing.RegisterRoute(nameof(EventMgmtPage), typeof(EventMgmtPage));
            Routing.RegisterRoute(nameof(EventPosPage), typeof(EventPosPage));
            Routing.RegisterRoute(nameof(EventOrderDetailsPage), typeof(EventOrderDetailsPage));
            Routing.RegisterRoute(nameof(EventOrderTicetsPage), typeof(EventOrderTicetsPage));
            Routing.RegisterRoute(nameof(TicketDetailsPage), typeof(TicketDetailsPage));
            Routing.RegisterRoute(nameof(ContactsProviderPage), typeof(ContactsProviderPage));
            Routing.RegisterRoute(nameof(SearchUsersPage), typeof(SearchUsersPage));
            Routing.RegisterRoute(nameof(UserProfilePage), typeof(UserProfilePage));
            Routing.RegisterRoute(nameof(NameEditorPage), typeof(NameEditorPage));
            Routing.RegisterRoute(nameof(PhoneEditorPage), typeof(PhoneEditorPage));
            Routing.RegisterRoute(nameof(LanguageProviderPage), typeof(LanguageProviderPage));
            Routing.RegisterRoute(nameof(LocationPickerPage), typeof(LocationPickerPage));
            Routing.RegisterRoute(nameof(ShoppingProductDiscussionPage), typeof(ShoppingProductDiscussionPage));
            Routing.RegisterRoute(nameof(JobDiscussionRepliesPage), typeof(JobDiscussionRepliesPage));
            Routing.RegisterRoute(nameof(JobDiscussionPage), typeof(JobDiscussionPage));
            Routing.RegisterRoute(nameof(EventDiscussionPage), typeof(EventDiscussionPage));
            Routing.RegisterRoute(nameof(DeleteMyAccountPage), typeof(DeleteMyAccountPage));
            Routing.RegisterRoute(nameof(CreateJobPage), typeof(CreateJobPage));
            Routing.RegisterRoute(nameof(MyActiveListedJobsPage), typeof(MyActiveListedJobsPage));
            Routing.RegisterRoute(nameof(MyJobApplicationsPage), typeof(MyJobApplicationsPage));
            Routing.RegisterRoute(nameof(MySavedJobsPage), typeof(MySavedJobsPage));
            Routing.RegisterRoute(nameof(JobCategoryPickerPage), typeof(JobCategoryPickerPage));
            Routing.RegisterRoute(nameof(PickJobTypePage), typeof(PickJobTypePage));
            Routing.RegisterRoute(nameof(AddCustomQuestionPage), typeof(AddCustomQuestionPage));
            Routing.RegisterRoute(nameof(QuestionSuggestionsPage), typeof(QuestionSuggestionsPage));
            Routing.RegisterRoute(nameof(JobApplicantsPage), typeof(JobApplicantsPage));
            Routing.RegisterRoute(nameof(JobApplicationDetailsPage), typeof(JobApplicationDetailsPage));
            Routing.RegisterRoute(nameof(ServiceCenterInfoPage), typeof(ServiceCenterInfoPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(CountryCodePickerPage), typeof(CountryCodePickerPage));
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(SettingsPage)}", true);
        }
    }
}
