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

        #region computed

        private bool _recommendationsExist;
        public bool RecommendationsExist
        {
            get => _recommendationsExist;
            set { _recommendationsExist = value; OnPropertyChanged(); }
        }

        #endregion

        #region store

        private Procedure _procedure;
        public Procedure Procedure
        {
            get => _procedure;
            set { _procedure = value; OnPropertyChanged(); OnProcedureChange(); }
        }

        #endregion

        #region commands

        private RelayCommand? _makeAppointment;
        public RelayCommand MakeAppointment => _makeAppointment ??= new RelayCommand(() =>
        {
            (new AppointmentFormWindow(Patient, OnRepoChange, Procedure)).ShowDialog();
        });

        #endregion

        private void OnProcedureChange()
        {
            RecommendationsExist = !string.IsNullOrWhiteSpace(Procedure.Appointment?.Result?.Recommendations);
        }
    }
}
