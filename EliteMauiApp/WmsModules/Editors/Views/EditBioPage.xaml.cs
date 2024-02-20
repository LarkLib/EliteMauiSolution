using System;
using Elite.LMS.Maui.Wms;
using Elite.LMS.Maui.WmsModules.Editors.ViewModels;
using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui.Views;

public partial class EditBioPage : WmsPage {
    SettingsFormViewModel settings;
    public SettingsFormViewModel Settings {
        get => this.settings;
        set {
            this.settings = value;
            this.bioEditor.Text = value.Bio;
        }
    }
    public EditBioPage() {
        InitializeComponent();
    }

    async void OnAccept(object sender, EventArgs e) {
        Settings.Bio = this.bioEditor.Text;
        await Shell.Current.Navigation.PopAsync();
    }

    void OnEditorLoaded(object sender, EventArgs e) {
        this.bioEditor.Focus();
    }
}