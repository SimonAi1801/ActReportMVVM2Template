using ActReport.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ActReport.UI
{
    public class MainController : IController
    {
        private Dictionary<BaseViewModel, Window> _windows;

        public MainController()
        {
            _windows = new Dictionary<BaseViewModel, Window>();
        }

        public void CloseWindow(BaseViewModel viewModel)
        {
            if (_windows.ContainsKey(viewModel))
            {
                _windows[viewModel].Close();
                _windows.Remove(viewModel);
            }
        }

        public void ShowWindow(BaseViewModel viewModel)
        {
            Window window = viewModel switch
            {
                null => throw new ArgumentNullException(nameof(viewModel)),

                EmployeeViewModel _ => new MainWindow(),
                ActivityViewModel _ => new ActivityWindow(),

                _ => throw new InvalidOperationException($"Unkown ViewModel of type {viewModel}")
            };

            _windows[viewModel] = window;
            window.DataContext = viewModel;
            window.ShowDialog();
        }
    }
}
