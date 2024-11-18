using Clinic.ViewModel.Utils;
using Clinic.ViewModel.Base;
using DAL.Repositories;
using Clinic.View.Windows;

namespace Clinic.ViewModel.Main
{
    public class DoctorsVM : BaseVM
    {
        private Repositories Repositories = Repositories.Instance;

        public DoctorsVM()
        {
            LoadDoctors(); 
        }

        private List<DoctorCardVM> _doctors = [];
        public List<DoctorCardVM> Doctors
        {
            get => _doctors;
            set { _doctors = value; OnPropertyChanged(); }
        }

        private string _query = "";
        public string Query
        {
            get => _query;
            set { _query = value; OnPropertyChanged(); LoadDoctors(); }
        }

        private void LoadDoctors()
        {
            var newDoctors = new List<DoctorCardVM>();
            foreach (var doctor in Repositories.DoctorProfiles.FindAll(query: Query))
            {
                newDoctors.Add(new DoctorCardVM(doctor, LoadDoctors));
            }
            Doctors = newDoctors;
        }

        private RelayCommand _addDoctor;
        public RelayCommand AddDoctor
        {
            get
            {
                if (_addDoctor == null)
                {
                    _addDoctor = new RelayCommand(() =>
                    {
                        (new DoctorFormWindow(null, LoadDoctors)).ShowDialog();
                    });
                }
                return _addDoctor;
            }
        }
    }
}
