using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Text;
using System.Xml;
using Common;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;

namespace Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RunTime();
           
        }

        private static void RunTime()
        {
            //tworzymy nowe AnnouncementService na podstawie którego stworzymy wewnętrzny serwerek nasłuchujący announcement 
            var announcementService = new AnnouncementService();


            //Reakcja na event, gdy serwer ogłosi, ze jest ONLINE
            //Dla każdego endpoint dostaniemy jeden event - analigicznie jak dla poniższego OfflineAnnouncementReceived
            announcementService.OnlineAnnouncementReceived += (sender, e) =>
            {
                //Pobieramy contract dla mex danej service - to dostaniemy niezależnie od tego jaki endpoint echodzi jako argument eventu. 
                //Dostaniemy mex endpoint o ile tylko był zdefiniowany w service
                var mexContractDescrition = ContractDescription.GetContract(typeof(IMetadataExchange));
                //Pobieramy name pobranego mex
                var mexQualifiedName = new XmlQualifiedName(mexContractDescrition.Name, mexContractDescrition.Namespace);

                //Pobieramy listę nazw endpointów pobranych przez metadata discovery
                e.EndpointDiscoveryMetadata.ContractTypeNames.ToList().ForEach((name) =>
                {
                    //Kończymy jeśli to endpoint nie mex, z którego bedziemy mogli pobrać dane o service
                    if (mexQualifiedName != name) return;

                    //MetadataResolver zajmuje się odczytem metadata service
                    //lista endpointów dla service implementującego IMessageServices pobrana z adresu pobrango przez endpoint discovery 
                    var endpoints = MetadataResolver.Resolve(typeof(IMessageServices), e.EndpointDiscoveryMetadata.Address);
                    //poniżej pobieramy endpointy z innego service
                    //var endpoints2 = MetadataResolver.Resolve(typeof(IService1), e.EndpointDiscoveryMetadata.Address);

                    if (endpoints.Count <= 0) return;

                    //Tworzymy połączenie do service oparte o binding i address endpointu dla service
                    //W poniższym przykładnie odwołujemy się tylko do pierwszego endpointu, ale poniższy kod powienien działać w pętli
                    var factory = new ChannelFactory<IMessageServices>(endpoints[0].Binding, endpoints[0].Address);
                    var channel = factory.CreateChannel();

                    Console.WriteLine("\nService is online now..");
                    var replyMessage = channel.GetMessage("Server is responding");
                    Console.WriteLine(replyMessage);
                    ((ICommunicationObject)channel).Close();
                });
            };

            //Reakcja na event, gdy serwer ogłosi, ze jest OFFLINE
            announcementService.OfflineAnnouncementReceived += (sender, e) =>
            {
                //prawdzamy, czy metedata połaczona z discovery endpoint posiada aktywną service(serwer?) obsługującą IMessageServices.
                //Jest bowiem tak, ze zamykając serwer dla kazdego endpoint serwera wysyłane jest annoucement, a nas interesuje tylko zamknięcie service od IMessageServices.
                //Zamykając ten serwer z (2 service, gdzie każdy ma 2 endpointy: mex oraz standardowy) dostajemy łacznie 4 eventy
                if (e.EndpointDiscoveryMetadata.ContractTypeNames.FirstOrDefault(contract => contract.Name == typeof(IMessageServices).Name) == null)
                    return;
                Console.WriteLine("\nService says 'Good Bye'");
            };



            //TAK, to jest kod tworzenia serwera i jest w kliencie!

            using (var announecementServiceHost = new ServiceHost(announcementService))
            {
                announecementServiceHost.AddServiceEndpoint(new UdpAnnouncementEndpoint());
                announecementServiceHost.Open();
                Console.WriteLine("Please enter to exit\n\n");
                Console.ReadLine();
            }
        }
    }
}
