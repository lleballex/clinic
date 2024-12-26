using Clinic.ViewModel.Utils;
using Clinic.ViewModel.Base;
using Clinic.View.Windows;
using DAL.Repositories;

namespace Clinic.ViewModel.Main
{
    public class PatientsVM : BaseVM
    {
        private Repositories Repositories = Repositories.Instance;

        public PatientsVM()
        {
            LoadPatients();
        }

        private List<PatientCardVM> _patients = [];
        public List<PatientCardVM> Patients
        {
            get => _patients;
            set { _patients = value; OnPropertyChanged(); }
        }

        private string _query = "";
        public string Query
        {
            get => _query;
            set { _query = value; OnPropertyChanged(); LoadPatients(); }
        }

        private void LoadPatients()
        {
            List<PatientCardVM> newPatients = [];
            foreach (var patient in Repositories.Patients.FindAll(Query))
            {
                newPatients.Add(new PatientCardVM(patient, LoadPatients));
            }
            Patients = newPatients;
        }

        private RelayCommand _goToPatientForm;
        public RelayCommand GoToPatientForm
        {
            get
            {
                if (_goToPatientForm == null)
                {
                    _goToPatientForm = new RelayCommand(() =>
                    {
                        (new PatientFormWindow(null, () => LoadPatients())).ShowDialog();
                    });
                }
                return _goToPatientForm;
            }
        }

        // chmi

        public void OnLoad()
        {
            ProgressBarBackground = Store.Instance.BarSecondaryBackground;
            ProgressBarForeground = Store.Instance.BarPrimaryBackground;
            ProgressBarBorder = Store.Instance.BarBorder;
        }

        private double _scrollProgress;
        public double ScrollProgress
        {
            get => _scrollProgress;
            set { _scrollProgress = value; OnPropertyChanged(); }
        }

        private string _progressBarBackground;
        public string ProgressBarBackground
        {
            get => _progressBarBackground;
            set { _progressBarBackground = value; OnPropertyChanged(); }
        }

        private string _progressBarForeground;
        public string ProgressBarForeground
        {
            get => _progressBarForeground;
            set { _progressBarForeground = value; OnPropertyChanged(); }
        }

        private string _progressBarBorder;
        public string ProgressBarBorder
        {
            get => _progressBarBorder;
            set { _progressBarBorder = value; OnPropertyChanged(); }
        }
    }
}
