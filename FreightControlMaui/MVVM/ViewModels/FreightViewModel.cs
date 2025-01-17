﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using DevExpress.Maui.Controls;
using FreightControlMaui.Constants;
using FreightControlMaui.Controls.Alerts;
using FreightControlMaui.Models;
using FreightControlMaui.MVVM.Base;
using FreightControlMaui.MVVM.Models;
using FreightControlMaui.MVVM.Views;
using FreightControlMaui.Repositories;

namespace FreightControlMaui.MVVM.ViewModels
{
    public class FreightViewModel : BaseViewModel
    {
        #region Properties
       
        private ObservableCollection<FreightModel> _freightCollection = new();
        public ObservableCollection<FreightModel> FreightCollection
        {
            get => _freightCollection;
            set
            {
                _freightCollection = value;
                OnPropertyChanged();
            }
        }

        private List<FreightModel> _freightListRemainingItems = new();
        public List<FreightModel> FreightListRemainingItems
        {
            get => _freightListRemainingItems;
            set
            {
                _freightListRemainingItems = value;
                OnPropertyChanged();
            }
        }

        private bool _isVisibleTextAddNewFreight = false;
        public bool IsVisibleTextAddNewFreight
        {
            get => _isVisibleTextAddNewFreight;
            set
            {
                _isVisibleTextAddNewFreight = value;
                OnPropertyChanged();
            }
        }

        private FreightModel _selectedFreightToEdit;
        public FreightModel SelectedFreightToEdit
        {
            get => _selectedFreightToEdit;
            set
            {
                _selectedFreightToEdit = value;
                OnPropertyChanged();
            }
        }

        private DateTime _initialDate = DateTime.Now;
        public DateTime InitialDate
        {

            get => _initialDate;
            set
            {
                _initialDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime _finalDate = DateTime.Now;
        public DateTime FinalDate
        {

            get => _finalDate;
            set
            {
                _finalDate = value;
                OnPropertyChanged();
            }
        }

        private bool _isRefreshingView;
        public bool IsRefreshingView
        {
            get => _isRefreshingView;
            set
            {
                _isRefreshingView = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<HeaderButtonFreight> _headerButtonFreightCollection;
        public ObservableCollection<HeaderButtonFreight> HeaderButtonFreightCollection
        {
            get => _headerButtonFreightCollection;
            set
            {
                _headerButtonFreightCollection = value;
                OnPropertyChanged();
            }
        }

        private BottomSheetState _bottomSheetFilterState;
        public BottomSheetState BottomSheetFilterState
        {
            get => _bottomSheetFilterState;
            set
            {
                _bottomSheetFilterState = value;
                OnPropertyChanged();
            }
        }

        private BottomSheetState _bottomSheetExportState;
        public BottomSheetState BottomSheetExportState
        {
            get => _bottomSheetExportState;
            set
            {
                _bottomSheetExportState = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingMoreFreightItems;
        public bool IsLoadingMoreFreightItems
        {
            get => _isLoadingMoreFreightItems;
            set
            {
                _isLoadingMoreFreightItems = value;
                OnPropertyChanged();
            }
        }

        private readonly int _freightQtyItemsPage = 3;

        private readonly FreightRepository _freightRepository;
        private readonly ToFuelRepository _toFuelRepository;

        public ICommand RefreshingCommand;
        public ICommand NewFreightCommand;
        public ICommand FilterFreightCommand;
        public ICommand ExportFreightCommand;
        public ICommand DeleteAllFreightCommand;
        public ICommand LoadMoreItemFreightCommand;

        #endregion

        public FreightViewModel()
        {
            _freightRepository = new();
            _toFuelRepository = new();

            CreateCommands();           
        }

        #region Methods Privates

        private void CreateCommands()
        {
            RefreshingCommand = new Command(async () => await OnRefreshingCommand());
            NewFreightCommand = new Command(OnNewFreightCommand);
            FilterFreightCommand = new Command(OnFilterFreightCommand);
            ExportFreightCommand = new Command(OnExportFreightCommand);
            DeleteAllFreightCommand = new Command(OnDeleteAllFreightCommand);
            LoadMoreItemFreightCommand = new Command(OnLoadMoreItemFreightCommand);
        }

        private async Task OnRefreshingCommand()
        {
            await LoadFreigths();

            IsRefreshingView = false;
        }

        private async void OnNewFreightCommand()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddFreightView());
        }

        private void OnFilterFreightCommand()
        {
            if (FreightCollection.Count == 0) return;

            BottomSheetFilterState = BottomSheetState.HalfExpanded;
        }

        private void OnExportFreightCommand()
        {
            if (FreightCollection.Count == 0) return;

            BottomSheetExportState = BottomSheetState.HalfExpanded;
        }

        private async void OnDeleteAllFreightCommand()
        {
            if (FreightCollection.Count == 0) return;

            var response = await ControlAlert.DefaultAlertActionSheet("Você deseja efetivamente excluir todos os registros?",
                                StringConstants.Cancelar, null, new string[] { StringConstants.ExcluirTudo, StringConstants.Exportar });

            if (response == StringConstants.Cancelar) return;

            if (response == StringConstants.ExcluirTudo)
            {
                var res = await ControlAlert.DefaultAlertWithResponse("Excluir Tudo",
                            "Ao excluir todos os fretes você também eliminará todos os abastecimentos relacionados e eles.", "Aceitar", "Cancelar");
                
                if (!res) return;

                await DeleteAllFreights();
                return;
            }

            OnExportFreightCommand();
        }

        private async void OnLoadMoreItemFreightCommand()
        {           
            if (IsLoadingMoreFreightItems) return;

            try
            {
                if (FreightListRemainingItems.Count > 0 && FreightCollection.Count < FreightListRemainingItems.Count)
                {
                    IsLoadingMoreFreightItems = true;

                    await Task.Delay(700);

                    var remaningItems = FreightListRemainingItems.Skip(FreightCollection.Count).Take(_freightQtyItemsPage).ToList();

                    remaningItems.ForEach(FreightCollection.Add);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsLoadingMoreFreightItems = false;               
            }
        }

        private async Task DeleteAllFreights()
        {
            IsBusy = true;

            try
            {
                await _toFuelRepository.DeleteAllAsync();

                await _freightRepository.DeleteAllAsync();

                FreightCollection.Clear();

                await ControlAlert.DefaultAlert("Sucesso", "Todos os registros foram excluídos com sucesso.");
               
                await OnRefreshingCommand();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await ControlAlert.DefaultAlert("Erro", "Ocorreu um erro durante a exclusão. Por favor, tente novamente.");                
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadFreigths()
        {
            FreightCollection.Clear();

            FreightListRemainingItems = await _freightRepository.GetByUserLocalId(App.UserLocalIdLogged);
           
            var toBeAdded = FreightListRemainingItems.Take(_freightQtyItemsPage).ToList();

            toBeAdded.ForEach(FreightCollection.Add);

            CheckIfThereAreFreightItemsInCollection();
        }

        private bool CheckDatesToFilterData()
        {
            if (FinalDate < InitialDate) return false;

            if (InitialDate > FinalDate) return false;

            return true;
        }

        private void LoadHeaderButtons()
        {
            var list = new List<HeaderButtonFreight>
            {
                new HeaderButtonFreight
                {
                    Text = "Novo",
                    Command = NewFreightCommand,
                },
                new HeaderButtonFreight
                {
                    Text = "Filtrar",
                    Command = FilterFreightCommand,
                },
                new HeaderButtonFreight
                {
                    Text = "Exportar",
                    Command = ExportFreightCommand,
                },
                new HeaderButtonFreight
                {
                    Text = "Excluir Todos",
                    Command = DeleteAllFreightCommand,
                },
            };

            HeaderButtonFreightCollection = new ObservableCollection<HeaderButtonFreight>(list);
        }

        private void CheckIfThereAreFreightItemsInCollection()
        {
            IsVisibleTextAddNewFreight = FreightCollection.Count == 0;
        }

        #endregion

        #region Methods Publics

        public async void OnAppearing()
        {
            LoadHeaderButtons();
            await LoadFreigths();
            CheckIfThereAreFreightItemsInCollection();
        }

        public async Task DeleteFreight(FreightModel model)
        {
            try
            {
                var supplys = await _toFuelRepository.GetAllById(model.Id);

                if (supplys.Count != 0)
                {
                    _ = await _toFuelRepository.DeleteByIdFreightAsync(model.Id);
                }

                var result = await _freightRepository.DeleteAsync(model);

                if (result > 0)
                {
                    FreightCollection.Remove(model);

                    await ControlAlert.DefaultAlert("Sucesso", "Item excluido com sucesso!");                    
                }
                else
                {
                    await ControlAlert.DefaultAlert("Ops",
                        "Parece que ocorreu um problema ao tentar excluir este Frete ou seus abastecimentos. Favor conferir.");                                            

                    return;
                }

                CheckIfThereAreFreightItemsInCollection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task FilterFreights()
        {           
            if (!CheckDatesToFilterData())
            {
                await ControlAlert.DefaultAlert("Ops",
                    "A data final deve ser maior ou igual a data inicial. Favor verificar.");
               
                return;
            }

            var dataFiltered = await _freightRepository.GetByDateInitialAndFinal(initial: InitialDate, final: FinalDate);

            if (dataFiltered.Count == 0)
            {
                await ControlAlert.DefaultAlert("Filtro",
                    "Nenhum registro foi encontrado para o período informado. Favor verificar as datas informadas.");
                
                return;
            }

            FreightCollection.Clear();
            FreightListRemainingItems.Clear();

            FreightCollection = new ObservableCollection<FreightModel>(dataFiltered);
        }

        public async Task<List<FreightModel>> GetFreightsToExport()
        {
            if (!CheckDatesToFilterData())
            {
                await ControlAlert.DefaultAlert("Ops",
                    "A data final deve ser maior ou igual a data inicial. Favor verificar.");
               
                return null;
            }

            var dataFiltered = await _freightRepository.GetByDateInitialAndFinal(initial: InitialDate, final: FinalDate);

            if (dataFiltered.Count == 0)
            {
                await ControlAlert.DefaultAlert("Filtro",
                    "Nenhum registro foi encontrado para o período informado.");
              
                return null;
            }

            return dataFiltered;
        }

        public async Task<List<ToFuelModel>> GetFreightSupplies(FreightModel item)
        {
            var list = await _toFuelRepository.GetAllById(item.Id);

            return list.ToList();
        }

        #endregion              
    }
}