using DAL.Entities;
using System.Globalization;
using System.Windows.Data;

namespace Clinic.View.Utils
{
    public class ProcedureStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Appointment appointment)
            {
                switch (appointment.Status)
                {
                    case AppointmentStatus.Created:
                        return "Запланировано";
                    case AppointmentStatus.Finished:
                        return "Завершено";
                    case AppointmentStatus.Canceled:
                        return "Отменено";
                }
            }
            return "Не записано";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsProcedureWithoutAppointment : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Procedure procedure && procedure.Appointment == null)
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

    public class IsProcedureWithAppointment : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Procedure procedure && procedure.Appointment != null)
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
