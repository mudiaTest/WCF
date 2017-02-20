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

namespace Service
{
    public partial class ServiceForm : Form
    {
        public ServiceForm()
        {
            InitializeComponent();
            InitService();
        }

        public void Info(string astText)
        {
            rtbInfo.AppendText(astText + "\n");
            rtbInfo.ScrollToCaret();
        }

        private void InitService()
        {
            ServiceHost host;
            try
            {
                //ServiceHost host = new ServiceHost(typeof(MyService));
                MyService service = new MyService();
                service.Info = Info;
                host = new ServiceHost(service);
                host.Open();
                Info("Service online.");
            }
            catch (Exception ex)
            {
                host = null;
                Info(string.Format("There is an issue with MyService: '" + ex.Message + "'"));
            }
        }
    }
}
