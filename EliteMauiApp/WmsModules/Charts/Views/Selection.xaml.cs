using System;
using System.Globalization;
using Elite.LMS.Maui.Data;
using DevExpress.Maui.Charts;
using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui.Views {
    public partial class Selection : Wms.WmsPage {
        public Selection() {
            InitializeComponent();
        }
    }

    public class SelectionConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null)
                return true;
            DataSourceKey key = (DataSourceKey)value;
            PieData pie = (PieData)key.DataObject;
            return pie.Label.Equals(parameter);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
    public class ChartTitleConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture = null) {
            string prefix = String.Empty;
            if (value != null) {
                DataSourceKey key = (DataSourceKey)value;
                PieData pie = (PieData)key.DataObject;
                prefix = pie.Label;
            }
            return String.Format("{0} Sales by Year", prefix);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
