namespace Elite.LMS.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = new ViewModels.MainViewModel();
        }
    }
}
