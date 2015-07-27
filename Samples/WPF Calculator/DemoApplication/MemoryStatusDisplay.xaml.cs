using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoApplication
{
    /// <summary>
    /// Interaction logic for MemoryStatusDisplay.xaml
    /// </summary>
    public partial class MemoryStatusDisplay : System.Windows.Window
    {
        public MemoryStatusDisplay()
        {
            InitializeComponent();
        }

        public void UpdateUsage(String use)
        {
            this.MemLabel.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new UpdateUsageD(this.UpdateUsageInvoker), use);
        }

        internal void UpdateUsageInvoker(String use)
        {
            this.MemLabel.Text = use;
            this.UpdateLayout();
        }
        
        private delegate void UpdateUsageD(String use);
    }
}
