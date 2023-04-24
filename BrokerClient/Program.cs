using System.Reflection;

namespace BrokerClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NetworkHandler.Initialize("","");


            var proxy = DispatchProxy.Create<Test,NetworkProxy>();

            //var value = proxy.testRoute3("sadads",23,42.23f);



            //Console.WriteLine(value);


            Console.WriteLine(proxy.test(2,3,15.3f));
        }
    }
}