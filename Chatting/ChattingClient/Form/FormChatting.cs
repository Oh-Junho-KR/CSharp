using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChattingClient
{
    public partial class FormChatting : UserControl
    {
        public delegate void ExitChatEventHandler();
        public ExitChatEventHandler ExitChatEvent;

        public FormChatting()
        {
            InitializeComponent();
        }

        private void FormChatting_Load(object sender, EventArgs e)
        {
            
        }

        public void Reset(string sTitle)
        {
            GlobalFunction.SetLabel(lblTitle, sTitle);

            FormFramework.m_Client.ReceiveMessageEvent += new Client.ReceiveMessageEventHandler(onRecieveCallback);
            FormFramework.m_Client.SendMessage((int)GlobalDefine.eProtocol.EnterChat + FormFramework.m_UserInfo.ID);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            FormFramework.m_Client.SendMessage(((int)GlobalDefine.eProtocol.ExitChat).ToString());
            FormFramework.m_Client.ReceiveMessageEvent -= new Client.ReceiveMessageEventHandler(onRecieveCallback);
            ExitChatEvent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string sMsg = this.txtMessage.Text;
            FormFramework.m_Client.SendMessage((int)GlobalDefine.eProtocol.SendMsg + sMsg.Trim());

            this.txtMessage.Clear();
            this.txtMessage.SelectionStart = 0;
        }

        private void onRecieveCallback(string sMessage)
        {
            if (this.txtMessage.Text.ToString() != null)
            {
                GlobalFunction.AppendRichTextBox(txtChatting, sMessage + "\r\n");
                this.txtChatting.Focus();
                this.txtChatting.ScrollToCaret();
            }
        }
    }
}
