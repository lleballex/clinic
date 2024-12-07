using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;

namespace Clinic.ViewModel.Main
{
    public class ProcedureTypeFormVM : BaseVM
    {
        private Action OnSuccess;
        private Action OnCancel;
        private ProcedureType? Type;

        public ProcedureTypeFormVM(Action onSuccess, Action onCancel, ProcedureType? type = null)
        {
            OnSuccess = onSuccess;
            OnCancel = onCancel;
            Type = type;

            if (Type != null)
            {
                FormName = Type.Name;
                WindowTitle = "Изменение процедуры";
            }
        }

        #region computed

        private string _windowTitle = "Добавление процедуры";
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
            if (Type == null)
            {
                Repositories.Instance.ProcedureTypes.Create(new ProcedureType()
                {
                    Name = FormName
                });
            } else
            {
                Type.Name = FormName;
                Repositories.Instance.ProcedureTypes.Update(Type);
            }

            Repositories.Instance.SaveChanges();
            OnSuccess();
        });

        private RelayCommand? _cancel;
        public RelayCommand Cancel => _cancel ??= new RelayCommand(() => OnCancel());

        #endregion
    }
}
