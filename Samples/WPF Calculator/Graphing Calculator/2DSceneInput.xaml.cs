using System;
using System.Collections;
using System.Collections.Generic;
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

namespace GraphCalc
{
    /// <summary>
    /// Interaction logic for _DSceneInput.xaml
    /// </summary>
    public partial class SceneInput2D : System.Windows.Controls.UserControl
    {
        public SceneInput2D()
        {
            InitializeComponent();
            GraphIt.Click += new RoutedEventHandler(GraphIt_Click);
        }

        void GraphIt_Click(object sender, RoutedEventArgs e)
        {
            Display.Children.Clear();
            Grapher grapher = new Grapher();
            Display.Children.Add(grapher.Show2D(this.Equation.Text));
        }
    }
}
