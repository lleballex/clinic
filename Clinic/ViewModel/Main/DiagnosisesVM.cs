using Clinic.View.Windows;
using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
using System.Collections.ObjectModel;
using System.Windows;

namespace Clinic.ViewModel.Main
{
    public class DiagnosisesVM : BaseVM
    {
        public DiagnosisesVM()
        {
            LoadDiagnosises();
        }

        #region store

        private ObservableCollection<Diagnosis> _diagnosises = [];
        public ObservableCollection<Diagnosis> Diagnosises
        {
            get => _diagnosises;
            set { _diagnosises = value; OnPropertyChanged(); }
        }

        #endregion

        #region form

        private string _formQuery = "";
        public string FormQuery
        {
            get => _formQuery;
            set { _formQuery = value; OnPropertyChanged(); LoadDiagnosises(); }
        }

        #endregion

        #region commands

        private RelayCommand? _addDiagnosis;
        public RelayCommand AddDiagnosis => _addDiagnosis ??= new RelayCommand(() =>
        {
            (new DiagnosisFormWindow(onRepoChange: LoadDiagnosises)).ShowDialog();
        });

        private RelayCommand? _deleteDiagnosis;
        public RelayCommand DeleteDiagnosis => _deleteDiagnosis ??= new RelayCommand((obj) =>
        {
            if (obj is Diagnosis diagnosis)
            {
                if (MessageBox.Show(
                    "Точно удалить диагноз? Все связанные с ним объекты будут также удалены", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning
                ) == MessageBoxResult.Yes)
                {
                    Repositories.Instance.Diagnosises.Delete(diagnosis);
                    Repositories.Instance.SaveChanges();
                    LoadDiagnosises();
                    MessageBox.Show("Диагноз успешно удален");
                }
            }
        });

        private RelayCommand? _editDiagnosis;
        public RelayCommand EditDiagnosis => _editDiagnosis ??= new RelayCommand((obj) =>
        {
            if (obj is Diagnosis diagnosis)
            {
                (new DiagnosisFormWindow(onRepoChange: LoadDiagnosises, diagnosis: diagnosis)).ShowDialog();
            }
        });

        #endregion

        private void LoadDiagnosises()
        {
            Diagnosises = new ObservableCollection<Diagnosis>(Repositories.Instance.Diagnosises.FindAll(query: FormQuery));
        }
    }
}
