using Clinic.ViewModel.Main;
using DAL.Entities;
using System.Windows;

namespace Clinic.View.Windows
{
    public partial class DoctorSpecializationFormWindow : Window
    {
        public DoctorSpecializationFormWindow(Action onRepoChange, DoctorSpecialization? specialization = null)
        {
            InitializeComponent();

            DataContext = new DoctorSpecializationFormVM(
                onSuccess: () =>
                {
                    onRepoChange();
                    Close();
                },
                onCancel: () =>
                {
                    Close();
                },
                specialization: specialization
            );
        }
    }
}
