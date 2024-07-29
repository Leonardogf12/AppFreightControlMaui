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
    public class AddFreightView : BaseContentPage
    {
        #region Properties

        private readonly AddFreightViewModel _viewModel = new();

        public ClickAnimation ClickAnimation = new();

        public ComboboxEditCustom OriginUfComboboxEditCustom;

        public ComboboxEditCustom DestinationUfComboboxEditCustom;

        #endregion

        public AddFreightView()
        {
            BackgroundColor = Colors.White;

            Content = BuildAddFreightView;

            CreateLoadingPopupView(this, _viewModel);

            BindingContext = _viewModel;
        }

        #region UI

        private View BuildAddFreightView
        {
            get
            {
                var mainGrid = CreateMainGrid();

                CreateHeader(mainGrid);

                CreateForm(mainGrid);

                CreateSaveButton(mainGrid);

                return mainGrid;
            }
        }

        private static Grid CreateMainGrid()
        {
            return new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new () {Height = 100},
                    new () {Height = GridLength.Star},
                    new () {Height = 50},
                }
            };
        }

        private void CreateHeader(Grid mainGrid)
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
                HorizontalOptions = LayoutOptions.Start
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped_GoBack;

            imageBackButton.GestureRecognizers.Add(tapGestureRecognizer);

            contentGridStackTitle.Add(imageBackButton, 0, 0);

            var labelTitle = new Label
            {
                TextColor = ControlResources.GetResource<Color>("PrimaryDark"),
                Style = ControlResources.GetResource<Style>("labelTitleView")
            };
            labelTitle.SetBinding(Label.TextProperty, nameof(_viewModel.TextTitlePage));
            contentGridStackTitle.Add(labelTitle, 1, 0);

            stackTitle.Children.Add(contentGridStackTitle);

            mainGrid.Children.Add(stackTitle);
        }

        private void CreateForm(Grid mainGrid)
        {
            var scroll = new ScrollView
            {
                Orientation = ScrollOrientation.Vertical,
                VerticalScrollBarVisibility = ScrollBarVisibility.Always
            };

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
                    new () { Width = GridLength.Star},
                    new () { Width = GridLength.Star},                  
                },               
            };

            CreateTravelDateFieldForm(contentGridBorderForm);

            CreateOriginFieldForm(contentGridBorderForm);

            CreateDestinationFieldForm(contentGridBorderForm);

            CreateKmAndFreightValueFields(contentGridBorderForm);

            CreateObservationFieldCustom(contentGridBorderForm);

            borderForm.Content = contentGridBorderForm;

            scroll.Content = borderForm;

            mainGrid.Add(scroll, 0, 1);
        }

        private static void CreateTravelDateFieldForm(Grid contentGridBorderForm)
        {
            var travel = new DatePickerFieldCustom();
            travel.DatePicker.SetBinding(DatePicker.DateProperty, nameof(AddFreightViewModel.TravelDate));
            contentGridBorderForm.AddWithSpan(view: travel, row: 0, column: 0, rowSpan: 1, columnSpan: 5);
        }

        private void CreateOriginFieldForm(Grid contentGridBorderForm)
        {
            var grid = new Grid
            {                
                RowDefinitions = new RowDefinitionCollection
                {
                    new () { Height = GridLength.Auto},
                    new () { Height = GridLength.Star},
                },
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new () { Width = GridLength.Star},
                    new () { Width = GridLength.Star},
                    new () { Width = GridLength.Star},
                    new () { Width = GridLength.Star},
                    new () { Width = GridLength.Star},
                },              
            };

            var title = new Label
            {
                Text = "Origem",
                TextColor = Colors.Gray,
                FontFamily = "MontserratRegular",
                FontSize = 16,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(10, 5, 0, 0),
            };

            grid.AddWithSpan(view: title, row: 0, column: 0, rowSpan: 1, columnSpan: 5);
          
            OriginUfComboboxEditCustom = new ComboboxEditCustom(icon: "uf_24", labelText: "Uf")
            {
                Margin = new Thickness(10, 0, 5, 0),
            };
            OriginUfComboboxEditCustom.SetBinding(ItemsEditBase.ItemsSourceProperty, nameof(_viewModel.OriginUfCollection));
            OriginUfComboboxEditCustom.SetBinding(ComboBoxEdit.SelectedItemProperty, nameof(_viewModel.SelectedItemOriginUf), BindingMode.TwoWay);
            OriginUfComboboxEditCustom.SetBinding(EditBase.BorderColorProperty, nameof(_viewModel.BorderColorOriginUf));
            OriginUfComboboxEditCustom.SetBinding(EditBase.FocusedBorderColorProperty, nameof(_viewModel.BorderColorFocusedOriginUf));
            OriginUfComboboxEditCustom.SelectionChanged += OriginUf_SelectionChanged;

            grid.AddWithSpan(view: OriginUfComboboxEditCustom, row: 1, column: 0, rowSpan: 1, columnSpan: 2);
           
            var origin = new ComboboxEditCustom(icon: "local_24", labelText: "Origem")
            {
                Margin = new Thickness(0, 0, 10, 0),
            };
            origin.SetBinding(ItemsEditBase.ItemsSourceProperty, nameof(_viewModel.OriginCollection));
            origin.SetBinding(ComboBoxEdit.SelectedItemProperty, nameof(_viewModel.SelectedItemOrigin), BindingMode.TwoWay);
            origin.SetBinding(EditBase.BorderColorProperty, nameof(_viewModel.BorderColorOrigin));
            origin.SetBinding(EditBase.FocusedBorderColorProperty, nameof(_viewModel.BorderColorFocusedOrigin));
            origin.SelectionChanged += Origin_SelectionChanged;

            grid.AddWithSpan(view: origin, row: 1, column: 2, rowSpan: 1, columnSpan: 3);
          
            contentGridBorderForm.AddWithSpan(view: grid, row: 1, column: 0, rowSpan: 1, columnSpan: 5);                     
        }

        private void CreateDestinationFieldForm(Grid contentGridBorderForm)
        {
            var grid = new Grid
            {               
                RowDefinitions = new RowDefinitionCollection
                {
                    new () { Height = GridLength.Star},
                    new () { Height = GridLength.Star},
                },
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new () { Width = GridLength.Star},
                    new () { Width = GridLength.Star},
                    new () { Width = GridLength.Star},
                    new () { Width = GridLength.Star},
                    new () { Width = GridLength.Star},
                },               
            };

            var title = new Label
            {
                Text = "Destino",
                FontFamily = "MontserratRegular",
                TextColor = Colors.Gray,
                FontSize = 16,
                HorizontalOptions = LayoutOptions.Start,                
                Margin = new Thickness(10, 5, 0, 0),
            };

            grid.AddWithSpan(view: title, row: 0, column: 0, rowSpan: 1, columnSpan: 5);
          
            DestinationUfComboboxEditCustom = new ComboboxEditCustom(icon: "uf_24", labelText: "Uf")
            {
                Margin = new Thickness(10, 0, 5, 0),
            };
            DestinationUfComboboxEditCustom.SetBinding(ItemsEditBase.ItemsSourceProperty, nameof(_viewModel.DestinationUfCollection));
            DestinationUfComboboxEditCustom.SetBinding(ComboBoxEdit.SelectedItemProperty, nameof(_viewModel.SelectedItemDestinationUf), BindingMode.TwoWay);
            DestinationUfComboboxEditCustom.SetBinding(EditBase.BorderColorProperty, nameof(_viewModel.BorderColorDestinationUf));
            DestinationUfComboboxEditCustom.SetBinding(EditBase.FocusedBorderColorProperty, nameof(_viewModel.BorderColorFocusedDestinationUf));
            DestinationUfComboboxEditCustom.SelectionChanged += DestinationUf_SelectionChanged;

            grid.AddWithSpan(view: DestinationUfComboboxEditCustom, row: 2, column: 0, rowSpan: 1, columnSpan: 2);            
          
            var destination = new ComboboxEditCustom(icon: "local_24", labelText: "Destino")
            {
                Margin = new Thickness(0, 0, 10, 0),
            };
            destination.SetBinding(ItemsEditBase.ItemsSourceProperty, nameof(_viewModel.DestinationCollection));
            destination.SetBinding(ComboBoxEdit.SelectedItemProperty, nameof(_viewModel.SelectedItemDestination));
            destination.SetBinding(EditBase.BorderColorProperty, nameof(_viewModel.BorderColorDestination));
            destination.SetBinding(EditBase.FocusedBorderColorProperty, nameof(_viewModel.BorderColorFocusedDestination));
            destination.SelectionChanged += Destination_SelectionChanged;

            grid.AddWithSpan(view: destination, row: 2, column: 2, rowSpan: 1, columnSpan: 3);            
            
            contentGridBorderForm.AddWithSpan(view: grid, row: 2, column: 0, rowSpan: 1, columnSpan: 5);                     
        }

        private void CreateKmAndFreightValueFields(Grid contentGridBorderForm)
        {
            var grid = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new () { Width = GridLength.Star},
                    new () { Width = GridLength.Star},
                },                
            };

            CreateKmFieldForm(grid);

            CreateFreightValueFieldForm(grid);

            contentGridBorderForm.AddWithSpan(view: grid, row: 3, column: 0, rowSpan: 1, columnSpan: 5);
        }

        private void CreateKmFieldForm(Grid grid)
        {
            var km = new TextEditCustom(icon: "km_24", placeholder: "Km: 1000", keyboard: Keyboard.Numeric)
            {
                Margin = new Thickness(10, 15, 5, 0),
            };
            km.SetBinding(TextEditBase.TextProperty, nameof(_viewModel.Kilometer));
            km.SetBinding(EditBase.BorderColorProperty, nameof(_viewModel.BorderColorKm));
            km.SetBinding(EditBase.FocusedBorderColorProperty, nameof(_viewModel.BorderColorFocusedKm));
            km.TextChanged += Km_TextChanged;

            grid.AddWithSpan(view: km, row: 0, column: 0, rowSpan: 1, columnSpan: 1);
        }

        private void CreateFreightValueFieldForm(Grid grid)
        {
            var freigthField = new TextEditCustom(icon: "money_24", placeholder: "R$ 1000.00", keyboard: Keyboard.Numeric)
            {
                Margin = new Thickness(0, 15, 10, 0),
            };
            freigthField.SetBinding(TextEditBase.TextProperty, nameof(_viewModel.FreightValue));
            freigthField.SetBinding(EditBase.BorderColorProperty, nameof(_viewModel.BorderColorFreightValue));
            freigthField.SetBinding(EditBase.FocusedBorderColorProperty, nameof(_viewModel.BorderColorFocusedFreightValue));
            freigthField.TextChanged += FreigthField_TextChanged;

            grid.AddWithSpan(view: freigthField, row: 0, column: 1, rowSpan: 1, columnSpan: 1);                      
        }

        private void CreateObservationFieldCustom(Grid contentGridBorderForm)
        {
            var observation = new MultilineEditCustom(icon: "comment_24", placeholder: "Observacão");
            observation.SetBinding(TextEditBase.TextProperty, nameof(_viewModel.Observation));

            contentGridBorderForm.AddWithSpan(observation, 4, 0, 1, 5);
        }

        private void CreateSaveButton(Grid mainGrid)
        {
            var button = new Button
            {
                Text = "Salvar",
                Style = ControlResources.GetResource<Style>("buttonDarkPrimary")
            };

            button.Clicked += SaveClicked;

            mainGrid.Add(button, 0, 2);
        }

        #endregion

        #region Events

        private void OriginUf_SelectionChanged(object sender, EventArgs e)
        {
            if (sender is ComboBoxEdit element)
            {
                if (element.SelectedItem is string uf)
                {
                    _viewModel.SelectedItemOriginUf = uf;
                    _viewModel.ChangedItemOriginUf(uf);
                    SetBorderColorDefaultForDropdownOriginUf();
                    OriginUfComboboxEditCustom.Unfocus();
                }
            }
        }

        private void Origin_SelectionChanged(object sender, EventArgs e)
        {
            if (sender is ComboBoxEdit element)
            {
                if (element.SelectedItem is string)
                {
                    SetBorderColorDefaultForDropdownOrigin();
                }
            }
        }

        private void DestinationUf_SelectionChanged(object sender, EventArgs e)
        {
            if (sender is ComboBoxEdit element)
            {
                if (element.SelectedItem is string uf)
                {
                    _viewModel.SelectedItemDestinationUf = uf;
                    _viewModel.ChangedItemDestinationUf(uf);
                    SetBorderColorDefaultForDropdownDestinationUf();
                    DestinationUfComboboxEditCustom.Unfocus();
                }
            }
        }

        private void Destination_SelectionChanged(object sender, EventArgs e)
        {
            if (sender is ComboBoxEdit element)
            {
                if (element.SelectedItem is string)
                {
                    SetBorderColorDefaultForDropdownDestination();
                }
            }
        }

        private void Km_TextChanged(object sender, EventArgs e)
        {
            var element = sender as TextEdit;

            string text = element.Text;

            SetBorderColorToElementKmTextField(text);
        }

        private void FreigthField_TextChanged(object sender, EventArgs e)
        {
            var element = sender as TextEdit;

            string text = element.Text;

            SetBorderColorToElementFreightValueTextField(text);
        }

        private async void SaveClicked(object sender, EventArgs e)
        {
            if (!ValidationOfFieldsOriginAndDestination())
            {
                await ControlAlert.DefaultAlert("Atenção", "Um ou mais campos precisam de correção. Favor verificar.");                
                return;
            }

            if (!CheckIfIsAllValidToSave())
            {
                await ControlAlert.DefaultAlert("Atenção", "Um ou mais campos precisam de correção. Favor verificar.");                
                return;
            }

            _viewModel.OnSave();
        }

        private async void TapGestureRecognizer_Tapped_GoBack(object sender, TappedEventArgs e)
        {
            View element = sender as Image;

            await ClickAnimation.SetFadeOnElement(element);

            await Navigation.PopAsync();
        }

        #endregion

        #region Actions

        private void SetBorderColorDefaultKmField()
        {
            _viewModel.BorderColorKm = App.GetLightGrayColor();
            _viewModel.BorderColorFocusedKm = App.GetGrayColor();
        }

        private void SetBorderColorDefaultFreightField()
        {
            _viewModel.BorderColorFreightValue = App.GetLightGrayColor();
            _viewModel.BorderColorFocusedFreightValue = App.GetGrayColor();
        }

        private void SetBorderColorToElementKmTextField(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                SetBorderColorDefaultKmField();
                _viewModel.IsValidToSave = true;
                return;
            }

            if (!CheckTheEntrys.IsValidEntry(text, CheckTheEntrys.patternKilometer))
            {
                _viewModel.BorderColorKm = App.GetRedColor();
                _viewModel.BorderColorFocusedKm = App.GetRedColor();
                _viewModel.IsValidToSave = false;
                return;
            }

            SetBorderColorDefaultKmField();

            _viewModel.IsValidToSave = true;
        }

        private void SetBorderColorToElementFreightValueTextField(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                SetBorderColorDefaultFreightField();
                _viewModel.IsValidToSave = true;
                return;
            }

            if (!CheckTheEntrys.IsValidEntry(text, CheckTheEntrys.patternMoney))
            {
                _viewModel.BorderColorFreightValue = App.GetRedColor();
                _viewModel.BorderColorFocusedFreightValue = App.GetRedColor();
                _viewModel.IsValidToSave = false;

                return;
            }

            SetBorderColorDefaultFreightField();
            _viewModel.IsValidToSave = true;
        }

        #region Origin

        private void SetBorderColorForDropdownOriginUf()
        {
            _viewModel.BorderColorOriginUf = App.GetRedColor();
            _viewModel.BorderColorFocusedOriginUf = App.GetRedColor();
        }

        private void SetBorderColorForDropdownOrigin()
        {
            _viewModel.BorderColorOrigin = App.GetRedColor();
            _viewModel.BorderColorFocusedOrigin = App.GetRedColor();
        }

        private void SetBorderColorDefaultForDropdownOriginUf()
        {
            _viewModel.BorderColorOriginUf = App.GetLightGrayColor();
            _viewModel.BorderColorFocusedOriginUf = App.GetGrayColor();
        }

        private void SetBorderColorDefaultForDropdownOrigin()
        {
            _viewModel.BorderColorOrigin = App.GetLightGrayColor();
            _viewModel.BorderColorFocusedOrigin = App.GetGrayColor();
        }

        #endregion

        #region Destination

        private void SetBorderColorDefaultForDropdownDestinationUf()
        {
            _viewModel.BorderColorDestinationUf = App.GetLightGrayColor();
            _viewModel.BorderColorFocusedDestinationUf = App.GetGrayColor();
        }

        private void SetBorderColorDefaultForDropdownDestination()
        {
            _viewModel.BorderColorDestination = App.GetLightGrayColor();
            _viewModel.BorderColorFocusedDestination = App.GetGrayColor();
        }

        private void SetBorderColorForDropdownDestinationUf()
        {
            _viewModel.BorderColorDestinationUf = App.GetRedColor();
            _viewModel.BorderColorFocusedDestinationUf = App.GetRedColor();
        }

        private void SetBorderColorForDropdownDestination()
        {
            _viewModel.BorderColorDestination = App.GetRedColor();
            _viewModel.BorderColorFocusedDestination = App.GetRedColor();
        }

        #endregion

        private bool CheckIfIsAllValidToSave()
        {
            return _viewModel.IsValidToSave;
        }

        private bool ValidationOfFieldsOriginAndDestination()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(_viewModel.SelectedItemOriginUf))
            {
                SetBorderColorForDropdownOriginUf();
                isValid = false;
            }
            else
            {
                SetBorderColorDefaultForDropdownOriginUf();
            }

            if (string.IsNullOrEmpty(_viewModel.SelectedItemOrigin))
            {
                SetBorderColorForDropdownOrigin();
                isValid = false;
            }
            else
            {
                SetBorderColorDefaultForDropdownOrigin();
            }

            if (string.IsNullOrEmpty(_viewModel.SelectedItemDestinationUf))
            {
                SetBorderColorForDropdownDestinationUf();
                isValid = false;
            }
            else
            {
                SetBorderColorDefaultForDropdownDestinationUf();
            }

            if (string.IsNullOrEmpty(_viewModel.SelectedItemDestination))
            {
                SetBorderColorForDropdownDestination();
                isValid = false;
            }
            else
            {
                SetBorderColorDefaultForDropdownDestination();
            }

            return isValid;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        #endregion
    }
}