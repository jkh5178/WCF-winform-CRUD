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
using wcf_service;

namespace wcf_service
{
    public partial class Form1 : Form
    {
        ServiceHost host;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            host = new ServiceHost(typeof(Service1));
            host.AddServiceEndpoint(typeof(IService1), new BasicHttpBinding(),"");
            

        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            host.Open();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            host.Close();
            host = new ServiceHost(typeof(Service1));
            host.AddServiceEndpoint(typeof(IService1), new BasicHttpBinding(), "");
        }
    }
}
