using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameServer;

namespace server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Server _server;
        private async void startBtn_Click(object sender, EventArgs e)
        {
            try
            {
                _server = new Server(IPAddress.Parse(addressBox.Text), decimal.ToInt32(portBox.Value));
                _server.Inform += _server_Inform;
                startBtn.Enabled = false;
                await _server.StartAsync();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void _server_Inform(string message)
        {
            Invoke(new Action(() => { logBox.Items.Add(message); }));
        }
    }
}
