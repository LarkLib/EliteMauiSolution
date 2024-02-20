using System.Collections.Generic;
using Elite.LMS.Maui.WmsModules.Grid.Data;

namespace Elite.LMS.Maui.ViewModels {
    public class DragDropModel : NotificationObject {
        readonly EmployeeTasksRepository repository;

        public IList<EmployeeTask> ItemSource => this.repository.EmployeeTasks;

        public DragDropModel(EmployeeTasksRepository repository) {
            this.repository = repository;
        }
    }
}
