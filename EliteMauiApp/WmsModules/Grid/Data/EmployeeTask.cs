using System;
using System.Windows.Input;
using Elite.LMS.Maui.ViewModels;
using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui.WmsModules.Grid.Data {
    public class EmployeeTask : NotificationObject {
        public EmployeeTask() {
            CompleteTaskCommand = new Command(() => Status = 100);
            UnCompleteTaskCommand = new Command(() => Status = 0);
        }

        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }

        public ICommand CompleteTaskCommand { get; }
        public ICommand UnCompleteTaskCommand { get; }

        int status;
        public int Status {
            get => this.status;
            set => SetProperty(ref this.status, value, () => OnPropertyChanged(nameof(Completed)));
        }

        public bool Completed => Status == 100;
    }
}
