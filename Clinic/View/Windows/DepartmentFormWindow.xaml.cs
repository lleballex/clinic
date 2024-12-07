using Clinic.ViewModel.Main;
using DAL.Entities;
using System.Windows;

namespace Clinic.View.Windows
{
    public partial class DepartmentFormWindow : Window
    {
        public DepartmentFormWindow(Action onRepoChange, Department? department = null)
        {
            InitializeComponent();

            DataContext = new DepartmentFormVM(
                onSuccess: () =>
                {
                    onRepoChange();
                    Close();
                },
                onCancel: () =>
                {
                    Close();
                },
                department: department 
            );
        }
    }
}
