using FreightControlMaui.Controls.Resources;

namespace FreightControlMaui.Components.UI
{
    public class FooterActivityIndicator : ActivityIndicator
    {
        public FooterActivityIndicator()
        {
            Color = ControlResources.GetResource<Color>("PrimaryDark");
            IsRunning = true;
            HeightRequest = 40;
            WidthRequest = 40;
            HorizontalOptions = LayoutOptions.Center;
        }
    }
}

