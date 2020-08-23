using Friendshit.Protocols;
using UnityEngine;

namespace Friendshit
{
    namespace Services
    {
        public class RegisterService:Service
        {
            private ReceivingRegisterPacket _packet;

            public RegisterService(ReceivingRegisterPacket packet)
            {
                _packet = packet;
            }

            public override void Execute()
            {
                // 회원가입 실패
                if(!_packet.IsSuccess)
                {
                    // 이미 존재하는 아이디입니다 알림창
                    Debug.Log("이미 존재하는 아이디이거나 닉네임입니다.");
                    return;
                }

                // 회원가입 성공 알림창
                Debug.Log("환영합니다. " + _packet.Id + "님");
                GameObject.Find("Register Panel").GetComponent<Animator>().Play("Close");
            }
        }
    }
}