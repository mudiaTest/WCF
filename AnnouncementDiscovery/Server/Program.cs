using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Common;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof (MessageService));
            host.Open();
            host.Description.Endpoints.ToList().ForEach((endpoint)=> Console.WriteLine(endpoint.ListenUri.ToString()));

            var host2 = new ServiceHost(typeof(Service1));
            host2.Open();
            host2.Description.Endpoints.ToList().ForEach((endpoint) => Console.WriteLine(endpoint.ListenUri.ToString()));

            Console.WriteLine("Please Enter to Exit");
            Console.ReadLine();
            host.Close();
            host2.Close();
        }
    }
}
