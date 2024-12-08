using Clinic.ViewModel.Base;
using Clinic.ViewModel.Utils;
using DAL.Repositories;
using DAL.Entities;
using System.Collections.ObjectModel;

namespace Clinic.ViewModel.Main
{
    public class CurDoctorAppointmentsVM : BaseVM
    {
        public CurDoctorAppointmentsVM()
        {
            LoadAppointments();
        }

        #region store

        private ObservableCollection<AppointmentCardVM> _appointments = [];
        public ObservableCollection<AppointmentCardVM> Appointments
        {
            get => _appointments;
            set { _appointments = value; OnPropertyChanged(); OnAppointmentsChange(); }
        }

        #endregion

        #region computed

        private bool _appointmentsExist = false;
        public bool AppointmentsExist
        {
            get => _appointmentsExist;
            set { _appointmentsExist = value; OnPropertyChanged(); }
        }

        #endregion

        #region form

        private DateTime _formDate = DateTime.Now;
        public DateTime FormDate
        {
            get => _formDate;
            set { _formDate = value; OnPropertyChanged(); LoadAppointments(); }
        }

        #endregion

        private void LoadAppointments()
        {
            Appointments = new ObservableCollection<AppointmentCardVM>(Repositories.Instance.Appointments
                .FindAll(doctorId: Store.Instance.CurUser.Doctor.Id, date: DateOnly.FromDateTime(FormDate), status: AppointmentStatus.Created)
                .Select(i => new AppointmentCardVM(i, AppointmentCardVM.ForRoleEnum.Doctor, LoadAppointments))
                .ToList());
        }

        private void OnAppointmentsChange()
        {
            AppointmentsExist = Appointments.Count > 0;
        }
    }
}
