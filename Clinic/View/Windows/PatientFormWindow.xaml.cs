using Clinic.ViewModel.Main;
using DAL.Entities;
using System.Windows;

namespace Clinic.View.Windows
{
    public partial class PatientFormWindow : Window
    {
        public PatientFormWindow(Action onRepoChange, Patient? patient = null)
        {
            InitializeComponent();

            DataContext = new PatientFormVM(
                onSuccess: () =>
                {
                    onRepoChange();
                    Close();
                },
                onCancel: () =>
                {
                    Close();
                },
                patient: patient
            );
        }
    }
}
