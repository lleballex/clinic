using Clinic.View.Windows;
using Clinic.ViewModel.Utils;
using DAL.Entities;

namespace Clinic.ViewModel.Base
{
    public class ProcedureCardVM : BaseVM
    {
        private Patient Patient;
        private Action OnRepoChange;

        public ProcedureCardVM(Procedure procedure, Patient patient, Action onRepoChange)
        {
            Patient = patient;
            OnRepoChange = onRepoChange;
            Procedure = procedure;
        }

        #region store

        private Procedure _procedure;
        public Procedure Procedure
        {
            get => _procedure;
            set { _procedure = value; OnPropertyChanged(); }
        }

        #endregion

        #region commands

        private RelayCommand? _makeAppointment;
        public RelayCommand MakeAppointment => _makeAppointment ??= new RelayCommand(() =>
        {
            (new AppointmentFormWindow(Patient, OnRepoChange, Procedure)).ShowDialog();
        });

        #endregion
    }
}
