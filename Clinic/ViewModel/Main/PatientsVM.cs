using Clinic.ViewModel.Utils;
using Clinic.ViewModel.Base;
using Clinic.View.Windows;
using DAL.Repositories;
using DAL.Entities;
using System.Collections.ObjectModel;

namespace Clinic.ViewModel.Main
{
    public class PatientsVM : BaseVM
    {
        public PatientsVM()
        {
            LoadPatients();
        }

        #region store

        private UserRole _userRole;
        public UserRole UserRole
        {
            get => _userRole;
            set { _userRole = value; OnPropertyChanged(); LoadPatients(); }
        }

        private ObservableCollection<PatientCardVM> _patients = [];
        public ObservableCollection<PatientCardVM> Patients
        {
            get => _patients;
            set { _patients = value; OnPropertyChanged(); }
        }

        #endregion

        #region form

        private string _formQuery = "";
        public string FormQuery
        {
            get => _formQuery;
            set { _formQuery = value; OnPropertyChanged(); LoadPatients(); }
        }

        #endregion

        #region commands

        private RelayCommand? _addPatient;
        public RelayCommand AddPatient => _addPatient ??= new RelayCommand(() =>
        {
            (new PatientFormWindow(onRepoChange: LoadPatients)).ShowDialog();
        });

        #endregion

        private void LoadPatients()
        {
            Patients = new ObservableCollection<PatientCardVM>(Repositories.Instance.Patients
                .FindAll(query: FormQuery)
                .Select(i => new PatientCardVM(patient: i, onRepoChange: LoadPatients) { UserRole = UserRole }));
        }
    }
}
