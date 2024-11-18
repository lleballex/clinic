using Clinic.ViewModel.Base;
using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;

namespace Clinic.ViewModel.Main
{
    public class PatientVM : BaseVM
    {
        private Repositories Repositories = Repositories.Instance;

        public PatientVM(Patient patient)
        {
            Patient = patient;
        }

        private Patient _patient;
        public Patient Patient
        {
            get => _patient;
            set { _patient = value; OnPropertyChanged(); LoadAppointments();  }
        }

        private List<AppointmentCardVM> _appointments;
        public List<AppointmentCardVM> Appointments
        {
            get => _appointments;
            set { _appointments = value; OnPropertyChanged(); }
        }

        private void LoadAppointments()
        {
            Appointments = [];
            foreach (var appointment in Repositories.Appointments.FindByPatient(Patient.Id))
            {
                Appointments.Add(new AppointmentCardVM(appointment, AppointmentCardVM.ForRoleEnum.Patient, () => LoadAppointments()));
            }
        }
    }
}
