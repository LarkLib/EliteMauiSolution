using Elite.LMS.Maui.Data;
using Elite.LMS.Maui.Models;
using System.Reflection;
using System.Windows.Input;

namespace Elite.LMS.Maui.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        string version;
        public string ProductTitle => "LMS.Maui.Wms";
        public string Version => this.version;
        public ICommand OpenWebCommand { get; }
        void InitVersion()
        {
            Version assemblyVersion = Assembly.GetAssembly(GetType()).GetName().Version;
            this.version = $"{assemblyVersion.Major}.{assemblyVersion.Minor}.{assemblyVersion.Build}";
        }
        public string TitleText => "仓库管理系统";
        public string SubTitle => "for Wms";
        public List<WmsItem> Items => GetItems();
        //public IList<MenuItemDescription> MenuItems { get; }
        //public ICommand NavigationControlCommand { get; }
        //public ICommand NavigationWmsCommand { get; }
        //public string Description { get; } = "内容简介\n\n 主要功能";

        //readonly MauiUriOpener openService;

        public MainViewModel()
        {
            InitVersion();
            //this.openService = new MauiUriOpener();
            //OpenWebCommand = new Command<MenuItemDescription>((p) => this.openService.Open(p.Url));
            //MenuItems = new List<MenuItemDescription>
            //{
            //    //new MenuItemDescription("Product Page", "productpage", OpenWebCommand, ProductUrl),
            //    //new MenuItemDescription("Documentation", "documentation", OpenWebCommand, DocumentationUrl),
            //    //new MenuItemDescription("Source Code", "sourcecode", OpenWebCommand, SourceCodeUrl)
            //};
        }

        List<WmsItem> GetItems()
        {
            List<WmsItem> result = new List<WmsItem>();
            result.AddRange(WmsGroupsData.WmsItems);
            return result;
        }
    }

    public record MenuItemDescription(string Name, string Icon, ICommand Command, string Url);
}