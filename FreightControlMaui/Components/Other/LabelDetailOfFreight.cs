using FreightControlMaui.Controls.Resources;

namespace FreightControlMaui.Components.Other
{
    public class LabelDetailOfFreight : StackLayout
    {
        public Label ContentLabel { get; set; }

        public LabelDetailOfFreight(string title)
        {
            var stack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 10
            };

            var titleLabel = new Label
            {
                Text = title,
                TextColor = ControlResources.GetResource<Color>("PrimaryDark"),
                FontFamily = "MontserratSemiBold",
                FontAttributes = FontAttributes.Bold,
                FontSize = 16,
                VerticalOptions = LayoutOptions.Center,
            };

            ContentLabel = new Label
            {
                TextColor = ControlResources.GetResource<Color>("PrimaryDark"),
                FontFamily = "MontserratRegular",
                FontSize = 16,
                VerticalOptions = LayoutOptions.Center
            };

            stack.Children.Add(titleLabel);
            stack.Children.Add(ContentLabel);

            Children.Add(stack);
        }
    }

}

