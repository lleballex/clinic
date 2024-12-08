using Clinic.Model;
using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
using System.Collections.ObjectModel;
using System.Windows;

namespace Clinic.ViewModel.Main
{
    public class AppointmentResultFormVM : BaseVM
    {
        public Appointment Appointment;
        private Action OnSuccess;
        private Action OnCancel;

        public AppointmentResultFormVM(Appointment appointment, Action onSuccess, Action onCancel)
        {
            Appointment = appointment;
            OnSuccess = onSuccess;
            OnCancel = onCancel;

            if (Appointment.ProcedureId != null)
            {
                WindowTitle = "Внесение результатов процедуры";
            }

            IsProcedure = Appointment.ProcedureId != null;

            LoadDiagnosises();
            LoadProcedureTypes();
        }

        #region computed

        private string _windowTitle = "Внесение результатов приема";
        public string WindowTitle
        {
            get => _windowTitle;
            set { _windowTitle = value; OnPropertyChanged(); }
        }

        private bool _isProcedure;
        public bool IsProcedure
        {
            get => _isProcedure;
            set { _isProcedure = value; OnPropertyChanged(); }
        }

        #endregion

        #region store

        private ObservableCollection<ProcedureType> _procedureTypes = [];
        public ObservableCollection<ProcedureType> ProcedureTypes
        {
            get => _procedureTypes;
            set { _procedureTypes = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Diagnosis> _diagnosises = [];
        public ObservableCollection<Diagnosis> Diagnosises
        {
            get => _diagnosises;
            set { _diagnosises = value; OnPropertyChanged(); }
        }

        #endregion

        #region form

        private int? _formDiagnosisId;
        public int? FormDiagnosisId
        {
            get => _formDiagnosisId;
            set { _formDiagnosisId = value; OnPropertyChanged(); }
        }

        private string _formDiagnosisDescription = "";
        public string FormDiagnosisDescription
        {
            get => _formDiagnosisDescription;
            set { _formDiagnosisDescription = value; OnPropertyChanged(); }
        }

        private string _formRecommendations = "";
        public string FormRecommendations
        {
            get => _formRecommendations;
            set { _formRecommendations = value; OnPropertyChanged(); }
        }

        private ObservableCollection<AppointmentProcedureFormModel> _formProcedures = [];
        public ObservableCollection<AppointmentProcedureFormModel> FormProcedures
        {
            get => _formProcedures;
            set { _formProcedures = value; OnPropertyChanged(); }
        }

        #endregion

        #region commands

        private RelayCommand? _submit;
        public RelayCommand Submit => _submit ??= new RelayCommand(() =>
        {
            Repositories.Instance.UseTransaction(() =>
            {
                var appointmentResultData = new AppointmentResult()
                {
                    AppointmentId = Appointment.Id,
                    DiagnosisId = FormDiagnosisId!.Value,
                    DiagnosisDescription = FormDiagnosisDescription,
                    Recommendations = FormRecommendations
                };
                Repositories.Instance.AppointmentsResults.Create(appointmentResultData);
                Appointment.Status = AppointmentStatus.Finished;
                Repositories.Instance.Appointments.Update(Appointment);
                Repositories.Instance.SaveChanges();
                foreach (var procedure in FormProcedures)
                {
                    Repositories.Instance.Procedures.Create(new Procedure()
                    {
                        AssignedAppointmentId = Appointment.Id,
                        TypeId = procedure.TypeId!.Value,
                    });
                }
                Repositories.Instance.SaveChanges();
            });
            MessageBox.Show("Данные успешно сохранены");
            OnSuccess();
        }, () => FormDiagnosisId != null && FormProcedures.All(i => i.TypeId != null));

        private RelayCommand? _cancel;
        public RelayCommand Cancel => _cancel ??= new RelayCommand(OnCancel);

        private RelayCommand? _addProcedure;
        public RelayCommand AddProcedure => _addProcedure ??= new RelayCommand(() =>
        {
            FormProcedures.Add(new AppointmentProcedureFormModel());
        });

        private RelayCommand? _removeProcedure;
        public RelayCommand RemoveProcedure => _removeProcedure ??= new RelayCommand((obj) =>
        {
            if (obj is AppointmentProcedureFormModel procedure)
            {
                FormProcedures.Remove(procedure);
            }
        });

        #endregion

        private void LoadDiagnosises()
        {
            Diagnosises = new ObservableCollection<Diagnosis>(Repositories.Instance.Diagnosises.FindAll());
        }

        private void LoadProcedureTypes()
        {
            ProcedureTypes = new ObservableCollection<ProcedureType>(Repositories.Instance.ProcedureTypes.FindAll());
        }
    }
}
