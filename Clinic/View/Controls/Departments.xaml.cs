using System.Windows.Controls;
using Clinic.ViewModel.Main;

namespace Clinic.View.Controls
{
    public partial class Departments : UserControl
    {
        public Departments()
        {
            InitializeComponent();

            DataContext = new DepartmentsVM();
        }
    }
}
