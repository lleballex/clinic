using Clinic.ViewModel.Main;
using System.Windows.Controls;

namespace Clinic.View.Controls
{
    public partial class CurDoctorAppointments : UserControl
    {
        public CurDoctorAppointments()
        {
            InitializeComponent();

            DataContext = new CurDoctorAppointmentsVM();
        }
    }
}
