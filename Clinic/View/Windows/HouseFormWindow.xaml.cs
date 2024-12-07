using Clinic.ViewModel.Main;
using DAL.Entities;
using System.Windows;

namespace Clinic.View.Windows
{
    public partial class HouseFormWindow : Window
    {
        public HouseFormWindow(Action onRepoChange, House? house = null)
        {
            InitializeComponent();

            DataContext = new HouseFormVM(
                onSuccess: () =>
                {
                    onRepoChange();
                    Close();
                },
                onCancel: () =>
                {
                    Close();
                },
                house: house 
            );
        }
    }
}
