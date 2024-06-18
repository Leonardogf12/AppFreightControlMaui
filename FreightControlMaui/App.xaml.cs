using FreightControlMaui.Components.Popups;
using FreightControlMaui.Constants;
using FreightControlMaui.Controls;

namespace FreightControlMaui;

public partial class App : Application
{
    #region Properties

    private static PopupLoadingView popupLoading = new();

    public static PopupLoadingView PopupLoading { get => popupLoading; set => popupLoading = value; }

    public static string UserLocalIdLogged = string.Empty;

    #endregion


    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

    public static void SetLocalIdByUserLogged()
    {
        UserLocalIdLogged = ControlPreferences.GetKeyOfPreferences(StringConstants.firebaseUserLocalIdKey);
    }

    #region Style - Colors

    public static Color GetRedColor() => Colors.Red;

    public static Color GetLightGrayColor() => Colors.LightGray;

    public static Color GetGrayColor() => Colors.Gray;

    #endregion
}

