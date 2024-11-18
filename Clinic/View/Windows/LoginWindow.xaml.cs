using Clinic.ViewModel.Main;
using DAL.Entities;
using System.Windows;

namespace Clinic.View.Windows
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            DataContext = new LoginVM(OnSuccess);
        }

        private void OnSuccess(User user)
        {
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

            Close();
        }
    }
}
