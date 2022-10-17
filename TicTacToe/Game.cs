using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Player
    {
        public Player(char symbol)
        {
            Symbol = symbol;
        }
        public char Symbol { get; }

        public void Win()
        {
            WinEvent?.Invoke(this);
        }
        public void Loose()
        {
            LooseEvent?.Invoke(this);
        }

        public void Draw()
        {
            DrawEvent?.Invoke(this);
        }
        public event Action<Player> WinEvent;
        public event Action<Player> LooseEvent;
        public event Action<Player> DrawEvent;
    }

    public class Game
    {
        private Player[,] _field;
        private int _size = 3;
        private Player _player1, _player2;
        public Game(Player p1, Player p2)
        {
            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));
            if (p1.Symbol == p2.Symbol)
                throw new ArgumentException("Same Symbols");
            _player1 = p1;
            _player2 = p2;
            _player1.WinEvent += Player_Win;
            _player2.WinEvent += Player_Win;
            _field = new Player[_size, _size];
        }
        public event Action EndGame;
        private void Player_Win(Player player)
        {
            if (player == _player1)
                _player2.Loose();
            else _player1.Loose();
            EndGame?.Invoke();
            _reset();
        }

        private void _reset()
        {
            _field = new Player[_size, _size];
        }

        public event Action<Player, int> StepEvent;
       
        public void Step(Player player, int cell)
        {
            var x = (cell - 1) / _size;
            var y = (cell-1) % _size;
            _field[x,y] = player;
            _check();
            StepEvent?.Invoke(player, cell);
        }

        #region Check
        private void _check()
        {
            Task<Player>[] checks = {
                Task<Player>.Factory.StartNew(_diagonal1),
                Task<Player>.Factory.StartNew(_diagonal2),
                Task<Player>.Factory.StartNew(_horizontal),
                Task<Player>.Factory.StartNew(_vertical),
            };

            Task.WaitAll(checks);
            var draw = Task<bool>.Factory.StartNew(_draw);
            foreach (var check in checks)
            {
                if (check.Result == null) continue;

                var player = check.Result;
                player.Win();
                break;
            }

            if (draw.Result)
            {
                EndGame?.Invoke();
            }
        }

        private bool _draw()
        {
            return _field.Cast<Player>().All(player => player != null);
        }

        private Player _vertical()
        {
            for (int i = 0; i < _size; i++)
            {
                if (_field[0, i] == null)
                    continue;
                var element = _field[i, 0];
                bool complete = true;
                for (int j = 1; j < _size; j++)
                {
                    if (_field[j, i] == null || element != _field[j, i])
                    {
                        complete = false;
                        break;
                    }
                }
                if (complete)
                    return element;
            }
            return null;
        }
        private Player _horizontal()
        {
            for (int i = 0; i < _size; i++)
            {
                if (_field[i, 0] == null)
                    continue;
                var element = _field[i, 0];
                bool complete = true;

                for (int j = 1; j < _size; j++)
                {
                    if (_field[i, j] == null || element != _field[i, j])
                    {
                        complete = false;
                        break;
                    }
                }

                if (complete)
                    return element;
            }
            return null;
        }

        private Player _diagonal1()
        {
            if (_field[_size - 1, _size-1] == null)
                return null;

            bool complete = true;
            var element = _field[0, 0];
            if (element == null)
                return null;
            for (int i = 0; i < _size; i++)
            {
                if (element != _field[i, i])
                {
                    complete = false;
                    break;
                }
            }

            return complete ? element : null;
        }

        private Player _diagonal2()
        {
            if (_field[_size - 1, 0] == null )
                return null;
            bool complete = true;
            var element = _field[0, _size - 1];
            if (element == null)
                return null;
            for (int i = 0; i < _size; i++)
            {
                if (element != _field[i, _size - 1 - i])
                {
                    complete = false;
                    break;
                }
            }
            return complete ? element : null;
        }

        #endregion

    }
}
