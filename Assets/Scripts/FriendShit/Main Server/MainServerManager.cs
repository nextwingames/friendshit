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
using UnityEngine.UI;
using System;

namespace Friendshit
{
    namespace MainServer
    {
        public class MainServerManager:MonoBehaviour
        {
            public static Player PlayerInformation { get; set; }

            private NetworkManager _networkManager;
            private Thread _networkThread;
            private Queue<OriginPacket> _serviceQueue = new Queue<OriginPacket>();
            private object _locker = new object();

            private ButtonController _buttonController;

            public static int CurrentPanel { get; set; }
            public const int LoginPanel = 0;
            public const int RegisterPanel = 1;
            public const int LobbyPanel = 2;

            // 회원가입
            [SerializeField]
            private InputField _nicknameInput;
            [SerializeField]
            private InputField _regIdInput;
            [SerializeField]
            private InputField _regPwInput;
            [SerializeField]
            private InputField _regPwConfirmInput;
            [SerializeField]
            private InputField _mailInput;

            // 로그인
            [SerializeField]
            private InputField _idInput;
            [SerializeField]
            private InputField _pwInput;

            // 채팅
            [SerializeField]
            private Text _lobbyChatMessages;
            [SerializeField]
            private InputField _lobbyChatInput;

            // Start is called before the first frame update
            void Start()
            {
                _networkManager = NetworkManager.Instance;
                _networkManager.Connect(NetworkManager.MainPort);

                _idInput.ActivateInputField();

                _buttonController = GameObject.Find("Login Button").GetComponent<ButtonController>();
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
                ChangeFocus();
                InputReturn();
            }

            /// <summary>
            /// 쓰레드에서 계속해서 데이터 수신 후 작업큐에 넣음
            /// </summary>
            private void Run()
            {
                while(_networkManager.IsConnected)
                {
                    Header header = _networkManager.Receive();
                    byte[] data = _networkManager.Receive(header.Length);
                    OriginPacket originPacket = new OriginPacket(header, data);
                    lock(_locker)
                    {
                        _serviceQueue.Enqueue(originPacket);
                    }
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

                Service service = null;
                Packet packet;
                switch(header.MsgType)
                {
                    case Protocol.Register:
                        packet = JsonManager.BytesToObject<ReceivingRegisterPacket>(data);
                        service = new RegisterService(packet);
                        break;

                    case Protocol.Login:
                        packet = JsonManager.BytesToObject<ReceivingLoginPacket>(data);
                        service = new LoginService(packet);
                        break;

                    case Protocol.LobbyChat:
                        packet = JsonManager.BytesToObject<ReceivingChatPacket>(data);
                        service = new LobbyChatService(packet);
                        break;

                    case Protocol.CreateRoom:
                        packet = JsonManager.BytesToObject<ReceivingCreateRoomPacket>(data);
                        service = new CreateRoomService(packet);
                        break;

                    case Protocol.EnterRoom:
                        packet = JsonManager.BytesToObject<ReceivingEnterRoomPacket>(data);
                        service = new EnterRoomService(packet);
                        break;
                }
                if(service != null)
                    service.Execute();
            }

            private void ChangeFocus()
            {
                if(!Input.GetKeyDown(KeyCode.Tab))
                    return;

                // 역방향
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    if(_regIdInput.isFocused)
                        _nicknameInput.ActivateInputField();
                    else if(_regPwInput.isFocused)
                        _regIdInput.ActivateInputField();
                    else if(_regPwConfirmInput.isFocused)
                        _regPwInput.ActivateInputField();
                    else if(_mailInput.isFocused)
                        _regPwConfirmInput.ActivateInputField();
                    else if(_pwInput.isFocused)
                        _idInput.ActivateInputField();
                }
                // 순방향
                else
                {
                    if(_nicknameInput.isFocused)
                        _regIdInput.ActivateInputField();
                    else if(_regIdInput.isFocused)
                        _regPwInput.ActivateInputField();
                    else if(_regPwInput.isFocused)
                        _regPwConfirmInput.ActivateInputField();
                    else if(_regPwConfirmInput.isFocused)
                        _mailInput.ActivateInputField();
                    else if(_idInput.isFocused)
                        _pwInput.ActivateInputField();
                }
            }

            private void InputReturn()
            {
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    switch(CurrentPanel)
                    {
                        case LoginPanel:
                            _buttonController.OnClickLogin();
                            break;
                        case RegisterPanel:
                            _buttonController.OnClickRegisterComplete();
                            break;
                        case LobbyPanel:
                            SendChat();
                            break;
                    }
                }
            }

            private void SendChat()
            {
                _lobbyChatInput.ActivateInputField();

                if(_lobbyChatMessages.Equals(""))
                    return;

                string message = _lobbyChatInput.text;
                _networkManager.Send(Protocol.LobbyChat, new SendingChatPacket(PlayerInformation.Nickname, message));
                _lobbyChatInput.text = "";
            }

            private void OnApplicationQuit()
            {
                _networkManager.Disconnect();
                _networkThread.Join();
            }
        }
    }

}