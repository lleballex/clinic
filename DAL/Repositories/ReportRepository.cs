using DAL.Entities;

using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ReportRepository
    {
        public class DiagnosisStatistics
        {
            public Diagnosis Diagnosis { get; set; }
            public int Count { get; set; }
        }

        private ClinicContext Context;

        public ReportRepository(ClinicContext context)
        {
            Context = context;
        }

        public List<DiagnosisStatistics> FindDiagnosisStatistics(DateTime? startedAt = null, DateTime? endedAt = null)
        {
            return Context.Appointments
                .Include(i => i.Result)
                .ThenInclude(i => i.Diagnosis)
                .Where(i => (i.Status == AppointmentStatus.Finished) && 
                            (startedAt == null || DateOnly.FromDateTime(i.Datetime.ToLocalTime()) >= DateOnly.FromDateTime(startedAt.Value)) &&
                            (endedAt == null || DateOnly.FromDateTime(i.Datetime.ToLocalTime()) <= DateOnly.FromDateTime(endedAt.Value)))
                .GroupBy(i => i.Result.Diagnosis)
                .Select(i => new DiagnosisStatistics() {
                    Diagnosis = i.Key,
                    Count = i.Count()
                })
                .OrderByDescending(i => i.Count)
                .ToList();
        }
    }
}
