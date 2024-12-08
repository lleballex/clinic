using Clinic.ViewModel.Utils;
using Clinic.ViewModel.Base;
using DAL.Repositories;
using Clinic.View.Windows;
using DAL.Entities;
using System.Collections.ObjectModel;

namespace Clinic.ViewModel.Main
{
    public class DoctorsVM : BaseVM
    {
        public DoctorsVM()
        {
            LoadDoctors(); 
        }

        #region store

        private UserRole _userRole;
        public UserRole UserRole
        {
            get => _userRole;
            set { _userRole = value; OnPropertyChanged(); LoadDoctors(); }
        }

        private ObservableCollection<DoctorCardVM> _doctors = [];
        public ObservableCollection<DoctorCardVM> Doctors
        {
            get => _doctors;
            set { _doctors = value; OnPropertyChanged(); }
        }

        #endregion

        #region form

        private string _formQuery = "";
        public string FormQuery
        {
            get => _formQuery;
            set { _formQuery = value; OnPropertyChanged(); LoadDoctors(); }
        }

        #endregion

        #region commands

        private RelayCommand? _addDoctor;
        public RelayCommand AddDoctor => _addDoctor ??= new RelayCommand(() =>
        {
            (new DoctorFormWindow(onRepoChange: LoadDoctors)).ShowDialog();
        });

        #endregion

        private void LoadDoctors()
        {
            Doctors = new ObservableCollection<DoctorCardVM>(Repositories.Instance.DoctorProfiles
                .FindAll(query: FormQuery)
                .Select(i => new DoctorCardVM(doctor: i, onRepoChange: LoadDoctors) { UserRole = UserRole }));
        }
    }
}
