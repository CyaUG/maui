using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;



namespace Youth.Utils
{
    public class LinksLabel : Label
    {
        public static BindableProperty LinksTextProperty = BindableProperty.Create(nameof(LinksText), typeof(string), typeof(LinksLabel), propertyChanged: OnLinksTextPropertyChanged);

        private readonly ICommand _linkTapGesture = new Command<string>((url) => OpenUri(url));

        private static async void OpenUri(string url)
        {
            var uri = new Uri(url);

            await Browser.OpenAsync(uri, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
            });
        }

        public string LinksText
        {
            get => GetValue(LinksTextProperty) as string;
            set => SetValue(LinksTextProperty, value);
        }

        private void SetFormattedText()
        {
            var formattedString = new FormattedString();

            if (!string.IsNullOrEmpty(LinksText))
            {
                var splitText = LinksText.Split(' ');

                foreach (string textPart in splitText)
                {
                    var span = new Span { Text = $"{textPart} " };

                    if (IsUrl(textPart)) // a link
                    {
                        //span.TextColor = Color.DeepSkyBlue;
                        span.GestureRecognizers.Add(new TapGestureRecognizer
                        {
                            Command = _linkTapGesture,
                            CommandParameter = textPart
                        });
                    }

                    formattedString.Spans.Add(span);
                }
            }

            this.FormattedText = formattedString;
        }

        private bool IsUrl(string input)
        {
            return Uri.TryCreate(input, UriKind.Absolute, out var uriResult) &&
              (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        private static void OnLinksTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var linksLabel = bindable as LinksLabel;
            linksLabel.SetFormattedText();
        }
    }
}
