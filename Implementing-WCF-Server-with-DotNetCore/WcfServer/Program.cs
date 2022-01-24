using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WcfServer
{
    internal class Program
    {
        static void Main()
        {
            var http = new Uri("http://localhost:8088");
            var nettcp = new Uri("net.tcp://localhost:8089");

            var baseUriList = new[] { http, nettcp };

            Type contract = typeof(Contract.IEchoService);
            var host = new ServiceHost(typeof(EchoService), baseUriList);
            host.AddServiceEndpoint(contract,
                new NetTcpBinding(),
                nettcp.AbsolutePath);
            host.AddServiceEndpoint(contract, 
                new BasicHttpBinding(BasicHttpSecurityMode.None),
                http.AbsolutePath + "/basic");
            host.AddServiceEndpoint(contract, 
                new WSHttpBinding(SecurityMode.None),
                http.AbsolutePath + "/wshttp");

            var smb = new ServiceMetadataBehavior();
            host.Description.Behaviors.Add(smb);
            var mexTcpBinding = MetadataExchangeBindings.CreateMexTcpBinding();
            var mexHttpbinding = MetadataExchangeBindings.CreateMexHttpBinding();
            host.AddServiceEndpoint(typeof(IMetadataExchange), mexTcpBinding, "mex");
            host.AddServiceEndpoint(typeof(IMetadataExchange), mexHttpbinding, "mex");

            host.Open();

            LogHostUrls(host);

            Console.WriteLine("Hit enter to close");
            Console.ReadLine();
        }

        private static void LogHostUrls(ServiceHost host)
        {
            Console.WriteLine("Listening on :");
            foreach (ServiceEndpoint endpoint in host.Description.Endpoints)
            {
                Console.WriteLine("\t" + endpoint.ListenUri.ToString());
            }
        }

    }
}
