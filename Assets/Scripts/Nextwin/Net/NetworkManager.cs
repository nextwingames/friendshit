using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.Threading;
using TMPro;
using Nextwin.Protocol;
using Nextwin.Util;

namespace Nextwin
{
    namespace Net
    {
        public class NetworkManager
        {
            private const int Port = 8899;
            private const string Ip = "127.0.0.1";
            private Socket _socket;

            private ManualResetEvent _connectDone = new ManualResetEvent(false);
            private ManualResetEvent _sendDone = new ManualResetEvent(false);
            private ManualResetEvent _receiveDone = new ManualResetEvent(false);
            private ManualResetEvent _disconnectDone = new ManualResetEvent(false);

            private static NetworkManager _instance;
            public static NetworkManager Instance
            {
                get
                {
                    if(_instance == null)
                        _instance = new NetworkManager();
                    return _instance;
                }
            }

            private NetworkManager() { }

            /// <summary>
            /// 비동기 연결
            /// </summary>
            public void Connect()
            {
                IPAddress ipAddress = IPAddress.Parse(Ip);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, Port);

                _socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                _socket.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), _socket);
                _connectDone.WaitOne();
                Debug.Log(_socket.GetHashCode() + " 서버 접속");
            }

            private void ConnectCallback(IAsyncResult ar)
            {
                try
                {
                    Socket socket = (Socket)ar.AsyncState;
                    socket.EndConnect(ar);
                    Debug.Log("Socket connected to " + socket.RemoteEndPoint.ToString());
                    _connectDone.Set();
                }
                catch(Exception e)
                {
                    Debug.Log(e);
                }
            }

            public void Send(int test)
            {
                //byte[] data = BitConverter.GetBytes(test);
                //_socket.Send(data);
                TestProtocol obj = new TestProtocol(test, test.ToString() + "frame");
                byte[] data = JsonManager.ObjectToBytes(obj);
                try
                {
                    _socket.Send(data);
                }
                catch(Exception)
                {
                    Debug.Log("연결되지 않음");
                }
            }

            /// <summary>
            /// 구조체 전송
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="obj"></param>
            public void Send<T>(T obj)
            {
                
            }

            /// <summary>
            /// 연결 해제
            /// </summary>
            public void Disconnect()
            {
                _socket.Close();
                //_socket.Shutdown(SocketShutdown.Both);
                //_socket.Disconnect(true);
            }

            private void DisconnectCallback(IAsyncResult ar)
            {
                Socket socket = (Socket)ar.AsyncState;
                socket.EndDisconnect(ar);
                Debug.Log("Socket disconnected");
                _disconnectDone.Set();
            }
        }
    }
}
