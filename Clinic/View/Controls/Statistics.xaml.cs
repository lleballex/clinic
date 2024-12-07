using System.Windows.Controls;
using Clinic.ViewModel.Main;

namespace Clinic.View.Controls
{
    public partial class Statistics : UserControl
    {
        public Statistics()
        {
            InitializeComponent();

            DataContext = new StatisticsVM();
        }
    }
}
