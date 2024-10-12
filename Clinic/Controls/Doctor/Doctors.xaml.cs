using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clinic.Controls.Doctor
{
    public partial class Doctors : UserControl
    {
        // TODO: make it right
        private bool _allowAdding;

        // TODO: make it right
        public bool AllowAdding
        {
            get { return _allowAdding; }
            set
            {
                AddDoctorBtn.Visibility = value ? Visibility.Visible : Visibility.Hidden;
                _allowAdding = value;
            }
        }

        public Doctors()
        {
            InitializeComponent();
        }
    }
}
