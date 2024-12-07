using Clinic.ViewModel.Utils;
using ClosedXML.Excel;
using DAL.Repositories;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace Clinic.ViewModel.Main
{
    public class StatisticsVM : BaseVM
    {
        #region store

        private ObservableCollection<ReportRepository.DiagnosisStatistics> _report = [];
        public ObservableCollection<ReportRepository.DiagnosisStatistics> Report
        {
            get => _report;
            set { _report = value; OnPropertyChanged(); }
        }

        #endregion

        #region form

        private DateTime? _formStartedAt;
        public DateTime? FormStartedAt
        {
            get => _formStartedAt;
            set { _formStartedAt = value; OnPropertyChanged(); }
        }

        private DateTime? _formEndedAt;
        public DateTime? FormEndedAt
        {
            get => _formEndedAt;
            set { _formEndedAt = value; OnPropertyChanged(); }
        }

        #endregion

        #region commands

        private RelayCommand? _generateReport;
        public RelayCommand GenerateReport => _generateReport ??= new RelayCommand(() =>
        {
            Report = new ObservableCollection<ReportRepository.DiagnosisStatistics>(Repositories.Instance.Reports.FindDiagnosisStatistics(startedAt: FormStartedAt, endedAt: FormEndedAt));
        });

        private RelayCommand? _exportReport;
        public RelayCommand ExportReport => _exportReport ??= new RelayCommand(() =>
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Лист 1");

                worksheet.Cell(1, 1).Value = "Диагноз";
                worksheet.Cell(1, 2).Value = "Количество";

                int rowIdx = 2;
                foreach (var column in Report)
                {
                    worksheet.Cell(rowIdx, 1).Value = column.Diagnosis.Name;
                    worksheet.Cell(rowIdx, 2).Value = column.Count;
                    rowIdx++;
                }

                var dialog = new SaveFileDialog()
                {
                    FileName = "Статистика по диагнозам",
                    DefaultExt = ".xlsx",
                    Filter = "Excel Files|*.xlsx"
                };

                if (dialog.ShowDialog() == true)
                {
                    workbook.SaveAs(dialog.FileName);
                }
            }
        });

        #endregion
    }
}
