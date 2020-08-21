using Nextwin.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                GameObject.Find("Register Panel").GetComponent<Animator>().Play("Close");
            }
        }
    }
}
