using Clinic.View.Windows;
using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
using System.Collections.ObjectModel;
using System.Windows;

namespace Clinic.ViewModel.Main
{
    public class StreetsVM : BaseVM
    {
        public StreetsVM()
        {
            LoadStreets();
        }

        #region store

        private ObservableCollection<Street> _streets = [];
        public ObservableCollection<Street> Streets
        {
            get => _streets;
            set { _streets = value; OnPropertyChanged(); }
        }

        #endregion

        #region form

        private string _formQuery = "";
        public string FormQuery
        {
            get => _formQuery;
            set { _formQuery = value; OnPropertyChanged(); LoadStreets(); }
        }

        #endregion

        #region commands

        private RelayCommand? _addStreet;
        public RelayCommand AddStreet => _addStreet ??= new RelayCommand(() =>
        {
            (new StreetFormWindow(onRepoChange: LoadStreets)).ShowDialog();
        });

        private RelayCommand? _deleteStreet;
        public RelayCommand DeleteStreet => _deleteStreet ??= new RelayCommand((obj) =>
        {
            if (obj is Street street)
            {
                if (MessageBox.Show(
                    "Точно удалить улицу? Все связанные с ней объекты будут также удалены", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning
                ) == MessageBoxResult.Yes)
                {
                    Repositories.Instance.Streets.Delete(street);
                    Repositories.Instance.SaveChanges();
                    LoadStreets();
                    MessageBox.Show("Улица успешно удалена");
                }
            }
        });

        private RelayCommand? _editStreet;
        public RelayCommand EditStreet => _editStreet ??= new RelayCommand((obj) =>
        {
            if (obj is Street street)
            {
                (new StreetFormWindow(onRepoChange: LoadStreets, street: street)).ShowDialog();
            }
        });

        #endregion

        private void LoadStreets()
        {
            Streets = new ObservableCollection<Street>(Repositories.Instance.Streets.FindAll(query: FormQuery));
        }
    }
}
