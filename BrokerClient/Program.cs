using System.Reflection;

namespace BrokerClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RmiHost.Initialize("http://localhost:8000/getService");

            var factory = RmiHost.GetServiceProxyFactory("test");

            var proxy = factory.CreateProxy<Test>();

            proxy.A();
            Console.WriteLine(proxy.B());
            Console.WriteLine(proxy.C(12,13,"numbers"));

            
        }
    }
}