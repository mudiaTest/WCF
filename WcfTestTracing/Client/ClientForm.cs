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
//using Client.Service;
using BingingTest;

namespace Client
{
    public partial class ClientForm : Form
    {
        IService10000 sp10000;
        IService10000 sp10000b;
        public ClientForm()
        {
            InitializeComponent();
            ChannelFactory<IService10000> chf10000 = new ChannelFactory<IService10000>("EPService10001");
            sp10000 = chf10000.CreateChannel();
            ChannelFactory<IService10000> chf10000b = new ChannelFactory<IService10000>("EPService10002");
            sp10000b = chf10000b.CreateChannel();
        }

        private void Info(string stText)
        {
            rtbInfo.AppendText(stText + "\n");
            rtbInfo.ScrollToCaret();
        }

        // Asynchronous callbacks for displaying results.
        /*static void AddCallback(object sender, AddCompletedEventArgs e)
        {
            Info(e.Result);
        }*/

        private async void Test(int alp)
        {
            var task = Task.Factory.StartNew(() => sp10000.GetData(alp));
            string str = "";
            try
            {
                str = await task;
            }
            catch (Exception e)
            {
                Info("Wystąpił wyjątek: " + e.Message);
            }
            Info(str);
        }

        private async void Test2(int alp)
        {
            var task = Task.Factory.StartNew(() => sp10000b.GetData2(alp));
            string str = "";
            try
            {
                str = await task;
            }
            catch (Exception e)
            {
                Info("Wystąpił wyjątek: " + e.Message);
            }            
            Info(str);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // Info(sp10000.GetData(1));
            Test(1111);    
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Test2(2);
        }
    }
}
