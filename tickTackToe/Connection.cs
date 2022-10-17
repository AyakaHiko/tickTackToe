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
using GameClient;

namespace tickTackToe
{
    public partial class Connection : Form
    {
        public Connection()
        {
            InitializeComponent();
        }

        private Client _client;
        private async void connectBtn_Click(object sender, EventArgs e)
        {
            _switch();

            try
            {
                _client = new Client(IPAddress.Parse(addressBox.Text),decimal.ToInt32(portBox.Value));
                _client.IsConnected += _client_IsConnected;
                await _client.ConnectAsync();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void _client_IsConnected(bool isConnect)
        {
            Invoke(isConnect ? new Action(_connection) : new Action(_disconnection));
        }

        private void _disconnection()
        {
            _switch();
        }

        public GameInterface Game;
        private void _connection()
        {
            this.Hide();
            Game = new GameInterface(_client);
            Close();
        }

        private void _switch()
        {
            connectBtn.Enabled = !connectBtn.Enabled;
            (informLbl.Visible, connectConfig.Enabled) = (connectConfig.Enabled, informLbl.Visible);
        }

        private void Connection_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
