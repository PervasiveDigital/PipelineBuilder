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
using System.Windows.Markup;
using HostView;


[assembly: XmlnsDefinition("http://myapplication", "DemoApplication")]
   
namespace DemoApplication
{

     /// <summary>
    /// Interaction logic for ActionLayout.xaml
    /// </summary>
    public partial class ActionLayout : System.Windows.Controls.UserControl
    {

        CalculatorBase _calc;
        CalculatorHost _app;
        EventHandler<StackChangedEventArgs> _stackChangedHandler;

        public ActionLayout()
        {
            InitializeComponent();
            _stackChangedHandler = new EventHandler<StackChangedEventArgs>(application_StackChanged);
        }

        public CalculatorBase Calculator
        {
            get
            {
                return _calc;
            }
            set
            {
                _calc = value;
                if (_calc == null)
                {
                    _app.StackChanged -= _stackChangedHandler;
                }
            }
        }

        public ActionLayout(CalculatorBase calc, CalculatorHost application) : this()
        {
            _calc = calc;
            _app = application;
            this.Title.Text = calc.Name;
            foreach (Operation op in calc.Operations)
            {
                Button b = new Button();
                b.Tag = op;
                b.Content = op.Name;
                b.Click += new RoutedEventHandler(b_Click);
                Actions.Children.Add(b);
            }
            ValidateButtons(application.Stack.Items.Count);
            application.StackChanged += _stackChangedHandler;
        }

        void application_StackChanged(object sender, StackChangedEventArgs e)
        {
            ValidateButtons(e.Count);
        }

        private void ValidateButtons(int numOperands)
        {
            foreach (Object obj in Actions.Children)
            {
                Button b = (Button)obj;
                Operation op = (Operation)b.Tag;
                if (numOperands >= op.NumOperands)
                {
                    b.IsEnabled = true;
                }
                else
                {
                    b.IsEnabled = false;
                }

            }
        }

        void b_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            Operation op = (Operation)b.Tag;
            _app.Operate(_calc, op);
        }

    }
}
