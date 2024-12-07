using Clinic.ViewModel.Main;
using DAL.Entities;
using System.Windows;

namespace Clinic.View.Windows
{
    public partial class AppointmentFormWindow : Window
    {
        private Action OnRepoChange;

        public AppointmentFormWindow(Patient patient, Action onRepoChange, Procedure? procedure = null)
        {
            InitializeComponent();

            OnRepoChange = onRepoChange;

            DataContext = new AppointmentFormVM(patient, OnSubmit, OnCancel, procedure);
        }

        private void OnSubmit()
        {
            OnRepoChange();
            Close();
        }

        private void OnCancel()
        {
            Close();
        }
    }
}
