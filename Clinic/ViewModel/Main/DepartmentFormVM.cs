using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;

namespace Clinic.ViewModel.Main
{
    public class DepartmentFormVM : BaseVM
    {
        private Action OnSuccess;
        private Action OnCancel;
        private Department? Department;

        public DepartmentFormVM(Action onSuccess, Action onCancel, Department? department = null)
        {
            OnSuccess = onSuccess;
            OnCancel = onCancel;
            Department = department;

            if (Department != null)
            {
                FormNumber = Department.Number.ToString();
                WindowTitle = "Изменение участка";
            }
        }

        #region computed

        private string _windowTitle = "Добавление участка";
        public string WindowTitle
        {
            get => _windowTitle;
            set { _windowTitle = value; OnPropertyChanged(); }
        }

        #endregion

        #region form

        private string _formNumber = "";
        public string FormNumber
        {
            get => _formNumber;
            set { _formNumber = value; OnPropertyChanged(); }
        }

        #endregion

        #region commands

        private RelayCommand? _save;
        public RelayCommand Save => _save ??= new RelayCommand(() =>
        {
            if (Department == null)
            {
                Repositories.Instance.Departments.Create(new Department()
                {
                    Number = int.Parse(FormNumber)
                });
            } else
            {
                Department.Number = int.Parse(FormNumber);
                Repositories.Instance.Departments.Update(Department);
            }

            Repositories.Instance.SaveChanges();
            OnSuccess();
        });

        private RelayCommand? _cancel;
        public RelayCommand Cancel => _cancel ??= new RelayCommand(() => OnCancel());

        #endregion
    }
}
