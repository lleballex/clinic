using System.Windows.Controls;
using Clinic.ViewModel.Main;

namespace Clinic.View.Controls
{
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();

            DataContext = new SettingsVM();
        }
    }
}
