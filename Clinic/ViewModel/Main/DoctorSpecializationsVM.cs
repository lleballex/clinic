using Clinic.View.Windows;
using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
using System.Collections.ObjectModel;
using System.Windows;

namespace Clinic.ViewModel.Main
{
    public class DoctorSpecializationsVM : BaseVM
    {
        public DoctorSpecializationsVM()
        {
            LoadSpecializations();
        }

        #region store

        private ObservableCollection<DoctorSpecialization> _specializations = [];
        public ObservableCollection<DoctorSpecialization> Specializations
        {
            get => _specializations;
            set { _specializations = value; OnPropertyChanged(); }
        }

        #endregion

        #region form

        private string _formQuery = "";
        public string FormQuery
        {
            get => _formQuery;
            set { _formQuery = value; OnPropertyChanged(); LoadSpecializations(); }
        }

        #endregion

        #region commands

        private RelayCommand? _addSpecialization;
        public RelayCommand AddSpecialization => _addSpecialization ??= new RelayCommand(() =>
        {
            (new DoctorSpecializationFormWindow(onRepoChange: LoadSpecializations)).ShowDialog();
        });

        private RelayCommand? _deleteSpecialization;
        public RelayCommand DeleteSpecialization => _deleteSpecialization ??= new RelayCommand((obj) =>
        {
            if (obj is DoctorSpecialization specialization)
            {
                if (MessageBox.Show(
                    "Точно удалить специальность? Все связанные с ней объекты будут также удалены", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning
                ) == MessageBoxResult.Yes)
                {
                    Repositories.Instance.DoctorSpecializations.Delete(specialization);
                    Repositories.Instance.SaveChanges();
                    LoadSpecializations();
                    MessageBox.Show("Специальность успешно удалена");
                }
            }
        });

        private RelayCommand? _editSpecialization;
        public RelayCommand EditSpecialization => _editSpecialization ??= new RelayCommand((obj) =>
        {
            if (obj is DoctorSpecialization specialization)
            {
                (new DoctorSpecializationFormWindow(onRepoChange: LoadSpecializations, specialization: specialization)).ShowDialog();
            }
        });

        #endregion

        private void LoadSpecializations()
        {
            Specializations = new ObservableCollection<DoctorSpecialization>(Repositories.Instance.DoctorSpecializations.FindAll(query: FormQuery));
        }
    }
}
