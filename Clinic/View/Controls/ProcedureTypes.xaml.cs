using System.Windows.Controls;
using Clinic.ViewModel.Main;

namespace Clinic.View.Controls
{
    public partial class ProcedureTypes : UserControl
    {
        public ProcedureTypes()
        {
            InitializeComponent();

            DataContext = new ProcedureTypesVM();
        }
    }
}
