using DevExpress.Maui.Editors;
using FreightControlMaui.Components.UI;
using FreightControlMaui.Controls.Alerts;
using FreightControlMaui.Controls.Animations;
using FreightControlMaui.Controls.ControlCheckers;
using FreightControlMaui.Controls.Resources;
using FreightControlMaui.MVVM.Base;
using FreightControlMaui.MVVM.ViewModels;
using Microsoft.Maui.Controls.Shapes;

namespace FreightControlMaui.MVVM.Views
{
    public class ToFuelView : BaseContentPage
    {
        #region Properties

        private readonly ToFuelViewModel _viewModel = new();
        
        public const int Zero = 0;

        #endregion

        public ToFuelView()
        {
            BackgroundColor = Colors.White;

            Content = BuildToFuelView;

            BindingContext = _viewModel;
        }

        #region UI

        private View BuildToFuelView
        {
            get
            {
                var mainGrid = CreateMainGrid();

                CreateStackTitle(mainGrid);

                CreateStackInfoFreight(mainGrid);

                CreateForm(mainGrid);

                CreateButtonSave(mainGrid);

                return mainGrid;
            }
        }

        private static Grid CreateMainGrid()
        {
            return new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new () {Height = 80},
                    new () {Height = 60},
                    new () {Height = GridLength.Star},
                    new () {Height = 50},
                }
            };
        }

        private void CreateStackTitle(Grid mainGrid)
        {
            var stackTitle = new StackLayout
            {
                BackgroundColor = Colors.White
            };

            var contentGridStackTitle = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new () {Height = 50},
                },
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new () { Width = GridLength.Star},
                    new () { Width = GridLength.Auto},
                    new () { Width = GridLength.Star},
                },
                ColumnSpacing = 15,
                Margin = 10
            };

            var imageBackButton = new Image
            {
                Source = ImageSource.FromFile("back_primary_dark"),
                Margin = new Thickness(20, 0, 0, 0),
                HeightRequest = 20,
                HorizontalOptions = LayoutOptions.Start,
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped_GoBack;

            imageBackButton.GestureRecognizers.Add(tapGestureRecognizer);

            contentGridStackTitle.Add(imageBackButton, 0, 0);

            var labelTitle = new Label
            {
                Text = "Abastecimento",
                TextColor = ControlResources.GetResource<Color>("PrimaryDark"),
                Style = ControlResources.GetResource<Style>("labelTitleView"),
            };
            contentGridStackTitle.Add(labelTitle, 1, 0);

            stackTitle.Children.Add(contentGridStackTitle);

            mainGrid.Children.Add(stackTitle);
        }

        private void CreateStackInfoFreight(Grid mainGrid)
        {
            var stack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 5,
                Margin = new Thickness(15, 0, 0, 0)
            };

            var stackDate = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 5
            };
            var titleDate = new Label
            {
                Text = "Data:",
                FontFamily = "MontserratSemiBold",
                FontSize = 14,
                TextColor = ControlResources.GetResource<Color>("PrimaryDark")
            };
            stackDate.Children.Add(titleDate);
            var contentDate = new Label
            {
                FontFamily = "MontserratRegular",
                FontSize = 14,
                TextColor = ControlResources.GetResource<Color>("PrimaryDark")
            };
            contentDate.SetBinding(Label.TextProperty, nameof(_viewModel.DetailTravelDate));
            stackDate.Children.Add(contentDate);

            stack.Children.Add(stackDate);

            var stackOrigin = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 5
            };
            var titleOrigin = new Label
            {
                Text = "Origem:",
                FontFamily = "MontserratSemiBold",
                FontSize = 14,
                TextColor = ControlResources.GetResource<Color>("PrimaryDark")
            };
            stackOrigin.Children.Add(titleOrigin);
            var contentOrigin = new Label
            {
                FontFamily = "MontserratRegular",
                FontSize = 14,
                TextColor = ControlResources.GetResource<Color>("PrimaryDark")
            };
            contentOrigin.SetBinding(Label.TextProperty, nameof(_viewModel.DetailOrigin));
            stackOrigin.Children.Add(contentOrigin);

            stack.Children.Add(stackOrigin);

            var stackDestination = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 5
            };
            var titleDestination = new Label
            {
                Text = "Destino:",
                FontFamily = "MontserratSemiBold",
                FontSize = 14,
                TextColor = ControlResources.GetResource<Color>("PrimaryDark")
            };
            stackDestination.Children.Add(titleDestination);
            var contentDestination = new Label
            {
                FontFamily = "MontserratRegular",
                FontSize = 14,
                TextColor = ControlResources.GetResource<Color>("PrimaryDark")
            };
            contentDestination.SetBinding(Label.TextProperty, nameof(_viewModel.DetailDestination));
            stackDestination.Children.Add(contentDestination);

            stack.Children.Add(stackDestination);

            mainGrid.Add(stack, 0, 1);
        }

        private void CreateForm(Grid mainGrid)
        {
            var borderForm = new Border
            {
                Stroke = App.GetLightGrayColor(),
                Background = Colors.Transparent,
                StrokeThickness = 1,
                Margin = DeviceInfo.Platform == DevicePlatform.Android ? 10 : 20,
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(10)
                }
            };

            var contentGridBorderForm = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new () {Height = GridLength.Auto},
                    new () {Height = GridLength.Auto},
                    new () {Height = GridLength.Auto},
                    new () {Height = GridLength.Auto},
                    new () {Height = GridLength.Auto},
                },
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new () { Width = GridLength.Star},
                    new () { Width = GridLength.Star},
                }
            };

            CreateToFuelDateFieldForm(contentGridBorderForm);

            CreateFuelBoxFieldForm(contentGridBorderForm);

            CreateExpensesFieldForm(contentGridBorderForm);

            CreateObservationFieldForm(contentGridBorderForm);

            borderForm.Content = contentGridBorderForm;

            mainGrid.Add(borderForm, 0, 2);
        }

        private void CreateToFuelDateFieldForm(Grid contentGridBorderForm)
        {
            var date = new DatePickerFieldCustom();
            date.DatePicker.SetBinding(DatePicker.DateProperty, nameof(_viewModel.Date));
            date.Border.SetBinding(Border.StrokeProperty, nameof(_viewModel.StrokeDate));
            date.DatePicker.DateSelected += DatePicker_DateSelected;

            contentGridBorderForm.AddWithSpan(view: date, row: 0, column: 0, rowSpan: 1, columnSpan: 3);           
        }

        private void CreateFuelBoxFieldForm(Grid contentGridBorderForm)
        {
            var borderFuel = new Border
            {
                Stroke = App.GetLightGrayColor(),
                Background = Colors.Transparent,
                StrokeThickness = 1,
                Margin = DeviceInfo.Platform == DevicePlatform.Android ? 10 : 20,
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(10)
                },
            };

            var gridFuel = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new() {Height = GridLength.Star},
                    new() {Height = GridLength.Star},
                },
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new () {Width = GridLength.Star},
                    new () {Width = GridLength.Star},
                },
                RowSpacing = 10,
            };

            var liters = new TextEditCustom(icon: "liters_24", placeholder: "Litros", keyboard: Keyboard.Numeric);
            liters.SetBinding(TextEditBase.TextProperty, nameof(_viewModel.Liters));
            liters.SetBinding(EditBase.BorderColorProperty, nameof(_viewModel.BorderColorLiters));
            liters.SetBinding(EditBase.FocusedBorderColorProperty, nameof(_viewModel.BorderColorFocusedLiters));
            liters.TextChanged += Liters_TextChanged;

            gridFuel.AddWithSpan(view: liters, row: 0, column: 0);
            
            var amountSpentFuel = new TextEditCustom(icon: "money_24", placeholder: "Valor", keyboard: Keyboard.Numeric);
            amountSpentFuel.SetBinding(TextEditBase.TextProperty, nameof(_viewModel.AmountSpentFuel));
            amountSpentFuel.SetBinding(EditBase.BorderColorProperty, nameof(_viewModel.BorderColorAmountSpentFuel));
            amountSpentFuel.SetBinding(EditBase.FocusedBorderColorProperty, nameof(_viewModel.BorderColorFocusedAmountSpentFuel));
            amountSpentFuel.TextChanged += AmountSpentFuel_TextChanged;
            gridFuel.Add(amountSpentFuel, 1, 0);

            var titleValuePerLiter = new Label
            {
                Text = "Valor do Litro:",
                FontFamily = "MontserratRegular",
                FontSize = 16,
                TextColor = ControlResources.GetResource<Color>("PrimaryDark"),
                Margin = new Thickness(10, 0, 0, 10),
            };

            gridFuel.AddWithSpan(view: titleValuePerLiter, row: 1, column: 0);
           
            var contentValuePerLiter = new Label
            {
                FontFamily = "MontserratRegular",
                FontSize = 16,
                TextColor = App.GetLightGrayColor(),
                HorizontalOptions = LayoutOptions.End,
                Margin = new Thickness(0, 0, 10, 0),
            };
            contentValuePerLiter.SetBinding(Label.TextProperty, nameof(_viewModel.ValuePerLiter));

            gridFuel.AddWithSpan(view: contentValuePerLiter, row: 1, column: 1);
            
            borderFuel.Content = gridFuel;

            contentGridBorderForm.AddWithSpan(view: borderFuel, row: 1, column: 0, rowSpan:1, columnSpan:3);         
        }

        private void CreateExpensesFieldForm(Grid contentGridBorderForm)
        {
            var expenses = new TextEditCustom(icon: "money_24", placeholder: "Despesas", keyboard: Keyboard.Numeric);
            expenses.SetBinding(TextEditBase.TextProperty, nameof(_viewModel.Expenses));
            expenses.SetBinding(EditBase.BorderColorProperty, nameof(_viewModel.BorderColorExpenses));
            expenses.SetBinding(EditBase.FocusedBorderColorProperty, nameof(_viewModel.BorderColorFocusedExpenses));
            expenses.TextChanged += Expenses_TextChanged;

            contentGridBorderForm.AddWithSpan(view: expenses, row: 3, column: 0, rowSpan: 1, columnSpan: 3);         
        }

        private void CreateObservationFieldForm(Grid contentGridBorderForm)
        {
            var observation = new MultilineEditCustom(icon: "comment_24", placeholder: "Observacão");
            observation.SetBinding(TextEditBase.TextProperty, nameof(_viewModel.Observation));

            contentGridBorderForm.AddWithSpan(view: observation, row: 4, column: 0, rowSpan: 1, columnSpan: 3);        
        }

        private void CreateButtonSave(Grid mainGrid)
        {
            var button = new Button
            {
                Text = "Salvar",
                Style = ControlResources.GetResource<Style>("buttonDarkPrimary")
            };
            button.SetBinding(IsEnabledProperty, nameof(_viewModel.IsEnabledSaveButton));

            button.Clicked += SaveClicked;

            mainGrid.Add(button, 0, 3);
        }

        #endregion

        #region Events

        private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var element = sender as DatePicker;

            var dateSelected = element.Date;

            if (dateSelected < _viewModel.DetailsFreight.TravelDate)
            {
                await SetBorderColorErrorToDateField();

                return;
            }

            _viewModel.StrokeDate = App.GetLightGrayColor();
        }

        private async void TapGestureRecognizer_Tapped_GoBack(object sender, TappedEventArgs e)
        {
            View element = sender as Image;

            await ClickAnimation.SetFadeOnElement(element);

            await Navigation.PopAsync();
        }

        private void Liters_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(GetValueStringOfObject(sender)))
            {
                _viewModel.ValuePerLiter = Zero.ToString("c");
                SetBorderColorDefaultLitersField();
                return;
            }

            if (!CheckTheEntrys.IsValidEntry(GetValueStringOfObject(sender), CheckTheEntrys.patternLiters))
            {
                SetBorderColorErrorToLitersField();
                return;
            }

            SetBorderColorDefaultLitersField();

            _viewModel.CalculatePriceOfFuel();
        }

        private void AmountSpentFuel_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(GetValueStringOfObject(sender)))
            {
                _viewModel.ValuePerLiter = Zero.ToString("c");
                SetBorderColorDefaultAmountSpentFuelField();
                return;
            }

            if (!CheckTheEntrys.IsValidEntry(GetValueStringOfObject(sender), CheckTheEntrys.patternMoney))
            {
                SetBorderColorErrorToAmountSpentFuelField();
                return;
            }

            SetBorderColorDefaultAmountSpentFuelField();

            _viewModel.CalculatePriceOfFuel();
        }

        private void Expenses_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(GetValueStringOfObject(sender)))
            {
                SetBorderColorDefaultExpensesField();
                return;
            }

            if (!CheckTheEntrys.IsValidEntry(GetValueStringOfObject(sender), CheckTheEntrys.patternMoney))
            {
                SetBorderColorErrorToExpensesField();
                return;
            }

            SetBorderColorDefaultExpensesField();
        }

        private async void SaveClicked(object sender, EventArgs e)
        {
            if (!ValidationToFieldsRequireds())
            {
                await ControlAlert.DefaultAlert("Atenção", "Um ou mais campos precisam de correção. Favor verificar.");                
                return;
            }

            _viewModel.OnSave();
        }

        #endregion

        #region Actions

        private bool ValidationToFieldsRequireds()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(_viewModel.Liters))
            {
                SetBorderColorErrorToLitersField();
                isValid = false;
            }

            if (string.IsNullOrEmpty(_viewModel.AmountSpentFuel))
            {
                SetBorderColorErrorToAmountSpentFuelField();
                isValid = false;
            }

            if (_viewModel.BorderColorExpenses == App.GetRedColor())
            {
                isValid = false;
            }
            if (_viewModel.BorderColorLiters == App.GetRedColor())
            {
                isValid = false;
            }

            return isValid;
        }

        private static string GetValueStringOfObject(object sender)
        {
            var element = sender as TextEdit;
            return element.Text;
        }

        private void SetBorderColorDefaultLitersField()
        {
            _viewModel.BorderColorLiters = App.GetLightGrayColor();
            _viewModel.BorderColorFocusedLiters = App.GetGrayColor();
        }

        private void SetBorderColorDefaultAmountSpentFuelField()
        {
            _viewModel.BorderColorAmountSpentFuel = App.GetLightGrayColor();
            _viewModel.BorderColorFocusedAmountSpentFuel = App.GetGrayColor();
        }

        private void SetBorderColorDefaultExpensesField()
        {
            _viewModel.BorderColorExpenses = App.GetLightGrayColor();
            _viewModel.BorderColorFocusedExpenses = App.GetGrayColor();
        }

        private void SetBorderColorErrorToLitersField()
        {
            _viewModel.BorderColorLiters = App.GetRedColor();
            _viewModel.BorderColorFocusedLiters = App.GetRedColor();
        }

        private void SetBorderColorErrorToAmountSpentFuelField()
        {
            _viewModel.BorderColorAmountSpentFuel = App.GetRedColor();
            _viewModel.BorderColorFocusedAmountSpentFuel = App.GetRedColor();
        }

        private void SetBorderColorErrorToExpensesField()
        {
            _viewModel.BorderColorExpenses = App.GetRedColor();
            _viewModel.BorderColorFocusedExpenses = App.GetRedColor();
        }

        private async Task SetBorderColorErrorToDateField()
        {
            _viewModel.StrokeDate = App.GetRedColor();
            await ControlAlert.DefaultAlert("Ops", "A data selecionada não pode ser menor que a data do frete.");
        }

        #endregion
    }

}