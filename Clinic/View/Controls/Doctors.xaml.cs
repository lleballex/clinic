using System.Windows.Controls;
using Clinic.ViewModel.Main;

namespace Clinic.View.Controls
{
    public partial class Doctors : UserControl
    {
        public Doctors()
        {
            InitializeComponent();

            DataContext = new DoctorsVM();
        }
    }
}
