using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            int num1;
            int num2;

            if (int.TryParse(input1Tbx.Text, out num1)
                && int.TryParse(input2Tbx.Text, out num2))
            {
                sumTbx.Text = (num1 + num2).ToString();
                return;
            }

            sumTbx.Text = "Error";
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            input1Tbx.Clear();
            input2Tbx.Clear();
            sumTbx.Clear();
        }
    }
}
