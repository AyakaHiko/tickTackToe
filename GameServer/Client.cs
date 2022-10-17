using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using GameMessage;
using TicTacToe;

namespace GameServer
{
    public class Client : Player
    {
        private TcpClient _client;
        private NetworkStream _stream;
        public Client(char symbol, TcpClient client) : base(symbol)
        {
            _client = client;
            _stream = _client.GetStream();
        }
        private void _sendCommand(MessagePacket message)
        {
            var buffer = message.ToBytes();
            _stream.Write(buffer, 0, buffer.Length);
        }
        public void SendCommand(Commands command)
        {
            var packet = new MessagePacket(MessageType.Command)
            {
                Text = command.ToString()
            };
            _sendCommand(packet);
        }

        public void SendResultCommand(bool? result)
        {
            var packet = new MessagePacket(MessageType.Command)
            {
                Text = Commands.End.ToString(), Result = result
            };
            _sendCommand(packet);
        }

        public void SendCellCommand(int cell, char symbol)
        {
            var packet = new MessagePacket(MessageType.Command)
            {
                Text = Commands.MarkCell.ToString(), Cell = cell, Content = symbol
            };
            _sendCommand(packet);
        }

        public int Read()
        {
            var buffer = new byte[1024];
            do
            {
                _stream.Read(buffer, 0, buffer.Length);
            } while (_stream.DataAvailable);

            var message = MessagePacket.FromBytes(buffer);
            return message?.Cell??-1;
        }

        private int _readCell()
        {
            var buffer = new byte[32];
            do
            {
                _stream.Read(buffer, 0, buffer.Length);
            } while (_stream.DataAvailable);

            var res = MessagePacket.FromBytes(buffer);
            return res?.Cell ?? -1;
        }

        public void Close()
        {
            _client.Close();
        }
    }
}