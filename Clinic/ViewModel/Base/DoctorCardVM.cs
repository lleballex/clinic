using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
using System.Windows;

namespace Clinic.ViewModel.Base
{
    public class DoctorCardVM : BaseVM
    {
        public class ComputedDoctorWorkDay
        {
            public string WeekDay { get; set; }
            public string Status { get; set; }
        }

        private Repositories Repositories = Repositories.Instance;

        private Action OnRepoChange;

        public DoctorCardVM(DoctorProfile doctor, Action onRepoChange)
        {
            OnRepoChange = onRepoChange;
            Doctor = doctor;
        }

        private UserRole _userRole;
        public UserRole UserRole
        {
            get => _userRole;
            set { _userRole = value; OnPropertyChanged(); }
        }

        private DoctorProfile _doctor;
        public DoctorProfile Doctor
        {
            get => _doctor;
            set { _doctor = value; OnPropertyChanged(); UpdateDoctorComputerd(); }
        }

        private DateTime _doctorBornAt;
        public DateTime DoctorBornAt
        {
            get => _doctorBornAt;
            set { _doctorBornAt = value; OnPropertyChanged(); }
        }

        private List<ComputedDoctorWorkDay> _doctorWorkDays;
        public List<ComputedDoctorWorkDay> DoctorWorkDays
        {
            get => _doctorWorkDays;
            set { _doctorWorkDays = value; OnPropertyChanged(); }
        }

        private void UpdateDoctorComputerd()
        {
            DoctorBornAt = Doctor.User.BornAt.ToLocalTime();

            var newWorkDays = new List<ComputedDoctorWorkDay>();
            var weekDaysStr = new List<string>() { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС" };
            var weekDays = new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
            for (int i = 0; i < 7; i++)
            {
                var found = Doctor.WorkDays.Where(j => j.WeekDay == weekDays[i]).FirstOrDefault();
                newWorkDays.Add(new ComputedDoctorWorkDay()
                {
                    WeekDay = weekDaysStr[i],
                    Status = found == null ? "Выходной" : found.StartedAt.ToString("HH:mm") + " - " + found.EndedAt.ToString("HH:mm")
                });
            }
            DoctorWorkDays = newWorkDays;
        }

        private RelayCommand _updateDoctor;
        public RelayCommand UpdateDoctor
        {
            get
            {
                if (_updateDoctor == null)
                {
                    _updateDoctor = new RelayCommand(() =>
                    {

                    });
                }
                return _updateDoctor;
            }
        }

        private RelayCommand _deleteDoctor;
        public RelayCommand DeleteDoctor
        {
            get
            {
                if (_deleteDoctor == null)
                {
                    _deleteDoctor = new RelayCommand(() =>
                    {
                        if (MessageBox.Show(
                            "Точно удалить доктора?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning
                        ) == MessageBoxResult.Yes)
                        {
                            Repositories.Users.Delete(Doctor.User);
                            Repositories.SaveChanges();
                            OnRepoChange();
                            MessageBox.Show("Доктор успешно удален");
                        }
                    });
                }
                return _deleteDoctor;
            }
        }
    }
}
