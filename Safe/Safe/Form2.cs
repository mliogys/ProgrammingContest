using System;
using System.Threading;
using System.Windows.Forms;

namespace Safe
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            

            //Thread.Sleep(2000);
            this.Close();
        }
    }
}
