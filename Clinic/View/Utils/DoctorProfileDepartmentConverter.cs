using DAL.Entities;
using System.Globalization;
using System.Windows.Data;

namespace Clinic.View.Utils
{
    public class DoctorProfileDepartmentNumberConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? "любой" : (value as Department).Number;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
