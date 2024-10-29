using System;

namespace ChattingClient
{
    public class GlobalDefine
    {
        public enum eProtocol
        {
            None        = 000,
            Login       = 100,
            Logout      = 101,
            EnterRoom   = 200,
            OutRoom     = 201,
            EnterChat   = 300,
            ExitChat    = 301,
            SendMsg     = 400,
            RecvMsg     = 401
        }

        public static string    sServerIP       = "127.0.0.1";
        public static int       nServerPort     = 10001;

        public static string    sAdminID        = "admin";
        public static string    sAdminPW        = null;

        public static bool      bSimulationMode = true;
    }
}
