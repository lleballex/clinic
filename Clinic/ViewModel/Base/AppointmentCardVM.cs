using Clinic.ViewModel.Utils;
using Clinic.View.Windows;
using DAL.Entities;

namespace Clinic.ViewModel.Base
{
    public class AppointmentCardVM : BaseVM
    {
        public enum ForRoleEnum
        {
            Doctor, Patient
        }

        public class ComputedClass
        {
            public string Title { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
            public string Status { get; set; }
            public string IsFinished { get; set; }
            public string IsCreated { get; set; }
        }

        private ForRoleEnum ForRole;
        private Action OnAppointmentsChange;

        public AppointmentCardVM(Appointment appointment, ForRoleEnum forRole, Action onAppointmentsChange)
        {
            ForRole = forRole;
            Appointment = appointment;
            OnAppointmentsChange = onAppointmentsChange;
        }

        private Appointment _appointment;
        public Appointment Appointment
        {
            get => _appointment;
            set { _appointment = value; OnPropertyChanged(); UpdateAppointmentComputedData(); }
        }

        private ComputedClass _computed;
        public ComputedClass Computed 
        {
            get => _computed;
            set { _computed= value; OnPropertyChanged(); }
        }

        private void UpdateAppointmentComputedData()
        {
            var computed = new ComputedClass();

            if (ForRole == ForRoleEnum.Doctor)
            {
                computed.Title = "";
            }
            else if (ForRole == ForRoleEnum.Patient)
            {
                computed.Title = Appointment.Doctor.Specialization.Name;
            }

            var localDatetime = TimeZoneInfo.ConvertTimeFromUtc(Appointment.Datetime, TimeZoneInfo.Local);
            computed.Date = localDatetime.ToString("dd.MM.yyyy");
            computed.Time = localDatetime.ToString("HH:mm") + " - " + (localDatetime.Add(Appointment.Doctor.AppointmentDuration.ToTimeSpan())).ToString("HH:mm");

            switch (Appointment.Status)
            {
                case AppointmentStatus.Created:
                    computed.Status = "Запланирован";
                    break;
                case AppointmentStatus.Finished:
                    computed.Status = "Завершен";
                    break;
                case AppointmentStatus.Canceled:
                    computed.Status = "Отменен";
                    break;
            }

            computed.IsFinished = Appointment.Status == AppointmentStatus.Finished ? "Visible" : "Collapsed";
            computed.IsCreated = Appointment.Status == AppointmentStatus.Created ? "Visible" : "Collapsed";

            Computed = computed;
        }

        private RelayCommand _onGoToResultForm;
        public RelayCommand OnGoToResultForm
        {
            get {
                if (_onGoToResultForm == null)
                {
                    _onGoToResultForm = new RelayCommand(() =>
                    {
                        (new AppointmentResultFormWindow(Appointment, OnAppointmentsChange)).ShowDialog();
                    });
                }
                return _onGoToResultForm;
            }
        }
    }
}
