using System.Windows;
using DAL.Entities;
using Clinic.ViewModel.Main;
using System.Windows.Data;
using System.Globalization;

namespace Clinic.View.Windows
{
    public partial class PatientWindow : Window
    {
        public PatientWindow(Patient patient)
        {
            InitializeComponent();

            DataContext = new PatientVM(patient);
        }
    }

    // TODO: refactor
    public class AConverter: IMultiValueConverter
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
