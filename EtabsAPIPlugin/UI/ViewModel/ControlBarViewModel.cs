using EtabsAPIPlugin.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EtabsAPIPlugin.UI.Utilities
{
    public class ControlBarViewModel : BaseViewModel
    {
        // Expose ICommand for binding
        public ICommand CloseWindowCommand => CloseWindowCommandImpl;
        public ICommand MiniMizeWindowCommand => MiniMizeWindowCommandImpl;
        public ICommand MaxiMizeWindowCommand => MaxiMizeWindowCommandImpl;
        public ICommand MouseMoveWindowCommand => MouseMoveWindowCommandImpl;

        // Keep as RelayCommand<T> to avoid casting when needed
        private RelayCommand<UserControl> CloseWindowCommandImpl { get; }
        private RelayCommand<UserControl> MiniMizeWindowCommandImpl { get; }
        private RelayCommand<UserControl> MaxiMizeWindowCommandImpl { get; }
        private RelayCommand<UserControl> MouseMoveWindowCommandImpl { get; }

        public ControlBarViewModel()
        {
            CloseWindowCommandImpl = new RelayCommand<UserControl>(
                execute: CloseWindow,
                canExecute: p => p != null);

            MiniMizeWindowCommandImpl = new RelayCommand<UserControl>(
                execute: ToggleMinimizeWindow,
                canExecute: p => p != null);

            MaxiMizeWindowCommandImpl = new RelayCommand<UserControl>(
                execute: ToggleMaximizeWindow,
                canExecute: p => p != null);

            MouseMoveWindowCommandImpl = new RelayCommand<UserControl>(
                execute: DragMoveWindow,
                canExecute: p => p != null);
        }

        // =========================
        // Command handlers
        // =========================
        private void CloseWindow(UserControl control)
        {
            if (TryGetWindow(control, out var window))
                window.Close();
        }

        private void ToggleMinimizeWindow(UserControl control)
        {
            if (!TryGetWindow(control, out var window)) return;

            window.WindowState = window.WindowState == WindowState.Minimized
                ? WindowState.Normal
                : WindowState.Minimized;
        }

        private void ToggleMaximizeWindow(UserControl control)
        {
            if (!TryGetWindow(control, out var window)) return;

            window.WindowState = window.WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
        }

        private void DragMoveWindow(UserControl control)
        {
            if (TryGetWindow(control, out var window))
                window.DragMove();
        }

        // =========================
        // Helpers
        // =========================
        private static bool TryGetWindow(UserControl control, out Window window)
        {
            window = null;
            if (control == null) return false;

            var parent = control as FrameworkElement;

            while (parent != null)
            {
                if (parent is Window w)
                {
                    window = w;
                    return true;
                }

                parent = parent.Parent as FrameworkElement;
            }

            return false;
        }
    }
}
