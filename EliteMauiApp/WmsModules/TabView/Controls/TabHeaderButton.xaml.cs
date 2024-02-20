using DevExpress.Maui.Core.Internal;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui;

namespace Elite.LMS.Maui.Wms {
    public partial class TabHeaderButton : StackLayout {
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(TabHeaderButton), false, propertyChanged: OnIsSelectedPropertyChanged);
        static void OnIsSelectedPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((TabHeaderButton)bindable).UpdateSelection();
        public bool IsSelected { get => (bool)GetValue(IsSelectedProperty); set => SetValue(IsSelectedProperty, value); }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(TabHeaderButton), propertyChanged: OnTextPropertyChanged, defaultValue: string.Empty);
        static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((TabHeaderButton)bindable).UpdateText();
        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(TabHeaderButton), propertyChanged: OnTextColorPropertyChanged, defaultValue: DXColor.Default);
        static void OnTextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((TabHeaderButton)bindable).UpdateTextColor();
        public Color TextColor { get => (Color)GetValue(TextColorProperty); set => SetValue(TextColorProperty, value); }

        public static readonly BindableProperty IconColorProperty = BindableProperty.Create(nameof(IconColor), typeof(Color), typeof(TabHeaderButton), propertyChanged: OnIconColorPropertyChanged, defaultValue: DXColor.Default);
        static void OnIconColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((TabHeaderButton)bindable).UpdateIconColor();
        public Color IconColor { get => (Color)GetValue(IconColorProperty); set => SetValue(IconColorProperty, value); }

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(TabHeaderButton), propertyChanged: OnFontFamilyPropertyChanged, defaultValue: string.Empty);
        static void OnFontFamilyPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((TabHeaderButton)bindable).UpdateFontFamily();
        public string FontFamily { get => (string)GetValue(FontFamilyProperty); set => SetValue(FontFamilyProperty, value); }

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(TabHeaderButton), propertyChanged: OnFontSizePropertyChanged);
        static void OnFontSizePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((TabHeaderButton)bindable).UpdateFontSize();
        public double FontSize { get => (double)GetValue(FontSizeProperty); set => SetValue(FontSizeProperty, value); }

        public static readonly BindableProperty SelectedBackgroundColorProperty = BindableProperty.Create(nameof(SelectedBackgroundColor), typeof(Color), typeof(TabHeaderButton), propertyChanged: OnSelectedBackgroundColorPropertyChanged, defaultValue: DXColor.Default);
        static void OnSelectedBackgroundColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((TabHeaderButton)bindable).UpdateBackgroundSelection();
        public Color SelectedBackgroundColor { get => (Color)GetValue(SelectedBackgroundColorProperty); set => SetValue(SelectedBackgroundColorProperty, value); }

        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(TabHeaderButton), propertyChanged: OnSelectedColorPropertyChanged, defaultValue: DXColor.Default);
        static void OnSelectedColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((TabHeaderButton)bindable).UpdateColorSelection();
        public Color SelectedColor { get => (Color)GetValue(SelectedColorProperty); set => SetValue(SelectedColorProperty, value); }

        public static readonly BindableProperty ShowIconProperty = BindableProperty.Create(nameof(ShowIcon), typeof(bool), typeof(TabHeaderButton), false, propertyChanged: OnShowIconPropertyChanged);
        static void OnShowIconPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((TabHeaderButton)bindable).UpdateShowIcon();
        public bool ShowIcon { get => (bool)GetValue(ShowIconProperty); set => SetValue(ShowIconProperty, value); }

        public static readonly BindableProperty IconSourceProperty = BindableProperty.Create(nameof(IconSource), typeof(ImageSource), typeof(TabHeaderButton), propertyChanged: OnIconSourcePropertyChanged, defaultValue: null);
        static void OnIconSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((TabHeaderButton)bindable).UpdateIconSource();
        public ImageSource IconSource { get => (ImageSource)GetValue(IconSourceProperty); set => SetValue(IconSourceProperty, value); }

        public TabHeaderButton() {
            InitializeComponent();
            UpdateShowIcon();
        }

        void UpdateText() {
            label.Text = Text;
        }
        void UpdateTextColor() {
            label.TextColor = TextColor;
        }
        void UpdateIconColor() {
            icon.ForegroundColor = IconColor;
        }
        void UpdateFontFamily() {
            label.FontFamily = FontFamily;
        }
        void UpdateFontSize() {
            label.FontSize = FontSize;
        }
        void UpdateSelection() {
            UpdateBackgroundSelection();
            UpdateColorSelection();
        }
        void UpdateBackgroundSelection() {
            Color color = DXColor.Transparent;
            if (IsSelected && SelectedBackgroundColor != DXColor.Default) {
                color = SelectedBackgroundColor;
            }
            BackgroundColor = color;
            label.BackgroundColor = color;
        }
        void UpdateColorSelection() {
            Color textColor = TextColor;
            Color iconColor = IconColor;
            if (IsSelected && SelectedColor != DXColor.Default) {
                textColor = SelectedColor;
                iconColor = SelectedColor;
            }
            label.TextColor = textColor;
            icon.ForegroundColor = iconColor;
        }
        void UpdateShowIcon() {
            this.Children.Clear();
            if (ShowIcon) {
                this.Children.Add(icon);
                this.Children.Add(label);
            } else {
                label.VerticalOptions = LayoutOptions.Center;
                label.VerticalTextAlignment = TextAlignment.Center;

                this.Children.Add(label);
            }
        }
        void UpdateIconSource() {
            icon.Source = IconSource;
        }
    }
}