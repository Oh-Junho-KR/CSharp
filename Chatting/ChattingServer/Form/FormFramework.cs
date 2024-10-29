using System;
using System.Windows.Forms;

namespace Chatting
{
    public partial class FormFramework : Form
    {
        ServerManager ServerManager;

        public FormFramework()
        {
            InitializeComponent();
        }

        private void InitServer()
        {
            ServerManager = new ServerManager();
            ServerManager.WriteLogEventCallback += new ServerManager.WriteLogEventHandler(WriteLogCallback);
            ServerManager.init();
        }

        private void WriteLogCallback(string sMsg)
        {
            if (txtServerLog.Text.ToString() != "")
            {
                GlobalFunction.AppendTextBox(txtServerLog, "\r\n" + sMsg);
            }
            else
            {
                GlobalFunction.AppendTextBox(txtServerLog, sMsg);
            }
        }

        private void FormFramework_Load(object sender, EventArgs e)
        {

        }

        private void FormFramework_Shown(object sender, EventArgs e)
        {
            InitServer();
        }
    }
}
