using Nextwin.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Friendshit
{
    namespace Main
    {
        public class MainManager:MonoBehaviour
        {
            [SerializeField]
            private GameObject _logoPanel;
            [SerializeField]
            private GameObject _mainScreenPanel;

            // Update is called once per frame
            void Update()
            {
                try
                {
                    // 아무 키나 입력
                    if(_logoPanel.transform.localScale.x == 0 && Input.anyKeyDown)
                    {
                        Destroy(_logoPanel);
                        Destroy(_mainScreenPanel.transform.GetChild(0).gameObject);
                        _mainScreenPanel.GetComponent<Animator>().Play("Destroy");
                    }
                }
                catch(MissingReferenceException) { }
            }
        }
    }
}