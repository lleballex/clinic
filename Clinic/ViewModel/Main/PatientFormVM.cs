using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;
using System.Windows;

namespace Clinic.ViewModel.Main
{
    public class PatientFormVM : BaseVM
    {
        public class FormClass
        {
            public string Name { get; set; } = "";
            public string Surname { get; set; } = "";
            public string? Patronymic { get; set; }
            public PatientGender Gender { get; set; } = PatientGender.Male;
            public DateTime BornAt { get; set; } = DateTime.Now;
            public string PhoneNumber { get; set; } = "";
            public string MedicalPolicyNumber { get; set; } = "";
            public int HouseId { get; set; }
        }

        private Repositories Repositories = Repositories.Instance;
        private Patient? Patient;
        private Action OnSubmit;
        private Action OnCancel;

        public PatientFormVM(Patient? patient, Action onSubmit, Action onCancel)
        {
            Patient = patient;
            OnSubmit = onSubmit;
            OnCancel = onCancel;

            if (patient != null)
            {
                FormStreetId = patient.House.StreetId;
                Form = new FormClass()
                {
                    Name = patient.Name,
                    Surname = patient.Surname,
                    Patronymic = patient.Patronymic,
                    Gender = patient.Gender,
                    BornAt = patient.BornAt,
                    PhoneNumber = patient.PhoneNumber,
                    MedicalPolicyNumber = patient.MedicalPolicyNumber,
                    HouseId = patient.HouseId,
                };

                WindowTitle = "Изменение пациента";
            } else
            {
                WindowTitle = "Добавление пациента";
            }

            LoadStreets();
            LoadHouses();
        }

        private string _windowTitle;
        public string WindowTitle
        {
            get => _windowTitle;
            set { _windowTitle = value; OnPropertyChanged(); }
        }

        private List<Street> _streets;
        public List<Street> Streets
        {
            get => _streets;
            set { _streets = value; OnPropertyChanged(); }
        }

        private List<House> _houses;
        public List<House> Houses 
        {
            get => _houses;
            set { _houses = value; OnPropertyChanged(); }
        }

        private FormClass _form = new FormClass();
        public FormClass Form
        {
            get => _form;
            set { _form = value; OnPropertyChanged(); }
        }

        private int? _formStreetId;
        public int? FormStreetId
        {
            get => _formStreetId;
            set { _formStreetId = value; OnPropertyChanged(); LoadHouses(); }
        }

        private void LoadStreets()
        {
            Streets = Repositories.Streets.FindAll();
        }

        private void LoadHouses()
        {
            if (FormStreetId.HasValue)
            {
                Houses = Repositories.Houses.FindAll(FormStreetId.Value);
            }
        }

        private RelayCommand _submit;
        public RelayCommand Submit
        {
            get
            {
                if (_submit == null)
                {
                    _submit = new RelayCommand(() =>
                    {
                        var data = new Patient()
                        {
                            Name = Form.Name,
                            Surname = Form.Surname,
                            Patronymic = Form.Patronymic,
                            Gender = Form.Gender,
                            BornAt = Form.BornAt.ToUniversalTime(),
                            PhoneNumber = Form.PhoneNumber,
                            MedicalPolicyNumber = Form.MedicalPolicyNumber,
                            HouseId = Form.HouseId
                        };

                        if (Patient == null)
                        {
                            Repositories.Patients.Create(data);
                        }
                        else
                        {
                            data.Id = Patient.Id;
                            Repositories.Patients.Update(data);
                        }

                        Repositories.SaveChanges();
                        MessageBox.Show("Данные успешно сохранены");
                        OnSubmit();
                    });
                }
                return _submit;
            }
        }

        private RelayCommand _cancel;
        public RelayCommand Cancel
        {
            get
            {
                if (_cancel == null)
                {
                    _cancel = new RelayCommand(() => OnCancel());
                }
                return _cancel;
            }
        }
    }
}
