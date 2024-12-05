using Clinic.ViewModel.Utils;
using Clinic.View.Windows;
using DAL.Entities;
using DAL.Repositories;
using System.Windows;

namespace Clinic.ViewModel.Base
{
    public class PatientCardVM : BaseVM
    {
        private Repositories Repositories = Repositories.Instance;

        private Action OnRepoChange;

        public PatientCardVM(Patient patient, Action onRepoChange)
        {
            Patient = patient;
            OnRepoChange = onRepoChange;
        }

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
            set { _patient = value; OnPropertyChanged("Patient"); UpdatePatientComputed(); }
        }

        private string _patientBornAtFormatted = "";
        public string PatientBornAtFormatted
        {
            get => _patientBornAtFormatted;
            set { _patientBornAtFormatted = value; OnPropertyChanged(); }
        }

        private void UpdatePatientComputed()
        {
            PatientBornAtFormatted = TimeZoneInfo.ConvertTimeFromUtc(Patient.BornAt, TimeZoneInfo.Local).ToString("dd.MM.yyyy");
        }

        private RelayCommand _goToPatient;
        public RelayCommand GoToPatient
        {
            get
            {
                if (_goToPatient == null)
                {
                    _goToPatient = new RelayCommand(() =>
                    {
                        (new PatientWindow(Patient)).ShowDialog();
                    });
                }
                return _goToPatient;
            }
        }

        private RelayCommand _goToAppointmentForm;
        public RelayCommand GoToAppointmentForm
        {
            get
            {
                if (_goToAppointmentForm == null)
                {
                    _goToAppointmentForm = new RelayCommand(() =>
                    {
                        (new AppointmentFormWindow(Patient, OnRepoChange)).ShowDialog();
                    });
                }
                return _goToAppointmentForm;
            }
        }

        private RelayCommand _editPatient;
        public RelayCommand EditPatient
        {
            get
            {
                if (_editPatient == null)
                {
                    _editPatient = new RelayCommand(() =>
                    {
                        (new PatientFormWindow(Patient, OnRepoChange)).ShowDialog();
                    });
                }
                return _editPatient;
            }
        }

        private RelayCommand _deletePatient;
        public RelayCommand DeletePatient
        {
            get
            {
                if (_deletePatient == null)
                {
                    _deletePatient = new RelayCommand(() =>
                    {
                        if (MessageBox.Show(
                            "Точно удалить пациента?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning
                        ) == MessageBoxResult.Yes)
                        {
                            Repositories.Patients.Delete(Patient);
                            Repositories.SaveChanges();
                            OnRepoChange();
                            MessageBox.Show("Пацент успешно удален");
                        }
                    });
                }
                return _deletePatient;
            }
        }
    }
}
