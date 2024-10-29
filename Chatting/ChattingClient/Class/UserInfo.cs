using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChattingClient
{
    public class UserInfo
    {
        private string sIP;
        private int nPort;
        private string sName;
        private string sID;
        private List<string> lstRoom;

        public string IP
        {
            get { return sIP; }
            set { sIP = value; }
        }

        public int Port
        {
            get { return nPort; }
            set { nPort = value; }
        }

        public string Name
        {
            get { return sName; }
            set { sName = value; }
        }

        public string ID
        {
            get { return sID; }
            set { sID = value; }
        }

        public List<string> Room
        {
            get { return lstRoom; }
            set { lstRoom = value; }
        }

        public UserInfo() 
        {

        }

        public UserInfo(string sIP, int nPort, string sName, string sID, List<string> lstRoom)
        {
            this.sIP        = sIP;
            this.nPort      = nPort;
            this.sName      = sName;
            this.sID        = sID;
            this.lstRoom    = lstRoom;
        }
    }
}
