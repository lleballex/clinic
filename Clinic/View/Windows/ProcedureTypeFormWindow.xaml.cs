using Clinic.ViewModel.Main;
using DAL.Entities;
using System.Windows;

namespace Clinic.View.Windows
{
    public partial class ProcedureTypeFormWindow : Window
    {
        public ProcedureTypeFormWindow(Action onRepoChange, ProcedureType? type = null)
        {
            InitializeComponent();

            DataContext = new ProcedureTypeFormVM(
                onSuccess: () =>
                {
                    onRepoChange();
                    Close();
                },
                onCancel: () =>
                {
                    Close();
                },
                type: type
            );
        }
    }
}
