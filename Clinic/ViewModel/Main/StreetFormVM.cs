using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;

namespace Clinic.ViewModel.Main
{
    public class StreetFormVM : BaseVM
    {
        private Action OnSuccess;
        private Action OnCancel;
        private Street? Street;

        public StreetFormVM(Action onSuccess, Action onCancel, Street? street = null)
        {
            OnSuccess = onSuccess;
            OnCancel = onCancel;
            Street = street;

            if (Street != null)
            {
                FormName = Street.Name;
                WindowTitle = "Изменение улицы";
            }
        }

        #region computed

        private string _windowTitle = "Добавление улицы";
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
            if (Street == null)
            {
                Repositories.Instance.Streets.Create(new Street()
                {
                    Name = FormName
                });
            } else
            {
                Street.Name = FormName;
                Repositories.Instance.Streets.Update(Street);
            }

            Repositories.Instance.SaveChanges();
            OnSuccess();
        }, () =>
        {
            return !string.IsNullOrWhiteSpace(FormName);
        });

        private RelayCommand? _cancel;
        public RelayCommand Cancel => _cancel ??= new RelayCommand(() => OnCancel());

        #endregion
    }
}
