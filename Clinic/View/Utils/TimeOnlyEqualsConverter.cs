using System.Globalization;
using System.Windows.Data;

namespace Clinic.View.Utils
{
    public class TimeOnlyEqualsConverter : IMultiValueConverter 
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is TimeOnly && values[1] is TimeOnly)
            {
                if (((TimeOnly)values[0]).ToTimeSpan() == ((TimeOnly)values[1]).ToTimeSpan())
                {
                    return "true";
                }
            }
            return "false";
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
