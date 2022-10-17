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
using TicTacToe;

namespace GameServer
{
    public enum Commands
    {
        Start, Lock,
        UnLock,
        MarkCell,
        End
    }
    public class Server
    {
        private TcpListener _listener;
        public Server(IPAddress address, int port)
        {
            _listener = new TcpListener(address, port);
            _clients = new List<Client>();

            _cts = new CancellationTokenSource();
            _token = _cts.Token;
        }

        private CancellationTokenSource _cts;
        private CancellationToken _token;
        public Task StartAsync() => Task.Factory.StartNew(Start, _token);
        public event Action<string> Inform;
        private List<Client> _clients;
        public void Start()
        {
            try
            {
                _listener.Start();
                while (true)
                {
                    _accept();
                }
            }
            catch (Exception e)
            {
                Inform?.Invoke(e.Message);
            }
            finally
            {
                Stop();
            }

        }

        private void _accept()
        {
            if (_clients.Count == 2)
                return;
            var client = _listener.AcceptTcpClient();
            Client player = _clients.Count == 0 ? new Client('x', client) : new Client('o', client);
            player.WinEvent += Player_WinEvent;
            player.LooseEvent += Player_LooseEvent;
            player.IsDisconnected += Player_IsDisconnected;
            _clients.Add(player);
            if (_clients.Count == 2)
                _gameInit();
        }

        private void Player_IsDisconnected(Client client)
        {
            _clients.Remove(client);
            if(!_isRun)
                return;
            
            _clients[0].Win();
        }

        private Client _getClient(Player player)
        {
            return _clients.FirstOrDefault(p => p.Equals(player));
        }
        private void Player_LooseEvent(Player player)
        {
            var client = _getClient(player);
            client?.SendResultCommand(false);
        }

        private void Player_WinEvent(Player player)
        {
            var client = _getClient(player);
            client?.SendResultCommand(true);
        }

        private Game _game;
        private bool _isRun = false;

        private void _gameInit()
        {
            _cts = new CancellationTokenSource();
            _token = _cts.Token;

            Client p1 = _clients[0], p2 = _clients[1];
            _game = new Game(p1, p2);
            _game.EndGame += Game_EndGame;
            _game.Draw += _game_Draw;
            _game.StepEvent += Game_StepEvent;
            try
            {
                p1.SendCommand(Commands.Start);
                p2.SendCommand(Commands.Start);
            }
            catch (Exception e)
            {   
                return;
            }


            Task.Run(() => _gameStart(p1, p2), _token);
        }

        private void _game_Draw()
        {
            foreach (var client in _clients)
            {
                client.SendResultCommand(null);
            }
        }

        private void _gameStart(Client p1, Client p2)
        {
            _isRun = true;
            while (_isRun)
            {
                _step(p1, p2);
                _step(p2, p1);
            }
        }
        private void _step(Client p1, Client p2)
        {
            p1.SendCommand(Commands.UnLock);
            var cell = p1.Read();
            Task.Run(() => p2.SendCellCommand(cell, p1.Symbol), _token);
            Task.Run(() => p1.SendCellCommand(cell, p1.Symbol), _token);
            _game.Step(p1, cell);
            p1.SendCommand(Commands.Lock);
        }
        private void Game_StepEvent(Player player, int sell)
        {
        }

        private void Game_EndGame()
        {
            _isRun = false;
            _cts.Cancel();
            for (var i = 0; i < _clients.Count; i++)
            {
                var client = _clients[i];
                _clients.Remove(client);
                i--;
                client.Close();
            }

            _clients.Clear();

        }

        public event Action Stopped;


        public void Stop()
        {
            _cts.Cancel();
            _listener.Stop();
            Stopped?.Invoke();
            Inform?.Invoke("Server stopped");
        }
    }
}
