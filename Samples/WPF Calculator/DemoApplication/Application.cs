using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Xml;

namespace DemoApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public class Application : System.Windows.Application
    {
        public Application()
        {
            this.StartupUri = new System.Uri("CalculatorHost.xaml", System.UriKind.Relative);
        }

        public static Application App;

        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.LoaderOptimization(LoaderOptimization.MultiDomainHost)]
        public static void Main()
        {
            DemoApplication.Application app = new DemoApplication.Application();
            App = app;
            app.Run();
        }
    }
}
