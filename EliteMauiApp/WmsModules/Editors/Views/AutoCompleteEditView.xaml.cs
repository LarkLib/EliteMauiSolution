using System;
using System.Collections.Generic;
using Elite.LMS.Maui.WmsModules.Grid.Data;
using DevExpress.Maui.Core;
using DevExpress.Maui.Core.Internal;
using DevExpress.Maui.Editors;
using Microsoft.Maui.Graphics;

namespace Elite.LMS.Maui.Views {
    public partial class AutoCompleteEditView : Wms.WmsPage {
        IList<Employee> employees;
        Color accentColor;

        public AutoCompleteEditView() {
            InitializeComponent();
            this.accentColor = ThemeManager.Theme.Scheme.Primary.OverrideAlpha(0.38);
            this.employees = new EmployeesRepository().Employees;
        }

        void AutocompleteEdit_TextChanged(object sender, AutoCompleteEditTextChangedEventArgs e) {
            if (e.Reason == AutoCompleteEditTextChangeReason.ItemSelected)
                return;

            if (String.IsNullOrEmpty(this.autocompleteEdit.Text)) {
                this.autocompleteEdit.ItemsSource = null;
                this.autocompleteEdit.IsDropDownOpen = false;
                return;
            }

            List<EmployeeCardViewModel> source = new List<EmployeeCardViewModel>();
            foreach (Employee employee in this.employees) {
                PhoneBookEntryMatch phoneNumberMatch = PhonebookMatchHelper.MatchPhoneNumber(employee.Phone, this.autocompleteEdit.Text);
                PhoneBookEntryMatch fullNameMatch = PhonebookMatchHelper.MatchPhoneLetters(employee.FullName, this.autocompleteEdit.Text);
                if (phoneNumberMatch == null && fullNameMatch == null)
                    continue;
                source.Add(new EmployeeCardViewModel(employee, phoneNumberMatch?.AsFormattedString(this.accentColor), fullNameMatch?.AsFormattedString(this.accentColor)));
            }

            this.autocompleteEdit.ItemsSource = source;
        }
    }
}
