using FreightControlMaui.Components.Popups;

namespace FreightControlMaui;

public partial class App : Application
{
    #region Properties

    private static PopupLoadingView popupLoading = new();

    public static PopupLoadingView PopupLoading { get => popupLoading; set => popupLoading = value; }

    #endregion


    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}

