using Clinic.Model;
using System.Globalization;
using System.Windows.Data;

namespace Clinic.View.Utils
{
    class DoctorFormModelWorkDayIsWeekendConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is DoctorFormModelWorkDay model && model.IsWeekend)
            {
                return "Visible";
            }
            return "Collapsed";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class DoctorFormModelWorkDayIsNotWeekendConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is DoctorFormModelWorkDay model && !model.IsWeekend)
            {
                return "Visible";
            }
            return "Collapsed";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
