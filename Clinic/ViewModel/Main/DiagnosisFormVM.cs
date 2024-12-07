using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;

namespace Clinic.ViewModel.Main
{
    public class DiagnosisFormVM : BaseVM
    {
        private Action OnSuccess;
        private Action OnCancel;
        private Diagnosis? Diagnosis;

        public DiagnosisFormVM(Action onSuccess, Action onCancel, Diagnosis? diagnosis = null)
        {
            OnSuccess = onSuccess;
            OnCancel = onCancel;
            Diagnosis = diagnosis;

            if (Diagnosis != null)
            {
                FormName = Diagnosis.Name;
                WindowTitle = "Изменение диагноза";
            }
        }

        #region computed

        private string _windowTitle = "Добавление диагноза";
        public string WindowTitle
        {
            get => _windowTitle;
            set { _windowTitle = value; OnPropertyChanged(); }
        }

        #endregion

        #region form

        private string _formName = "";
        public string FormName
        {
            get => _formName;
            set { _formName = value; OnPropertyChanged(); }
        }

        #endregion

        #region commands

        private RelayCommand? _save;
        public RelayCommand Save => _save ??= new RelayCommand(() =>
        {
            if (Diagnosis == null)
            {
                Repositories.Instance.Diagnosises.Create(new Diagnosis()
                {
                    Name = FormName
                });
            } else
            {
                Diagnosis.Name = FormName;
                Repositories.Instance.Diagnosises.Update(Diagnosis);
            }

            Repositories.Instance.SaveChanges();
            OnSuccess();
        });

        private RelayCommand? _cancel;
        public RelayCommand Cancel => _cancel ??= new RelayCommand(() => OnCancel());

        #endregion
    }
}
