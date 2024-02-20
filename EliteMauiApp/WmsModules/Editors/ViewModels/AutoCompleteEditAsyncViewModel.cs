namespace Elite.LMS.Maui.WmsModules.Editors.ViewModels {
    public class AutoCompleteEditAsyncViewModel : ComboBoxEditViewModel {
        public AutoCompleteEditAsyncViewModel() {
            ShowHelpText = false;
        }

        protected override string DefaultHelpText { get => "Type a person name"; }

    }
}

