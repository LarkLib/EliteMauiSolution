using Elite.LMS.Maui.ViewModels;

namespace Elite.LMS.Maui.WmsModules.Grid.Data {
    public class Commodity : NotificationObject {
        readonly string name;

        public string Name {
            get { return this.name; }
        }

        public Commodity(string name) {
            this.name = name;
        }
    }
}
