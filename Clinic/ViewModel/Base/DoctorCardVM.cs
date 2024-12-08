using Clinic.Model;
using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
using System.Collections.ObjectModel;
using System.Windows;

namespace Clinic.ViewModel.Base
{
    public class DoctorCardVM : BaseVM
    {
        private Action OnRepoChange;

        public DoctorCardVM(DoctorProfile doctor, Action onRepoChange)
        {
            OnRepoChange = onRepoChange;
            Doctor = doctor;
        }

        #region store

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
            set { _doctor = value; OnPropertyChanged(); UpdateDoctorComputed(); }
        }

        #endregion

        #region computed

        private ObservableCollection<DoctorWorkDayModel> _doctorWorkDays = [];
        public ObservableCollection<DoctorWorkDayModel> DoctorWorkDays
        {
            get => _doctorWorkDays;
            set { _doctorWorkDays = value; OnPropertyChanged(); }
        }

        #endregion

        #region commands

        private RelayCommand? _updateDoctor;
        public RelayCommand UpdateDoctor => _updateDoctor ??= new RelayCommand(() =>
        {
            // TODO: implement
        });

        private RelayCommand? _deleteDoctor;
        public RelayCommand DeleteDoctor => _deleteDoctor ??= new RelayCommand(() =>
        {
            if (MessageBox.Show(
                "Точно удалить врача? Все свяазанные с ним объекты также будут удалены", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning
            ) == MessageBoxResult.Yes)
            {
                Repositories.Instance.Users.Delete(Doctor.User);
                Repositories.Instance.SaveChanges();
                OnRepoChange();
                MessageBox.Show("Врач успешно удален");
            }
        });

        #endregion

        private void UpdateDoctorComputed()
        {
            var weekDays = new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };

            DoctorWorkDays = new ObservableCollection<DoctorWorkDayModel>(weekDays
                .Select(weekDay =>
                {
                    var found = Doctor.WorkDays.Where(i => i.WeekDay == weekDay).FirstOrDefault();
                    return new DoctorWorkDayModel()
                    {
                        WeekDay = weekDay,
                        IsWeekend = found == null,
                        StartedAt = found?.StartedAt,
                        EndedAt = found?.EndedAt
                    };
                })
                .ToList()
            );
        }
    }
}
