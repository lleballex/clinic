using System.Windows.Controls;
using Clinic.ViewModel.Main;
using DAL.Entities;

namespace Clinic.View.Controls
{
    public partial class Patients : UserControl
    {
        public Patients()
        {
            InitializeComponent();

            DataContext = new PatientsVM();
        }

        private UserRole _userRole;
        public UserRole UserRole
        {
            get => _userRole;
            set { _userRole = value; (DataContext as PatientsVM).UserRole = value; }
        }
    }
}
