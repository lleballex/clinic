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
        private Repositories Repositories = Repositories.Instance;

        private DoctorProfile? Doctor;
        private Action OnSubmit;
        private Action OnCancel;

        public DoctorFormVM(DoctorProfile? doctor, Action onSubmit, Action onCancel)
        {
            Doctor = doctor;
            OnSubmit = onSubmit;
            OnCancel = onCancel;

            if (doctor != null)
            {
                DoctorFormModel = new DoctorFormModel()
                {
                    Email = doctor.User.Email,
                    Password = doctor.User.Password,
                    Name = doctor.User.Name,
                    Surname = doctor.User.Surname,
                    Patronymic = doctor.User.Patronymic,
                    Gender = doctor.User.Gender,
                    BornAt = doctor.User.BornAt,
                    AppointmentDuration = doctor.AppointmentDuration,
                    SpecializationId = doctor.SpecializationId,
                    DepartmentId = doctor.DepartmentId
                };

                WindowTitle = "Изменение врача";
            } else
            {
                WindowTitle = "Добавление врача";
            }

            FormWorkDays = [];
            var weekDays = new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
            for (int i = 0; i < 7; i++)
            {
                FormWorkDays.Add(new DoctorFormModelWorkDay()
                {
                    WeekDay = weekDays[i],
                    IsWeekend = true
                });
            }

            LoadSpecializations();
            LoadDepartments();
        }

        private ObservableCollection<DoctorFormModelWorkDay> _formWorkDays;
        public ObservableCollection<DoctorFormModelWorkDay> FormWorkDays
        {
            get => _formWorkDays;
            set { _formWorkDays = value; OnPropertyChanged(); }
        }

        private string _windowTitle;
        public string WindowTitle
        {
            get => _windowTitle;
            set { _windowTitle = value; OnPropertyChanged(); }
        }

        private string _selectedHours;
        public string SelectedHours
        {
            get => _selectedHours;
            set { _selectedHours = value; OnPropertyChanged(); UpdateDoctorFormModelAppointmentDuration(); }
        }

        private string _selectedMinutes;
        public string SelectedMinutes
        {
            get => _selectedMinutes;
            set { _selectedMinutes = value; OnPropertyChanged(); UpdateDoctorFormModelAppointmentDuration(); }
        }

        private DoctorFormModel _doctorFormModel = new DoctorFormModel();
        public DoctorFormModel DoctorFormModel
        {
            get => _doctorFormModel;
            set { _doctorFormModel = value; OnPropertyChanged(); }
        }

        private List<DoctorSpecialization> _specializations = [];
        public List<DoctorSpecialization> Specializations
        {
            get => _specializations;
            set { _specializations = value; OnPropertyChanged(); }
        }

        private List<Department> _departments = [];
        public List<Department> Departments 
        {
            get => _departments;
            set { _departments = value; OnPropertyChanged(); }
        }

        private void LoadSpecializations()
        {
            Specializations = Repositories.DoctorSpecializations.FindAll();
        }

        private void LoadDepartments()
        {
            Departments = Repositories.Departments.FindAll();
        }

        private void UpdateDoctorFormModelAppointmentDuration()
        {
            DoctorFormModel.AppointmentDuration = new TimeOnly(
                string.IsNullOrEmpty(SelectedHours) ? 0 : int.Parse(SelectedHours),
                string.IsNullOrEmpty(SelectedMinutes) ? 0 :int.Parse(SelectedMinutes)
            );
        }

        private RelayCommand _makeWorkDayNotWeekend;
        public RelayCommand MakeWorkDayNotWeekend
        {
            get => _makeWorkDayNotWeekend ??= new RelayCommand((obj) =>
            {
                if (obj is DoctorFormModelWorkDay workDay)
                {
                    var idx = FormWorkDays.IndexOf(workDay);
                    if (idx != -1)
                    {
                        FormWorkDays[idx]= new DoctorFormModelWorkDay()
                        {
                            WeekDay = workDay.WeekDay,
                            IsWeekend = false,
                        };
                    }
                }
            });
        }

        private RelayCommand _makeWorkDayWeekend;
        public RelayCommand MakeWorkDayWeekend
        {
            get => _makeWorkDayWeekend ??= new RelayCommand((obj) =>
            {
                if (obj is DoctorFormModelWorkDay workDay)
                {
                    var idx = FormWorkDays.IndexOf(workDay);
                    if (idx != -1)
                    {
                        FormWorkDays[idx]= new DoctorFormModelWorkDay()
                        {
                            WeekDay = workDay.WeekDay,
                            IsWeekend = true,
                        };
                    }
                }
            });
        }

        private RelayCommand? _submit;
        public RelayCommand Submit
        {
            get
            {
                if (_submit == null)
                {
                    _submit = new RelayCommand(() =>
                    {
                        var userData = new User()
                        {
                            Email = DoctorFormModel.Email,
                            Password = DoctorFormModel.Password,
                            Name = DoctorFormModel.Name,
                            Surname = DoctorFormModel.Surname,
                            Patronymic = DoctorFormModel.Patronymic,
                            Gender = DoctorFormModel.Gender,
                            BornAt = DoctorFormModel.BornAt,
                        };

                        var Doctor = new DoctorProfile()
                        {
                            SpecializationId = DoctorFormModel.SpecializationId,
                            DepartmentId = DoctorFormModel.DepartmentId,
                            AppointmentDuration = DoctorFormModel.AppointmentDuration
                        };

                        Repositories.UseTransaction(() =>
                        {
                            Repositories.Users.Create(userData);
                            Repositories.SaveChanges();
                            Doctor.UserId = userData.Id;

                            Repositories.DoctorProfiles.Create(Doctor);
                            Repositories.SaveChanges();

                            foreach (var workDay in FormWorkDays)
                            {
                                if (!workDay.IsWeekend)
                                {
                                    Repositories.DoctorWorkDays.Create(new DoctorWorkDay()
                                    {
                                        DoctorId = Doctor.Id,
                                        WeekDay = workDay.WeekDay,
                                        StartedAt = new TimeOnly(workDay.StartedAtHours, workDay.StartedAtMinutes),
                                        EndedAt = new TimeOnly(workDay.EndedAtHours, workDay.EndedAtMinutes),
                                    });
                                }
                            }
                            Repositories.SaveChanges();
                        });

                        MessageBox.Show("Данные успешно сохранены");
                        OnSubmit();
                    });
                }
                return _submit;
            }
        }

        private RelayCommand? _cancel;
        public RelayCommand Cancel
        {
            get
            {
                if (_cancel == null)
                {
                    _cancel = new RelayCommand(OnCancel);
                }
                return _cancel;
            }
        }
    }
}
