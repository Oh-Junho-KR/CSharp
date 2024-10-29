using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChattingClient
{
    public partial class FormFramework : Form
    {
        public delegate void LogoutEventHandler();
        public LogoutEventHandler LogoutEvent;

        public static UserInfo m_UserInfo;
        public static Client m_Client;

        private FormUser frmUser = null;
        private FormRoom frmRoom = null;
        private FormChatting frmChatting = null;

        public FormFramework()
        {
            InitializeComponent();
        }

        #region Frm Event
        private void FrmFramework_Load(object sender, EventArgs e)
        {
            InitUser();
            InitForm();
        }

        private void FrmFramework_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (frmUser != null)
            {
                frmUser.Dispose();
            }
            
            if (frmRoom != null)
            {
                frmRoom.Dispose();
            }
            
            if (frmChatting != null)
            {
                frmChatting.Dispose();
            }
        }

        private void FrmFramework_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        #endregion

        #region Btn Event
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            if (!GlobalFunction.ConfirmMsgBox("로그아웃", "로그아웃하시겠습니까?"))
            {
                return;
            }

            Logout();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (!GlobalFunction.ConfirmMsgBox("종료", "종료하시겠습니까?"))
            {
                return;
            }

            this.Close();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            ChangeMenu("User");
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            ChangeMenu("Room");
        }
        #endregion

        #region User Function
        private void InitUser()
        {
            m_UserInfo = new UserInfo();
        }

        private void InitForm()
        {
            frmUser = new FormUser();
            pnlMain.Controls.Add(frmUser);

            frmRoom = new FormRoom();
            frmRoom.EnterChatEvent += new FormRoom.EnterChatEventHandler(EnterChat);
            pnlMain.Controls.Add(frmRoom);

            frmChatting = new FormChatting();
            frmChatting.ExitChatEvent += new FormChatting.ExitChatEventHandler(ExitChat);
            pnlMain.Controls.Add(frmChatting);

            ChangeMenu("User");
        }

        private void ConnectionSever(string sName)
        {
            m_Client = new Client();
            m_Client.SendMessage((int)GlobalDefine.eProtocol.Login + sName);
        }

        private void DisConnectionServer()
        {
            m_Client.SendMessage(((int)GlobalDefine.eProtocol.Logout).ToString());
        }

        private void SetUserInfo(string sName)
        {
            m_UserInfo.IP = GlobalDefine.sServerIP;
            m_UserInfo.Port = GlobalDefine.nServerPort;
            m_UserInfo.Name = sName;

            GlobalFunction.SetLabel(lblName, sName);
        }

        public void Login(string sName)
        {
            ConnectionSever(sName);
            SetUserInfo(sName);
            ChangeMenu("User");
        }

        private void Logout()
        {
            DisConnectionServer();
            LogoutEvent();
        }

        private void EnterChat(string sID, string sTitle)
        {
            m_UserInfo.ID = sID;
            frmChatting.Reset(sTitle);
            ChangeMenu("Chatting");
        }

        private void ExitChat()
        {
            ChangeMenu("Room");
        }

        private void ChangeMenu(string sMenu)
        {
            frmUser.Hide();
            frmRoom.Hide();
            frmChatting.Hide();

            switch (sMenu)
            {
                case "User":
                    if (frmUser != null)
                    {
                        frmUser.Show();
                    }
                    break;

                case "Room":
                    if (frmRoom != null)
                    {
                        frmRoom.Show();
                    }
                    break;

                case "Chatting":
                    if (frmChatting != null)
                    {
                        frmChatting.Show();
                    }
                    break;

                default:

                    break;
            }
        }
        #endregion
    }
}
