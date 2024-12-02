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

        // chmi

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (DataContext is PatientsVM vm && sender is ScrollViewer scrollViewer)
            {
                vm.ScrollProgress = scrollViewer.VerticalOffset / scrollViewer.ScrollableHeight * 100;
            }
        }
    }
}
