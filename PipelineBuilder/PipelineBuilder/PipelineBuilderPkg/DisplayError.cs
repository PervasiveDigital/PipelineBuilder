using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PipelineBuilderPkg
{
    public partial class DisplayError : Form
    {
        public DisplayError()
        {
            InitializeComponent();
        }

        public void SetError(object error)
        {
            t_errorText.Text = error.ToString();
        }
    }
}