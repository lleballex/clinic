using System.Windows.Controls;
using Clinic.ViewModel.Main;

namespace Clinic.View.Controls
{
    public partial class Diagnosises : UserControl
    {
        public Diagnosises()
        {
            InitializeComponent();

            DataContext = new DiagnosisesVM();
        }
    }
}
