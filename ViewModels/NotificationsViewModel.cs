using Youth.Models;
using Youth.Utils;
using Youth.ViewModels.Interfaces;
using Youth.Views.Events;
using Youth.Views.ReferralProgram;
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
    public class NotificationsViewModel : BaseViewModel, INotificationsViewModel
    {
        private NotificationModule _selectedItem;
        public Command LoadNotificationsCommand { get; }
        public Command<NotificationModule> NotificationTapped { get; }
        public ObservableCollection<NotificationModule> NotificationModules { get; set; }
        public NotificationsViewModel()
        {
            Title = "Notifications";
            NotificationTapped = new Command<NotificationModule>(OnNotificationSelected);
            NotificationModules = new ObservableCollection<NotificationModule>();
            LoadNotificationsCommand = new Command(async () => await ExecuteLoadNotificationsCommand());
        }

        async Task ExecuteLoadNotificationsCommand()
        {
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

                JObject serverObj = await NotificationModule.FetchNotifications();
                NotificationModules.Clear();
                JArray notificationsArray = (JArray)serverObj.GetValue("data");

                foreach (JToken token in notificationsArray)
                {
                    JObject notificationObj = (JObject)token;
                    NotificationModule notificationModule = notificationObj.ToObject<NotificationModule>(serializer);

                    try
                    {
                        switch (notificationModule.notificationKey)
                        {
                            case Constants.notification_key_new_safe_post_like:
                            case Constants.notification_key_new_safe_post_comment:
                            case Constants.notification_key_new_safe_post_comment_reply:

                                SafePost safePost = new SafePost();
                                if (notificationModule.notificationKey == Constants.notification_key_new_safe_post_like)
                                {
                                    JObject safePostServerObj = await SafePost.getLikedSafePost(int.Parse(notificationModule.tocken));
                                    JObject safePostObj = (JObject)safePostServerObj.GetValue("data");
                                    safePost = safePostObj.ToObject<SafePost>(serializer);
                                }

                                if (notificationModule.notificationKey == Constants.notification_key_new_safe_post_comment || notificationModule.notificationKey == Constants.notification_key_new_safe_post_comment_reply)
                                {
                                    JObject safePostServerObj = await SafePost.getSelectedSafePost(int.Parse(notificationModule.tocken));
                                    JObject safePostObj = (JObject)safePostServerObj.GetValue("data");
                                    safePost = safePostObj.ToObject<SafePost>(serializer);
                                }

                                try
                                {
                                    switch (notificationModule.notificationKey)
                                    {
                                        case Constants.notification_key_new_safe_post_like:
                                            try
                                            {
                                                if (notificationModule.notificationKey == Constants.notification_key_new_safe_post_like)
                                                {
                                                    notificationModule.notificationDescription = $"\"{safePost.message}\"";
                                                    notificationModule.notificationTitle = $"Hello, {safePost.name} liked your comment.";
                                                    notificationModule.imageUrl = Constants.appDomain + safePost.profile_picture;
                                                    notificationModule.postId = safePost.id;
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Debug.WriteLine(e);
                                            }
                                            break;
                                        case Constants.notification_key_new_safe_post_comment:
                                            try
                                            {
                                                if (notificationModule.notificationKey == Constants.notification_key_new_safe_post_comment)
                                                {
                                                    String commentImageUrl = Constants.appDomain + safePost.profile_picture;
                                                    if (safePost.postContent == "video")
                                                    {
                                                        commentImageUrl = "https://img.youtube.com/vi/" + safePost.videoId + "/0.jpg";
                                                    }
                                                    else if (safePost.postContent == "image")
                                                    {
                                                        commentImageUrl = Constants.appDomain + safePost.fileUrl;
                                                    }

                                                    notificationModule.notificationDescription = $"\"{safePost.message}\"";
                                                    notificationModule.notificationTitle = $"Hello, {safePost.name} has commented on your post.";
                                                    notificationModule.imageUrl = commentImageUrl;
                                                    notificationModule.postId = safePost.id;
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Debug.WriteLine(e);
                                            }
                                            break;
                                        case Constants.notification_key_new_safe_post_comment_reply:
                                            try
                                            {
                                                if (notificationModule.notificationKey == Constants.notification_key_new_safe_post_comment_reply)
                                                {
                                                    String replyImageUrl = Constants.appDomain + safePost.profile_picture;
                                                    if (safePost.postContent == "video")
                                                    {
                                                        replyImageUrl = "https://img.youtube.com/vi/" + safePost.videoId + "/0.jpg";
                                                    }
                                                    else if (safePost.postContent == "image")
                                                    {
                                                        replyImageUrl = Constants.appDomain + safePost.fileUrl;
                                                    }

                                                    notificationModule.notificationDescription = $"\"{safePost.message}\"";
                                                    notificationModule.notificationTitle = $"Hello, {safePost.name} has replied to your comment.";
                                                    notificationModule.imageUrl = replyImageUrl;
                                                    notificationModule.postId = safePost.id;
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Debug.WriteLine(e);
                                            }
                                            break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Debug.WriteLine(e);
                                }
                                break;
                            case Constants.notification_key_new_job:
                                break;
                            case Constants.notification_key_new_referral:
                                break;
                            case Constants.notification_key_new_referral_schedule:
                                break;
                            case Constants.notification_key_new_referral_assignment:
                                break;
                            case Constants.notification_key_event_reminder:
                                break;
                            case Constants.notification_key_new_quiz_challenge:
                                break;
                            case Constants.notification_key_event_ticket_claim:
                                break;
                            case Constants.notification_key_payment_received:
                                break;
                            case Constants.notification_key_new_shopping_order_placed:

                                ObservableCollection<InvoiceProduct> invoiceProducts = new ObservableCollection<InvoiceProduct>();
                                ShoppingOrder shoppingOrder = new ShoppingOrder();
                                if (notificationModule.notificationKey == Constants.notification_key_new_shopping_order_placed)
                                {
                                    JObject shoppingOrderServerObj = await ShoppingOrder.fetchProductsInvoice(notificationModule.tocken);
                                    JObject shoppingOrderObj = (JObject)shoppingOrderServerObj.GetValue("data");
                                    shoppingOrder = shoppingOrderObj.ToObject<ShoppingOrder>(serializer);

                                    JObject invoiceProductServerObj = await InvoiceProduct.fetchInvoiceProducts(shoppingOrder.invoiceId);
                                    JArray invoiceProductsArray = (JArray)invoiceProductServerObj.GetValue("data");
                                    invoiceProducts.Clear();

                                    foreach (JToken shoppingOrderToken in invoiceProductsArray)
                                    {
                                        JObject invoiceProductObj = (JObject)shoppingOrderToken;
                                        InvoiceProduct mInvoiceProduct = invoiceProductObj.ToObject<InvoiceProduct>(serializer);
                                        invoiceProducts.Add(mInvoiceProduct);
                                    }
                                }

                                try
                                {
                                    if (notificationModule.notificationKey == Constants.notification_key_new_shopping_order_placed)
                                    {
                                        String productsLabel = "";
                                        if (invoiceProducts.Count == 1)
                                        {
                                            productsLabel += invoiceProducts[0].label;
                                        }
                                        else
                                        {
                                            for (int x = 0; x < invoiceProducts.Count; x++)
                                            {
                                                InvoiceProduct invoiceProduct = invoiceProducts[x];
                                                if (x == (invoiceProducts.Count - 1))
                                                {
                                                    productsLabel += invoiceProduct.label;
                                                }
                                                else
                                                {
                                                    productsLabel += (invoiceProduct.label + " . ");
                                                }
                                            }
                                        }

                                        notificationModule.notificationDescription = $"Hello, your order of {productsLabel} has been created on {notificationModule.created_at}, click here to check it out.";
                                        notificationModule.notificationTitle = "Your order has been placed.";
                                        notificationModule.imageUrl = Constants.appDomain + invoiceProducts[0].imageUrl;
                                        notificationModule.invoiceId = shoppingOrder.invoiceId;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Debug.WriteLine(e);
                                }
                                break;
                            case Constants.notification_key_new_shopping_product_comment:
                                break;
                            case Constants.notification_key_new_shopping_product_comment_reply:

                                ShoppingProductDiscussion shoppingProductDiscussion = new ShoppingProductDiscussion();
                                if (notificationModule.notificationKey == Constants.notification_key_new_shopping_product_comment_reply)
                                {
                                    JObject shoppingProductDiscussionServerObj = await ShoppingProductDiscussion.fetchShoppingProductDiscussionReplyDetails(notificationModule.tocken);
                                    JObject shoppingProductDiscussionObj = (JObject)shoppingProductDiscussionServerObj.GetValue("data");
                                    shoppingProductDiscussion = shoppingProductDiscussionObj.ToObject<ShoppingProductDiscussion>(serializer);
                                }
                                try
                                {
                                    try
                                    {
                                        if (notificationModule.notificationKey == Constants.notification_key_new_shopping_product_comment_reply)
                                        {
                                            notificationModule.notificationDescription = $"\"{shoppingProductDiscussion.message}\" on {shoppingProductDiscussion.label}";
                                            notificationModule.notificationTitle = $"Hello, {shoppingProductDiscussion.name} has replied to your comment.";
                                            notificationModule.imageUrl = Constants.appDomain + shoppingProductDiscussion.imageUrl;
                                            notificationModule.discussionId = shoppingProductDiscussion.discussionId;
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Debug.WriteLine(e);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Debug.WriteLine(e);
                                }
                                break;
                            case Constants.notification_key_new_event_comment_reply:

                                EventDiscussion eventDiscussion = new EventDiscussion();
                                if (notificationModule.notificationKey == Constants.notification_key_new_event_comment_reply)
                                {
                                    JObject eventDiscussionServerObj = await EventDiscussion.fetchDiscussionLastReply(notificationModule.tocken);
                                    JObject eventDiscussionObj = (JObject)eventDiscussionServerObj.GetValue("data");
                                    eventDiscussion = eventDiscussionObj.ToObject<EventDiscussion>(serializer);
                                }

                                try
                                {
                                    if (notificationModule.notificationKey == Constants.notification_key_new_event_comment_reply)
                                    {
                                        notificationModule.notificationDescription = $"\" {eventDiscussion.message}\" on {eventDiscussion.eventTitle}";
                                        notificationModule.notificationTitle = $"Hello, {eventDiscussion.name} has replied to your comment.";
                                        notificationModule.imageUrl = Constants.appDomain + eventDiscussion.imageUrl;
                                        notificationModule.discussionId = eventDiscussion.discussionId;

                                    }
                                }
                                catch (Exception e)
                                {
                                    Debug.WriteLine(e);
                                }
                                break;
                            case Constants.notification_key_new_shopping_product_like:
                                break;
                            case Constants.notification_key_new_event_tickets_order:

                                ObservableCollection<EventsOrder> eventsOrders = new ObservableCollection<EventsOrder>();
                                if (notificationModule.notificationKey == Constants.notification_key_new_event_tickets_order)
                                {
                                    JObject eventsOrderServerObj = await EventsOrder.fetchMyEventsOrder(notificationModule.tocken);
                                    JArray eventsOrderArray = (JArray)eventsOrderServerObj.GetValue("data");
                                    eventsOrders.Clear();

                                    foreach (JToken shoppingOrderToken in eventsOrderArray)
                                    {
                                        JObject eventsOrderObj = (JObject)shoppingOrderToken;
                                        EventsOrder mEventsOrder = eventsOrderObj.ToObject<EventsOrder>(serializer);
                                        eventsOrders.Add(mEventsOrder);
                                    }
                                }
                                try
                                {
                                    if (notificationModule.notificationKey == Constants.notification_key_new_event_tickets_order)
                                    {
                                        String eventsLabel = "";
                                        if (eventsOrders.Count == 1)
                                        {
                                            eventsLabel += eventsOrders[0].eventTitle;
                                        }
                                        else
                                        {
                                            for (int x = 0; x < eventsOrders.Count; x++)
                                            {
                                                EventsOrder eventsOrder = eventsOrders[x];
                                                if (x == (eventsOrders.Count - 1))
                                                {
                                                    eventsLabel += eventsOrder.eventTitle;
                                                }
                                                else
                                                {
                                                    eventsLabel += eventsOrder.eventTitle + " . ";
                                                }
                                            }
                                        }

                                        notificationModule.notificationDescription = "Hello, your order of " + eventsLabel + " ticket has been created, click here to check it out.";
                                        notificationModule.notificationTitle = "Your events have been booked.";

                                        notificationModule.imageUrl = Constants.appDomain + eventsOrders[0].imageUrl;

                                    }
                                }
                                catch (Exception e)
                                {
                                    Debug.WriteLine(e);
                                }
                                break;
                            case Constants.notification_key_new_event_comment:
                                break;
                            case Constants.notification_key_new_event_like:
                                break;
                            case Constants.notification_key_new_job_comment:
                                break;
                            case Constants.notification_key_new_job_comment_reply:
                                break;
                            case Constants.notification_key_new_job_like:
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                    }
                    NotificationModules.Add(notificationModule);
                }
                OnPropertyChanged("NotificationModules");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
            SelectedItem = null;
        }

        public NotificationModule SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnNotificationSelected(value);
            }
        }

        async void OnNotificationSelected(NotificationModule notificationModule)
        {
            if (notificationModule == null)
                return;

            try
            {
                switch (notificationModule.notificationKey)
                {
                    case Constants.notification_key_new_safe_post_like:
                    case Constants.notification_key_new_safe_post_comment:
                    case Constants.notification_key_new_safe_post_comment_reply:
                        await Shell.Current.GoToAsync($"{nameof(PostDetailsPage)}?{nameof(PostDetailsViewModel.PostId)}={notificationModule.postId}");
                        break;
                    case Constants.notification_key_new_job:
                        break;
                    case Constants.notification_key_new_referral:
                        break;
                    case Constants.notification_key_new_referral_schedule:
                        break;
                    case Constants.notification_key_new_referral_assignment:
                        break;
                    case Constants.notification_key_event_reminder:
                        break;
                    case Constants.notification_key_new_quiz_challenge:
                        break;
                    case Constants.notification_key_event_ticket_claim:
                        break;
                    case Constants.notification_key_payment_received:
                        break;
                    case Constants.notification_key_new_shopping_order_placed:
                        await Shell.Current.GoToAsync($"{nameof(ShoppingOrderProductsPage)}?{nameof(ShoppingOrderProductsViewModel.InvoiceId)}={notificationModule.invoiceId}");
                        break;
                    case Constants.notification_key_new_shopping_product_comment:
                        break;
                    case Constants.notification_key_new_shopping_product_comment_reply:
                        await Shell.Current.GoToAsync($"{nameof(ShoppingProductDiscussionRepliesPage)}?{nameof(ShoppingProductDiscussionRepliesViewModel.DiscussionId)}={notificationModule.discussionId}");
                        break;
                    case Constants.notification_key_new_event_comment_reply:
                        await Shell.Current.GoToAsync($"{nameof(EventDiscussionRepliesPage)}?{nameof(EventDiscussionRepliesViewModel.DiscussionId)}={notificationModule.discussionId}");
                        break;
                    case Constants.notification_key_new_shopping_product_like:
                        break;
                    case Constants.notification_key_new_event_tickets_order:
                        await Shell.Current.GoToAsync($"{nameof(MyEventOrdersPage)}");
                        break;
                    case Constants.notification_key_new_event_comment:
                        break;
                    case Constants.notification_key_new_event_like:
                        break;
                    case Constants.notification_key_new_job_comment:
                        break;
                    case Constants.notification_key_new_job_comment_reply:
                        break;
                    case Constants.notification_key_new_job_like:
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}