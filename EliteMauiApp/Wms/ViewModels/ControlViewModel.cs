using System.Collections.Generic;
using System.Windows.Input;
using Elite.LMS.Maui.Data;
using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.Services;
using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui.ViewModels
{
    public class ControlViewModel : BaseViewModel, IQueryAttributable
    {
        IWmsData data;
        WmsItem selectedItem;
        public List<WmsItem> WmsItems => this.data?.WmsItems;
        public WmsItem SelectedItem
        {
            get => this.selectedItem;
            set
            {
                SetProperty(ref this.selectedItem, value);
                if (this.selectedItem == null)
                    return;
                NavigationWmsCommand.Execute(this.selectedItem);
            }
        }
        public ICommand NavigationWmsCommand { get; }

        public ControlViewModel()
        {
            NavigationWmsCommand = new Command<WmsItem>(async (wmsItem) =>
            {
                await NavigationService.NavigateToWms(wmsItem);
                SelectedItem = null;
            });
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("WmsData", out object data))
            {
                SetProperty(ref this.data, data as IWmsData, propertyName: nameof(WmsItems));
            }
        }
    }
}
