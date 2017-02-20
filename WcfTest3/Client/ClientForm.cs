using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Service2Library;
using Service;
using BehaviorTest;

namespace Client
{
    public partial class ClientForm : Form
    {
        IMyService serviceProxy;
        IMyService2 service21Proxy;
        IMyService2 service22Proxy;
        IMyService3 service3Proxy;
        /* ******************************************************************************* */
        IService10000 sp10000;
        int i = 0;
        public ClientForm()
        {
            InitializeComponent();
            ChannelFactory<IMyService> channelFactory = new ChannelFactory<IMyService>("ServiceEndpoint1");
            serviceProxy = channelFactory.CreateChannel();

            ChannelFactory<IMyService2> channelFactory21 = new ChannelFactory<IMyService2>("Service21Endpoint1");
            service21Proxy = channelFactory21.CreateChannel();

            ChannelFactory<IMyService2> channelFactory22 = new ChannelFactory<IMyService2>("Service22Endpoint1");
            service22Proxy = channelFactory22.CreateChannel();

            /*Nazwa endpoint -BleBleEndpoint3- musi być zgodna z app.cfg klienta, ale nie musi z serwerem*/
            ChannelFactory<IMyService3> channelFactory3 = new ChannelFactory<IMyService3>("BleBleEndpoint3");
            service3Proxy = channelFactory3.CreateChannel();

            /* *************************************************************************************** */

            ChannelFactory<IService10000> chf10000 = new ChannelFactory<IService10000>("EPService10000");
            sp10000 = chf10000.CreateChannel();
        }

        private void Info(string stText)
        {
            rtbInfo.AppendText(stText + "\n");
            rtbInfo.ScrollToCaret();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Info(serviceProxy.Add(0, i));
                i++;
            }
            catch (Exception ex)
            {
                Info(string.Format("There is an issue with MyService: '" + ex.Message + "'"));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Info(service21Proxy.Add2(0, i));
                i++;
            }
            catch (Exception ex)
            {
                Info(string.Format("There is an issue with MyService2: '" + ex.Message + "'"));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Info(service3Proxy.Get31(i));
                i++;
            }
            catch (Exception ex)
            {
                Info(string.Format("There is an issue with MyService3: '" + ex.Message + "'"));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Info(service3Proxy.Get32(i));
                i++;
            }
            catch (Exception ex)
            {
                Info(string.Format("There is an issue with MyService3: '" + ex.Message + "'"));
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Info(service22Proxy.Add2(0, i));
                i++;
            }
            catch (Exception ex)
            {
                Info(string.Format("There is an issue with MyService2: '" + ex.Message + "'"));
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Info(sp10000.GetData(10000));
                i++;
            }
            catch (Exception ex)
            {
                Info(string.Format("There is an issue with Service10000: '" + ex.Message + "'"));
            }
        }
    }
}
