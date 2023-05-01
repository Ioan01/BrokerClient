using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrokerClient
{
    static class RmiHost
    {
        private static HttpClient _httpClient = new HttpClient();

        private static string _dispatcherAddress;
        public static void Initialize(string dispatcherAddress)
        {
            _dispatcherAddress = dispatcherAddress;
        }

        public static ProxyFactory GetServiceProxyFactory(string serviceName)
        {
            var response = _httpClient.Send(new HttpRequestMessage(HttpMethod.Get, $"{_dispatcherAddress}?serviceName={serviceName}"));
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Failed to contact dispatcher : {response.Content.ReadAsStringAsync().Result}");

            Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            var host = JsonSerializer.Deserialize<Service>(response.Content.ReadAsStringAsync().Result);

            

            return new ProxyFactory(host.Address,host.Port);
        }

    }
}
