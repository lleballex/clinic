using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
using System.Collections.ObjectModel;

namespace Clinic.ViewModel.Main
{
    public class HouseFormVM : BaseVM
    {
        private Action OnSuccess;
        private Action OnCancel;
        private House? House;

        public HouseFormVM(Action onSuccess, Action onCancel, House? house = null)
        {
            OnSuccess = onSuccess;
            OnCancel = onCancel;
            House = house;

            if (House != null)
            {
                FormNumber = House.Number;
                FormStreetId = House.StreetId;
                FormDepartmentId = House.DepartmentId;
                WindowTitle = "Изменение дома";
            }

            Streets = new ObservableCollection<Street>(Repositories.Instance.Streets.FindAll());
            Departments = new ObservableCollection<Department>(Repositories.Instance.Departments.FindAll());
        }

        #region computed

        private string _windowTitle = "Добавление дома";
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

        private int? _formStreetId;
        public int? FormStreetId
        {
            get => _formStreetId;
            set { _formStreetId = value; OnPropertyChanged(); }
        }

        private int? _formDepartmentId;
        public int? FormDepartmentId
        {
            get => _formDepartmentId;
            set { _formDepartmentId = value; OnPropertyChanged(); }
        }

        #endregion

        #region store

        private ObservableCollection<Street> _streets = [];
        public ObservableCollection<Street> Streets
        {
            get => _streets;
            set { _streets = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Department> _departments = [];
        public ObservableCollection<Department> Departments
        {
            get => _departments;
            set { _departments = value; OnPropertyChanged(); }
        }

        #endregion

        #region commands

        private RelayCommand? _save;
        public RelayCommand Save => _save ??= new RelayCommand(() =>
        {
            if (House == null)
            {
                Repositories.Instance.Houses.Create(new House()
                {
                    Number = FormNumber,
                    StreetId = FormStreetId!.Value,
                    DepartmentId = FormDepartmentId!.Value,
                });
            } else
            {
                House.Number = FormNumber;
                House.StreetId = FormStreetId!.Value;
                House.Street = null;
                House.DepartmentId = FormDepartmentId!.Value;
                House.Department = null;
                Repositories.Instance.Houses.Update(House);
            }

            Repositories.Instance.SaveChanges();
            OnSuccess();
        }, () => !string.IsNullOrWhiteSpace(FormNumber) && FormStreetId != null && FormDepartmentId != null);

        private RelayCommand? _cancel;
        public RelayCommand Cancel => _cancel ??= new RelayCommand(() => OnCancel());

        #endregion
    }
}
