using Clinic.ViewModel.Main;
using DAL.Entities;
using System.Windows;

namespace Clinic.View.Windows
{
    public partial class AppointmentFormWindow : Window
    {
        public AppointmentFormWindow(Patient patient, Action onRepoChange, Procedure? procedure = null)
        {
            InitializeComponent();

            DataContext = new AppointmentFormVM(
                patient: patient,
                onSuccess: () =>
                {
                    onRepoChange();
                    Close();
                },
                onCancel: () =>
                {
                    Close();
                },
                procedure: procedure
            );
        }
    }
}
