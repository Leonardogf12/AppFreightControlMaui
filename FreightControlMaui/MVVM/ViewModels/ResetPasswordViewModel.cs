using FreightControlMaui.Controls.Alerts;
using FreightControlMaui.MVVM.Base;
using FreightControlMaui.Services.Authentication;

namespace FreightControlMaui.MVVM.ViewModels
{
    public class ResetPasswordViewModel : BaseViewModel
    {
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public ResetPasswordViewModel()
        {
        }

        public async Task ResetPassword()
        {
            IsBusy = true;

            try
            {
                var instanceAuthenticationResetPassword = MyInterfaceFactoryAuthenticationService.CreateInstance();

                await instanceAuthenticationResetPassword.ResetPassword(Email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await ControlAlert.DefaultAlert("Ops", "Ocorreu um erro inesperado. Tente novamente em alguns instantes.");                
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

}

