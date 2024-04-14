using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls;
#if PaidWmsModules
using SkiaSharp.Views.Maui.Controls.Hosting;
#endif
using DevExpress.Maui;
using DevExpress.Maui.Core;


namespace Elite.LMS.Maui {
    public static class MauiProgram {

        public static MauiApp CreateMauiApp() {
            ThemeManager.UseAndroidSystemColor = false;
            ThemeManager.ApplyThemeToSystemBars = true;
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseDevExpress()
#if DEBUG

#endif
#if PaidWmsModules
        .UseSkiaSharp()
#endif
                .ConfigureMauiHandlers(handlers => {
                    handlers.AddHandler<Shell, CustomShellRenderer>();
                });
            //DevExpress.Maui.Charts.Initializer.Init();
            DevExpress.Maui.CollectionView.Initializer.Init();
            DevExpress.Maui.Controls.Initializer.Init();
            DevExpress.Maui.Editors.Initializer.Init();
            //DevExpress.Maui.DataGrid.Initializer.Init();
            //DevExpress.Maui.Scheduler.Initializer.Init();
            return builder.Build();
        }
    }
}
