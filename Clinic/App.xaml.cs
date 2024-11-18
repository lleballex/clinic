using Clinic.View.Windows;
using System.Windows;

namespace Clinic
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            (new AdminHomeWindow()).Show();
        }
    }
}
