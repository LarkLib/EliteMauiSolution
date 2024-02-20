using Elite.LMS.Maui.WmsModules.Grid.Data;
using Elite.LMS.Maui.ViewModels;
using DevExpress.Maui.CollectionView;
using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui {
    sealed class ItemTemplateSelector : DataTemplateSelector {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) {
            if (!(item is EmployeeTask task))
                return null;

            return task.Completed ? CompletedDataTemplate : UncompletedDataTemplate;
        }

        public DataTemplate CompletedDataTemplate { get; set; }
        public DataTemplate UncompletedDataTemplate { get; set; }
    }
}

namespace Elite.LMS.Maui.WmsModules.CollectionView.Views {
    public partial class CollectionViewDragDropView : Wms.WmsPage {
        public CollectionViewDragDropView() {
            InitializeComponent();
            ViewModel = new DragDropModel(new EmployeeTasksRepository());
            BindingContext = ViewModel;
        }

        DragDropModel ViewModel { get; }

        void DragItem(object sender, DragItemEventArgs e) {
            e.Cancel = !IsItemDraggable(e.DragItem);
        }

        void DragItemOver(object sender, DropItemEventArgs e) {
            e.Cancel = !IsItemDraggable(e.DropItem);
        }

        bool IsItemDraggable(object item) {
            return !((EmployeeTask)item).Completed;
        }
    }
}
