using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BrokerClient
{
    public static class NetworkHandler
    {
        private static Socket _socket;

        private static byte[] buffer = new byte[1024];


        public static void Initialize(string dispatcherAddress,string serverName)
        {
            IPEndPoint endPoint = new(IPAddress.Parse("127.0.0.1"), 8001);

            _socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _socket.Connect(endPoint);
        }

        public static byte[] SendAndReceive(byte[] data)
        {
            _socket.Send(data);

            _socket.Receive(buffer);

            return buffer;
        }


    }
}
