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

        private ForRoleEnum ForRole;
        private Action OnRepoChange;

        public AppointmentCardVM(Appointment appointment, ForRoleEnum forRole, Action onRepoChange)
        {
            OnRepoChange = onRepoChange;
            ForRole = forRole;
            Appointment = appointment;

            IsForPatient = ForRole == ForRoleEnum.Patient;
        }

        #region store

        private bool _isForPatient;
        public bool IsForPatient
        {
            get => _isForPatient;
            set { _isForPatient = value; OnPropertyChanged(); }
        }

        private Appointment _appointment;
        public Appointment Appointment
        {
            get => _appointment;
            set { _appointment = value; OnPropertyChanged(); OnAppointmentChange(); }
        }

        private ObservableCollection<ProcedureCardVM> _procedures;
        public ObservableCollection<ProcedureCardVM> Procedures
        {
            get => _procedures;
            set { _procedures = value; OnPropertyChanged(); OnProceduresChange(); }
        }

        #endregion

        #region computed

        private bool _proceduresExist;
        public bool ProceduresExist
        {
            get => _proceduresExist;
            set { _proceduresExist = value; OnPropertyChanged(); }
        }

        private bool _isProcedure;
        public bool IsProcedure 
        {
            get => _isProcedure;
            set { _isProcedure = value; OnPropertyChanged(); }
        }

        #endregion

        #region commands

        private RelayCommand? _addAppointmentResult;
        public RelayCommand AddAppointmentResult => _addAppointmentResult ??= new RelayCommand(() =>
        {
            (new AppointmentResultFormWindow(appointment: Appointment, onRepoChange: OnRepoChange)).ShowDialog();
        });

        private RelayCommand? _cancelAppointment;
        public RelayCommand CancelAppointment => _cancelAppointment ??= new RelayCommand(() =>
        {
            // TODO: implement
        });

        #endregion

        private void OnAppointmentChange()
        {
            Procedures = new ObservableCollection<ProcedureCardVM>(Appointment.AssignedProcedures.Select(i => new ProcedureCardVM(
                procedure: i,
                patient: Appointment.Patient,
                onRepoChange: OnRepoChange
            )));
            IsProcedure = Appointment.ProcedureId != null;
        }

        private void OnProceduresChange()
        {
            ProceduresExist = Procedures.Count > 0;
        }
    }
}
