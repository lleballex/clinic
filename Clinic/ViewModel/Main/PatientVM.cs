using Clinic.ViewModel.Base;
using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
using System.Collections.ObjectModel;

namespace Clinic.ViewModel.Main
{
    public class PatientVM : BaseVM
    {
        public PatientVM(Patient patient)
        {
            Patient = patient;
        }

        #region store

        private Patient _patient;
        public Patient Patient
        {
            get => _patient;
            set { _patient = value; OnPropertyChanged(); LoadAppointments();  }
        }

        private ObservableCollection<AppointmentCardVM> _appointments;
        public ObservableCollection<AppointmentCardVM> Appointments
        {
            get => _appointments;
            set { _appointments = value; OnPropertyChanged(); }
        }

        #endregion

        private void LoadAppointments()
        {
            Appointments = new ObservableCollection<AppointmentCardVM>(Repositories.Instance.Appointments
                .FindAll(patientId: Patient.Id, isProcedure: false)
                .Select(i => new AppointmentCardVM(
                    appointment: i,
                    forRole: AppointmentCardVM.ForRoleEnum.Patient,
                    onRepoChange: LoadAppointments
                ))
                .ToList());
        }
    }
}
