using Elite.LMS.Maui.WmsModules.Grid.Data;
using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui.Views {
    public class EmployeeCardViewModel {
        public EmployeeCardViewModel(Employee employee, FormattedString phoneFormattedString, FormattedString nameFormattedString) {
            Employee = employee;
            FormattedPhone = phoneFormattedString ?? Employee.Phone;
            FormattedFullName = nameFormattedString ?? Employee.FullName;
        }

        public Employee Employee { get; }
        public FormattedString FormattedPhone { get; }
        public FormattedString FormattedFullName { get; }
        public ImageSource EmployeeIcon { get { return Employee.Image; } }
    }
}
