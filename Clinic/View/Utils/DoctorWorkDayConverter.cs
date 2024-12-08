using System.Globalization;
using System.Windows.Data;

namespace Clinic.View.Utils
{
    public class DoctorWorkDayWeekDayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DayOfWeek weekDay)
            {
                switch (weekDay)
                {
                    case DayOfWeek.Monday:
                        return "ПН";
                    case DayOfWeek.Tuesday:
                        return "ВТ";
                    case DayOfWeek.Wednesday:
                        return "СР";
                    case DayOfWeek.Thursday:
                        return "ЧТ";
                    case DayOfWeek.Friday:
                        return "ПТ";
                    case DayOfWeek.Saturday:
                        return "СБ";
                    case DayOfWeek.Sunday:
                        return "ВС";
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
