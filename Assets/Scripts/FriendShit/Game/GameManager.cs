using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nextwin.Net;
using Nextwin.Protocol;
using Nextwin.Util;
using Friendshit.Protocols;
using Friendshit.Services;

namespace Friendshit
{
    namespace Game
    {
        public class GameManager:MonoBehaviour
        {
            private NetworkManager _networkManager;

            private int _frame = 0;

            // Start is called before the first frame update
            void Start()
            {
                _networkManager = NetworkManager.Instance;
                // 서버 없이 확인 시 아래 Connect문 주석
                _networkManager.Connect(NetworkManager.GamePort);
            }

            // Update is called once per frame
            void Update()
            {
                _frame++;
                if(_frame % 300 == 20)
                {
                    TestPacket packet = new TestPacket(_frame, "str : " + _frame.ToString());
                    _networkManager.Send(Protocol.Test, packet);
                    Run();
                }
            }

            private void Run()
            {
                Header header = _networkManager.Receive();
                byte[] data = _networkManager.Receive(header.Length);

                Service service;
                switch(header.MsgType)
                {
                    case Protocol.Test:
                        TestPacket testPacket = JsonManager.BytesToObject<TestPacket>(data);
                        service = new TestService(testPacket);
                        service.Execute();
                        break;
                }
            }

            private void OnApplicationQuit()
            {
                _networkManager.Disconnect();
            }
        }
    }
}
