using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Safe
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            ServerConfig.URL = "http://contest-001-site1.etempurl.com/";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                ServerConfig.URL = textBox1.Text;
            }

            MessageBox.Show("Server: " + ServerConfig.URL);
            this.Close();
        }
    }
}
