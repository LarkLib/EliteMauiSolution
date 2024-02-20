using System.Collections.Generic;
using Elite.LMS.Maui.WmsModules.CollectionView.Data;

namespace Elite.LMS.Maui.ViewModels {
    public class DefaultSwipeViewModel : NotificationObject {
        readonly EmployeeTasksRepository repository;

        public IList<EmployeeTask> ItemSource => this.repository.EmployeeTasks;

        public DefaultSwipeViewModel(EmployeeTasksRepository repository) {
            this.repository = repository;
        }
    }
}
