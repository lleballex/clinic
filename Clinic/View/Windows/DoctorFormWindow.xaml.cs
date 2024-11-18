using Clinic.ViewModel.Main;
using DAL.Entities;
using System.Windows;

namespace Clinic.View.Windows
{
    public partial class DoctorFormWindow : Window
    {
        private Action OnRepoChange;

        public DoctorFormWindow(DoctorProfile? doctor, Action onRepoChange)
        {
            InitializeComponent();

            OnRepoChange = onRepoChange;

            DataContext = new DoctorFormVM(doctor, OnSubmit, OnCancel);
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
