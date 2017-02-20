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
using Service2Library;

namespace Service
{
    public partial class ServiceForm : Form
    {
        public ServiceForm()
        {
            InitializeComponent();
            ServiceInitializer si = new ServiceInitializer(Info, Info11, Info12);
            //si.InitService1();
            //si.InitService21();
            //si.InitService22();
            //si.InitService3();
            si.InitService10000();
        }

        private void Info(string astText)
        {
            rtbInfo.AppendText("Info: " + astText + "\n");
            rtbInfo.ScrollToCaret();
        }
        public void Info11(string astText)
        {
            rtbInfo.AppendText("Info11: " + astText + "\n");
            rtbInfo.ScrollToCaret();
        }
        public void Info12(string astText)
        {
            rtbInfo.AppendText("Info12: " + astText + "\n");
            rtbInfo.ScrollToCaret();
        }        
    }
}
