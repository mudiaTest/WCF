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
using WcfClient1Proxy;
using WcfClient1.ServiceReference1;


namespace WcfClient1
{
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
            Test1();
        }

        private void Info(string stTekst)
        {
            rtbInfo.AppendText(stTekst + "\n");
        }

        private void Test1()
        {
            /*
                WcfClientProxy client = new WcfClientProxy();
                Info("Client calling the service...");
                Info("Hello Ram");
                Info(client.GetData(8).ToString());
            */

            /*ServiceReference1.Service1Client client2 = new
                ServiceReference1.Service1Client();
            Info(client2.GetData(8).ToString());*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
/*WcfClient1Proxy.WcfClientProxy client2 = new
                WcfClient1Proxy.WcfClientProxy();
            Info(client2.GetData(8).ToString());*/
            ChannelFactory<IService1> channelFactory = new ChannelFactory<IService1>("ServiceEndpoint1");
            IService1 proxy = channelFactory.CreateChannel();
        }
    }
}
