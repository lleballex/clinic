using Clinic.Windows;
using System.Windows;

namespace Clinic
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            (new LoginWindow()).Show();
            (new AdminWindow()).Show();
            (new DoctorWindow()).Show();
            (new RegistrarWindow()).Show();
        }
    }
}
