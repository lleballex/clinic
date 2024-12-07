using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;

namespace Clinic.ViewModel.Main
{
    public class DoctorSpecializationFormVM : BaseVM
    {
        private Action OnSuccess;
        private Action OnCancel;
        private DoctorSpecialization? Specialization;

        public DoctorSpecializationFormVM(Action onSuccess, Action onCancel, DoctorSpecialization? specialization = null)
        {
            OnSuccess = onSuccess;
            OnCancel = onCancel;
            Specialization = specialization;

            if (Specialization != null)
            {
                FormName = Specialization.Name;
                WindowTitle = "Изменение специальности";
            }
        }

        #region computed

        private string _windowTitle = "Добавление специальности";
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
            if (Specialization == null)
            {
                Repositories.Instance.DoctorSpecializations.Create(new DoctorSpecialization()
                {
                    Name = FormName
                });
            } else
            {
                Specialization.Name = FormName;
                Repositories.Instance.DoctorSpecializations.Update(Specialization);
            }

            Repositories.Instance.SaveChanges();
            OnSuccess();
        });

        private RelayCommand? _cancel;
        public RelayCommand Cancel => _cancel ??= new RelayCommand(() => OnCancel());

        #endregion
    }
}
