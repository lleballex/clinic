using System.Windows.Controls;
using Clinic.ViewModel.Main;

namespace Clinic.View.Controls
{
    public partial class Patients : UserControl
    {
        public Patients()
        {
            InitializeComponent();

            DataContext = new PatientsVM();
        }
    }
}
