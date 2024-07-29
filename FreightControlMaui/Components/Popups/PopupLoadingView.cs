﻿using CommunityToolkit.Maui.Views;
using SkiaSharp.Extended.UI.Controls;

namespace FreightControlMaui.Components.Popups
{
    public class PopupLoadingView : Popup
    {
        public bool IsOpen;

        public PopupLoadingView()
        {
            Color = Colors.Transparent;
            CanBeDismissedByTappingOutsideOfPopup = false;

            Content = new VerticalStackLayout
            {
                BackgroundColor = Colors.Transparent,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    CreateLoadingComponent()
                }
            };
        }

        private static SKLottieView CreateLoadingComponent()
        {
            var load = new SKLottieView
            {
                Source = (SKLottieImageSource)SKLottieImageSource.FromFile("loading_truck.json"),
                HeightRequest = 120,
                WidthRequest = 120,
                BackgroundColor = Colors.Transparent,
                RepeatCount = -1,
            };

            return load;
        }
    }
}