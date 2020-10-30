using ActReport.Core.Entities;
using ActReport.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ActReport.ViewModel
{
    public class ActivityViewModel : BaseViewModel
    {
        private Employee _employee;
        private ObservableCollection<Activity> _activities;
        public ObservableCollection<Activity> Activities
        {
            get => _activities;
            set 
            {
                _activities = value;
                OnPropertyChanged(nameof(Activities));
            }
        }

        public string FullName => $"{_employee.FirstName} {_employee.LastName}";
        public ActivityViewModel(IController controller, Employee employee) : base(controller)
        {
            _employee = employee;
            using UnitOfWork uow = new UnitOfWork();
            Activities = new ObservableCollection<Activity>(uow.ActivityRepository.Get(
                filter: x => x.Employee_Id == employee.Id,
                orderBy: coll => coll.OrderBy(activity => activity.Date).ThenBy(activity => activity.StartTime)));
        }
    }
}
