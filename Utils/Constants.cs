using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System.Reflection;
using System.Drawing;

namespace Youth.Utils
{
    internal class Constants
    {
        public const string AUTH_TOCKEN_REF = "auth_token";
        public const string AUTH_BOOLEAN_REF = "auth_boolean";

        public static String appName = "CYA App";
        public static String appDomain = "https://cya.wagaana.com/";
        public static String apiUrl = appDomain + "api/";

        public static String API_LOGIN = "login";
        public static String API_LOGOUT = "logout";
        public static String API_REGISTER = "register";
        public static String API_FORGOT_PASSWORD = "forgot_password";
        public static String API_CHANGE_PASSWORD = "change_password";
        public static String API_DELETE_USER_ACCOUNT = "deleteUserAccount";
        public static String API_PAYMENT_METHODS = "loadPaymentMethods";
        public static String API_SEND_ACTIVE_PAYMENT_METHODS = "loadSendActivePaymentMethods";
        public static String API_PAYMENT_METHOD_CURRENCIES = "loadPaymentMethodCurencies";
        public static String API_PAYMENT_CREATE_TOP_UP_REQUEST = "createTopupRequest";
        public static String API_PAYMENT_CASH_OUT = "cashOut";
        public static String API_SEND_MONEY = "sendMoney";
        public static String API_LOAD_STABLE_CURRENCY = "loadStebleCurency";
        public static String API_PAYMENT_FETCH_SUPPORTED_COUNTRIES = "fetchSupportedCountries";
        public static String API_SYNCHRONIZE_CONTACT = "synchronizeContact";
        public static String API_GET_CONTACT_USERS = "getContactsUsers";
        public static String API_GET_MY_PROFILE = "getUserProfile";
        public static String API_SEARCH_USERS = "searchUsers/";
        public static String API_GET_SELECTED_USER_PROFILE = "getSelectedUserProfile/";
        public static String API_CREATE_NEW_FRIEND = "createNewFriend";
        public static String API_CHECK_FRIENDSHIP_STATUS = "checkFriendshipStatus/";
        public static String API_FETCH_FRIENDS = "fetchFriends";
        public static String API_GET_CHAT_DETAILS = "getChatDetails/";
        public static String API_SEND_STRING_MESSAGE = "sendStringMessage";
        public static String API_SEND_CONTACT = "sendContact";
        public static String API_SEND_LOCATION = "sendLocation";
        public static String API_SEND_FILE = "sendFile";
        public static String API_GET_CHAT_CONVERSATION = "fetchChatConversation/";
        public static String API_UPDATE_USER_PROFILE_VALUE = "updateUserProfileValue";
        public static String API_GET_CHATS = "getChats";
        public static String API_GET_CONVERSATION_MEDIA = "fetchFriendChatConversationMedia/";
        public static String API_GET_CHATS_MESSAGES = "getNewChatsMessages";
        public static String API_GET_CHATS_CONTACT = "fetchMessageContacts/";
        public static String API_UPLOAD_COVER_PHOTO = "uploadUserCoverPhoto";
        public static String API_UPLOAD_PROFILE_PHOTO = "saveUserAvator";
        public static String API_UPDATE_USER_ADDRESS = "updateUserAddress";
        public static String API_UPDATE_USER_LOCATION = "updateUserLocation";
        public static String API_GET_CUSTOMER_CARE_GROUP_ID = "getCustomerCareChatGroupId";
        public static String API_GET_LOCALIZATIONS = "loadLocalizations";
        public static String API_GET_NEW_NOTIFICATIONS = "fetchUnNewNotifications";
        public static String API_GET_USER_NOTIFICATIONS = "fetchUserNotifications";
        public static String API_SUBMIT_REPORT = "submitReport";
        public static String API_LOAD_DASHBOARD_SUMMARY = "loadUserDashboardSummary";

        //sopping
        public static String API_GET_SHOPPING_CATEGORIES = "getShoppingCategories";
        public static String API_GET_SHOPPING_CATEGORY = "getShoppingCategory/";
        public static String API_GET_SHOPPING_SUB_CATEGORIES = "getShopingSubCategories/";
        public static String API_GET_SHOPPING_CHILD_SUB_CATEGORIES = "getShopingChildSubCategories/";
        public static String API_GET_SHOPPING_SUB_CATEGORY = "getShopingSubCategory/";
        public static String API_GET_SHOPPING_QUERY_PARAMETER = "getShoppingQueryParameter/";
        public static String API_GET_SHOPPING_BRANDS = "getShopingBrands/";
        public static String API_GET_SHOPPING_SUB_CATEGORY_BRANDS = "getShopingSubCategoryBrands/";
        public static String API_GET_SHOPPING_BRAND = "getShopingBrand/";
        public static String API_GET_SHOPPING_PRODUCTS = "getShopingProducts";
        public static String API_GET_SHOPPING_POPULAR_PRODUCTS = "getShoppingPopularProducts";
        public static String API_GET_SHOPPING_FEATURED_PRODUCTS = "getShoppingFeaturedProducts";
        public static String API_SEARCH_SHOPPING_PRODUCTS = "searchShoppingProducts/";
        public static String API_GET_SUBCATEGORY_SHOPPING_PRODUCTS = "getSubCategoryShopingProducts/";
        public static String API_FILTER_SUBCATEGORY_SHOPPING_PRODUCTS = "filterSubCategoryShoppingProducts";
        public static String API_GET_BRAND_SHOPPING_PRODUCTS = "getBrandShopingProducts/";
        public static String API_GET_SHOPPING_BRAND_MODELS = "getShopingBrandModels/";
        public static String API_GET_SHOPPING_PRODUCT = "getShopingProduct/";
        public static String API_GET_SHOPPING_PRODUCT_SPECIFICATIONS = "getShopingProductSpecifications/";
        public static String API_GET_SHOPPING_LAST_PRODUCT_DISCUSSION = "getShoppingLastProductDiscussion/";
        public static String API_GET_SHOPPING_PRODUCT_DISCUSSIONS = "getShoppingProductDiscussions/";
        public static String API_GET_SHOPPING_PRODUCT_DISCUSSION_DETAILS = "fetchJobDiscussionDetails/";
        public static String API_GET_SHOPPING_PRODUCT_DISCUSSION_REPLY_DETAILS = "fetchShoppingProductDiscussionReplyDetails/";
        public static String API_GET_SHOPPING_PRODUCT_DISCUSSION_REPLIES = "getShoppingProductDiscussionReplies/";
        public static String API_GET_SHOPPING_PRODUCT_COLOR_OPTIONS = "getShopingProductColorOptions/";
        public static String API_GET_SHOPPING_PRODUCT_SIZE_OPTIONS = "getShopingProductSizeOptions/";
        public static String API_GET_SHOPPING_PRODUCT_GALLERY = "getShopingProductGallery/";
        public static String API_GET_SHOPPING_WISH_LIST = "getShopingWishList";
        public static String API_GET_SHOPPING_CART = "getShopingCart";

        public static String API_GET_SAFE_POSTS = "fetchSafePosts";
        public static String API_GET_SAFE_IMAGE_POSTS = "fetchSafeImagePosts/";
        public static String API_GET_SAFE_VIDEO_POSTS = "fetchSafeVideoPosts/";
        public static String API_SUBMIT_SAFE_POST_LIKE = "submitSafePostLike";
        public static String API_GET_SELECTED_SAFE_POST = "getSelectedSafePost/";
        public static String API_GET_SAFE_POST_AUDIENCES = "fetchSafePostAudiences";
        public static String API_GET_SAFE_POST_DEFAULT_AUDIENCES = "fetchSafePostDefaultAudiences";
        public static String API_SUBMIT_SAFE_POST = "submitSafePost";
        public static String API_SUBMIT_SAFE_POST_COMMENT = "submitSafePostComment";
        public static String API_GET_SAFE_POST_COMMENTS = "fetchSafePostComments/";
        public static String API_SUBMIT_SHOPPING_PRODUCT_DISCUSSION = "submitShoppingProductDiscussion";
        public static String API_SUBMIT_SHOPPING_PRODUCT_DISCUSSION_REPLY = "submitShoppingProductDiscussionReply";
        public static String API_SUBMIT_SHOPPING_WISH_LIST = "submitShopingWishList";
        public static String API_SUBMIT_SHOPPING_CART = "submitShopingCart";
        public static String API_DELETE_CART_ITEM = "deleteCartItem/";
        public static String API_UPDATE_CART_ITEM_QTY = "updateCartItemQuantity/";
        public static String API_COMPUTE_DELIVERY_FEE = "computeDeliveryFee";
        public static String API_GET_FEATURED_SAFE_POSTS = "fetchFeaturedSafePosts";
        public static String API_CHECKOUT_ON_DELIVERY = "checkoutPayOnDelivery";
        public static String API_CHECKOUT_WITH_NSIIMBI = "checkoutPayWithNsiimbi";
        public static String API_FETCH_PRODUCTS_INVOICES = "fetchProductsInvoices";
        public static String API_FETCH_PRODUCTS_INVOICE = "fetchProductsInvoice/";
        public static String API_FETCH_INVOICE_PRODUCTS = "fetchInvoiceProducts/";
        public static String API_FETCH_INVOICE_PRODUCT_DETAILS = "fetchInvoiceProductDetails";

        public static String API_GET_SUGESTION_QUESTIONS = "fetchSugestionQuestions";
        public static String API_GET_DEFAULT_SUGESTION_QUESTIONS = "fetchDefaultSugestionQuestions";
        public static String API_SUBMIT_JOB = "submitJob";
        public static String API_SUBMIT_JOB_APPLICATION = "submitJobApplication";
        public static String API_GET_JOBS = "getJobs";
        public static String API_GET_RELATED_JOBS = "fetchRelatedJobs/";
        public static String API_GET_JOB_DETAILS = "getJobDetails/";
        public static String API_GET_JOB_APPLICATION_QUESTIONS = "fetchJobApplicationQuestions/";
        public static String API_GET_JOB_CATEGORIES = "fetchJobCategories";
        public static String API_GET_MY_JOB_CATEGORIES = "fetchMyJobCategories";
        public static String API_GET_USER_JOB_CATEGORIES = "fetchUserJobCategories";
        public static String API_GET_JOB_DISCUSSION = "fetchJobDiscussions/";
        public static String API_GET_LAST_JOB_DISCUSSION = "fetchLastJobDiscussion/";
        public static String API_GET_JOB_DISCUSSION_DETAILS = "fetchJobDiscussionDetails/";
        public static String API_GET_JOB_DISCUSSION_REPLIES = "fetchJobDiscussionReplies/";
        public static String API_SUBMIT_JOB_DISCUSSION = "submitJobDiscussion";
        public static String API_SUBMIT_JOB_DISCUSSION_REPLIES = "submitJobDiscussionReply";
        public static String API_GET_MY_LISTED_JOB = "fetchMyListedJobs";
        public static String API_GET_MY_JOB_APPLICATIONS = "getMyApplications";
        public static String API_GET_JOB_APPLICATIONS = "getJobApplications/";
        public static String API_GET_JOB_APPLICATION_DETAILS = "fetchJobApplicationDetails/";
        public static String API_GET_JOB_APPLICATION_QUESTION_ANSWERS = "fetchJobQuestionAnswers/";
        public static String API_SAVE_JOB = "saveJob";
        public static String API_GET_MY_SAVED_JOBS = "fetchMySavedJobs";
        public static String API_GET_JOB_QUERY_PARAMETER = "saveJobQueryParameter";
        public static String API_SUBMIT_JOB_LIKE = "submitJobLike";

        public static String API_GET_EVENT_CATEGORIES = "fetchEventCategories";
        public static String API_GET_QUERY_PARAMETER_EVENT_CATEGORIES = "fetchUserQueryParameterEventCategories";
        public static String API_GET_MY_EVENT_CATEGORIES = "fetchMyEventCategories";
        public static String API_GET_EVENTS = "fetchEvents";
        public static String API_GET_RELATED_EVENTS = "fetchRelatedEvents/";
        public static String API_GET_EVENTS_DETAILS = "fetchEventDetails/";
        public static String API_GET_EVENT_QUERY_PARAMETER = "saveEventUsersQueryParameter";
        public static String API_GET_EVENT_TICKETS = "fetchEventTickets/";
        public static String API_GET_EVENT_TICKET_TOKEN = "fetchEventTiketTockets/";
        public static String API_GET_EVENT_SCANNED_TICKET_TOKENS = "fetchEventScanedTiketTockets/";
        public static String API_GET_LAST_EVENT_DISCUSSION = "fetchLastEventDiscussion/";
        public static String API_GET_LAST_EVENT_DISCUSSION_REPLY = "fetchEventDiscussionReply/";
        public static String API_GET_EVENT_DISCUSSIONS = "fetchEventDiscussions/";
        public static String API_GET_EVENT_DISCUSSION_DETAILS = "fetchEventDiscussionDetails/";
        public static String API_GET_EVENT_DISCUSSION_REPLIES = "fetchEventDiscussionReplies/";
        public static String API_SUBMIT_EVENT_DISCUSSION = "submitEventDiscussion";
        public static String API_SUBMIT_EVENT_DISCUSSION_REPLY = "submitEventDiscussionReply";
        public static String API_SAVE_EVENT = "saveEvent";
        public static String API_SUBMIT_EVENT = "submitEvent";
        public static String API_SUBMIT_EVENT_TICKET = "submitEventTicket";
        public static String API_GET_MY_SAVED_EVENTS = "fetchMySavedEvents";
        public static String API_GET_MY_LISTED_EVENTS = "fetchMyListedEvents";
        public static String API_UPLOAD_EVENT_PHOTO = "uploadEventPhoto";
        public static String API_GET_EVENT_PHOTOS = "fetchEventPhotos/";
        public static String API_GET_MY_CART_EVENTS = "fetchMyCartEvents";
        public static String API_SAVE_EVENT_TO_CART = "saveToEventCart";
        public static String API_DELETE_TICKET_CART_ITEM = "deleteEventTicketCartQuantity/";
        public static String API_UPDATE_TICKET_CART_ITEM_QTY = "updateEventTicketCartQuantity";
        public static String API_CREATE_CART_INVOICE = "fetchCartInvoiceCheckOutUrl";
        public static String API_GET_MY_EVENTS_ORDERS = "fetchMyEventsOrders";
        public static String API_GET_MY_EVENTS_ORDER = "fetchMyEventsOrder/";
        public static String API_GET_EVENT_ATTENDEES = "fetchEventAttendees/";
        public static String API_GET_EVENT_MANAGEMENT = "fetchEventManagement/";
        public static String API_GET_EVENT_TICKET_INFO = "fetchEventTicketInfo";
        public static String API_USE_EVENT_TICKET = "useEventTicket";
        public static String API_SUBMIT_EVENT_MANAGER = "submitEventManager";
        public static String API_SUBMIT_EVENT_LIKE = "submitEventLike";

        public static String API_GET_QUIZ_CATEGORIES = "fetchQuizCategories";
        public static String API_GET_QUIZZES = "fetchQuizzes";
        public static String API_GET_CATEGORY_QUIZZES = "fetchCategoryQuizzes/";
        public static String API_GET_QUIZ_QUESTIONS = "fetchQuizQuestions/";
        public static String API_SUBMIT_QUIZ_RESULTS = "submitQuizResults";
        public static String API_GET_QUIZ_LEADER_BOARD = "getLeaderBoard";
        public static String API_GET_MY_QUIZ_PROFILE = "getMyQuizProfile";

        public static String BANK_TRANSFER = "BANK_TRANSFER";
        public static String MOBILE_MONEY = "MOBILE_MONEY";
        public static String PAYPAL = "PAYPAL";
        public static String CARD = "CARD";
        public static String API_GET_SYSTEM_SETTINGS = "getSystemSettings";

        public static String API_GET_NEAREST_SERVICE_POINTS = "fetchNearestServicePoints";
        public static String API_GET_NEAREST_SERVICE_POINT = "fetchRequestNearestServicePoint";
        public static String API_GET_ALL_SERVICE_POINT_DETAILS = "fetchServicePointDetails/";
        public static String API_GET_SERVICE_POINT_PLASTICS_PRICING = "fetchServicePointPlasticsPricing/";
        public static String API_GET_SERVICE_POINT_GALLERY = "getServicePointGallery/";

        public static String API_CREATE_MY_REFERRAL_ACCOUNT = "createMyReferralAccount";
        public static String API_CREATE_REFERRAL_ACCOUNT = "submitReferralAccount";
        public static String API_GET_REFERRAL_ACCOUNT_CATEGORIES = "fetchReferralAccountCategories";
        public static String API_GET_REFERRAL_ACCOUNT_CATEGORY = "fetchReferralAccountCategory/";
        public static String API_GET_REFERRAL_ACCOUNT_CITIZENSHIPS = "fetchReferralAccountCitizenships";
        public static String API_GET_GENDERS = "fetchGenders";
        public static String API_GET_HEALTH_CENTERS = "fetchAllHealthCenters";
        public static String API_GET_ALL_DISTRICTS = "fetchAllReferralDistricts";
        public static String API_GET_ALL_DISTRICT_SUB_COUNTIES = "fetchReferralDistrictSubCounties/";
        public static String API_GET_REFERRAL_ACCOUNT_DETAILS_BY_TOCKEN = "fetchReferralAccountByTocken/";
        public static String API_SUBMIT_REFERRAL_ACCOUNT_REQUEST = "submitReferralAccountRequest";
        public static String API_GET_MY_REFERRAL_ACCOUNT_DETAILS = "fetchMyReferralAccount";
        public static String API_GET_ALL_MY_REFERRALS = "fetchAllMyReferrals";
        public static String API_GET_REFERRAL_DETAILS = "fetchReferralDetails/";
        public static String API_GET_ALL_MY_ASSIGNEE_REFERRALS = "fetchAllMyAssigneeReferrals";
        public static String API_GET_ALL_MY_PEER_EDUCATION_REFERRALS = "fetchAllMyPeerEducationReferrals";
        public static String API_SEARCH_REFERRAL_ACCOUNTS = "searchReferralAccounts/";
        public static String API_GET_HEALTH_CENTER_STAFF_MEMBERS = "fetchAllHealthCenterStaffMembers/";
        public static String API_GET_REFERRAL_SERVICES = "fetchSingleReferralServices/";
        public static String API_GET_REFERRAL_OFFERED_SERVICES = "fetchReferralOfferedServices/";
        public static String API_SUBMIT_REFERRAL = "submitReferral";
        public static String API_SUBMIT_REFERRAL_OFFERED_SERVICE = "submitReferralOfferedService";
        public static String API_SUBMIT_SECONDARY_REFERRAL = "submitSecondaryReferral";
        public static String API_UPDATE_REFERRAL_HEALTH_CENTER = "updateReferralHealthCenter";
        public static String API_UPDATE_REFERRAL_APPOINTMENT = "setReferralAppointment";
        public static String API_GET_POPULAR_SYMPTOMS = "fetchPopularSymptoms";
        public static String API_GET_FEATURED_SYMPTOMS = "fetchFeaturedSymptoms";
        public static String API_GET_SEARCH_SYMPTOMS = "searchSymptoms/";
        public static String API_GET_SYMPTOM_DETAILS = "getSymptomDetails/";

        /*notification keys*/
        public const String notification_key_new_safe_post_like = "new_safe_post_like";
        public const String notification_key_new_safe_post_comment = "new_safe_post_comment";
        public const String notification_key_new_safe_post_comment_reply = "new_safe_post_comment_reply";
        public const String notification_key_new_job = "new_job";
        public const String notification_key_new_referral = "new_referral";
        public const String notification_key_new_referral_schedule = "new_referral_schedule";
        public const String notification_key_new_referral_assignment = "new_referral_assignment";
        public const String notification_key_event_reminder = "event_reminder";
        public const String notification_key_new_quiz_challenge = "new_quiz_challenge";
        public const String notification_key_event_ticket_claim = "event_ticket_claim";
        public const String notification_key_payment_received = "payment_received";
        public const String notification_key_new_shopping_order_placed = "new_shopping_order_placed";
        public const String notification_key_new_shopping_product_comment = "new_shopping_product_comment";
        public const String notification_key_new_shopping_product_comment_reply = "new_shopping_product_comment_reply";
        public const String notification_key_new_shopping_product_like = "new_shopping_product_like";
        public const String notification_key_new_event_tickets_order = "new_event_tickets_order";
        public const String notification_key_new_event_comment = "new_event_comment";
        public const String notification_key_new_event_comment_reply = "new_event_comment_reply";
        public const String notification_key_new_event_like = "new_event_like";
        public const String notification_key_new_job_comment = "new_job_comment";
        public const String notification_key_new_job_comment_reply = "new_job_comment_reply";
        public const String notification_key_new_job_like = "new_job_like";

        public static Task<string> GetAuthTocken()
        {
            // Return the Task<string>
            return SecureStorage.GetAsync(Constants.AUTH_TOCKEN_REF);
        }

        public static string GetYoutubeId(string messageText)
        {
            string videoId = ExtractYoutubeIdFromShortUrl(messageText);

            if (string.IsNullOrEmpty(videoId))
            {
                videoId = ExtractYoutubeIdFromLongUrl(messageText);
            }

            if (string.IsNullOrEmpty(videoId))
            {
                return "";
            }
            return videoId;
        }

        static string ExtractYoutubeIdFromShortUrl(string input)
        {
            string pattern = @"(http|https)://(www.)?youtu\.be/([a-zA-Z0-9\-_]+)";
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                // Get the last group, which is the video ID
                string videoId = match.Groups[3].Value;
                return videoId;
            }
            return null;
        }
        static string ExtractYoutubeIdFromLongUrl(string input)
        {
            // Use the Uri class to parse the input string
            string pattern = @"(http|https)://(www.)?youtube.com/watch\?v=([a-zA-Z0-9\-_]+)";
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                // Get the last group, which is the video ID
                string videoId = match.Groups[3].Value;
                return videoId;
            }
            return null;
        }

        public static Dictionary<string, object> ObjectToDictionary(object obj)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Type type = obj.GetType();
            foreach (FieldInfo field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                try
                {
                    dict.Add(field.Name, field.GetValue(obj));
                }
                catch (Exception e)
                {
                    // Handle exception
                }
            }
            return dict;
        }

        public static async void OpenUri(string url)
        {
            var uri = new Uri(url);

            await Browser.OpenAsync(uri, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
            });
        }
    }
}
