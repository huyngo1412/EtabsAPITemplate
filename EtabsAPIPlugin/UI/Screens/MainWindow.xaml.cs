using EtabsAPIPlugin.UI.Utilities;
using ETABSv1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EtabsAPIPlugin.UI.Screens
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private cSapModel _sapModel;
        private cPluginCallback _pluginCallback;
        private MainViewModel _mainWindowUtil { get; set; }
        public MainWindow()
        {

            InitializeComponent();
        }
        public void SetSapModel(ref cSapModel inSapModel, ref cPluginCallback inPluginCallback)
        {
            _sapModel = inSapModel;
            _pluginCallback = inPluginCallback;
            this.DataContext = _mainWindowUtil = new MainViewModel(ref _sapModel, ref _pluginCallback);

        }
    }
}
