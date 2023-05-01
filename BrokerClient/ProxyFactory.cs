using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BrokerClient
{
    public  class ProxyFactory
    {
        private static HttpClient _httpClient = new HttpClient();

        private  Socket _socket;

        private  byte[] buffer = new byte[1024];

        public ProxyFactory(string address,int port)
        {
            IPEndPoint endPoint = new(IPAddress.Parse(address), port);

            _socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _socket.Connect(endPoint);
        }

        public byte[] SendAndReceive(byte[] data,bool _void = false)
        {
            _socket.Send(data);

            if (!_void)
                _socket.Receive(buffer);

            return buffer;
        }

        ~ProxyFactory()
        {
            _socket.Close();
        }


        public T CreateProxy<T>()
        {
            var proxy = DispatchProxy.Create<T, NetworkProxy>();

            ((proxy as NetworkProxy)!).Factory = this;

            return proxy;
        }
    }
}
