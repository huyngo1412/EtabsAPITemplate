using AttachInstance.Core.Abstractions;
using AttachInstance.Infastructure.DataAccess;
using ETABSv1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AttachInstance.UI.Utilities
{
    public class MainViewModel : BaseViewModel
    {
        cHelper myHelper = new Helper();

        private readonly IEtabsConnectionService _conn;
        private readonly EtabsRepository _repo;


        // Expose ICommand for View binding
        public ICommand GetStoryCommand => GetStoryCommandImpl;
        public ICommand ConnectEtabsCommand => ConnectEtabsCommandImpl;

        // Keep it as a reference type so RaiseCanExecuteChanged() can be called without casting
        private RelayCommand GetStoryCommandImpl { get; }
        private RelayCommand ConnectEtabsCommandImpl { get; }

        public MainViewModel(IEtabsConnectionService conn, EtabsRepository repo)
        {
            _conn = conn;
            _repo = repo;
            ConnectEtabsCommandImpl = new RelayCommand(execute: ConnectEtabs);
            GetStoryCommandImpl = new RelayCommand(execute: CounStory);
        }

        public void ConnectEtabs()
        {

            ETABSv1.cOAPI myETABSObject = null;
            //create API helper object
            myHelper = new ETABSv1.Helper();
            //get the active ETABS object
            myETABSObject = myHelper.GetObject("CSI.ETABS.API.ETABSObject");
            if (myETABSObject == null)
            {
                //if the ETABS is not running , get nothing back
                MessageBox.Show("No running instance of ETABS found");
                return;
            }
            else
            {
                _conn.Connect(myETABSObject);
                MessageBox.Show("Connected to ETABS");
            }
        }
        public void CounStory() { 
            int storyCount = _repo.GetStoryCount();
            MessageBox.Show($"Number of Stories: {storyCount}");
        }
    }
}
