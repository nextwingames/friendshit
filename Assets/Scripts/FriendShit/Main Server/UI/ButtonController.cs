using Friendshit.Game;
using Friendshit.Protocols;
using Nextwin.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Friendshit
{
    namespace MainServer
    {
        public class ButtonController : MonoBehaviour
        {
            private NetworkManager _networkManager;

            // 경고 창
            private Animator _alertAnimator;
            private Text _alertMessage;

            // 회원가입
            private InputField _nicknameInput;
            private InputField _regIdInput;
            private InputField _regPwInput;
            private InputField _regPwConfirmInput;
            private InputField _mailInput;

            // 로그인
            private InputField _idInput;
            private InputField _pwInput;

            // 방 생성
            private Animator _createRoomPanelAnimator;
            private InputField _roomNameInput;
            private Dropdown _maxPeopleDropdown;
            private int _map;

            // 방과 로비
            private GameObject _roomPanel;
            private Animator _lobbyPanelAnimator;

            private void Start()
            {
                _networkManager = NetworkManager.Instance;

                _alertAnimator = GameObject.Find("Alert Panel").GetComponent<Animator>();
                _alertMessage = GameObject.Find("Alert Message").GetComponent<Text>();

                _nicknameInput = GameObject.Find("Nickname InputField").GetComponent<InputField>();
                _regIdInput = GameObject.Find("Reg ID InputField").GetComponent<InputField>();
                _regPwInput = GameObject.Find("Reg PW InputField").GetComponent<InputField>();
                _regPwConfirmInput = GameObject.Find("Reg PW Confirm InputField").GetComponent<InputField>();
                _mailInput = GameObject.Find("Mail InputField").GetComponent<InputField>();

                _idInput = GameObject.Find("ID InputField").GetComponent<InputField>();
                _pwInput = GameObject.Find("PW InputField").GetComponent<InputField>();
                _idInput.text = "";
                _pwInput.text = "";

                _createRoomPanelAnimator = GameObject.Find("Create Room Panel").GetComponent<Animator>();
                _roomNameInput = GameObject.Find("Room Name InputField").GetComponent<InputField>();
                _maxPeopleDropdown = GameObject.Find("Max People Dropdown").GetComponent<Dropdown>();


                _roomPanel = GameObject.Find("Room Panel");
                _lobbyPanelAnimator = GameObject.Find("Lobby Panel").GetComponent<Animator>();
            }

            public void OnClickLogin()
            {
                string id = _idInput.text;
                string pw = _pwInput.text;

                _networkManager.Send(Protocol.Login, new SendingLoginPacket(id, pw));
            }

            public void OnClickRegister()
            {
                GameObject.Find("Register Panel").GetComponent<Animator>().Play("Open");
                _idInput.text = "";
                _pwInput.text = "";
                _nicknameInput.ActivateInputField();
                MainServerManager.CurrentPanel = MainServerManager.RegisterPanel;
            }

            public void OnClickRegisterComplete()
            {
                string nickname = _nicknameInput.text;
                string id = _regIdInput.text;
                string pw = _regPwInput.text;
                string pwConfirm = _regPwConfirmInput.text;
                string mail = _mailInput.text;

                if(nickname.Equals(""))
                {
                    _alertMessage.text = "Enter your nickname.";
                    _alertAnimator.Play("Open");
                    Focus.Focusing = Focus.Nickname;
                    return;
                }
                if(nickname.Length < 4)
                {
                    _alertMessage.text = "The nickname is too short. The nickname must be at least 4 characters long.";
                    _alertAnimator.Play("Open");
                    Focus.Focusing = Focus.Nickname;
                    return;
                }
                if(id.Equals(""))
                {
                    _alertMessage.text = "Enter your ID.";
                    _alertAnimator.Play("Open");
                    Focus.Focusing = Focus.RegId;
                    return;
                }
                if(id.Length < 4)
                {
                    _alertMessage.text = "The ID is too short. The ID must be at least 4 characters long.";
                    _alertAnimator.Play("Open");
                    Focus.Focusing = Focus.RegId;
                    return;
                }
                if(pw.Equals(""))
                {
                    _alertMessage.text = "Enter your password.";
                    _alertAnimator.Play("Open");
                    Focus.Focusing = Focus.RegPw;
                    return;
                }
                if(pw.Length < 8)
                {
                    _alertMessage.text = "The password is too short. The password must be at least 8 characters long.";
                    _alertAnimator.Play("Open");
                    Focus.Focusing = Focus.RegPw;
                    return;
                }
                if(!pw.Equals(pwConfirm))
                {
                    _alertMessage.text = "Check your password again.";
                    _alertAnimator.Play("Open");
                    Focus.Focusing = Focus.RegPwConfirm;
                    return;
                }

                _networkManager.Send(Protocol.Register, new SendingRegisterPacket(nickname, id, pw, mail));
            }

            public void OnClickRegisterCancel()
            {
                _nicknameInput.text = "";
                _regIdInput.text = "";
                _regPwInput.text = "";
                _regPwConfirmInput.text = "";
                _mailInput.text = "";

                GameObject.Find("Register Panel").GetComponent<Animator>().Play("Close");
                _idInput.ActivateInputField();

                MainServerManager.CurrentPanel = MainServerManager.LoginPanel;
            }

            public void OnClickAlertOk()
            {
                _alertAnimator.Play("Close");

                switch(Focus.Focusing)
                {
                    case Focus.Nickname:
                        _nicknameInput.ActivateInputField();
                        break;
                    case Focus.RegId:
                        _regIdInput.ActivateInputField();
                        break;
                    case Focus.RegPw:
                        _regPwInput.ActivateInputField();
                        break;
                    case Focus.RegPwConfirm:
                        _regPwConfirmInput.ActivateInputField();
                        break;
                    case Focus.Id:
                        _idInput.ActivateInputField();
                        break;
                    case Focus.Pw:
                        _pwInput.ActivateInputField();
                        break;
                }
            }

            public void OnClickCreateRoom()
            {
                _roomNameInput.text = "";
                _maxPeopleDropdown.value = 0;
                _map = 0;
                _createRoomPanelAnimator.Play("Open");
            }

            public void OnClickCreateRoomOk()
            {
                string roomName = _roomNameInput.text;
                int maxPeople = _maxPeopleDropdown.value + 4;
                string nickname = MainServerManager.Player.Nickname;

                if(roomName.Equals(""))
                    return;

                _createRoomPanelAnimator.Play("Close");
                _lobbyPanelAnimator.Play("Close");

                _networkManager.Send(Protocol.CreateRoom, new SendingCreateRoomPacket(roomName, maxPeople, _map));
            }

            public void OnClickCreateRoomCancel()
            {
                _createRoomPanelAnimator.Play("Close");
            }

            public void OnClickEnterRoom()
            {

            }

            public void OnClickRefresh()
            {

            }

            public void OnClickExitRoom()
            {
                _roomPanel.GetComponent<Animator>().Play("Close");
                RoomPanelManager roomPanelManager = _roomPanel.GetComponent<RoomPanelManager>();

                int roomId = roomPanelManager.Room.Id;
                string nickname = MainServerManager.Player.Nickname;

                _networkManager.Send(Protocol.ExitRoom, new SendingExitRoomPacket(roomId, nickname));
            }
        }
    }
}
