namespace Elite.LMS.Maui.WmsModules.Editors.ViewModels {
    public class AutoCompleteEditViewModel : ComboBoxEditViewModel {
        public AutoCompleteEditViewModel() {
            ShowHelpText = false;
        }

        protected override string DefaultHelpText { get => "Type a contact name or phone number"; }

    }
}

