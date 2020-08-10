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
                Debug.Log("Socket connected to " + _socket.RemoteEndPoint.ToString());
            }

            private void ConnectCallback(IAsyncResult ar)
            {
                try
                {
                    Socket socket = (Socket)ar.AsyncState;
                    socket.EndConnect(ar);
                    _connectDone.Set();
                }
                catch(Exception e)
                {
                    Debug.Log(e);
                }
            }

            /// <summary>
            /// 구조체 전송
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="msgType"></param>
            /// <param name="obj"></param>            
            public void Send<T>(int msgType, T obj)
            {
                byte[] data = JsonManager.ObjectToBytes(obj);

                Header header = new Header(msgType, data.Length);
                byte[] head = JsonManager.ObjectToBytes(header);

                // 최종 전송할 패킷(헤더 + 데이터) 바이트화
                byte[] packet = new byte[head.Length + data.Length];
                Buffer.BlockCopy(head, 0, packet, 0, head.Length);
                Buffer.BlockCopy(data, 0, packet, head.Length, data.Length);

                _socket.Send(packet, packet.Length, SocketFlags.None);
            }

            /// <summary>
            /// 헤더 수신
            /// </summary>
            /// <returns></returns>
            public Header Receive()
            {
                byte[] head = new byte[Protocol.Protocol.HeaderLength];
                _socket.Receive(head, Protocol.Protocol.HeaderLength, SocketFlags.None);

                Header header = JsonManager.BytesToObject<Header>(head);
                return header;
            }

            /// <summary>
            /// 데이터 수신
            /// </summary>
            /// <param name="length"></param>
            /// <returns></returns>
            public byte[] Receive(int length)
            {
                byte[] data = new byte[length];
                _socket.Receive(data, length, SocketFlags.None);
                return data;
            }

            /// <summary>
            /// 연결 해제
            /// </summary>
            public void Disconnect()
            {
                _socket.Close();
            }

            [Obsolete]
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
