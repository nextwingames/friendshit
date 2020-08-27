using Friendshit.MainServer;
using Friendshit.Protocols;
using Nextwin.Protocol;
using UnityEngine;
using UnityEngine.UI;

namespace Friendshit
{
    namespace Services
    {
        public class RegisterService:Service
        {
            private const int RegisterFailNickname = 0;
            private const int RegisterFailId = -1;
            private const int RegisterSuccess = 1;

            private ReceivingRegisterPacket _receivingRegisterPacket;

            // 경고 창
            private Animator _alertAnimator;
            private Text _alertMessage;

            // 회원가입
            private InputField _inputNickname;
            private InputField _inputRegId;
            private InputField _inputRegPw;
            private InputField _inputRegPwConfirm;
            private InputField _inputMail;

            public RegisterService(Packet packet) : base(packet)
            {
                _receivingRegisterPacket = (ReceivingRegisterPacket)packet;

                _alertAnimator = GameObject.Find("Alert Panel").GetComponent<Animator>();
                _alertMessage = GameObject.Find("Alert Message").GetComponent<Text>();

                _inputNickname = GameObject.Find("Nickname InputField").GetComponent<InputField>();
                _inputRegId = GameObject.Find("Reg ID InputField").GetComponent<InputField>();
                _inputRegPw = GameObject.Find("Reg PW InputField").GetComponent<InputField>();
                _inputRegPwConfirm = GameObject.Find("Reg PW Confirm InputField").GetComponent<InputField>();
                _inputMail = GameObject.Find("Mail InputField").GetComponent<InputField>();
            }

            public override void Execute()
            {
                switch(_receivingRegisterPacket.Result)
                {
                    case RegisterFailNickname:
                        _alertMessage.text = "The Nickname is already in use.\nPlease enter another nickname.";
                        Focus.Focusing = Focus.Nickname;
                        break;

                    case RegisterFailId:
                        _alertMessage.text = "The ID is already in use.\nPlease enter another ID.";
                        Focus.Focusing = Focus.RegId;
                        break;

                    case RegisterSuccess:
                        _alertMessage.text = "Registered successfully!\nYour ID is " + _receivingRegisterPacket.Id + ".";
                        GameObject.Find("Register Panel").GetComponent<Animator>().Play("Close");
                        _inputNickname.text = "";
                        _inputRegId.text = "";
                        _inputRegPw.text = "";
                        _inputRegPwConfirm.text = "";
                        _inputMail.text = "";
                        break;
                }
                _alertAnimator.Play("Open");
            }
        }
    }
}