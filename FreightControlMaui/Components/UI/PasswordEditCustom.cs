﻿using DevExpress.Maui.Editors;
using FreightControlMaui.Controls.Resources;

namespace FreightControlMaui.Components.UI
{
    public class PasswordEditCustom : PasswordEdit
    {
        public PasswordEditCustom(string icon = "", string placeholder = "")
        {
            PlaceholderText = placeholder;
            IconIndent = 5;
            Margin = new Thickness(10, 15, 10, 0);
            HeightRequest = 50;
            CornerRadius = 10;
            IsLabelFloating = false;
            LabelText = null;
            StartIcon = ImageSource.FromFile(icon);
            PlaceholderColor = Colors.LightGray;
            FocusedBorderColor = Colors.Gray;
            BorderColor = Colors.LightGray;
            TextColor = ControlResources.GetResource<Color>("PrimaryDark");
            CursorColor = ControlResources.GetResource<Color>("BorderGray400");
            ClearIconColor = ControlResources.GetResource<Color>("BorderGray400");
            IconColor = ControlResources.GetResource<Color>("ColorOfIcons");
            IconVerticalAlignment = LayoutAlignment.Center;
            TextVerticalAlignment = TextAlignment.Center;
        }
    }
}