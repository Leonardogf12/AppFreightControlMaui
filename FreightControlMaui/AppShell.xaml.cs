﻿using FreightControlMaui.MVVM.Views;

namespace FreightControlMaui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(FreightView), typeof(FreightView));
        Routing.RegisterRoute(nameof(AddFreightView), typeof(AddFreightView));
        Routing.RegisterRoute(nameof(DetailFreightView), typeof(DetailFreightView));
        Routing.RegisterRoute(nameof(ToFuelView), typeof(ToFuelView));
        Routing.RegisterRoute(nameof(RegisterView), typeof(RegisterView));
        Routing.RegisterRoute(nameof(EditUserView), typeof(EditUserView));
        Routing.RegisterRoute(nameof(ChartsView), typeof(ChartsView));
        Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
    }
}

