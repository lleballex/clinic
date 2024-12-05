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

        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set { _selectedDate = value; OnPropertyChanged(); LoadAppointments(); }
        }

        #endregion

        private void LoadAppointments()
        {
            Appointments = new ObservableCollection<AppointmentCardVM>(Repositories.Instance.Appointments
                .FindAll(doctorId: Store.Instance.CurUser.Doctor.Id, date: DateOnly.FromDateTime(SelectedDate))
                .Select(i => new AppointmentCardVM(i, AppointmentCardVM.ForRoleEnum.Doctor, LoadAppointments))
                .ToList());
        }
    }
}
