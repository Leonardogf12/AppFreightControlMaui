﻿using FreightControlMaui.Components.UI;
using FreightControlMaui.Controls.Alerts;
using FreightControlMaui.Controls.Resources;
using FreightControlMaui.MVVM.Base;
using FreightControlMaui.MVVM.ViewModels;

namespace FreightControlMaui.MVVM.Views
{
    public class ResetPasswordView : BaseContentPage
    {
        public ResetPasswordViewModel ViewModel = new();

        public ResetPasswordView()
        {
            BackgroundColor = Colors.White;

            Content = BuildResetPasswordView();

            CreateLoadingPopupView(this, ViewModel);

            BindingContext = ViewModel;
        }

        #region UI

        private Grid CreateMainGrid()
        {
            return new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new() {Height = 200},
                    new() {Height = GridLength.Star},
                },
            };
        }

        private View BuildResetPasswordView()
        {
            var mainGrid = CreateMainGrid();

            CreatePhrases(mainGrid);

            CreateInputAndButtons(mainGrid);

            return mainGrid;
        }

        private void CreatePhrases(Grid mainGrid)
        {
            var stack = new StackLayout
            {
                Spacing = 5,
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Center
            };

            var title = new Label
            {
                Text = "Esqueceu a Senha?",
                Style = ControlResources.GetResource<Style>("labelTitleForgotPassword"),                
            };

            var phrase = new Label
            {
                Text = "Sem problemas, nós enviaremos um email com as instruções para redefinição.",
                Style = ControlResources.GetResource<Style>("labelPhraseForgotPassword"),
            };

            stack.Children.Add(title);
            stack.Children.Add(phrase);

            mainGrid.Add(stack, 0, 0);
        }

        private void CreateInputAndButtons(Grid mainGrid)
        {
            var grid = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new() {Height = GridLength.Auto},
                    new() {Height = GridLength.Auto},
                    new() {Height = GridLength.Auto},
                },
                RowSpacing = 8,
                VerticalOptions = LayoutOptions.Start
            };

            CreateEmailField(grid);
            CreateResetButton(grid);
            CreateBackButton(grid);

            mainGrid.Add(grid, 0, 1);
        }

        private void CreateEmailField(Grid mainGrid)
        {
            var email = new TextEditCustom(icon: "", placeholder: "Email da conta", keyboard: Keyboard.Email)
            {
                Margin = new Thickness(10, 0, 10, 0)
            };
            email.SetBinding(TextEditCustom.TextProperty, nameof(ViewModel.Email));
           
            mainGrid.Add(email, 0, 0);
        }

        private void CreateResetButton(Grid mainGrid)
        {
            var buttonReset = new Button
            {
                Text = "Redefinir Senha",
                Style = ControlResources.GetResource<Style>("buttonLoginDarkPrimary"),
            };

            buttonReset.Clicked += ButtonReset_Clicked; ;

            mainGrid.Add(buttonReset, 0, 1);
        }

        private void CreateBackButton(Grid mainGrid)
        {
            var buttonBack = new Button
            {
                Text = "Voltar",
                Style = ControlResources.GetResource<Style>("buttonLoginSecondaryDarkPrimary"),                
            };

            buttonBack.Clicked += ButtonBack_Clicked;

            mainGrid.Add(buttonBack, 0, 2);
        }

        #endregion

        #region Events

        private async void ButtonBack_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
        
        private async void ButtonReset_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ViewModel.Email)) return;

            if (!ViewModel.Email.Contains("@"))
            {
                await ControlAlert.DefaultAlert("Ops", "Parece que este não é um email válido. Favor verificar.");               
                return;
            }

            await ViewModel.ResetPassword();
        }

        #endregion
    }

}

