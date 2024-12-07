using Clinic.ViewModel.Main;
using DAL.Entities;
using System.Windows;

namespace Clinic.View.Windows
{
    public partial class DiagnosisFormWindow : Window
    {
        public DiagnosisFormWindow(Action onRepoChange, Diagnosis? diagnosis = null)
        {
            InitializeComponent();

            DataContext = new DiagnosisFormVM(
                onSuccess: () =>
                {
                    onRepoChange();
                    Close();
                },
                onCancel: () =>
                {
                    Close();
                },
                diagnosis: diagnosis
            );
        }
    }
}
