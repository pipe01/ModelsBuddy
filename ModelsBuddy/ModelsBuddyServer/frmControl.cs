using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelsBuddyServer
{
    public partial class frmControl : Form
    {
        public frmControl()
        {
            InitializeComponent();
        }

        private Thread serverT;

        private void frmControl_Load(object sender, EventArgs e)
        {

        }

        private void ProcessMessage(string msg)
        {
            if (msg.StartsWith("@newconn"))
            {
                string[] p = msg.Split('|');
                lbConnections.Items.Add(p[1] + "@" + p[2]);
            }
        }

        public void serverThread()
        {
            UdpClient udpClient = new UdpClient(8080);
            while (true)
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                string returnData = Encoding.ASCII.GetString(receiveBytes);
                lbLog.Items.Add(RemoteIpEndPoint.Address.ToString() + ":" + returnData.ToString());
            }
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (serverT == null)
            {
                serverT = new Thread(serverThread);
                serverT.Start();
            }
        }
    }
}
