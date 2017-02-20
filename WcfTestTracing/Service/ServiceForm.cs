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
            ServiceInitializer si = new ServiceInitializer(Info);
            si.InitService10000();
        }

        private void Info(string astText)
        {
            rtbInfo.AppendText("Info: " + astText + "\n");
            rtbInfo.ScrollToCaret();
        }     
    }
}
