using System.Globalization;

namespace Elite.LMS.Maui
{
    public partial class App : Application
    {
        public App()
        {
            var culture = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
