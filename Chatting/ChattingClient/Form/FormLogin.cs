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
    public partial class FormLogin : Form
    {
        private FormFramework frmFramework = null;

        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            frmFramework = new FormFramework();
            frmFramework.LogoutEvent += new FormFramework.LogoutEventHandler(CloseFramework);

            if (GlobalDefine.bSimulationMode)
            {
                GlobalFunction.SetTextBox(txtID, GlobalDefine.sAdminID);
                GlobalFunction.SetTextBox(txtPW, GlobalDefine.sAdminPW);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (GlobalDefine.bSimulationMode)
            {
                if (txtID.Text == null)
                {
                    GlobalFunction.ShowMsgBox("ID 필수 입력");
                }

                if (txtID.Text == GlobalDefine.sAdminID)
                {
                    if (GlobalDefine.sAdminPW != null)
                    {
                        if (txtPW.Text != GlobalDefine.sAdminPW)
                        {
                            GlobalFunction.ShowMsgBox("PW 미일치");
                        }
                        else
                        {
                            OpenFramework();
                        }
                    }
                    else
                    {
                        OpenFramework();
                    }
                }
                else
                {
                    //GlobalFunction.ShowMsgBox("ID 미일치");
                    OpenFramework();
                }
            }
            else
            {
                if (txtID.Text == null)
                {
                    GlobalFunction.ShowMsgBox("ID 필수 입력");
                }

                OpenFramework();
            }
        }

        private void OpenFramework()
        {
            frmFramework.Show();
            frmFramework.Login(txtID.Text);
            this.Hide();
        }

        private void CloseFramework()
        {
            frmFramework.Hide();
            this.Show();
        }
    }
}
