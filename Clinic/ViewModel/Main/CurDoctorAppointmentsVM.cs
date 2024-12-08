using Clinic.ViewModel.Base;
using Clinic.ViewModel.Utils;
using DAL.Repositories;
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
            set { _appointments = value; OnPropertyChanged(); }
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
                .FindAll(doctorId: Store.Instance.CurUser.Doctor.Id, date: DateOnly.FromDateTime(FormDate))
                .Select(i => new AppointmentCardVM(i, AppointmentCardVM.ForRoleEnum.Doctor, LoadAppointments))
                .ToList());
        }
    }
}
