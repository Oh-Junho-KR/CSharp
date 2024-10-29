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
    public partial class FormRoom : UserControl
    {
        public delegate void EnterChatEventHandler(string sID, string sTitle);
        public EnterChatEventHandler EnterChatEvent;

        public FormRoom()
        {
            InitializeComponent();
        }

        private void lstViewRoom_DoubleClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstViewRoom.SelectedItems)
            {
                string sRoomID      = item.SubItems[1].Text.ToString();
                string sRoomTitle   = item.SubItems[2].Text.ToString();

                EnterChatEvent(sRoomID, sRoomTitle);
            }
        }
    }
}
