using System.Windows.Controls;
using Clinic.ViewModel.Main;

namespace Clinic.View.Controls
{
    public partial class DoctorSpecializations : UserControl
    {
        public DoctorSpecializations()
        {
            InitializeComponent();

            DataContext = new DoctorSpecializationsVM();
        }
    }
}
