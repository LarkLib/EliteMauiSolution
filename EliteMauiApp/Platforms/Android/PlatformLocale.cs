using System.Globalization;

namespace Elite.LMS.Maui {
    public partial class PlatformLocale {
        public partial string GetPlatformLocale() {
            return CultureInfo.CurrentCulture.Name;
        }
    }
}

