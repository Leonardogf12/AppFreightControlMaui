﻿using DevExpress.Maui.Editors;
using FreightControlMaui.Components.UI;
using FreightControlMaui.Controls.Resources;
using FreightControlMaui.MVVM.Base;
using FreightControlMaui.MVVM.ViewModels;

namespace FreightControlMaui.MVVM.Views
{
    public class RegisterView : BaseContentPage
    {
        public RegisterViewModel ViewModel = new();

        public RegisterView()
        {
            BackgroundColor = Colors.White;

            Content = BuildRegisterView();

            BindingContext = ViewModel;
        }

        #region UI

        private Grid CreateMainGrid()
        {
            return new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new() {Height = GridLength.Auto},
                    new() {Height = GridLength.Auto},
                    new() {Height = GridLength.Auto},
                    new() {Height = GridLength.Auto},
                    new() {Height = GridLength.Auto},
                    new() {Height = GridLength.Auto},
                },
                RowSpacing = 10,
                VerticalOptions = LayoutOptions.Center
            };
        }

        private View BuildRegisterView()
        {
            var mainGrid = CreateMainGrid();

            CreateNameField(mainGrid);
            CreateEmailField(mainGrid);
            CreatePasswordField(mainGrid);
            CreateSecondPasswordField(mainGrid);
            CreateRegisterButton(mainGrid);
            CreateBackButton(mainGrid);

            return mainGrid;
        }

        private void CreateNameField(Grid mainGrid)
        {
            var name = new TextEditCustom(icon: "", placeholder: "Nome", keyboard: Keyboard.Email)
            {
                Margin = new Thickness(10, 0, 10, 0)
            };
            name.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.Name));

            name.TextChanged += Name_TextChanged;

            mainGrid.Add(name, 0, 0);
        }

        private void CreateEmailField(Grid mainGrid)
        {
            var email = new TextEditCustom(icon: "", placeholder: "Email", keyboard: Keyboard.Email)
            {
                Margin = new Thickness(10, 0, 10, 0)
            };
            email.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.Email));

            email.TextChanged += Email_TextChanged;

            mainGrid.Add(email, 0, 1);
        }

        private void CreatePasswordField(Grid mainGrid)
        {
            var password = new PasswordEditCustom(icon: "", placeholder: "Senha")
            {
                Margin = new Thickness(10, 0, 10, 0)
            };
            password.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.Password));

            mainGrid.Add(password, 0, 2);
        }

        private void CreateSecondPasswordField(Grid mainGrid)
        {
            var secondPassword = new PasswordEditCustom(icon: "", placeholder: "Confirmar Senha")
            {
                Margin = new Thickness(10, 0, 10, 0)
            };
            secondPassword.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.SecondPassword));

            mainGrid.Add(secondPassword, 0, 3);
        }

        private void CreateRegisterButton(Grid mainGrid)
        {
            var buttonRegister = new Button
            {
                Text = "Registrar",
                Style = (Style)Application.Current.Resources["buttonDarkPrimary"]
            };

            buttonRegister.Clicked += ButtonRegister_Clicked;

            mainGrid.Add(buttonRegister, 0, 4);
        }

        private void CreateBackButton(Grid mainGrid)
        {
            var buttonBack = new Button
            {
                Text = "Voltar",
                Style = ControlResources.GetResource<Style>("buttonDarkPrimary"),
            };

            buttonBack.Clicked += ButtonBack_Clicked;

            mainGrid.Add(buttonBack, 0, 5);
        }

        #endregion

        #region Events

        private void Name_TextChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Email_TextChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private async void ButtonRegister_Clicked(object sender, EventArgs e)
        {
            await ViewModel.RegisterNewUser();
        }

        private async void ButtonBack_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion
    }

}

