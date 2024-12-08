using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
using System.Collections.ObjectModel;
using System.Windows;

namespace Clinic.ViewModel.Main
{
    public class PatientFormVM : BaseVM
    {
        private Patient? Patient;
        private Action OnSuccess;
        private Action OnCancel;

        public PatientFormVM(Action onSuccess, Action onCancel, Patient? patient = null)
        {
            Patient = patient;
            OnSuccess= onSuccess;
            OnCancel = onCancel;

            if (Patient != null)
            {
                FormName = Patient.Name;
                FormSurname = Patient.Surname;
                FormPatronymic = Patient.Patronymic;
                FormGender = Patient.Gender;
                FormBornAt = Patient.BornAt;
                FormPhoneNumber = Patient.PhoneNumber;
                FormMedicalPolicyNumber = Patient.MedicalPolicyNumber;
                FormStreetId = Patient.House.StreetId;
                FormHouseId = Patient.HouseId;
                WindowTitle = "Изменение пациента";
            }

            LoadStreets();
            LoadHouses();
        }

        #region computed

        private string _windowTitle = "Добавление пациента";
        public string WindowTitle
        {
            get => _windowTitle;
            set { _windowTitle = value; OnPropertyChanged(); }
        }

        #endregion

        #region store

        private ObservableCollection<Street> _streets;
        public ObservableCollection<Street> Streets
        {
            get => _streets;
            set { _streets = value; OnPropertyChanged(); }
        }

        private ObservableCollection<House> _houses;
        public ObservableCollection<House> Houses 
        {
            get => _houses;
            set { _houses = value; OnPropertyChanged(); }
        }

        #endregion

        #region form

        private string _formName = "";
        public string FormName
        {
            get => _formName;
            set { _formName = value; OnPropertyChanged(); }
        }

        private string _formSurname = "";
        public string FormSurname 
        {
            get => _formSurname;
            set { _formSurname = value; OnPropertyChanged(); }
        }

        private string _formPatronymic = "";
        public string FormPatronymic
        {
            get => _formPatronymic;
            set { _formPatronymic = value; OnPropertyChanged(); }
        }

        private PatientGender? _formGender;
        public PatientGender? FormGender
        {
            get => _formGender;
            set { _formGender = value; OnPropertyChanged(); }
        }

        private DateTime? _formBornAt;
        public DateTime? FormBornAt
        {
            get => _formBornAt;
            set { _formBornAt = value; OnPropertyChanged(); }
        }

        private string _formPhoneNumber = "";
        public string FormPhoneNumber
        {
            get => _formPhoneNumber;
            set { _formPhoneNumber = value; OnPropertyChanged(); }
        }

        private string _formMedicalPolicyNumber = "";
        public string FormMedicalPolicyNumber
        {
            get => _formMedicalPolicyNumber;
            set { _formMedicalPolicyNumber = value; OnPropertyChanged(); }
        }

        private int? _formHouseId;
        public int? FormHouseId
        {
            get => _formHouseId;
            set { _formHouseId = value; OnPropertyChanged(); }
        }

        private int? _formStreetId;
        public int? FormStreetId
        {
            get => _formStreetId;
            set { _formStreetId = value; OnPropertyChanged(); LoadHouses(); }
        }

        #endregion

        #region commands

        private RelayCommand? _submit;
        public RelayCommand Submit => _submit ??= new RelayCommand(() =>
        {
            var data = new Patient()
            {
                Name = FormName,
                Surname = FormSurname,
                Patronymic = FormPatronymic,
                Gender = FormGender!.Value,
                BornAt = FormBornAt!.Value.ToUniversalTime(),
                PhoneNumber = FormPhoneNumber,
                MedicalPolicyNumber = FormMedicalPolicyNumber,
                HouseId = FormHouseId!.Value
            };

            if (Patient == null)
            {
                Repositories.Instance.Patients.Create(data);
            }
            else
            {
                data.Id = Patient.Id;
                Repositories.Instance.Patients.Update(data);
            }

            Repositories.Instance.SaveChanges();
            MessageBox.Show("Данные успешно сохранены");
            OnSuccess();
        }, () => !string.IsNullOrWhiteSpace(FormName) && !string.IsNullOrWhiteSpace(FormSurname) && FormGender != null &&
                 FormBornAt != null && !string.IsNullOrWhiteSpace(FormPhoneNumber) && !string.IsNullOrWhiteSpace(FormMedicalPolicyNumber) &&
                 FormHouseId != null);

        private RelayCommand? _cancel;
        public RelayCommand Cancel => _cancel ??= new RelayCommand(() => OnCancel());

        #endregion

        private void LoadStreets()
        {
            Streets = new ObservableCollection<Street>(Repositories.Instance.Streets.FindAll());
        }

        private void LoadHouses()
        {
            if (FormStreetId != null)
            {
                Houses = new ObservableCollection<House>(Repositories.Instance.Houses.FindAll(streetId: FormStreetId));
            }
        }
    }
}
