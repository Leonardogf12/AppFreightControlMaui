﻿namespace FreightControlMaui.Controls.Alerts
{
    public static class ControlAlert
	{
        public static async Task DefaultAlert(string title, string content, string textAccept = "Ok", string textCancel = "")
        {
            if (string.IsNullOrEmpty(textCancel))
            {
                await Application.Current.MainPage.DisplayAlert(title, content, textAccept);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(title, content, textAccept, textCancel);
            }            
        }
    }
}

