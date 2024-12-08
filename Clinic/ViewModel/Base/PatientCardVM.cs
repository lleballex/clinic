using Clinic.ViewModel.Utils;
using Clinic.View.Windows;
using DAL.Entities;
using DAL.Repositories;
using System.Windows;

namespace Clinic.ViewModel.Base
{
    public class PatientCardVM : BaseVM
    {
        private Action OnRepoChange;

        public PatientCardVM(Patient patient, Action onRepoChange)
        {
            OnRepoChange = onRepoChange;
            Patient = patient;
        }

        #region store

        private UserRole _userRole;
        public UserRole UserRole
        {
            get => _userRole;
            set { _userRole = value; OnPropertyChanged(); }
        }

        private Patient _patient;
        public Patient Patient
        {
            get => _patient;
            set { _patient = value; OnPropertyChanged(); }
        }

        #endregion

        #region commands

        private RelayCommand? _openPatient;
        public RelayCommand OpenPatient => _openPatient ??= new RelayCommand(() =>
        {
            (new PatientWindow(patient: Patient)).ShowDialog();
        });

        private RelayCommand? _makeAppointment;
        public RelayCommand MakeAppointment => _makeAppointment ??= new RelayCommand(() =>
        {
            (new AppointmentFormWindow(patient: Patient, onRepoChange: OnRepoChange)).ShowDialog();
        });

        private RelayCommand? _editPatient;
        public RelayCommand EditPatient => _editPatient ??= new RelayCommand(() =>
        {
            (new PatientFormWindow(patient: Patient, onRepoChange: OnRepoChange)).ShowDialog();
        });

        private RelayCommand? _deletePatient;
        public RelayCommand DeletePatient => _deletePatient ??= new RelayCommand(() =>
        {
            if (MessageBox.Show(
                "Точно удалить пациента? Все связанные с ним объекты также будут удалены", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning
            ) == MessageBoxResult.Yes)
            {
                Repositories.Instance.Patients.Delete(Patient);
                Repositories.Instance.SaveChanges();
                OnRepoChange();
                MessageBox.Show("Пацент успешно удален");
            }
        });

        #endregion
    }
}
