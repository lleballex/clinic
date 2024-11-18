using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
using System.Windows;

namespace Clinic.ViewModel.Main
{
    public class LoginVM : BaseVM
    {
        private Repositories Repositories = Repositories.Instance;

        private Action<User> OnSuccess;

        public LoginVM(Action<User> onSuccess)
        {
            OnSuccess = onSuccess;
        }

        private string _email = "";
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged("Email"); }
        }
        
        private string _password = "";
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged("Password"); }
        }

        private RelayCommand? _onSubmit;
        public RelayCommand OnSubmit 
        {
            get
            {
                if (_onSubmit == null) {
                    _onSubmit = new RelayCommand(() =>
                    {
                        var user = Repositories.Users.FindByAuthData(Email, Password);

                        if (user == null)
                        {
                            MessageBox.Show("Почта или пароль не подходят");
                        } else
                        {
                            OnSuccess(user);
                        }
                    });
                }
                return _onSubmit;
            }
        }

    }
}
