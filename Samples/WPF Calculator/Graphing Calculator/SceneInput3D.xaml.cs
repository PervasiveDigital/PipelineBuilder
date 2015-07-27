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
    /// Interaction logic for SceneInput3D.xaml
    /// </summary>
    public partial class SceneInput3D : System.Windows.Controls.UserControl
    {
        public SceneInput3D()
        {
            InitializeComponent();
            GraphIt.Click += new RoutedEventHandler(GraphIt_Click);
            Sphere.Click += new RoutedEventHandler(Sphere_Click);
            Torus.Click += new RoutedEventHandler(Torus_Click);
            Cone.Click += new RoutedEventHandler(Cone_Click);
        }

        void Cone_Click(object sender, RoutedEventArgs e)
        {
            EquationX.Text = ".6(1.5-v)cos(u)";
            EquationY.Text = "v";
            EquationZ.Text = ".6(1.5-v)sin(-u)";
        }

        void Torus_Click(object sender, RoutedEventArgs e)
        {
            EquationX.Text = "-(1+.25cos(v))cos(u)";
            EquationY.Text = "(1+.25cos(v))sin(u)";
            EquationZ.Text = "-.25sin(v)";
        }

        void Sphere_Click(object sender, RoutedEventArgs e)
        {
            EquationX.Text = "cos(u)sin(v)";
            EquationY.Text = "-cos(v)";
            EquationZ.Text = "sin(-u)sin(v)";

        }

        void GraphIt_Click(object sender, RoutedEventArgs e)
        {
            Display.Children.Clear();
            Grapher grapher = new Grapher();
            Display.Children.Add(grapher.Show3D(this.EquationX.Text, this.EquationY.Text,this.EquationZ.Text));
        }
    }
}
