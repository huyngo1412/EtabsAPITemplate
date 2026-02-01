using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EtabsAPIPlugin.Core.Abstractions
{
    public abstract class RelayCommandBase : ICommand, IDisposable
    {
        private readonly bool _useCommandManagerRequery;
        private readonly SynchronizationContext _syncContext;
        private bool _isDisposed;

        protected RelayCommandBase(bool useCommandManagerRequery = false)
        {
            _useCommandManagerRequery = useCommandManagerRequery;
            _syncContext = SynchronizationContext.Current; // UI thread context if created on UI
        }

        public event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);

        /// <summary>
        /// Actively update the CanExecute state in the UI.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (_isDisposed) return;

            void Raise()
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);

                // Optional: trigger WPF's global requery 
                if (_useCommandManagerRequery)
                {
                    //If InvalidateRequerySuggested is static =>WPF will automatically requery all commands.
                    CommandManager.InvalidateRequerySuggested();
                }
            }

            // Ensure UI thread if we captured one
            if (_syncContext != null && SynchronizationContext.Current != _syncContext)
            {
                _syncContext.Post(_ => Raise(), null);
            }
            else
            {
                Raise();
            }
        }

        public void Dispose()
        {
            _isDisposed = true;
        }

        protected void ThrowIfDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().Name);
        }
    }
}
