namespace Elite.LMS.Maui.Wms.ThemeLoader {
    internal sealed partial class ThemeEnvironment {
        static ThemeEnvironment instance;
        public static ThemeEnvironment Instance {
            get {
                if (instance == null)
                    instance = new ThemeEnvironment();

                return instance;
            }
        }
    }
}

