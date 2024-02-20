using System;
using System.Collections.Generic;
using System.Linq;
using Elite.LMS.Maui.WmsModules.CollectionView.Data;
using Elite.LMS.Maui.ViewModels;
using DevExpress.Maui.CollectionView;
using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui {
    sealed class ItemDataTemplateSelector : DataTemplateSelector {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) {
            if (!(item is EmployeeTask task))
                return null;

            switch (task.Status) {
                case TaskStatus.Urgent:
                    return UrgentDataTemplate;
                case TaskStatus.Completed:
                    return CompletedDataTemplate;
                case TaskStatus.Uncompleted:
                default:
                    return UncompletedDataTemplate;
            }
        }

        public DataTemplate UrgentDataTemplate { get; set; }
        public DataTemplate CompletedDataTemplate { get; set; }
        public DataTemplate UncompletedDataTemplate { get; set; }
    }
}

namespace Elite.LMS.Maui.WmsModules.CollectionView.Views {
    public partial class CollectionViewDefaultSwipes : Wms.WmsPage {
        bool isAnimated;

        public CollectionViewDefaultSwipes() {
            InitializeComponent();
            ViewModel = new DefaultSwipeViewModel(new EmployeeTasksRepository());
            BindingContext = ViewModel;
        }

        DefaultSwipeViewModel ViewModel { get; }

        void OnDeleteTask(object sender, SwipeItemTapEventArgs e) {
            this.collectionView.DeleteItem(e.ItemHandle);
        }

        void OnStatusChanged(object sender, SwipeItemTapEventArgs e) {
            if (this.isAnimated)
                return;

            IList<EmployeeTask> source = ViewModel.ItemSource;

            EmployeeTask task = e.Item as EmployeeTask;
            int newItemHandle = 0;

            switch (task.Status) {
                case TaskStatus.Urgent:
                    newItemHandle = 0;
                    break;
                case TaskStatus.Completed:
                    newItemHandle = source.Count - 1;
                    break;
                case TaskStatus.Uncompleted:
                    newItemHandle = source.Where(t => t.Status == TaskStatus.Urgent).Count();
                    break;
            }


            int itemHandle = e.ItemHandle;
            if (itemHandle == newItemHandle)
                return;

            this.isAnimated = true;
            Dispatcher.Dispatch(() => this.collectionView.MoveItem(itemHandle, newItemHandle, () => this.isAnimated = false));
        }
    }
}
