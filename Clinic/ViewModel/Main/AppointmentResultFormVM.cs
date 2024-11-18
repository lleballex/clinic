using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
using System.Windows;

namespace Clinic.ViewModel.Main
{
    public class AppointmentResultFormVM : BaseVM
    {
        public class ComputedClass
        {
            public string FullName { get; set; } = "";
            public string Datetime { get; set; } = "";
        }

        public class FormClass
        {
            public int Diagnosis { get; set; }
            public string DiagnosisDescription { get; set; } = "";
            public string Recommendations { get; set; } = "";
        }

        private Repositories Repositories = Repositories.Instance;

        private Action OnSuccess;
        private Action OnCancel;

        public AppointmentResultFormVM(Appointment appointment, Action onSuccess, Action onCancel)
        {
            Appointment = appointment;
            OnSuccess = onSuccess;
            OnCancel = onCancel;

            Diagnosises = Repositories.Diagnosises.FindAll();
        }

        private Appointment _appointment;
        public Appointment Appointment
        {
            get => _appointment;
            set { _appointment = value; OnPropertyChanged(); UpdateComputed(); }
        }

        private List<Diagnosis> _diagnosises;
        public List<Diagnosis> Diagnosises
        {
            get => _diagnosises;
            set { _diagnosises = value; OnPropertyChanged(); }
        }

        private ComputedClass _computed;
        public ComputedClass Computed
        {
            get => _computed;
            set { _computed = value; OnPropertyChanged(); }
        }

        private FormClass _form = new FormClass();
        public FormClass Form
        {
            get => _form;
            set { _form = value; OnPropertyChanged(); }
        }

        private void UpdateComputed()
        {
            var computed = new ComputedClass();

            computed.FullName = $"{Appointment.Patient.Surname} {Appointment.Patient.Name} {Appointment.Patient.Patronymic}";

            var localDatetime = TimeZoneInfo.ConvertTimeFromUtc(Appointment.Datetime, TimeZoneInfo.Local);
            computed.Datetime = localDatetime.ToString("dd.MM.yyyy") + " " + localDatetime.ToString("HH:mm") + " - " + (localDatetime.Add(Appointment.Doctor.AppointmentDuration.ToTimeSpan())).ToString("HH:mm");

            Computed = computed;
        }

        private RelayCommand _submit;
        public RelayCommand Submit
        {
            get
            {
                if (_submit == null)
                {
                    _submit = new RelayCommand(() =>
                    {
                        Repositories.AppointmentsResults.Create(new AppointmentResult()
                        {
                            AppointmentId = Appointment.Id,
                            DiagnosisId = Form.Diagnosis,
                            DiagnosisDescription = Form.DiagnosisDescription,
                            Recommendations = Form.Recommendations
                        });
                        Appointment.Status = AppointmentStatus.Finished;
                        Repositories.Appointments.Update(Appointment);
                        Repositories.SaveChanges();
                        MessageBox.Show("Данные успешно сохранены");
                        OnSuccess();
                    });
                }
                return _submit;
            }
        }

        private RelayCommand _cancel;
        public RelayCommand Cancel
        {
            get
            {
                if (_cancel == null)
                {
                    _cancel = new RelayCommand(() => OnCancel());
                }
                return _cancel;
            }
        }
    }
}
