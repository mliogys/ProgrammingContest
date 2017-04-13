using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Safe
{
    public partial class Form1 : Form
    {
        private HubConnection connection;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Respond(bool codeCorrect, string teamName)
        {
            if (codeCorrect)
            {
                int n = panel2.Controls.Count + 1;
                if (n <= 3)
                {
                    Label l = new Label();
                    string title = n.ToString() + ". " + teamName;
                    l.Text = title;
                    l.Size = new Size(500, 30);
                    l.Location = new Point(0, 30 * (n - 1));
                    l.ForeColor = Color.Yellow;
                    l.Font = new Font(new FontFamily("Microsoft Sans Serif"), 16, FontStyle.Bold);
                    panel2.Controls.Add(l);
                }
                else
                {
                    n = panel3.Controls.Count + 1;
                    Label l = new Label();
                    string title = (n + 3).ToString() + ". " + teamName;
                    l.Text = title;
                    l.Size = new Size(460, 30);
                    l.Location = new Point(0, 30 * (n - 1));
                    l.ForeColor = Color.Yellow;
                    l.Font = new Font(new FontFamily("Microsoft Sans Serif"), 16, FontStyle.Bold);
                    panel3.Controls.Add(l);
                }

                Thread th = new Thread(() =>
                {
                    System.Media.SoundPlayer sound = new System.Media.SoundPlayer();
                    sound.Stream = Properties.Resources.clapping;
                    sound.PlaySync();
                    
                });
                th.Start();

                Thread th2 = new Thread(() =>
                {
                    this.BeginInvoke((MethodInvoker)delegate {
                        pictureBox1.Image = Properties.Resources.large;
                    });
                    Thread.Sleep(4400);
                    this.BeginInvoke((MethodInvoker)delegate {
                        pictureBox1.Image = null;
                    });
                });
                th2.Start();
            }
            else
            {
                Thread th = new Thread(() =>
                {
                    System.Media.SoundPlayer sound = new System.Media.SoundPlayer();
                    sound.Stream = Properties.Resources.wrong;
                    sound.PlaySync();
                });
                th.Start();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox1.Text += "1";

            //Respond(true, "aaaa aaaaaaaaaaaaaaa aaaaaaaaaaaaaa aaa");
            

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += "2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "3";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "6";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "8";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "9";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text += "0";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals(""))
            {
                string text = textBox1.Text;
                text = text.Substring(0, text.Length - 1);
                textBox1.Text = text;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.FormClosed += F_FormClosed;
            f.ShowDialog();
        }

        private void F_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection = new HubConnection(ServerConfig.URL);

            try
            {
                if (connection.State != Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
                {
                    var hub = connection.CreateHubProxy("SafeCode");
                    connection.Start().Wait();
                    hub.On<bool, string>("hello", (status, teamName) =>
                    {
                        this.BeginInvoke((MethodInvoker)delegate
                        {
                            Respond(status, teamName);
                        });
                    });
                }
            }
            catch (Microsoft.AspNet.SignalR.Client.HubException exc)
            {
                MessageBox.Show(exc.Message);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
