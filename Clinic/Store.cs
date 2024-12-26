namespace Clinic
{
    public class Store
    {
        private static Store? _instance;
        public static Store Instance => _instance ??= new Store();

        public string BarPrimaryBackground = "Green";
        public string BarSecondaryBackground = "LightGray";
        public string BarBorder = "LightGray";
    }
}
