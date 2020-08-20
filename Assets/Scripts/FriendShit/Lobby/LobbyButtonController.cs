using Friendshit.Protocol;
using Nextwin.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Friendshit
{
    namespace Button
    {
        public class LobbyButtonController : MonoBehaviour
        {
            private NetworkManager _networkManager;

            private void Start()
            {
                _networkManager = NetworkManager.Instance;
            }

            public void OnClickPlayOnServer()
            {
                _networkManager.Connect(8899);
                GameObject.Find("Select Server Panel").GetComponent<Animator>().Play("Destroy");
            }

            public void OnClickPlayOnLocalServer()
            {
                GameObject.Find("Select Server Panel").GetComponent<Animator>().Play("Destroy");
                GameObject.Find("Login Panel").SetActive(false);
                GameObject.Find("Register Panel").SetActive(false);
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
