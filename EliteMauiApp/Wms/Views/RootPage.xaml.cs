using Elite.LMS.Maui.ViewModels;
using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui {
    public partial class AppShell : Shell {
        public AppShell() {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
