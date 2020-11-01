using ActReport.Core.Entities;
using ActReport.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ActReport.ViewModel
{
    public class CreateAndEditActivityViewModel : BaseViewModel
    {
        private bool _create;
        private DateTime _from;
        private DateTime _till;
        private DateTime _date;
        private string _text;
        private Activity _activity;

        public DateTime From
        {
            get => _from;

            set
            {
                _from = value;
                OnPropertyChanged(nameof(CreateAndEditActivityViewModel));
            }
        }

        public DateTime Till
        {
            get => _till;

            set
            {
                _till = value;
                OnPropertyChanged(nameof(CreateAndEditActivityViewModel));
            }
        }

        public DateTime Date
        {
            get => _date;

            set
            {
                _date = value;
                OnPropertyChanged(nameof(CreateAndEditActivityViewModel));
            }
        }

        public string Text
        {
            get => _text;

            set
            {
                _text = value;
                OnPropertyChanged(nameof(CreateAndEditActivityViewModel));
            }
        }

        public Activity Activity
        {
            get => _activity;

            set
            {
                _activity = value;
                OnPropertyChanged(nameof(CreateAndEditActivityViewModel));
            }
        }

        public CreateAndEditActivityViewModel(IController controller, Activity activity, Employee employee) : base(controller)
        {
            if (activity == null)
            {
                _create = true;
                _from = DateTime.Now;
                _till = DateTime.Now;
                _date = DateTime.Now;
                _activity = new Activity();
                _activity.Employee = employee;
                _activity.Employee_Id = employee.Id;
            }
            else
            {
                _create = false;
                _from = activity.StartTime;
                _till = activity.EndTime;
                _activity = activity;
                _text = activity.ActivityText;
                _date = activity.Date;
            }
        }

        private ICommand _cmdSaveActivities;

        public ICommand CmdSaveActivites
        {
            get
            {
                if (_cmdSaveActivities == null)
                {
                    _cmdSaveActivities = new RelayCommand(
                        execute: _ =>
                        {
                            using (UnitOfWork uow = new UnitOfWork())
                            {
                                Activity.ActivityText = Text;
                                Activity.Date = Date;
                                Activity.StartTime = From;
                                Activity.EndTime = Till;
                                if (_create)
                                {
                                    uow.ActivityRepository.Insert(Activity);
                                }
                                else
                                {
                                    uow.ActivityRepository.Update(Activity);
                                }
                                uow.Save();
                                _controller.CloseWindow(this);
                            }
                        },
                        canExecute: _ => _activity != null);
                }
                return _cmdSaveActivities;
            }
            set { _cmdSaveActivities = value; }
        }

        private ICommand _cmdReturnToEmployees;

        public ICommand CmdReturnToEmployees
        {
            get
            {
                if (_cmdReturnToEmployees == null)
                {
                    _cmdReturnToEmployees = new RelayCommand(
                        execute: _ => _controller.CloseWindow(this),
                        canExecute: _ => true);
                }
                return _cmdReturnToEmployees;
            }
            set { _cmdReturnToEmployees = value; }
        }
    }
}
