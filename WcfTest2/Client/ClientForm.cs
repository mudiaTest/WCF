using Service;
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

namespace Client
{
    public partial class ClientForm : Form
    {
        IMyService proxy;
        public ClientForm()
        {
            InitializeComponent();
            ChannelFactory<IMyService> channelFactory = new ChannelFactory<IMyService>("ServiceEndpoint1");
            proxy = channelFactory.CreateChannel();
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
                Info(proxy.Add(1, 2));
            }
            catch (Exception ex)
            {
                Info(string.Format("There is an issue with MyService: '" + ex.Message + "'"));
            }
        }
    }
}
