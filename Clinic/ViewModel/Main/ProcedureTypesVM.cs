using Clinic.View.Windows;
using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
using System.Collections.ObjectModel;
using System.Windows;

namespace Clinic.ViewModel.Main
{
    public class ProcedureTypesVM : BaseVM
    {
        public ProcedureTypesVM()
        {
            LoadTypes();
        }

        #region store

        private ObservableCollection<ProcedureType> _types = [];
        public ObservableCollection<ProcedureType> Types
        {
            get => _types;
            set { _types = value; OnPropertyChanged(); }
        }

        #endregion

        #region form

        private string _formQuery = "";
        public string FormQuery
        {
            get => _formQuery;
            set { _formQuery = value; OnPropertyChanged(); LoadTypes(); }
        }

        #endregion

        #region commands

        private RelayCommand? _addType;
        public RelayCommand AddType => _addType ??= new RelayCommand(() =>
        {
            (new ProcedureTypeFormWindow(onRepoChange: LoadTypes)).ShowDialog();
        });

        private RelayCommand? _deleteType;
        public RelayCommand DeleteType => _deleteType ??= new RelayCommand((obj) =>
        {
            if (obj is ProcedureType type)
            {
                if (MessageBox.Show(
                    "Точно удалить процедуру? Все связанные с ней объекты будут также удалены", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning
                ) == MessageBoxResult.Yes)
                {
                    Repositories.Instance.ProcedureTypes.Delete(type);
                    Repositories.Instance.SaveChanges();
                    LoadTypes();
                    MessageBox.Show("Процедура успешно удалена");
                }
            }
        });

        private RelayCommand? _editType;
        public RelayCommand EditType => _editType ??= new RelayCommand((obj) =>
        {
            if (obj is ProcedureType type)
            {
                (new ProcedureTypeFormWindow(onRepoChange: LoadTypes, type: type)).ShowDialog();
            }
        });

        #endregion

        private void LoadTypes()
        {
            Types = new ObservableCollection<ProcedureType>(Repositories.Instance.ProcedureTypes.FindAll(query: FormQuery));
        }
    }
}
