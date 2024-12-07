using Clinic.View.Windows;
using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
using System.Collections.ObjectModel;
using System.Windows;

namespace Clinic.ViewModel.Main
{
    public class DepartmentsVM : BaseVM
    {
        public DepartmentsVM()
        {
            LoadDepartments();
        }

        #region store

        private ObservableCollection<Department> _departments = [];
        public ObservableCollection<Department> Departments 
        {
            get => _departments;
            set { _departments = value; OnPropertyChanged(); }
        }

        #endregion

        #region form

        private string _formQuery = "";
        public string FormQuery
        {
            get => _formQuery;
            set { _formQuery = value; OnPropertyChanged(); LoadDepartments(); }
        }

        #endregion

        #region commands

        private RelayCommand? _addDepartment;
        public RelayCommand AddDepartment => _addDepartment ??= new RelayCommand(() =>
        {
            (new DepartmentFormWindow(onRepoChange: LoadDepartments)).ShowDialog();
        });

        private RelayCommand? _deleteDepartment;
        public RelayCommand DeleteDepartment => _deleteDepartment ??= new RelayCommand((obj) =>
        {
            if (obj is Department department)
            {
                if (MessageBox.Show(
                    "Точно удалить участок? Все связанные с ним объекты будут также удалены", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning
                ) == MessageBoxResult.Yes)
                {
                    Repositories.Instance.Departments.Delete(department);
                    Repositories.Instance.SaveChanges();
                    LoadDepartments();
                    MessageBox.Show("Участок успешно удален");
                }
            }
        });

        private RelayCommand? _editDepartment;
        public RelayCommand EditDepartment => _editDepartment ??= new RelayCommand((obj) =>
        {
            if (obj is Department department)
            {
                (new DepartmentFormWindow(onRepoChange: LoadDepartments, department: department)).ShowDialog();
            }
        });

        #endregion

        private void LoadDepartments()
        {
            Departments = new ObservableCollection<Department>(Repositories.Instance.Departments.FindAll(query: FormQuery));
        }
    }
}
