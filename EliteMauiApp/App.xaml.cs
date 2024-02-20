using System.Globalization;
using System.Threading;
using Application = Microsoft.Maui.Controls.Application;
#if PaidWmsModules
using Microsoft.Maui.Controls;
#endif

namespace Elite.LMS.Maui {
    public partial class App : Application {

        public App() {
            //var culture = new CultureInfo("en-US");
            var culture = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            InitializeComponent();
#if PaidWmsModules
            //Routing.RegisterRoute("editFieldsPage", typeof(Views.FillPDFEditFieldsPage));
#endif
            MainPage = new AppShell();
        }
    }
}
