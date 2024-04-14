using Elite.LMS.Maui.ViewModels;
using Elite.LMS.Maui.Views;
using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
