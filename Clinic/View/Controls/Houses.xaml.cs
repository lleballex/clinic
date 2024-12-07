using System.Windows.Controls;
using Clinic.ViewModel.Main;

namespace Clinic.View.Controls
{
    public partial class Houses : UserControl
    {
        public Houses()
        {
            InitializeComponent();

            DataContext = new HousesVM();
        }
    }
}
