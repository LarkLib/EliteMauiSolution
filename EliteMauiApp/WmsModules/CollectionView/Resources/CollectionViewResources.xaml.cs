using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui.WmsModules.Resources {
    public partial class CollectionViewResources : ResourceDictionary {
        public CollectionViewResources() {
            InitializeComponent();
            Add("formatTime", @"{0:HH:mm}");
        }
    }
}
