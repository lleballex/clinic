using Clinic.View.Windows;
using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
using System.Collections.ObjectModel;
using System.Windows;

namespace Clinic.ViewModel.Main
{
    public class HousesVM : BaseVM
    {
        public HousesVM()
        {
            LoadHouses();
        }

        #region store

        private ObservableCollection<House> _houses = [];
        public ObservableCollection<House> Houses
        {
            get => _houses;
            set { _houses = value; OnPropertyChanged(); }
        }

        #endregion

        #region form

        private string _formQuery = "";
        public string FormQuery
        {
            get => _formQuery;
            set { _formQuery = value; OnPropertyChanged(); LoadHouses(); }
        }

        #endregion

        #region commands

        private RelayCommand? _addHouse;
        public RelayCommand AddHouse => _addHouse ??= new RelayCommand(() =>
        {
            (new HouseFormWindow(onRepoChange: LoadHouses)).ShowDialog();
        });

        private RelayCommand? _deleteHouse;
        public RelayCommand DeleteHouse => _deleteHouse ??= new RelayCommand((obj) =>
        {
            if (obj is House house)
            {
                if (MessageBox.Show(
                    "Точно удалить дом? Все связанные с ним объекты будут также удалены", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning
                ) == MessageBoxResult.Yes)
                {
                    Repositories.Instance.Houses.Delete(house);
                    Repositories.Instance.SaveChanges();
                    LoadHouses();
                    MessageBox.Show("Дом успешно удален");
                }
            }
        });

        private RelayCommand? _editHouse;
        public RelayCommand EditHouse => _editHouse??= new RelayCommand((obj) =>
        {
            if (obj is House house)
            {
                (new HouseFormWindow(onRepoChange: LoadHouses, house: house)).ShowDialog();
            }
        });

        #endregion

        private void LoadHouses()
        {
            Houses = new ObservableCollection<House>(Repositories.Instance.Houses.FindAll(query: FormQuery));
        }
    }
}
