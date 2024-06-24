﻿using FreightControlMaui.Constants;
using FreightControlMaui.MVVM.Base;
using FreightControlMaui.MVVM.Models;
using FreightControlMaui.Repositories;
using FreightControlMaui.Services.Exportation;

namespace FreightControlMaui.MVVM.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Properties
       
        private readonly UserRepository _userRepository;

        private readonly FreightRepository _freightRepository;

        private UserModel _userLogged;
        public UserModel UserLogged
        {
            get => _userLogged;
            set
            {
                _userLogged = value;
                OnPropertyChanged();
            }
        }

        private string _nameUserLogged = StringConstants.Usuario;
        public string NameUserLogged
        {
            get => _nameUserLogged;
            set
            {
                _nameUserLogged = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public HomeViewModel()
        {
            _userRepository = new();
            _freightRepository = new();
        }

        #region Public Methods

        public async void LoadInfoByUserLogged()
        {
            var user = await _userRepository.GetUserByFirebaseLocalId(App.UserLocalIdLogged);

            if (user != null)
            {
                NameUserLogged = user.Name;
            }
            else
            {
                NameUserLogged = StringConstants.Usuario;
            }
        }

        public async Task<int> CheckIfExistRecordsToNavigate()
        {
            var result = await _freightRepository.GetByDateInitialAndFinal(initial: new DateTime(DateTime.Now.Year, 01, 01),
                                                                            final: new DateTime(DateTime.Now.Year, 12, 31));

            return result.Count;
        }       

        #endregion
    }

}

