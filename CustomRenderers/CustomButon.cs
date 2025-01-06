namespace Youth.CustomRenderers
{
    public class CustomButton : Button
    {
        public static readonly BindableProperty AutoCapitalizationProperty = BindableProperty.Create(nameof(AutoCapitalization), typeof(bool), typeof(CustomButton), false);
        public bool AutoCapitalization
        {
            get { return (bool)GetValue(AutoCapitalizationProperty); }
            set { SetValue(AutoCapitalizationProperty, value); }
        }
    }
}
