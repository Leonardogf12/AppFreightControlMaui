﻿using FreightControlMaui.Constants;
using FreightControlMaui.MVVM.Base;
using FreightControlMaui.MVVM.Models;
using FreightControlMaui.Repositories;

namespace FreightControlMaui.MVVM.ViewModels
{
    public class EditUserViewModel : BaseViewModel
    {
        #region Properties

        private readonly UserRepository _userRepository;

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public EditUserViewModel()
        {
            _userRepository = new();
        }

        #region Private Methods

        private UserModel CreateModelToEdit(UserModel oldModel)
        {
            var newModel = oldModel;
            newModel.Name = Name;

            return newModel;
        }

        private UserModel CreateModelToSave()
        {
            return new UserModel
            {
                Name = Name,
                FirebaseLocalId = App.UserLocalIdLogged
            };
        }

        #endregion

        public async Task SetNameForUser()
        {
            IsBusy = true;

            try
            {
                var user = await _userRepository.GetUserByFirebaseLocalId(App.UserLocalIdLogged);

                if (user != null && user.Name != StringConstants.Usuario)
                {
                    await _userRepository.UpdateAsync(CreateModelToEdit(user));
                }
                else
                {
                    await _userRepository.SaveAsync(CreateModelToSave());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }

}
