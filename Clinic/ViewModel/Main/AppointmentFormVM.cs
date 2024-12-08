using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
using System.Collections.ObjectModel;
using System.Windows;

namespace Clinic.ViewModel.Main
{
    public class AppointmentFormVM : BaseVM
    {
        private Patient Patient;
        private Procedure? Procedure;
        private Action OnSuccess;
        private Action OnCancel;

        public AppointmentFormVM(Patient patient, Action onSuccess, Action onCancel, Procedure? procedure = null)
        {
            OnSuccess = onSuccess;
            OnCancel = onCancel;
            Patient = patient;
            Procedure = procedure;

            if (Procedure != null)
            {
                WindowTitle = "Запись пациента на процедуру";
            }

            LoadSpecializations();
        }

        #region computed

        private string _windowTitle = "Запись пациента на прием";
        public string WindowTitle
        {
            get => _windowTitle;
            set { _windowTitle = value; OnPropertyChanged(); }
        }

        #endregion

        #region store

        private ObservableCollection<DoctorSpecialization> _specializations = [];
        public ObservableCollection<DoctorSpecialization> Specializations
        {
            get => _specializations;
            set { _specializations = value; OnPropertyChanged(); }
        }

        private ObservableCollection<DoctorProfile> _doctors = [];
        public ObservableCollection<DoctorProfile> Doctors 
        {
            get => _doctors;
            set { _doctors = value; OnPropertyChanged(); }
        }

        private ObservableCollection<TimeOnly> _timeSlots = [];
        public ObservableCollection<TimeOnly> TimeSlots
        {
            get => _timeSlots;
            set { _timeSlots = value; OnPropertyChanged(); }
        }

        #endregion

        #region form

        private int? _formSpecializationId;
        public int? FormSpecializationId
        {
            get => _formSpecializationId;
            set { _formSpecializationId = value; OnPropertyChanged(); LoadDoctors(); }
        }

        private int? _formDoctorId;
        public int? FormDoctorId
        {
            get => _formDoctorId;
            set { _formDoctorId = value; OnPropertyChanged(); LoadTimeSlots(); }
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

        #endregion

        #region commands

        private RelayCommand? _selectTimeSlot;
        public RelayCommand SelectTimeSlot => _selectTimeSlot ??= new RelayCommand((obj) =>
        {
            if (obj is TimeOnly)
            {
                FormTime = (TimeOnly)obj;
            }
        });

        private RelayCommand? _submit;
        public RelayCommand Submit => _submit ??= new RelayCommand(() =>
        {
            Repositories.Instance.Appointments.Create(new Appointment()
            {
                PatientId = Patient.Id,
                DoctorId = FormDoctorId!.Value,
                Status = AppointmentStatus.Created,
                Datetime = (FormDate!.Value.Date + FormTime!.Value.ToTimeSpan()).ToUniversalTime(),
                ProcedureId = Procedure?.Id
            });
            Repositories.Instance.SaveChanges();
            MessageBox.Show("Данные успешно сохранены");
            OnSuccess();
        }, () => FormDoctorId != null && FormDate != null && FormTime != null);

        private RelayCommand? _cancel;
        public RelayCommand Cancel => _cancel ??= new RelayCommand(OnCancel);

        #endregion

        private void LoadSpecializations()
        {
            Specializations = new ObservableCollection<DoctorSpecialization>(Repositories.Instance.DoctorSpecializations.FindAll());
        }

        private void LoadDoctors()
        {
            Doctors = new ObservableCollection<DoctorProfile>(Repositories.Instance.DoctorProfiles.FindAll(
                departmentId: Patient.House.DepartmentId,
                specializationId: FormSpecializationId
            ));
        }

        private void LoadTimeSlots()
        {
            FormTime = null;
            if (FormDoctorId != null && FormDate != null)
            {
                TimeSlots = new ObservableCollection<TimeOnly>(Repositories.Instance.DoctorProfiles.FindFreeTimeSlots(
                    doctorId: FormDoctorId.Value,
                    date: DateOnly.FromDateTime(FormDate.Value)
                ));
            }
        }
    }
}
