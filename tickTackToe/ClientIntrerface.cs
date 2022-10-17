using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameClient;
using TicTacToe;

namespace tickTackToe
{
    public partial class GameInterface : Form
    {
        private Client _client;
        public GameInterface(Client client)
        {
            InitializeComponent();
            _client = client;
            _client.Response += _client_Response;
            _client.Lock += _client_Lock;
            _client.UnLock += _client_UnLock;
            _client.MarkCell += _client_MarkCell;
            _client.IsConnected += _client_IsConnected;
            _client.End += _client_End;
        }

        private void _client_End(bool? result)
        {
            string resText = String.Empty;
            switch (result)
            {
                case null:
                    resText = "Draw";break;
                case true:
                    resText = "Win";break;
                case false:
                    resText = "Loose";break;
            }

            MessageBox.Show(resText);
            Action a = Close;
            if (InvokeRequired)
                Invoke(a);
            else
            {
                a();
            }

        }

        private void _client_MarkCell(int cell, char symbol)
        {
            if(cell == -1) return;
            Action a = () =>
            {
                foreach (var control in fieldPanel.Controls)
                {
                    if (!(control is Button b)) continue;
                    if (b.Tag.ToString().Equals(cell.ToString()))
                    {
                        b.Enabled = false;
                        b.Text = symbol.ToString();
                    }
                }
            };
            if (InvokeRequired)
                Invoke(a);
            else
            {
                a();
            }
        }

        private void _client_UnLock()
        {
            Action a = () => fieldPanel.Enabled = true;
            if (InvokeRequired)
                Invoke(a);
            else
            {
                a();
            }
        }

        private void _client_Lock()
        {
           Action a = ()=> fieldPanel.Enabled = false;
            if (InvokeRequired)
                Invoke(a);
            else
            {
                a();
            }
        }

        private void _client_IsConnected(bool isConnect)
        {
            Invoke(isConnect ? new Action(_connection) : new Action(_disconnection));
        }

        private void _disconnection()
        {
            this.Close();
        }

        private void _connection()
        {
            //todo
        }

        private void _client_Error(string error)
        {
            MessageBox.Show(error);
        }

        private void _client_Response(string response)
        {

        }

        private async void button_Click(object sender, EventArgs e)
        {
            if (!(sender is Button b))
                return;
            await _client.SendCellAsync(Int32.Parse(b.Tag.ToString()));
        }

        private async void GameInterface_Load(object sender, EventArgs e)
        {
            await _client.ReadResponseAsync();
        }

        private void GameInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            _client.Disconnect();
        }
    }
}
