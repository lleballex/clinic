using Clinic.ViewModel.Utils;
using Clinic.View.Windows;
using DAL.Entities;
using System.Collections.ObjectModel;
using System.Windows;
using DAL.Repositories;
using Microsoft.Win32;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

namespace Clinic.ViewModel.Base
{
    public class AppointmentCardVM : BaseVM
    {
        public enum ForRoleEnum
        {
            Doctor, Patient
        }

        private ForRoleEnum ForRole;
        private Action OnRepoChange;

        public AppointmentCardVM(Appointment appointment, ForRoleEnum forRole, Action onRepoChange)
        {
            OnRepoChange = onRepoChange;
            ForRole = forRole;
            Appointment = appointment;

            IsForPatient = ForRole == ForRoleEnum.Patient;
        }

        #region store

        private bool _isForPatient;
        public bool IsForPatient
        {
            get => _isForPatient;
            set { _isForPatient = value; OnPropertyChanged(); }
        }

        private Appointment _appointment;
        public Appointment Appointment
        {
            get => _appointment;
            set { _appointment = value; OnPropertyChanged(); OnAppointmentChange(); }
        }

        private ObservableCollection<ProcedureCardVM> _procedures;
        public ObservableCollection<ProcedureCardVM> Procedures
        {
            get => _procedures;
            set { _procedures = value; OnPropertyChanged(); OnProceduresChange(); }
        }

        #endregion

        #region computed

        private bool _proceduresExist;
        public bool ProceduresExist
        {
            get => _proceduresExist;
            set { _proceduresExist = value; OnPropertyChanged(); }
        }

        private bool _isProcedure;
        public bool IsProcedure 
        {
            get => _isProcedure;
            set { _isProcedure = value; OnPropertyChanged(); }
        }

        private bool _recommendationsExist;
        public bool RecommendationsExist
        {
            get => _recommendationsExist;
            set { _recommendationsExist = value; OnPropertyChanged(); }
        }

        #endregion

        #region commands

        private RelayCommand? _addAppointmentResult;
        public RelayCommand AddAppointmentResult => _addAppointmentResult ??= new RelayCommand(() =>
        {
            (new AppointmentResultFormWindow(appointment: Appointment, onRepoChange: OnRepoChange)).ShowDialog();
        });

        private RelayCommand? _cancelAppointment;
        public RelayCommand CancelAppointment => _cancelAppointment ??= new RelayCommand(() =>
        {
            if (MessageBox.Show(
                "Точно отменить прием?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning
            ) == MessageBoxResult.Yes)
            {
                Appointment.Status = AppointmentStatus.Canceled;
                Repositories.Instance.Appointments.Update(Appointment);
                Repositories.Instance.SaveChanges();
                OnRepoChange();
                MessageBox.Show("Прием успешно отменен");
            }
        });

        private RelayCommand? _exportAppointment;
        public RelayCommand ExportAppointment => _exportAppointment ??= new RelayCommand(() =>
        {
            var dialog = new SaveFileDialog()
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                DefaultExt = ".pdf",
                FileName = $"Прием {Appointment.Patient.Surname} {Appointment.Datetime.ToLocalTime().ToString("dd_MM_yyyy")}"
            };

            if (dialog.ShowDialog() == true)
            {
                var text = $"Прием у врача\n\n" +
                           $"Врач: {Appointment.Doctor.User.Surname} {Appointment.Doctor.User.Name} {Appointment.Doctor.User.Patronymic}, {Appointment.Doctor.Specialization.Name}\n" +
                           $"Пациент: {Appointment.Patient.Surname} {Appointment.Patient.Name} {Appointment.Patient.Patronymic}\n" +
                           $"Дата приема: {Appointment.Datetime.ToLocalTime().ToString("dd.MM.yyyy HH:mm")}\n";

                if (Appointment.Status == AppointmentStatus.Created)
                {
                    text += "Статус приема: запланирован\n";
                }
                else if (Appointment.Status == AppointmentStatus.Canceled)
                {
                    text += "Статус приема: отменен\n";
                }
                else if (Appointment.Status == AppointmentStatus.Finished)
                {
                    text += $"Статус приема: завершен\n" +
                            $"Диагноз: {Appointment.Result.Diagnosis.Name}\n" + 
                            $"Описание диагноза: {(string.IsNullOrWhiteSpace(Appointment.Result.DiagnosisDescription) ? '-' : Appointment.Result.DiagnosisDescription)}\n" +
                            $"Рекомендации: {(string.IsNullOrWhiteSpace(Appointment.Result.Recommendations) ? '-' : Appointment.Result.Recommendations)}\n\n" +
                            $"Назначенные процедуры: {(Appointment.AssignedProcedures.Count == 0 ? '-' : string.Join(", ", Appointment.AssignedProcedures.Select(i => i.Type.Name).ToList()))}";
                }

                var document = new PdfDocument();
                var page = document.AddPage();
                var gfx = XGraphics.FromPdfPage(page);
                var formatter = new XTextFormatter(gfx);
                var font = new XFont("Times New Roman", 16);
                var rect = new XRect(40, 40, page.Width - 80, page.Height - 80);
                formatter.DrawString(text, font, XBrushes.Black, rect);

                document.Save(dialog.FileName);
            }
        });

        #endregion

        private void OnAppointmentChange()
        {
            Procedures = new ObservableCollection<ProcedureCardVM>(Appointment.AssignedProcedures.Select(i => new ProcedureCardVM(
                procedure: i,
                patient: Appointment.Patient,
                onRepoChange: OnRepoChange
            )));
            IsProcedure = Appointment.ProcedureId != null;
            RecommendationsExist = !string.IsNullOrWhiteSpace(Appointment.Result?.Recommendations);
        }

        private void OnProceduresChange()
        {
            ProceduresExist = Procedures.Count > 0;
        }
    }
}
