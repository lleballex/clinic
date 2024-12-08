using Clinic.Model;
using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
using System.Collections.ObjectModel;
using System.Windows;

namespace Clinic.ViewModel.Main
{
    public class DoctorFormVM : BaseVM
    {
        private DoctorProfile? Doctor;
        private Action OnSuccess;
        private Action OnCancel;

        public DoctorFormVM(DoctorProfile? doctor, Action onSuccess, Action onCancel)
        {
            Doctor = doctor;
            OnSuccess = onSuccess;
            OnCancel = onCancel;

            if (Doctor != null)
            {
                FormName = Doctor.User.Name;
                FormSurname = Doctor.User.Surname;
                FormPatronymic = Doctor.User.Patronymic;
                FormEmail = Doctor.User.Email;
                FormPassword = Doctor.User.Password;
                FormGender = Doctor.User.Gender;
                FormBornAt = Doctor.User.BornAt;
                FormSpecializationId = Doctor.SpecializationId;
                FormDepartmentId = Doctor.DepartmentId;
                FormAppointmentDurationHours = Doctor.AppointmentDuration.Hour.ToString();
                FormAppointmentDurationMinutes = Doctor.AppointmentDuration.Minute.ToString();
                WindowTitle = "Изменение врача";
            }

            var weekDays = new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
            FormWorkDays = new ObservableCollection<DoctorWorkDayFormModel>(weekDays
                .Select(weekDay =>
                {
                    var found = Doctor?.WorkDays.Where(i => i.WeekDay == weekDay).FirstOrDefault();
                    return new DoctorWorkDayFormModel()
                    {
                        WeekDay = weekDay,
                        IsWeekend = found == null,
                        StartedAtHours = found?.StartedAt.Hour.ToString() ?? "",
                        StartedAtMinutes = found?.StartedAt.Minute.ToString() ?? "",
                        EndedAtHours = found?.EndedAt.Hour.ToString() ?? "",
                        EndedAtMinutes = found?.EndedAt.Minute.ToString() ?? "" 
                    };
                })
                .ToList()
            );

            LoadSpecializations();
            LoadDepartments();
        }

        #region store

        private ObservableCollection<DoctorSpecialization> _specializations = [];
        public ObservableCollection<DoctorSpecialization> Specializations
        {
            get => _specializations;
            set { _specializations = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Department> _departments = [];
        public ObservableCollection<Department> Departments 
        {
            get => _departments;
            set { _departments = value; OnPropertyChanged(); }
        }

        #endregion

        #region computed

        private string _windowTitle = "Добавление врача";
        public string WindowTitle
        {
            get => _windowTitle;
            set { _windowTitle = value; OnPropertyChanged(); }
        }

        #endregion

        #region form

        private string _formName = "";
        public string FormName
        {
            get => _formName;
            set { _formName = value; OnPropertyChanged(); }
        }

        private string _formSurname = "";
        public string FormSurname
        {
            get => _formSurname;
            set { _formSurname = value; OnPropertyChanged(); }
        }

        private string _formPatronymic = "";
        public string FormPatronymic
        {
            get => _formPatronymic;
            set { _formPatronymic = value; OnPropertyChanged(); }
        }

        private string _formEmail = "";
        public string FormEmail
        {
            get => _formEmail;
            set { _formEmail = value; OnPropertyChanged(); }
        }

        private string _formPassword = "";
        public string FormPassword
        {
            get => _formPassword;
            set { _formPassword = value; OnPropertyChanged(); }
        }

        private UserGender? _formGender;
        public UserGender? FormGender
        {
            get => _formGender;
            set { _formGender = value; OnPropertyChanged(); }
        }

        private DateTime? _formBornAt;
        public DateTime? FormBornAt
        {
            get => _formBornAt;
            set { _formBornAt = value; OnPropertyChanged(); }
        }

        private int? _formSpecializationId;
        public int? FormSpecializationId
        {
            get => _formSpecializationId;
            set { _formSpecializationId = value; OnPropertyChanged(); }
        }

        private int? _formDepartmentId;
        public int? FormDepartmentId
        {
            get => _formDepartmentId;
            set { _formDepartmentId = value; OnPropertyChanged(); }
        }

        private string _formAppointmentDurationHours = "";
        public string FormAppointmentDurationHours 
        {
            get => _formAppointmentDurationHours;
            set { _formAppointmentDurationHours = value; OnPropertyChanged(); }
        }

        private string _formAppointmentDurationMinutes = "";
        public string FormAppointmentDurationMinutes
        {
            get => _formAppointmentDurationMinutes;
            set { _formAppointmentDurationMinutes = value; OnPropertyChanged(); }
        }

        private ObservableCollection<DoctorWorkDayFormModel> _formWorkDays = [];
        public ObservableCollection<DoctorWorkDayFormModel> FormWorkDays
        {
            get => _formWorkDays;
            set { _formWorkDays = value; OnPropertyChanged(); }
        }

        #endregion

        #region commands

        private RelayCommand? _makeWorkDayNotWeekend;
        public RelayCommand MakeWorkDayNotWeekend => _makeWorkDayNotWeekend ??= new RelayCommand((obj) =>
        {
            if (obj is DoctorWorkDayFormModel workDay)
            {
                var idx = FormWorkDays.IndexOf(workDay);
                if (idx != -1)
                {
                    FormWorkDays[idx] = new DoctorWorkDayFormModel()
                    {
                        WeekDay = workDay.WeekDay,
                        IsWeekend = false,
                    };
                }
            }
        });

        private RelayCommand? _makeWorkDayWeekend;
        public RelayCommand MakeWorkDayWeekend => _makeWorkDayWeekend ??= new RelayCommand((obj) =>
        {
            if (obj is DoctorWorkDayFormModel workDay)
            {
                var idx = FormWorkDays.IndexOf(workDay);
                if (idx != -1)
                {
                    FormWorkDays[idx] = new DoctorWorkDayFormModel()
                    {
                        WeekDay = workDay.WeekDay,
                        IsWeekend = true,
                    };
                }
            }
        });

        private RelayCommand? _submit;
        public RelayCommand Submit => _submit ??= new RelayCommand(() =>
        {
            var userData = new User()
            {
                Email = FormEmail,
                Password = FormPassword,
                Name = FormName,
                Surname = FormSurname,
                Patronymic = FormPatronymic,
                Gender = FormGender!.Value,
                BornAt = FormBornAt!.Value.ToUniversalTime(),
            };

            var doctorData = new DoctorProfile()
            {
                SpecializationId = FormSpecializationId!.Value,
                DepartmentId = FormDepartmentId,
                AppointmentDuration = new TimeOnly(hour: int.Parse(FormAppointmentDurationHours), minute: int.Parse(FormAppointmentDurationMinutes))
            };

            // TODO: make all updates in one transaction 
            Repositories.Instance.UseTransaction(() =>
            {
                if (Doctor == null)
                {
                    Repositories.Instance.Users.Create(userData);
                    Repositories.Instance.SaveChanges();
                    doctorData.UserId = userData.Id;
                    Repositories.Instance.DoctorProfiles.Create(doctorData);
                    Repositories.Instance.SaveChanges();
                } else
                {
                    userData.Id = Doctor.User.Id;
                    doctorData.Id = Doctor.Id;
                    doctorData.UserId = userData.Id;
                    Repositories.Instance.DoctorProfiles.Update(doctorData);
                    Repositories.Instance.SaveChanges();
                    Repositories.Instance.Users.Update(userData);
                    Repositories.Instance.SaveChanges();
                }
            });

            foreach (var workDay in FormWorkDays)
            {
                var found = Doctor?.WorkDays.Where(i => i.WeekDay == workDay.WeekDay).FirstOrDefault();

                if (!workDay.IsWeekend)
                {
                    var workDayData = new DoctorWorkDay()
                    {
                        DoctorId = doctorData.Id,
                        WeekDay = workDay.WeekDay,
                        StartedAt = new TimeOnly(hour: int.Parse(workDay.StartedAtHours), minute: int.Parse(workDay.StartedAtMinutes)),
                        EndedAt = new TimeOnly(hour: int.Parse(workDay.EndedAtHours), minute: int.Parse(workDay.EndedAtMinutes)),
                    };

                    if (found == null)
                    {
                        Repositories.Instance.DoctorWorkDays.Create(workDayData);
                        Repositories.Instance.SaveChanges();
                    }
                    else
                    {
                        workDayData.Id = found.Id;
                        Repositories.Instance.DoctorWorkDays.Update(workDayData);
                        Repositories.Instance.SaveChanges();
                    }
                }
                else if (found != null)
                {
                    Repositories.Instance.DoctorWorkDays.Delete(found);
                    Repositories.Instance.SaveChanges();
                }
            }

            MessageBox.Show("Данные успешно сохранены");
            OnSuccess();
        }, () => !string.IsNullOrWhiteSpace(FormName) && !string.IsNullOrWhiteSpace(FormSurname) && !string.IsNullOrWhiteSpace(FormEmail) &&
                 !string.IsNullOrWhiteSpace(FormPassword) && FormGender != null && FormBornAt != null && FormSpecializationId != null &&
                 !string.IsNullOrWhiteSpace(FormAppointmentDurationHours) && int.TryParse(FormAppointmentDurationHours, out int _hours) && _hours >= 0 &&
                 !string.IsNullOrWhiteSpace(FormAppointmentDurationMinutes) && int.TryParse(FormAppointmentDurationMinutes, out int _minutes) && _minutes >= 0 && _minutes < 60 &&
                 FormWorkDays.All(i => i.IsWeekend || (
                    int.TryParse(i.StartedAtHours, out _hours) && _hours >= 0 && _hours < 24 &&
                    int.TryParse(i.StartedAtMinutes, out _minutes) && _minutes >= 0 && _minutes < 60 &&
                    int.TryParse(i.EndedAtHours, out _hours) && _hours >= 0 && _hours < 24 &&
                    int.TryParse(i.EndedAtMinutes, out _minutes) && _minutes >= 0 && _minutes < 60
                 )));

        private RelayCommand? _cancel;
        public RelayCommand Cancel => _cancel ??= new RelayCommand(OnCancel);

        #endregion

        private void LoadSpecializations()
        {
            Specializations = new ObservableCollection<DoctorSpecialization>(Repositories.Instance.DoctorSpecializations.FindAll());
        }

        private void LoadDepartments()
        {
            Departments = new ObservableCollection<Department>(Repositories.Instance.Departments.FindAll());
        }
    }
}
