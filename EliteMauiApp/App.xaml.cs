using System.Globalization;
using System.Threading;
using Application = Microsoft.Maui.Controls.Application;
using System;
using Elite.LMS.Maui.Views;

#if PaidWmsModules
using Microsoft.Maui.Controls;
#endif

namespace Elite.LMS.Maui
{
    public partial class App : Application
    {

        public App()
        {
            //var culture = new CultureInfo("en-US");
            var culture = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            InitializeComponent();

            // Global exception handling
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Exception exception = e.ExceptionObject as Exception;
                if (exception != null)
                {
                    // Log the exception if necessary
                    System.Diagnostics.Debug.WriteLine(exception);

                    // Display an alert to the user
                    //MainPage.DisplayAlert("Error", "An error occurred: " + exception.Message, "OK");
                }
            };
            MainPage = new AppShell();
        }
    }
}
