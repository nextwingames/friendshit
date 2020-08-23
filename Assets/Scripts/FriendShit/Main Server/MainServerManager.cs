using Nextwin.Net;
using Nextwin.Protocol;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using Friendshit.Services;
using Friendshit.Protocols;
using Nextwin.Util;

namespace Friendshit
{
    namespace MainServer
    {
        public class MainServerManager:MonoBehaviour
        {
            private NetworkManager _networkManager;
            private Thread _networkThread;
            private Queue<OriginPacket> _serviceQueue = new Queue<OriginPacket>();
            private object _locker = new object();

            // Start is called before the first frame update
            void Start()
            {
                _networkManager = NetworkManager.Instance;
                _networkManager.Connect(NetworkManager.MainPort);
            }

            // Update is called once per frame
            void Update()
            {
                if(!_networkManager.IsConnected)
                    return;
                if(_networkThread == null)
                {
                    _networkThread = new Thread(new ThreadStart(Run));
                    _networkThread.Start();
                    _networkThread.IsBackground = true;
                    Debug.Log("Network thread started");
                }

                Service();
            }

            /// <summary>
            /// 쓰레드에서 계속해서 데이터 수신 후 작업큐에 넣음
            /// </summary>
            private void Run()
            {
                Header header = _networkManager.Receive();
                byte[] data = _networkManager.Receive(header.Length);
                OriginPacket originPacket = new OriginPacket(header, data);
                lock(_locker)
                {
                    _serviceQueue.Enqueue(originPacket);
                }
            }

            /// <summary>
            /// 작업큐로부터 꺼내 작업 수행
            /// </summary>
            private void Service()
            {
                if(_serviceQueue.Count == 0)
                    return;
                OriginPacket originPacket;
                lock(_locker)
                {
                    originPacket = _serviceQueue.Dequeue();
                }
                Header header = originPacket.Header;
                byte[] data = originPacket.Data;

                Service service;
                switch(header.MsgType)
                {
                    case Protocol.Register:
                        ReceivingRegisterPacket packet = JsonManager.BytesToObject<ReceivingRegisterPacket>(data);
                        service = new RegisterService(packet);
                        service.Execute();
                        break;
                }
            }
        }
    }

}