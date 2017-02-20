using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rashim.RND.AdhocDiscovery.Services;
using System.ServiceModel.Description;

namespace Rashim.RND.AdhocDiscovery.ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {           
            //Najprostsza z możliwych inicjalizacja serwera
            var host = new System.ServiceModel.ServiceHost(typeof(MessageService));
            host.Open();
            var host2 = new System.ServiceModel.ServiceHost(typeof(MessageService2));
            host2.Open();

            //Nieistotne
            host.Description.Endpoints.ToList().ForEach((endpoint)=> Console.WriteLine(endpoint.ListenUri));
            host2.Description.Endpoints.ToList().ForEach((endpoint) => Console.WriteLine(endpoint.ListenUri));
            Console.WriteLine("Please enter to exit");
            Console.ReadLine();
            host.Close();
        }
    }
}
