using EtabsAPIPlugin.Core.Abstractions;
using ETABSv1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EtabsAPIPlugin.UI.Utilities
{
    public class MainViewModel : BaseViewModel
    {
        //Properties
        private cSapModel _sapModel;
        private cPluginCallback _pluginCallback;
        private int errorCode = 0; // default return code is no error
        private bool _isLockedModel;
        public bool IsLockedModel
        {
            get => _isLockedModel;
            set
            {
                if (SetProperty(ref _isLockedModel, value))
                {
                    LockModelImpl.RaiseCanExecuteChanged();
                }
            }
        }


        // Expose ICommand for View binding
        public ICommand LoadWindowCommand => LoadWindowCommandImpl;
        public ICommand ClosedWindowCommand => ClosedWindowCommandImpl;
        public ICommand LockModelCommand => LockModelImpl;


        // Keep it as a reference type so RaiseCanExecuteChanged() can be called without casting
        private RelayCommand LoadWindowCommandImpl { get; }
        private RelayCommand<bool> LockModelImpl { get; }
        private RelayCommand ClosedWindowCommandImpl { get; }
        public MainViewModel(ref cSapModel inSapModel, ref cPluginCallback inPluginCallback)
        {
            IsLockedModel = false;
            this._sapModel = inSapModel;
            this._pluginCallback = inPluginCallback;
            LoadWindowCommandImpl = new RelayCommand(execute : LoadWindow,canExecute:()=> _sapModel!=null);
            ClosedWindowCommandImpl = new RelayCommand(execute : ClosedWindow);
            LockModelImpl = new RelayCommand<bool>(execute: LockModelEtabs, canExecute: x =>  _sapModel != null);
        }
        private void LockModelEtabs(bool locked)
        {
            if (locked == false)
            {
                errorCode = _sapModel.SetModelIsLocked(true);

                if (errorCode == 0)
                {
                    MessageBox.Show("The model has been locked successfully.",
                                    "ETABS", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Failed to lock the model. Error code: {errorCode}",
                                    "ETABS", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("The model is already locked.",
                                "ETABS", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void ClosedWindow()
        {
            _pluginCallback.Finish(0);
        }
        private void LoadWindow()
        {
            IsLockedModel = _sapModel.GetModelIsLocked();
            LockModelImpl.RaiseCanExecuteChanged();
        }
    }
}
