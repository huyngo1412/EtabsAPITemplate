using AttachInstance.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttachInstance.UI.Utilities
{
    public sealed class RelayCommand : RelayCommandBase
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null, bool useCommandManagerRequery = false)
            : base(useCommandManagerRequery)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? (() => true);
        }

        public override bool CanExecute(object parameter)
        {
            ThrowIfDisposed();
            return _canExecute();
        }

        public override void Execute(object parameter)
        {
            ThrowIfDisposed();
            _execute();
        }
    }

    // =========================
    // RelayCommand<T> (type-safe + safe cast)
    // =========================
    public sealed class RelayCommand<T> : RelayCommandBase
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null, bool useCommandManagerRequery = false)
            : base(useCommandManagerRequery)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? (_ => true);
        }

        public override bool CanExecute(object parameter)
        {
            ThrowIfDisposed();

            if (!TryConvert(parameter, out T value))
            {
                // If the parameter can't be converted:
                // - you should usually return false to avoid enabling it by mistak
                return false;
            }

            return _canExecute(value);
        }

        public override void Execute(object parameter)
        {
            ThrowIfDisposed();

            if (!TryConvert(parameter, out T value))
                throw new ArgumentException(
                    $"CommandParameter không hợp lệ. Expected: {typeof(T).FullName}, Actual: {parameter?.GetType().FullName ?? "null"}");

            _execute(value);
        }

        private static bool TryConvert(object parameter, out T value)
        {
            // Case 1: parameter null
            if (parameter is null)
            {
                // null is valid if T is a reference type or Nullable<T>
                if (default(T) == null)
                {
                    value = default;
                    return true;
                }

                value = default;
                return false; //  null is not valid
            }

            // Case 2: already correct type
            if (parameter is T t)
            {
                value = t;
                return true;
            }

            // Case 3: allow convert for Nullable<T> and some primitives
            try
            {
                var targetType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
                var converted = Convert.ChangeType(parameter, targetType);
                value = (T)converted;
                return true;
            }
            catch
            {
                value = default;
                return false;
            }
        }
    }
}
