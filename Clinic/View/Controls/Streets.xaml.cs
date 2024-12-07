using System.Windows.Controls;
using Clinic.ViewModel.Main;

namespace Clinic.View.Controls
{
    public partial class Streets : UserControl
    {
        public Streets()
        {
            InitializeComponent();

            DataContext = new StreetsVM();
        }
    }
}
