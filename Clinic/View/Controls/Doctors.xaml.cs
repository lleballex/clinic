using System.Windows.Controls;
using Clinic.ViewModel.Main;
using DAL.Entities;

namespace Clinic.View.Controls
{
    public partial class Doctors : UserControl
    {
        public Doctors()
        {
            InitializeComponent();

            DataContext = new DoctorsVM();
        }

        private UserRole _userRole;
        public UserRole UserRole
        { 
            get => _userRole; 
            set { _userRole = value; (DataContext as DoctorsVM).UserRole = value; }
        }
    }
}
