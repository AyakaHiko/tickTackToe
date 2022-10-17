using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameMessage;

namespace GameClient
{
    public class Client
    {
        private TcpClient _client;
        private IPAddress _serverIp;
        private int _serverPort;

        public bool Connected => _client != null && _client.Connected;

        public Client(IPAddress address, int port)
        {
            _init();
            _client = new TcpClient();
            _serverIp = address;
            _serverPort = port;
        }
        public event Action<bool> IsConnected;
        private CancellationTokenSource _cts;
        private CancellationToken _token;
        public Task ConnectAsync() => Task.Run(Connect, _token);
        public void Connect()
        {
            if (Connected)
                return;
            try
            {
                _client.Connect(_serverIp, _serverPort);
                if (!_client.Connected)
                {
                    return;
                }

                _cts = new CancellationTokenSource();
                _token = _cts.Token;
                var message = _read();
                if (message != null && message.Text.Equals("start", StringComparison.CurrentCultureIgnoreCase))
                    IsConnected?.Invoke(true);
            }
            catch (Exception e)
            {
                IsConnected.Invoke(false);

            }
        }

        public void Disconnect()
        {
            try
            {
                _cts.Cancel();
                _client.Close();
                var message = new MessagePacket(MessageType.Command) { Text = "Disconnect"};
                Send(message);
                IsConnected?.Invoke(false);
            }
            catch (Exception e)
            {
            }
        }

        private void Send(MessagePacket message)
        {
            if (!Connected)
            {
                return;
            }
            var stream = _client.GetStream();
            try
            {
                var buffer = message.ToBytes();
                stream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception e)
            {
                Disconnect();
            }
        }

        public void SendCell(int cell)
        {
            var message = new MessagePacket(MessageType.Data) { Cell = cell };
            Send(message);
        }

        public async Task SendCellAsync(int query) => await Task.Run(() => SendCell(query), _token);
        public event Action<string> Response;
        private List<string> _endGameCommand = new List<string>();

        private void _init()
        {
            _endGameCommand.Add("Authorization false");
            _endGameCommand.Add("Query limit");
        }

        private MessagePacket _read()
        {
            var stream = _client.GetStream();
            byte[] buffer = new byte[1024];
            do
            {
                stream.Read(buffer, 0, buffer.Length);
            } while (stream.DataAvailable);

            return MessagePacket.FromBytes(buffer);
        }
        public event Action Lock;
        public event Action UnLock;
        public event Action<bool?> End;
        public event Action<int, char> MarkCell;

        public void ReadResponse()
        {
            if (!Connected)
            {
                return;
            }
            while (true)
            {
                var message = _read();
                if (message == null)
                    return;

                switch (message.Text)
                {
                    case "Lock":
                        Lock?.Invoke();
                        break;
                    case "UnLock":
                        UnLock?.Invoke();
                        break;
                    case "MarkCell":
                        MarkCell?.Invoke(message.Cell, message.Content);
                        break;
                    case "End":
                        End?.Invoke(message.Result);
                        break;
                }
            }


        }
        public async Task ReadResponseAsync() => await Task.Run(ReadResponse, _token);
        
    }

}
