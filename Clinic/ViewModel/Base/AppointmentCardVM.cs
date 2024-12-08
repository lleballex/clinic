using Clinic.ViewModel.Utils;
using Clinic.View.Windows;
using DAL.Entities;
using System.Collections.ObjectModel;

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
            public string HasProcedures { get; set; }
        }

        private ForRoleEnum ForRole;
        private Action OnRepoChange;

        public AppointmentCardVM(Appointment appointment, ForRoleEnum forRole, Action onRepoChange)
        {
            OnRepoChange = onRepoChange;
            ForRole = forRole;
            Appointment = appointment;
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
            set { _computed = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ProcedureCardVM> _procedures;
        public ObservableCollection<ProcedureCardVM> Procedures
        {
            get => _procedures;
            set { _procedures = value; OnPropertyChanged(); }
        }

        private void UpdateAppointmentComputedData()
        {
            var computed = new ComputedClass();

            if (ForRole == ForRoleEnum.Doctor)
            {
                computed.Title = $"{Appointment.Patient.Surname} {Appointment.Patient.Name} {Appointment.Patient.Patronymic}";
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
            computed.HasProcedures = Appointment.AssignedProcedures.Count > 0 ? "Visible" : "Collapsed";

            Computed = computed;

            Procedures = new ObservableCollection<ProcedureCardVM>(Appointment.AssignedProcedures.Select(i => new ProcedureCardVM(i, Appointment.Patient, OnRepoChange)));
        }

        private RelayCommand _onGoToResultForm;
        public RelayCommand OnGoToResultForm
        {
            get {
                if (_onGoToResultForm == null)
                {
                    _onGoToResultForm = new RelayCommand(() =>
                    {
                        (new AppointmentResultFormWindow(Appointment, OnRepoChange)).ShowDialog();
                    });
                }
                return _onGoToResultForm;
            }
        }
    }
}
