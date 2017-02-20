using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.ServiceModel.Discovery;
using Rashim.RND.AdhocDiscovery.Services;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;

namespace Rashim.RND.AdhocDiscovery.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //Tworzymy klienta discovery, którym będziemy łaczyć się z endpointem discovery
            //nako argument pobieramy UdpDiscoveryEndpoint, bo w server.app.cfg taki jest kind enpointa w service
            var discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());

            /*XmlQualifiedName pos1 = new XmlQualifiedName("Rashim.RND.AdhocDiscovery.Services.IMessageService2");
            XmlQualifiedName pos2 = new XmlQualifiedName("IMessageService2");
            XmlQualifiedName pos3 = new XmlQualifiedName("Rashim.RND.AdhocDiscovery.Services.MessageService2");
            XmlQualifiedName pos4 = new XmlQualifiedName("MessageService2");
            var list = new List<XmlQualifiedName>() {pos1, pos2, pos3, pos4};*/

            //Tworzymy filtr. Argument może być 
            // * pusty (brak filtrów) 
            // * typeof(IMessageService) 
            // * IEnumerable<XmlQualifiedName>, ale nie wiem jak tę listę wypełnić, bo powyższe nie działają
            var findCriteria = FindCriteria.CreateMetadataExchangeEndpointCriteria();// typeof(IMessageService));

            //Pobieramy tylko jedną (w tym przypadku i tak serwer ma jeden endpoint discovery, ale w innym  
            //przypadku trzeba oprogramować możliwość wystąpienia innych discovery endpointów)
            findCriteria.MaxResults = 1;
            Debug.Assert(findCriteria.Scopes != null, "findCriteria.Scopes != null");

            //Ustalamy adres pod którym będziemy szukać discovery endpoint
            findCriteria.Scopes.Add(new Uri("ldap:///ou=people,o=rashim"));
            //findCriteria.Scopes.Add(new Uri("http://localhost:8733/Mess"));

            //Zapuszczamy szukanie informacji. To może potrwać ładnych kilka sekund.
            var findResponse = discoveryClient.Find(findCriteria);

            //Jeśli mamy odpowiedź 
            if (findResponse != null)
            {
                if(findResponse.Endpoints!=null)
                {
                    if (findResponse.Endpoints.Count > 0)
                    {

                        //Pobieranie endointów z service wymienionych w contracts. Oczywiście findResponse.Endpoints[0] odda tylko endpointy dla jednego service
                        //Pobieranie endpointów tylko jednego rodzaju, oczywiście to samo mozna uzysjkać za pomocą jedno elementowej listy.
                        //var endpoints = MetadataResolver.Resolve( typeof (IMessageService), findResponse.Endpoints[0].Address);
                        var contracts = new List<ContractDescription>() {
                            ContractDescription.GetContract(typeof(IMessageService)), 
                            ContractDescription.GetContract(typeof(IMessageService2)) 
                        };
                        //W tym wypadku z findResponce pobieramy tylko pierwszy discovery endpoint
                        var endpoints = MetadataResolver.Resolve(contracts, findResponse.Endpoints[0].Address);

                        //Standardowo tworzymy połaczenie. Binding i address pobieamy ze znalezionego endpointa
                        //Tutaj też pobieramy tylko pierwszy endpoint, choć powinniśmy obsłużyć wszystkie z listy.
                        var factory = new ChannelFactory<IMessageService2>(endpoints[0].Binding, endpoints[0].Address);
                        var channel = factory.CreateChannel();

                        //Nieistotne
                        Console.WriteLine("Say something and press enter");
                        var input = Console.ReadLine();
                        while (input != null && input.ToString(CultureInfo.InvariantCulture).ToLower()!="exit")
                        {
                            Console.WriteLine(channel.GetMessage2(input));
                            input = Console.ReadLine();
                        }
                          ((IChannel)channel).Close();                    
                    }
                }
            }
            else
            {
                Console.WriteLine("Could not find the Services");
            }                    
        }
    }
}
