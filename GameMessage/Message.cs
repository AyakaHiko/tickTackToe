using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GameMessage
{
    public enum MessageType
    {
        Data,
        Command
    }
    [Serializable]
    public class MessagePacket
    {
        public MessageType Type { get; private set; }
        public string Text { get; set; }
        public int Cell { get; set; }
        public char Content { get; set; }
        public bool? Result { get; set; }

        public MessagePacket(MessageType type) 
        {
            Type = type;
        }


        public static MessagePacket FromBytes(byte[] messageArray)
        {
            using (MemoryStream stream = new MemoryStream(messageArray))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    if (formatter.Deserialize(stream) is MessagePacket message)
                        return message;
                    return null;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public byte[] ToBytes()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                return stream.ToArray();
            }
        }
    }
}
