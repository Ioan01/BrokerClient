# Remote Method Invocation Project - Client Side
## Overview
- A simple implementation providing the client access to RMI through a dispatcher. In this implementation, the dispatcher is only used to help clients locate services, and not reroute requests through them. 
- Utilizes reflection to abstract all the packet handling and is able to create dynamic proxies which will marshall the function parameters and unmarshall the results (if applicable).

## Usage
- Connect the RMI Host to a dispatcher and specify the getService Endpoint (a simple endpoint that returns the adress of the requested service as a string, so a simple key-value dictionary )
``
RmiHost.Initialize("http://localhost:8000/getService");
``
- Get a Proxy Factory for the wanted service from the server
``
var factory = RmiHost.GetServiceProxyFactory("test");
// here we get a proxy for the test service
``
- Create a proxy from an interface whose code will be implemented and ran server-side
```csharp
interface Test{
    public void A(); 
    public string B();
    public string C(int a, float b, string c);
}
```
``var proxy = factory.CreateProxy<Test>();``
- Then, invoke the functions normally
```csharp
proxy.A();
Console.WriteLine(proxy.B());
Console.WriteLine(proxy.C(12,13,"numbers"));
```

