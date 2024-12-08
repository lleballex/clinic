using Clinic.View.Windows;
using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
using System.Windows;

namespace Clinic.ViewModel.Main
{
    public class LoginVM : BaseVM
    {
        private Action OnSuccess;

        public LoginVM(Action onSuccess)
        {
            OnSuccess = onSuccess;
        }

        #region form

        private string _formEmail = "";
        public string FormEmail
        {
            get => _formEmail;
            set { _formEmail = value; OnPropertyChanged(); }
        }
        
        private string _formPassword = "";
        public string FormPassword
        {
            get => _formPassword;
            set { _formPassword = value; OnPropertyChanged(); }
        }

        #endregion

        #region commands

        private RelayCommand? _onSubmit;
        public RelayCommand OnSubmit => _onSubmit ??= new RelayCommand(() =>
        {
            var user = Repositories.Instance.Users.FindByAuthData(FormEmail, FormPassword);

            if (user == null)
            {
                MessageBox.Show("Почта или пароль не подходят");
            } else
            {
                Store.Instance.CurUser = user;

                switch (user.Role)
                {
                    case UserRole.Admin:
                        (new AdminHomeWindow()).Show();
                        break;
                    case UserRole.Registrar:
                        (new RegistrarHomeWindow()).Show();
                        break;
                    case UserRole.Doctor:
                        (new DoctorHomeWindow()).Show();
                        break;
                }

                OnSuccess();
            }
        }, () => !string.IsNullOrWhiteSpace(FormEmail) && !string.IsNullOrWhiteSpace(FormPassword));

        #endregion
    }
}
