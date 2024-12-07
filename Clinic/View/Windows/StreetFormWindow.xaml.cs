using Clinic.ViewModel.Main;
using DAL.Entities;
using System.Windows;

namespace Clinic.View.Windows
{
    public partial class StreetFormWindow : Window
    {
        public StreetFormWindow(Action onRepoChange, Street? street = null)
        {
            InitializeComponent();

            DataContext = new StreetFormVM(
                onSuccess: () =>
                {
                    onRepoChange();
                    Close();
                },
                onCancel: () =>
                {
                    Close();
                },
                street: street
            );
        }
    }
}
