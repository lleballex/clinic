using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
using System.Windows;

namespace Clinic.ViewModel.Main
{
    public class AppointmentFormVM : BaseVM
    {
        private Repositories Repositories = Repositories.Instance;
        private Patient Patient;
        private Action OnSubmit;
        private Action OnCancel;

        public AppointmentFormVM(Patient patient, Action onSubmit, Action onCancel)
        {
            OnSubmit = onSubmit;
            OnCancel = onCancel;
            Patient = patient;
            Specializations = Repositories.DoctorSpecializations.FindAll();
        }

        private List<DoctorSpecialization> _specializations = [];
        public List<DoctorSpecialization> Specializations
        {
            get => _specializations;
            set { _specializations = value; OnPropertyChanged(); }
        }

        private List<DoctorProfile> _doctors = [];
        public List<DoctorProfile> Doctors 
        {
            get => _doctors;
            set { _doctors = value; OnPropertyChanged(); }
        }

        private List<TimeOnly> _timeSlots = [];
        public List<TimeOnly> TimeSlots
        {
            get => _timeSlots;
            set { _timeSlots = value; OnPropertyChanged(); }
        }

        private int? _formSpecialization;
        public int? FormSpecialization
        {
            get => _formSpecialization;
            set { _formSpecialization = value; OnPropertyChanged(); LoadDoctors(); }
        }

        private int? _formDoctor;
        public int? FormDoctor
        {
            get => _formDoctor;
            set { _formDoctor = value; OnPropertyChanged(); LoadTimeSlots(); }
        }

        private DateTime? _formDate;
        public DateTime? FormDate
        {
            get => _formDate;
            set { _formDate = value; OnPropertyChanged(); LoadTimeSlots(); }
        }

        private TimeOnly? _formTime;
        public TimeOnly? FormTime
        {
            get => _formTime;
            set { _formTime = value; OnPropertyChanged(); }
        }

        private void LoadDoctors()
        {
            FormDoctor = null;
            Doctors = Repositories.DoctorProfiles.FindAll(departmentId: Patient.House.DepartmentId, specializationId: FormSpecialization);
        }

        private void LoadTimeSlots()
        {
            FormTime = null;
            if (FormDoctor.HasValue && FormDate.HasValue)
            {
                TimeSlots = Repositories.DoctorProfiles.FindFreeTimeSlots(FormDoctor.Value, DateOnly.FromDateTime(FormDate.Value));
            }
        }

        private RelayCommand _selectTimeSlot;
        public RelayCommand SelectTimeSlot
        {
            get
            {
                if (_selectTimeSlot == null)
                {
                    _selectTimeSlot = new RelayCommand((obj) =>
                    {
                        if (obj is TimeOnly)
                        {
                            FormTime = (TimeOnly)obj;
                        }
                    });
                }
                return _selectTimeSlot;
            }
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
                        if (!FormDoctor.HasValue || !FormDate.HasValue || !FormTime.HasValue)
                        {
                            return;
                        }

                        Repositories.Appointments.Create(new Appointment()
                        {
                            PatientId = Patient.Id,
                            DoctorId = FormDoctor.Value,
                            Status = AppointmentStatus.Created,
                            Datetime = (FormDate.Value.Date + FormTime.Value.ToTimeSpan()).ToUniversalTime()
                        });
                        Repositories.SaveChanges();
                        MessageBox.Show("Данные успешно сохранены");
                        OnSubmit();
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
