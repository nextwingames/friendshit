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

            private void Start()
            {
                _networkManager = NetworkManager.Instance;
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
                string nickname = GameObject.Find("Nickname InputField").GetComponent<InputField>().text;
                string id = GameObject.Find("Reg ID InputField").GetComponent<InputField>().text;
                string pw = GameObject.Find("Reg PW InputField").GetComponent<InputField>().text;
                string pwConfirm = GameObject.Find("Reg PW Confirm InputField").GetComponent<InputField>().text;
                string mail = GameObject.Find("Mail InputField").GetComponent<InputField>().text;

                if(nickname.Equals(""))
                {

                    return;
                }
                if(id.Equals(""))
                {

                    return;
                }
                if(pw.Equals(""))
                {

                    return;
                }
                if(!pw.Equals(pwConfirm))
                {

                    return;
                }

                _networkManager.Send(Protocol.Register, new SendingRegisterPacket(nickname, id, pw, mail));
            }
        }
    }
}
