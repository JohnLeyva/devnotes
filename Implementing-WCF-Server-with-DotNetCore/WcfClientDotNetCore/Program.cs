using System.ServiceModel;

var proxy = new WcfSample.EchoServiceClient(
       WcfSample.EchoServiceClient.EndpointConfiguration.WSHttpBinding_IEchoService, 
       new EndpointAddress("http://localhost:8188/wshttp"));

await proxy.OpenAsync();

var echoResult = await proxy.EchoAsync("Hi");

Console.WriteLine("EchoResult = " + echoResult);

await proxy.CloseAsync();

Console.ReadKey();