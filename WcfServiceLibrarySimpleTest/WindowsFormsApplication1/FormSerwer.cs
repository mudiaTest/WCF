using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WcfSerwer
{
    public partial class FormSerwer : Form
    {
        public FormSerwer()
        {
            InitializeComponent();

            ServiceHost service1Host = null;
            try
            {
                //Base Address for StudentService
                Uri httpBaseAddress = new Uri("http://localhost:9999/Service1");
                
                //Budowanie instancji serwisu.
                //Parametrami są:
                //  nazwę klasy serwisu
                //  adres pod którym serwis będzie działał
                service1Host = new ServiceHost(typeof(WcfServiceLibrarySimpleTest.Service1), httpBaseAddress);
                //WcfServiceLibrarySimpleTest.Service1 service1 = new WcfServiceLibrarySimpleTest.Service1();
                //service1.Info = Info;
                //service1Host = new ServiceHost(service1, httpBaseAddress);
 
                //Do instancji serwisu dodajemy Endpoint
                //Parametrami są:
                //  nazwa interfejsu
                //  obiekt typu połaczenia (binding); System.ServiceModel...: (WSHttpBinding, NetNamedPipeBinding etc)
                //  adres połaczenia
                service1Host.AddServiceEndpoint(typeof(WcfServiceLibrarySimpleTest.IService1), new WSHttpBinding(), "");            
 
                //Metadata Exchange
                ServiceMetadataBehavior serviceBehavior = new ServiceMetadataBehavior();
                serviceBehavior.HttpGetEnabled = true;
                service1Host.Description.Behaviors.Add(serviceBehavior);

                //Open
                service1Host.Open();
                Info(string.Format("Service is live now at : {0}", httpBaseAddress));
            }

            catch (Exception ex)
            {
                service1Host = null;
                Info(string.Format("There is an issue with Service1: '" + ex.Message + "'"));
            }
        }

        public void Info(string astText)
        {
            rtbInfo.AppendText(astText + "\n");
            rtbInfo.ScrollToCaret();
        }
    }
}
