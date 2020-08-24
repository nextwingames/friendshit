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
            private InputField _inputNickname;
            private InputField _inputRegId;
            private InputField _inputRegPw;
            private InputField _inputRegPwConfirm;
            private InputField _inputMail;

            public static int Focus { get; set; }
            public const int FocusNickname = 1;
            public const int FocusRegId = 2;
            public const int FocusRegPw = 3;
            public const int FocusRegPwConfirm = 4;

            private void Start()
            {
                _networkManager = NetworkManager.Instance;

                _alertAnimator = GameObject.Find("Alert Panel").GetComponent<Animator>();
                _alertMessage = GameObject.Find("Alert Message").GetComponent<Text>();

                _inputNickname = GameObject.Find("Nickname InputField").GetComponent<InputField>();
                _inputRegId = GameObject.Find("Reg ID InputField").GetComponent<InputField>();
                _inputRegPw = GameObject.Find("Reg PW InputField").GetComponent<InputField>();
                _inputRegPwConfirm = GameObject.Find("Reg PW Confirm InputField").GetComponent<InputField>();
                _inputMail = GameObject.Find("Mail InputField").GetComponent<InputField>();
            }

            public void OnClickLogin()
            {
                
            }

            public void OnClickRegister()
            {
                GameObject.Find("Register Panel").GetComponent<Animator>().Play("Open");
            }

            public void OnClickRegisterComplete()
            {
                string nickname = _inputNickname.text;
                string id = _inputRegId.text;
                string pw = _inputRegPw.text;
                string pwConfirm = _inputRegPwConfirm.text;
                string mail = _inputMail.text;

                if(nickname.Equals(""))
                {
                    _alertMessage.text = "Enter your nickname.";
                    _alertAnimator.Play("Open");
                    Focus = FocusNickname;
                    return;
                }
                if(nickname.Length < 4)
                {
                    _alertMessage.text = "The nickname is too short. The nickname must be at least 4 characters long.";
                    _alertAnimator.Play("Open");
                    Focus = FocusNickname;
                    return;
                }
                if(id.Equals(""))
                {
                    _alertMessage.text = "Enter your ID.";
                    _alertAnimator.Play("Open");
                    Focus = FocusRegId;
                    return;
                }
                if(id.Length < 4)
                {
                    _alertMessage.text = "The ID is too short. The ID must be at least 4 characters long.";
                    _alertAnimator.Play("Open");
                    Focus = FocusRegId;
                    return;
                }
                if(pw.Equals(""))
                {
                    _alertMessage.text = "Enter your password.";
                    _alertAnimator.Play("Open");
                    Focus = FocusRegPw;
                    return;
                }
                if(pw.Length < 8)
                {
                    _alertMessage.text = "The password is too short. The password must be at least 8 characters long.";
                    _alertAnimator.Play("Open");
                    Focus = FocusRegPw;
                    return;
                }
                if(!pw.Equals(pwConfirm))
                {
                    _alertMessage.text = "Check your password again.";
                    _alertAnimator.Play("Open");
                    Focus = FocusRegPwConfirm;
                    return;
                }

                _networkManager.Send(Protocol.Register, new SendingRegisterPacket(nickname, id, pw, mail));
            }

            public void OnClickRegisterCancel()
            {
                _inputNickname.text = "";
                _inputRegId.text = "";
                _inputRegPw.text = "";
                _inputRegPwConfirm.text = "";
                _inputMail.text = "";

                GameObject.Find("Register Panel").GetComponent<Animator>().Play("Close");
            }

            public void OnClickAlertOk()
            {
                _alertAnimator.Play("Close");

                switch(Focus)
                {
                    case FocusNickname:
                        _inputNickname.ActivateInputField();
                        break;
                    case FocusRegId:
                        _inputRegId.ActivateInputField();
                        break;
                    case FocusRegPw:
                        _inputRegPw.ActivateInputField();
                        break;
                    case FocusRegPwConfirm:
                        _inputRegPwConfirm.ActivateInputField();
                        break;
                }
                Focus = 0;
            }
        }
    }
}
