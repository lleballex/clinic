using Clinic.Model;
using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
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

            LoadSpecializations();
            LoadDepartments();
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
