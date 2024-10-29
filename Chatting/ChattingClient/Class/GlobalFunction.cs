using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChattingClient
{
    public class GlobalFunction
    {
        public static void ShowMsgBox(string sMsg)
        {
            MessageBox.Show(sMsg);
        }

        public static bool ConfirmMsgBox(string sTitle, string sMsg)
        {
            if (MessageBox.Show(sMsg, sTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void SetTextBox(TextBox txtObject, string sText)
        {
            if (txtObject.InvokeRequired)
            {
                txtObject.Invoke(new Action(delegate()
                {
                    txtObject.Text = sText;
                }));
            }
            else
            {
                txtObject.Text = sText;
            }
        }

        public static void AppendTextBox(TextBox txtObject, string sText)
        {
            if (txtObject.InvokeRequired)
            {
                txtObject.Invoke(new Action(delegate ()
                {
                    txtObject.AppendText(sText);
                }));
            }
            else
            {
                txtObject.AppendText(sText);
            }
        }

        public static void SetRichTextBox(RichTextBox txtObject, string sText)
        {
            if (txtObject.InvokeRequired)
            {
                txtObject.Invoke(new Action(delegate ()
                {
                    txtObject.Text = sText;
                }));
            }
            else
            {
                txtObject.Text = sText;
            }
        }

        public static void AppendRichTextBox(RichTextBox txtObject, string sText)
        {
            if (txtObject.InvokeRequired)
            {
                txtObject.Invoke(new Action(delegate ()
                {
                    txtObject.AppendText(sText);
                }));
            }
            else
            {
                txtObject.AppendText(sText);
            }
        }

        public static void SetLabel(Label lblObject, string sText)
        {
            if (lblObject.InvokeRequired)
            {
                lblObject.Invoke(new Action(delegate ()
                {
                    lblObject.Text = sText;
                }));
            }
            else
            {
                lblObject.Text = sText;
            }
        }

        public static void SetButton(Button btnObject, string sText)
        {
            if (btnObject.InvokeRequired)
            {
                btnObject.Invoke(new Action(delegate ()
                {
                    btnObject.Text = sText;
                }));
            }
            else
            {
                btnObject.Text = sText;
            }
        }

        public static void AddItem(ListView lstViewObject, ListViewItem lstViewItem)
        {
            if (lstViewObject.InvokeRequired)
            {
                lstViewObject.Invoke(new Action(delegate ()
                {
                    lstViewObject.Items.Add(lstViewItem);
                }));
            }
            else
            {
                lstViewObject.Items.Add(lstViewItem);
            }
        }

        public static void DeleteItem(ListView lstViewObject, int nIndex)
        {
            if (lstViewObject.InvokeRequired)
            {
                lstViewObject.Invoke(new Action(delegate ()
                {
                    lstViewObject.Items.RemoveAt(nIndex);
                }));
            }
            else
            {
                lstViewObject.Items.RemoveAt(nIndex);
            }
        }

        public static void ClearItem(ListView lstViewObject)
        {
            if (lstViewObject.InvokeRequired)
            {
                lstViewObject.Invoke(new Action(delegate ()
                {
                    lstViewObject.Items.Clear();
                }));
            }
            else
            {
                lstViewObject.Items.Clear();
            }
        }
    }
}
