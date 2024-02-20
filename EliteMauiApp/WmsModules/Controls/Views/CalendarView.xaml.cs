using System;
using Elite.LMS.Maui.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using DevExpress.Maui.Editors;
using Microsoft.Maui.Devices;
using DevExpress.Maui.Core;
using Elite.LMS.Maui.Wms;

namespace Elite.LMS.Maui.Views {
    public partial class CalendarView : AdaptivePage {
        public CalendarView() {
            AddResources();
            InitializeComponent();
            ViewModel = new CalendarViewModel();
            BindingContext = ViewModel;
            OrientationChanged += OnOrientationChanged;
        }

        void AddResources() {
            Resources.Add("ListItemTextSize", DeviceInfo.Idiom == DeviceIdiom.Tablet ? 24.0 : 16.0);
            Resources.Add("ListHeaderTextSize", DeviceInfo.Idiom == DeviceIdiom.Tablet ? 30.0 : 20.0);
            Resources.Add("CalendarCellHeight", DeviceInfo.Platform == DevicePlatform.Android ? 36.0 : 46.0);
        }

        CalendarViewModel ViewModel { get; }

        void CustomDayCellStyle(object sender, CustomSelectableCellAppearanceEventArgs e) {
            if (e.Date == DateTime.Today)
                return;

            if (ViewModel.SelectedDate != null && e.Date == ViewModel.SelectedDate.Value)
                return;

            if (e.IsTrailing)
                return;

            SpecialDate specialDate = ViewModel.TryFindSpecialDate(e.Date);
            if (specialDate == null)
                return;

            e.FontAttributes = FontAttributes.Bold;
            Color textColor;
            if (specialDate.IsHoliday) {
                textColor = ThemeManager.Theme.Scheme.OnTertiaryContainer;
                e.EllipseBackgroundColor = ThemeManager.Theme.Scheme.TertiaryContainer;
                e.TextColor = textColor;

                return;
            }
            textColor = ThemeManager.Theme.Scheme.OnSecondaryContainer;
            e.EllipseBackgroundColor = ThemeManager.Theme.Scheme.SecondaryContainer;
            e.TextColor = textColor;
        }

        void OnOrientationChanged(object sender, EventArgs e) {
            DXDockLayout.SetDock(calendar, (Orientation == PageOrientation.Portrait) ? Dock.Top : Dock.Left);
            ViewModel.IsHolidaysAndObservancesListVisible = false;
            ViewModel.IsHolidaysAndObservancesListVisible = true;
        }
    }
}
