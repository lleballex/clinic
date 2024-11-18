using Clinic.ViewModel.Utils;
using DAL.Entities;
using DAL.Repositories;

namespace Clinic.ViewModel.Main
{
    public class DoctorFormVM : BaseVM
    {
        public class FormModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string? Patronymic { get; set; }
        }

        private Repositories Repositories = Repositories.Instance;

        private DoctorProfile? Doctor;
        private Action OnSubmit;
        private Action OnCancel;

        public DoctorFormVM(DoctorProfile? doctor, Action onSubmit, Action onCancel)
        {
            Doctor = doctor;
            OnSubmit = onSubmit;
            OnCancel = onCancel;
        }
    }
}
