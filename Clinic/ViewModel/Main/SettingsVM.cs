using Clinic.ViewModel.Utils;
using System.IO;

namespace Clinic.ViewModel.Main
{
    public class SettingsVM : BaseVM
    {
        #region form

        public string FormPrimaryBackground
        {
            get => Store.Instance.BarPrimaryBackground;
            set
            {
                Store.Instance.BarPrimaryBackground = value;
                File.AppendAllText("./logs.txt", $"[{DateTime.Now}] Изменен цвет заполненного фона: {value}\n");
                OnPropertyChanged();
            }
        }

        public string FormSecondaryBackground
        {
            get => Store.Instance.BarSecondaryBackground;
            set 
            { 
                Store.Instance.BarSecondaryBackground = value;
                File.AppendAllText("./logs.txt", $"[{DateTime.Now}] Изменен цвет фона: {value}\n");
                OnPropertyChanged();
            }
        }

        public string FormBorder
        {
            get => Store.Instance.BarBorder;
            set
            {
                Store.Instance.BarBorder = value;
                File.AppendAllText("./logs.txt", $"[{DateTime.Now}] Изменен цвет рамки: {value}\n");
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
