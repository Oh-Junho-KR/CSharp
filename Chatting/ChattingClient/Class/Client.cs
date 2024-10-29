using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ChattingClient
{
    public class Client
    {
        public delegate void ReceiveMessageEventHandler(string sMessage);
        public ReceiveMessageEventHandler ReceiveMessageEvent;

        private NetworkStream ns = null;
        private StreamReader sr = null;
        private StreamWriter sw = null;
        private TcpClient client = null;

        public Client() 
        {
            Connection();
        }

        private void Connection()
        {
            try
            {
                client = new TcpClient(GlobalDefine.sServerIP, GlobalDefine.nServerPort);
                ns = client.GetStream();
                sr = new StreamReader(ns, Encoding.Default);
                sw = new StreamWriter(ns, Encoding.Default);

                Thread receiveThread = new Thread(new ThreadStart(run));
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show("서버 시작 실패");
                throw e;
            }
        }

        private void run()
        {
            string message = "start";
            try
            {
                if (client.Connected && sr != null)
                    while ((message = sr.ReadLine()) != null)
                        ReceiveMessage(message);
            }
            catch (Exception) { MessageBox.Show("error"); }
        }

        public void ReceiveMessage(string sMessage)
        {
            ReceiveMessageEvent(sMessage);
        }

        public void SendMessage(string message)
        {
            try
            {
                if (sw != null)
                {
                    sw.WriteLine(message);
                    sw.Flush();
                }
            }
            catch (Exception) { MessageBox.Show("전송실패"); }
        }
    }
}
