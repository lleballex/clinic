using System.IO;
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

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (DataContext is PatientsVM vm && sender is ScrollViewer scrollViewer)
            {
                vm.ScrollProgress = scrollViewer.VerticalOffset / scrollViewer.ScrollableHeight * 100;

                File.AppendAllText("./logs.txt", $"[{DateTime.Now}] Изменен прогресс: {vm.ScrollProgress}\n");
            }
        }

        private void OnLoad(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is PatientsVM vm)
            {
                vm.OnLoad();
            }
        }
    }
}
