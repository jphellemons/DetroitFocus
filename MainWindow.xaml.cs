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
using Coding4Fun.Obd.ObdManager;

namespace DetroitFocus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObdDevice obd;

        public MainWindow()
        {
            InitializeComponent();

            obd = new ObdDevice();
            obd.ObdChanged += _obd_ObdChanged;
            
            obd.Connect("COM1", 115200, ObdDevice.UnknownProtocol, true);

            Dictionary<int,List<int>> sPids = obd.GetSupportedPids();

            foreach(var sp in sPids.Keys)
            {
                log.Text += "Key : " + sp.ToString() + Environment.NewLine;
                List<int> keyValues = new List<int>();
                if (sPids.TryGetValue((int)sp, out keyValues))
                {
                    foreach (var kv in keyValues)
                    {
                        log.Text += "Value : " + kv.ToString() + Environment.NewLine;
                    }
                }
            }
            //obd.GetPidData()
        }

        private void _obd_ObdChanged(object sender, ObdChangedEventArgs e)
        {
            log.Text += e.ObdState.ToString() + Environment.NewLine;
        }
    }
}
