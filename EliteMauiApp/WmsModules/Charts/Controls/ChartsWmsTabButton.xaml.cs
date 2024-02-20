using DevExpress.Maui.Core.Internal;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Elite.LMS.Maui.Wms {
    public partial class ChartsWmsTabButton : ContentView {
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(ChartsWmsTabButton));
        public string ImageSource { get => (string)GetValue(ImageSourceProperty); set => SetValue(ImageSourceProperty, value); }

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(ChartsWmsTabButton), Color.FromArgb("#CCCCCC"));
        public Color BorderColor { get => (Color)GetValue(BorderColorProperty); set => SetValue(BorderColorProperty, value); }

        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(ChartsWmsTabButton), Color.FromArgb("#FFFFFF"));
        public Color SelectedColor { get => (Color)GetValue(SelectedColorProperty); set => SetValue(SelectedColorProperty, value); }
        public static readonly BindableProperty ActualBackgroundColorProperty = BindableProperty.Create(nameof(ActualBackgroundColor), typeof(Color), typeof(ChartsWmsTabButton), DXColor.Transparent);
        public Color ActualBackgroundColor { get => (Color)GetValue(ActualBackgroundColorProperty); set => SetValue(ActualBackgroundColorProperty, value); }

        public static readonly BindableProperty IsVerticalProperty = BindableProperty.Create(nameof(IsVertical), typeof(bool), typeof(ChartsWmsTabButton), false, propertyChanged: OnIsVerticalPropertyChanged);
        static void OnIsVerticalPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            ((ChartsWmsTabButton)bindable).Update();
        }

        public bool IsVertical { get => (bool)GetValue(IsVerticalProperty); set => SetValue(IsVerticalProperty, value); }

        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(ChartsWmsTabButton), false, propertyChanged: OnIsSelectedPropertyChanged);
        static void OnIsSelectedPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            ((ChartsWmsTabButton)bindable).Update();
        }

        public bool IsSelected { get => (bool)GetValue(IsSelectedProperty); set => SetValue(IsSelectedProperty, value); }

        public ChartsWmsTabButton() {
            InitializeComponent();
            this.icon.BindingContext = this;
        }
        void Update() {
            ActualBackgroundColor = IsSelected ? SelectedColor : BackgroundColor;
            this.horizontalBorder.IsVisible = !IsVertical;
            this.verticalBorder.IsVisible = IsVertical;
        }
    }
}
