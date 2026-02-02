using EtabsAPIPlugin.UI.Utilities;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EtabsAPIPlugin.UI.Components
{
    /// <summary>
    /// Interaction logic for ControlBarView.xaml
    /// </summary>
    public partial class ControlBarView : UserControl
    {
        private ControlBarViewModel _controlBarViewModel { get; set; }
        public ControlBarView()
        {
            this.DataContext = _controlBarViewModel = new ControlBarViewModel();
            InitializeComponent();
        }
    }
}
