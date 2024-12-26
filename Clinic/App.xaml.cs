using Clinic.View.Windows;
using System.IO;
using System.Windows;

namespace Clinic
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            File.Create("./logs.txt").Close();

            (new AdminHomeWindow()).Show();
        }
    }
}
