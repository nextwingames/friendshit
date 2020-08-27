using Friendshit.MainServer;
using Friendshit.Protocols;
using Nextwin.Protocol;
using UnityEngine;
using UnityEngine.UI;

namespace Friendshit
{
    namespace Services
    {
        public class LoginService:Service
        {
            private const int LoginFailInvalidId = 0;
            private const int LoginFailInvalidPw = -1;
            private const int LoginSuccess = 1;

            private ReceivingLoginPacket _receivingLoginPacket;

            // 경고 창
            private Animator _alertAnimator;
            private Text _alertMessage;

            // 로그인
            private InputField _inputId;
            private InputField _inputPw;

            public LoginService(Packet packet) : base(packet)
            {
                _receivingLoginPacket = (ReceivingLoginPacket)packet;

                _alertAnimator = GameObject.Find("Alert Panel").GetComponent<Animator>();
                _alertMessage = GameObject.Find("Alert Message").GetComponent<Text>();

                _inputId = GameObject.Find("ID InputField").GetComponent<InputField>();
                _inputPw = GameObject.Find("PW InputField").GetComponent<InputField>();
            }

            public override void Execute()
            {
                switch(_receivingLoginPacket.Result)
                {
                    case LoginFailInvalidId:
                        _alertMessage.text = "Invalid ID. Please try again.";
                        _alertAnimator.Play("Open");
                        Focus.Focusing = Focus.Id;
                        break;
                    case LoginFailInvalidPw:
                        _alertMessage.text = "Invalid Password. Please try again";
                        _alertAnimator.Play("Open");
                        Focus.Focusing = Focus.Pw;
                        break;
                    case LoginSuccess:
                        MainServerManager.PlayerInformation = new PlayerInformation(_receivingLoginPacket);
                        GameObject.Find("Login Panel").GetComponent<Animator>().Play("Close");
                        break;
                }
            }
        }
    }
}