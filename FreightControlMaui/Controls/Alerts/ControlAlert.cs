using FreightControlMaui.Constants;

namespace FreightControlMaui.Controls.Alerts
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

        public static async Task<bool> DefaultAlertWithResponse(string title, string content, string textAccept = "Sim", string textCancel = "Não")
        {
            return await Application.Current.MainPage.DisplayAlert(title, content, textAccept, textCancel);
        }

        public static async Task<string> DefaultAlertActionSheet(string title, string textCancel, string destruction, params string[] buttons)
        {
           return await App.Current.MainPage.DisplayActionSheet(title, textCancel, destruction, buttons);
        }
    }
}

