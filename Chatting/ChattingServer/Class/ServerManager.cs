using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chatting
{
    public class ServerManager
    {
        public delegate void WriteLogEventHandler(string sMsg);
        public WriteLogEventHandler WriteLogEventCallback;

        private Server server;

        public ServerManager() 
        {
            server = new Server();
            server.WriteLogEvent += new Server.WriteLogEventHandler(WriteLogCallback);
        }

        public void init()
        {
            Thread t = new Thread(new ThreadStart(server.Echo));
            t.Start(); 
        }

        private void WriteLogCallback(string sMsg)
        {
            WriteLogEventCallback(sMsg);
        }

        private class Server
        {
            public delegate void WriteLogEventHandler(string sMsg);
            public WriteLogEventHandler WriteLogEvent;

            TcpListener listener = null;
            public static Dictionary<string, ArrayList> dicHandleList = new Dictionary<string, ArrayList>();
            ArrayList lstHandler = new ArrayList();

            public Server()
            {

            }

            public void Echo()
            {
                try
                {
                    IPAddress address = Dns.GetHostEntry("").AddressList[0];
                    listener = new TcpListener(IPAddress.Any, GlobalDefine.nServerPort);
                    listener.Start();

                    while (true)
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        EchoHandler handler = new EchoHandler(this, client);

                        handler.start();
                    }
                }
                catch (Exception ee)
                {
                    System.Console.WriteLine(ee.Message);
                }
                finally
                {
                    listener.Stop();
                }
            }

            public void Connect(EchoHandler eHandler)
            {
                string sActionDate = "클라이언트 연결[" + DateTime.Now.ToString() + "] : ";

                lstHandler.Add(eHandler);

                WriteLogEvent(sActionDate + "[" + eHandler.ID + "]" + eHandler.Name + " " + eHandler.IP + ":" + eHandler.Port);
            }

            public void DisConnect(EchoHandler eHandler)
            {
                string sActionDate = "클라이언트 해제[" + DateTime.Now.ToString() + "] : ";

                lstHandler.Remove(eHandler);

                WriteLogEvent(sActionDate + "[" + eHandler.ID + "]" + eHandler.Name + " " + eHandler.IP + ":" + eHandler.Port);
            }

            public void Add(EchoHandler eHandler)
            {
                string sActionDate = "클라이언트 추가[" + DateTime.Now.ToString() + "] : ";
                if (!dicHandleList.ContainsKey(eHandler.ID))
                {
                    ArrayList handleList = new ArrayList(GlobalDefine.nListen);
                    dicHandleList[eHandler.ID] = handleList; 
                }

                lock (dicHandleList[eHandler.ID].SyncRoot)
                {
                    dicHandleList[eHandler.ID].Add(eHandler);
                }

                WriteLogEvent(sActionDate + "[" + eHandler.ID + "]" + eHandler.Name + " " + eHandler.IP + ":" + eHandler.Port);
            }

            public void Remove(EchoHandler eHandler)
            {
                string sActionDate = "클라이언트 제거[" + DateTime.Now.ToString() + "] : ";
                if (eHandler.ID != null)
                {
                    if (dicHandleList.ContainsKey(eHandler.ID))
                    {
                        lock (dicHandleList[eHandler.ID].SyncRoot)
                        {
                            dicHandleList[eHandler.ID].Remove(eHandler);
                            if (dicHandleList[eHandler.ID].Count == 0)
                            {
                                dicHandleList.Remove(eHandler.ID);
                            }
                        }

                        WriteLogEvent(sActionDate + "[" + eHandler.ID + "]" + eHandler.Name + " " + eHandler.IP + ":" + eHandler.Port);
                    }
                }
            }

            public void Broadcast(EchoHandler eHandler, String sStr)
            {
                string sActionDate = "브로드캐스트 송신[" + DateTime.Now.ToString() + "] : ";
                lock (lstHandler.SyncRoot)
                {
                    foreach (EchoHandler handler in lstHandler)
                    {
                        EchoHandler echo = handler as EchoHandler;
                        if (echo != null)
                            echo.sendMessage(sStr);
                    }
                }

                WriteLogEvent(sActionDate + "[" + eHandler.ID + "]" + eHandler.Name + " " + eHandler.IP + ":" + eHandler.Port + " Msg:" + sStr);
            }

            public void SendToAllRoom(EchoHandler eHandler, String sStr)
            {
                string sActionDate = "전체메세지 송신[" + DateTime.Now.ToString() + "] : ";
                foreach (string sKey in dicHandleList.Keys)
                {
                    lock (dicHandleList[sKey].SyncRoot)
                    {
                        foreach (EchoHandler handler in dicHandleList[sKey])
                        {
                            EchoHandler echo = handler as EchoHandler;
                            if (echo != null)
                                echo.sendMessage(sStr);
                        }
                    }
                }

                WriteLogEvent(sActionDate + "[" + eHandler.ID + "]" + eHandler.Name + " " + eHandler.IP + ":" + eHandler.Port + " Msg:" + sStr);
            }

            public void SendToRoom(EchoHandler eHandler, string sStr)
            {
                string sActionDate = "그룹메세지 송신[" + DateTime.Now.ToString() + "] : ";
                lock (dicHandleList[eHandler.ID].SyncRoot)
                {
                    foreach (EchoHandler handler in dicHandleList[eHandler.ID])
                    {
                        EchoHandler echo = handler as EchoHandler;
                        if (echo != null)
                            echo.sendMessage(sStr);
                    }
                }

                WriteLogEvent(sActionDate + "[" + eHandler.ID + "]" + eHandler.Name + " " + eHandler.IP + ":" + eHandler.Port + " Msg:" + sStr);
            }
        }

        private class EchoHandler
        {
            Server server;
            TcpClient client;
            NetworkStream ns = null;
            StreamReader sr = null;
            StreamWriter sw = null;
            string str = string.Empty;

            private string sID;
            private string sName;
            private string sIP;
            private string sPort;

            public string ID
            {
                get { return sID; }
            }

            public string Name
            {
                get { return sName; }
            } 
            
            public string IP
            {
                get { return sIP; }
            }

            public string Port
            {
                get { return sPort; }
            }

            public EchoHandler(Server server, TcpClient client)
            {
                this.server = server;
                this.client = client;
                try
                {
                    ns = client.GetStream();
                    Socket socket = client.Client;

                    sIP = ((IPEndPoint)socket.RemoteEndPoint).Address.ToString();
                    sPort = ((IPEndPoint)socket.RemoteEndPoint).Port.ToString();

                    sr = new StreamReader(ns, Encoding.Default);
                    sw = new StreamWriter(ns, Encoding.Default);
                }
                catch (Exception) { Console.WriteLine("연결 실패"); }
            }

            public void start()
            {
                Thread t = new Thread(new ThreadStart(ProcessClient));
                t.Start();
            }

            public void ProcessClient()
            {
                try
                {
                    bool bConnect = true;
                    while ((str = sr.ReadLine()) != null)
                    {
                        GlobalDefine.eProtocol nProtocol = (GlobalDefine.eProtocol)Enum.Parse(typeof(GlobalDefine.eProtocol), str.Substring(0, 3));

                        switch (nProtocol)
                        {
                            case GlobalDefine.eProtocol.Login:
                                sName = str.Substring(3);
                                server.Connect(this);
                                break;

                            case GlobalDefine.eProtocol.Logout:
                                server.Remove(this);
                                bConnect = false;
                                break;

                            case GlobalDefine.eProtocol.EnterChat:
                                sID = str.Substring(3);
                                server.Add(this);
                                break;

                            case GlobalDefine.eProtocol.ExitChat:
                                server.Remove(this);
                                break;

                            case GlobalDefine.eProtocol.SendMsg:
                                str = str.Substring(3);
                                server.SendToRoom(this, str);
                                break;

                            default:
                                Console.WriteLine(nProtocol.ToString());
                                break;
                        }

                        if (!bConnect)
                        {
                            break;
                        }
                    }

                    sw.Flush();
                    sw.Close();
                    sr.Close();
                    client.Close();
                    server.DisConnect(this);
                }
                catch (Exception)
                {
                    sw.Flush();
                }
                finally
                {
                    server.Remove(this);
                    sw.Close();
                    sr.Close();
                    client.Close();
                    server.DisConnect(this);
                }
            }

            public void sendMessage(string message)
            {
                sw.WriteLine(message);
                sw.Flush();
            }
        }
    }
}
