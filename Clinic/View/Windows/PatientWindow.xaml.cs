using System.Windows;
using DAL.Entities;
using Clinic.ViewModel.Main;

namespace Clinic.View.Windows
{
    public partial class PatientWindow : Window
    {
        public PatientWindow(Patient patient)
        {
            InitializeComponent();

            DataContext = new PatientVM(patient);
        }
    }
}
