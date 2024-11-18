using System.Windows;
using Clinic.ViewModel.Main;

namespace Clinic.View.Windows
{
    public partial class AppointmentResultFormWindow : Window
    {
        private Action OnAppointmentsChange;

        public AppointmentResultFormWindow(DAL.Entities.Appointment appointment, Action onAppointmentsChange)
        {
            InitializeComponent();

            OnAppointmentsChange = onAppointmentsChange;

            DataContext = new AppointmentResultFormVM(appointment, OnSuccess, OnCancel);
        }

        private void OnSuccess()
        {
            OnAppointmentsChange();
            Close(); 
        }

        private void OnCancel()
        {
            Close();
        }
    }
}
